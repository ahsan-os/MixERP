# finance.get_retained_earnings_statement function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_retained_earnings_statement(_date_to date, _office_id integer, _factor integer)
RETURNS TABLE(id integer, value_date date, tran_code text, statement_reference text, debit numeric, credit numeric, balance numeric, office text, book text, account_id integer, account_number text, account text, posted_on timestamp with time zone, posted_by text, approved_by text, verification_status integer)
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_retained_earnings_statement
* Arguments : _date_to date, _office_id integer, _factor integer
* Owner : frapid_db_user
* Result Type : TABLE(id integer, value_date date, tran_code text, statement_reference text, debit numeric, credit numeric, balance numeric, office text, book text, account_id integer, account_number text, account text, posted_on timestamp with time zone, posted_by text, approved_by text, verification_status integer)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_retained_earnings_statement(_date_to date, _office_id integer, _factor integer)
 RETURNS TABLE(id integer, value_date date, tran_code text, statement_reference text, debit numeric, credit numeric, balance numeric, office text, book text, account_id integer, account_number text, account text, posted_on timestamp with time zone, posted_by text, approved_by text, verification_status integer)
 LANGUAGE plpgsql
AS $function$
    DECLARE _accounts               integer[];
    DECLARE _date_from              date;
    DECLARE _net_profit             numeric(30, 6)  = 0;
    DECLARE _income_tax_rate        real            = 0;
    DECLARE _itp                    numeric(30, 6)  = 0;
BEGIN
    _date_from                      := finance.get_fiscal_year_start_date(_office_id);
    _net_profit                     := finance.get_net_profit(_date_from, _date_to, _office_id, _factor);
    _income_tax_rate                := finance.get_income_tax_rate(_office_id);

    IF(COALESCE(_factor , 0) = 0) THEN
        _factor                         := 1;
    END IF; 

    IF(_income_tax_rate != 0) THEN
        _itp                            := (_net_profit * _income_tax_rate) / (100 - _income_tax_rate);
    END IF;

    DROP TABLE IF EXISTS temp_account_statement;
    CREATE TEMPORARY TABLE temp_account_statement
    (
        id                          SERIAL,
        value_date                  date,
        tran_code                   text,
        statement_reference         text,
        debit                       numeric(30, 6),
        credit                      numeric(30, 6),
        balance                     numeric(30, 6),
        office                      text,
        book                        text,
        account_id                  integer,
        account_number              text,
        account                     text,
        posted_on                   TIMESTAMP WITH TIME ZONE,
        posted_by                   text,
        approved_by                 text,
        verification_status         integer
    ) ON COMMIT DROP;

    SELECT array_agg(finance.accounts.account_id) INTO _accounts
    FROM finance.accounts
    WHERE finance.accounts.account_master_id BETWEEN 15300 AND 15400;

    INSERT INTO temp_account_statement(value_date, tran_code, statement_reference, debit, credit, office, book, account_id, posted_on, posted_by, approved_by, verification_status)
    SELECT
        _date_from,
        NULL,
        'Beginning balance on this fiscal year.',
        NULL,
        SUM
        (
            CASE finance.transaction_details.tran_type
            WHEN 'Cr' THEN amount_in_local_currency
            ELSE amount_in_local_currency * -1 
            END            
        ) as credit,
        NULL,
        NULL,
        NULL,
        NULL,
        NULL,
        NULL,
        NULL
    FROM finance.transaction_master
    INNER JOIN finance.transaction_details
    ON finance.transaction_master.transaction_master_id = finance.transaction_details.transaction_master_id
    WHERE
        finance.transaction_master.verification_status_id > 0
    AND
        finance.transaction_master.value_date < _date_from
    AND
       finance.transaction_master.office_id IN (SELECT * FROM core.get_office_ids(_office_id)) 
    AND
       finance.transaction_details.account_id = ANY(_accounts);

    INSERT INTO temp_account_statement(value_date, tran_code, statement_reference, debit, credit)
    SELECT _date_to, '', format('Add: Net Profit as on %1$s.', _date_to::text), 0, _net_profit;

    INSERT INTO temp_account_statement(value_date, tran_code, statement_reference, debit, credit)
    SELECT _date_to, '', 'Add: Income Tax provison.', 0, _itp;

--     DELETE FROM temp_account_statement
--     WHERE COALESCE(temp_account_statement.debit, 0) = 0
--     AND COALESCE(temp_account_statement.credit, 0) = 0;
    

    UPDATE temp_account_statement SET 
    debit = temp_account_statement.credit * -1,
    credit = 0
    WHERE temp_account_statement.credit < 0;


    INSERT INTO temp_account_statement(value_date, tran_code, statement_reference, debit, credit, office, book, account_id, posted_on, posted_by, approved_by, verification_status)
    SELECT
        finance.transaction_master.value_date,
        finance.transaction_master. transaction_code,
        finance.transaction_details.statement_reference,
        CASE finance.transaction_details.tran_type
        WHEN 'Dr' THEN amount_in_local_currency / _factor
        ELSE NULL END,
        CASE finance.transaction_details.tran_type
        WHEN 'Cr' THEN amount_in_local_currency / _factor
        ELSE NULL END,
        core.get_office_name_by_office_id(finance.transaction_master.office_id),
        finance.transaction_master.book,
        finance.transaction_details.account_id,
        finance.transaction_master.transaction_ts,
        account.get_name_by_user_id(finance.transaction_master.user_id),
        account.get_name_by_user_id(finance.transaction_master.verified_by_user_id),
        finance.transaction_master.verification_status_id
    FROM finance.transaction_master
    INNER JOIN finance.transaction_details
    ON finance.transaction_master.transaction_master_id = finance.transaction_details.transaction_master_id
    WHERE
        finance.transaction_master.verification_status_id > 0
    AND
        finance.transaction_master.value_date >= _date_from
    AND
        finance.transaction_master.value_date <= _date_to
    AND
       finance.transaction_master.office_id IN (SELECT * FROM core.get_office_ids(_office_id)) 
    AND
       finance.transaction_details.account_id = ANY(_accounts)
    ORDER BY 
        finance.transaction_master.value_date,
        finance.transaction_master.last_verified_on;


    UPDATE temp_account_statement
    SET balance = c.balance
    FROM
    (
        SELECT
            temp_account_statement.id, 
            SUM(COALESCE(c.credit, 0)) 
            - 
            SUM(COALESCE(c.debit,0)) As balance
        FROM temp_account_statement
        LEFT JOIN temp_account_statement AS c 
            ON (c.id <= temp_account_statement.id)
        GROUP BY temp_account_statement.id
        ORDER BY temp_account_statement.id
    ) AS c
    WHERE temp_account_statement.id = c.id;

    UPDATE temp_account_statement SET 
        account_number = finance.accounts.account_number,
        account = finance.accounts.account_name
    FROM finance.accounts
    WHERE temp_account_statement.account_id = finance.accounts.account_id;


    UPDATE temp_account_statement SET debit = NULL WHERE temp_account_statement.debit = 0;
    UPDATE temp_account_statement SET credit = NULL WHERE temp_account_statement.credit = 0;

    RETURN QUERY
    SELECT * FROM temp_account_statement
    ORDER BY id;    
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

