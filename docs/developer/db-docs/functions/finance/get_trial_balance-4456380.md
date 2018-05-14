# finance.get_trial_balance function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_trial_balance(_date_from date, _date_to date, _user_id integer, _office_id integer, _compact boolean, _factor numeric, _change_side_when_negative boolean DEFAULT true, _include_zero_balance_accounts boolean DEFAULT true)
RETURNS TABLE(id integer, account_id integer, account_number text, account text, previous_debit numeric, previous_credit numeric, debit numeric, credit numeric, closing_debit numeric, closing_credit numeric)
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_trial_balance
* Arguments : _date_from date, _date_to date, _user_id integer, _office_id integer, _compact boolean, _factor numeric, _change_side_when_negative boolean DEFAULT true, _include_zero_balance_accounts boolean DEFAULT true
* Owner : frapid_db_user
* Result Type : TABLE(id integer, account_id integer, account_number text, account text, previous_debit numeric, previous_credit numeric, debit numeric, credit numeric, closing_debit numeric, closing_credit numeric)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_trial_balance(_date_from date, _date_to date, _user_id integer, _office_id integer, _compact boolean, _factor numeric, _change_side_when_negative boolean DEFAULT true, _include_zero_balance_accounts boolean DEFAULT true)
 RETURNS TABLE(id integer, account_id integer, account_number text, account text, previous_debit numeric, previous_credit numeric, debit numeric, credit numeric, closing_debit numeric, closing_credit numeric)
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF(_date_from = 'infinity') THEN
        RAISE EXCEPTION 'Invalid date.'
        USING ERRCODE='P3008';
    END IF;

    IF NOT EXISTS
    (
        SELECT 0 FROM core.offices
        WHERE office_id IN 
        (
            SELECT * FROM core.get_office_ids(_office_id)
        )
        HAVING count(DISTINCT currency_code) = 1
   ) THEN
        RAISE EXCEPTION 'Cannot produce trial balance of office(s) having different base currencies.'
        USING ERRCODE='P8002';
   END IF;


    DROP TABLE IF EXISTS temp_trial_balance;
    CREATE TEMPORARY TABLE temp_trial_balance
    (
        id                      integer,
        account_id              integer,
        account_number          text,
        account                 text,
        previous_debit          numeric(30, 6),
        previous_credit         numeric(30, 6),
        debit                   numeric(30, 6),
        credit                  numeric(30, 6),
        closing_debit           numeric(30, 6),
        closing_credit          numeric(30, 6),
        root_account_id         integer,
        normally_debit          boolean
    ) ON COMMIT DROP;

    INSERT INTO temp_trial_balance(account_id, previous_debit, previous_credit)    
    SELECT 
        verified_transaction_mat_view.account_id, 
        SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE 0 END),
        SUM(CASE tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE 0 END)        
    FROM finance.verified_transaction_mat_view
    WHERE value_date < _date_from
    AND office_id IN (SELECT * FROM core.get_office_ids(_office_id))
    GROUP BY verified_transaction_mat_view.account_id;

    IF(_date_to = 'infinity') THEN
        INSERT INTO temp_trial_balance(account_id, debit, credit)    
        SELECT 
            verified_transaction_mat_view.account_id, 
            SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE 0 END),
            SUM(CASE tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE 0 END)        
        FROM finance.verified_transaction_mat_view
        WHERE value_date > _date_from
        AND office_id IN (SELECT * FROM core.get_office_ids(_office_id))
        GROUP BY verified_transaction_mat_view.account_id;
    ELSE
        INSERT INTO temp_trial_balance(account_id, debit, credit)    
        SELECT 
            verified_transaction_mat_view.account_id, 
            SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE 0 END),
            SUM(CASE tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE 0 END)        
        FROM finance.verified_transaction_mat_view
        WHERE value_date >= _date_from AND value_date <= _date_to
        AND office_id IN (SELECT * FROM core.get_office_ids(_office_id))
        GROUP BY verified_transaction_mat_view.account_id;    
    END IF;

    UPDATE temp_trial_balance SET root_account_id = finance.get_root_account_id(temp_trial_balance.account_id);


    DROP TABLE IF EXISTS temp_trial_balance2;
    
    IF(_compact) THEN
        CREATE TEMPORARY TABLE temp_trial_balance2
        ON COMMIT DROP
        AS
        SELECT
            temp_trial_balance.root_account_id AS account_id,
            ''::text as account_number,
            ''::text as account,
            SUM(temp_trial_balance.previous_debit) AS previous_debit,
            SUM(temp_trial_balance.previous_credit) AS previous_credit,
            SUM(temp_trial_balance.debit) AS debit,
            SUM(temp_trial_balance.credit) as credit,
            SUM(temp_trial_balance.closing_debit) AS closing_debit,
            SUM(temp_trial_balance.closing_credit) AS closing_credit,
            temp_trial_balance.normally_debit
        FROM temp_trial_balance
        GROUP BY 
            temp_trial_balance.root_account_id,
            temp_trial_balance.normally_debit
        ORDER BY temp_trial_balance.normally_debit;
    ELSE
        CREATE TEMPORARY TABLE temp_trial_balance2
        ON COMMIT DROP
        AS
        SELECT
            temp_trial_balance.account_id,
            ''::text as account_number,
            ''::text as account,
            SUM(temp_trial_balance.previous_debit) AS previous_debit,
            SUM(temp_trial_balance.previous_credit) AS previous_credit,
            SUM(temp_trial_balance.debit) AS debit,
            SUM(temp_trial_balance.credit) as credit,
            SUM(temp_trial_balance.closing_debit) AS closing_debit,
            SUM(temp_trial_balance.closing_credit) AS closing_credit,
            temp_trial_balance.normally_debit
        FROM temp_trial_balance
        GROUP BY 
            temp_trial_balance.account_id,
            temp_trial_balance.normally_debit
        ORDER BY temp_trial_balance.normally_debit;
    END IF;
    
    UPDATE temp_trial_balance2 SET
        account_number = finance.accounts.account_number,
        account = finance.accounts.account_name,
        normally_debit = finance.account_masters.normally_debit
    FROM finance.accounts
    INNER JOIN finance.account_masters
    ON finance.accounts.account_master_id = finance.account_masters.account_master_id
    WHERE temp_trial_balance2.account_id = finance.accounts.account_id;

    UPDATE temp_trial_balance2 SET 
        closing_debit = COALESCE(temp_trial_balance2.previous_debit, 0) + COALESCE(temp_trial_balance2.debit, 0),
        closing_credit = COALESCE(temp_trial_balance2.previous_credit, 0) + COALESCE(temp_trial_balance2.credit, 0);
        


     UPDATE temp_trial_balance2 SET previous_debit = COALESCE(temp_trial_balance2.previous_debit, 0) - COALESCE(temp_trial_balance2.previous_credit, 0), previous_credit = NULL WHERE normally_debit;
     UPDATE temp_trial_balance2 SET previous_credit = COALESCE(temp_trial_balance2.previous_credit, 0) - COALESCE(temp_trial_balance2.previous_debit, 0), previous_debit = NULL WHERE NOT normally_debit;
 
     UPDATE temp_trial_balance2 SET debit = COALESCE(temp_trial_balance2.debit, 0) - COALESCE(temp_trial_balance2.credit, 0), credit = NULL WHERE normally_debit;
     UPDATE temp_trial_balance2 SET credit = COALESCE(temp_trial_balance2.credit, 0) - COALESCE(temp_trial_balance2.debit, 0), debit = NULL WHERE NOT normally_debit;
 
     UPDATE temp_trial_balance2 SET closing_debit = COALESCE(temp_trial_balance2.closing_debit, 0) - COALESCE(temp_trial_balance2.closing_credit, 0), closing_credit = NULL WHERE normally_debit;
     UPDATE temp_trial_balance2 SET closing_credit = COALESCE(temp_trial_balance2.closing_credit, 0) - COALESCE(temp_trial_balance2.closing_debit, 0), closing_debit = NULL WHERE NOT normally_debit;


    IF(NOT _include_zero_balance_accounts) THEN
        DELETE FROM temp_trial_balance2 WHERE COALESCE(temp_trial_balance2.closing_debit, 0) + COALESCE(temp_trial_balance2.closing_credit, 0) = 0;
    END IF;
    
    IF(_factor > 0) THEN
        UPDATE temp_trial_balance2 SET previous_debit   = temp_trial_balance2.previous_debit/_factor;
        UPDATE temp_trial_balance2 SET previous_credit  = temp_trial_balance2.previous_credit/_factor;
        UPDATE temp_trial_balance2 SET debit            = temp_trial_balance2.debit/_factor;
        UPDATE temp_trial_balance2 SET credit           = temp_trial_balance2.credit/_factor;
        UPDATE temp_trial_balance2 SET closing_debit    = temp_trial_balance2.closing_debit/_factor;
        UPDATE temp_trial_balance2 SET closing_credit   = temp_trial_balance2.closing_credit/_factor;
    END IF;

    --Remove Zeros
    UPDATE temp_trial_balance2 SET previous_debit = NULL WHERE temp_trial_balance2.previous_debit = 0;
    UPDATE temp_trial_balance2 SET previous_credit = NULL WHERE temp_trial_balance2.previous_credit = 0;
    UPDATE temp_trial_balance2 SET debit = NULL WHERE temp_trial_balance2.debit = 0;
    UPDATE temp_trial_balance2 SET credit = NULL WHERE temp_trial_balance2.credit = 0;
    UPDATE temp_trial_balance2 SET closing_debit = NULL WHERE temp_trial_balance2.closing_debit = 0;
    UPDATE temp_trial_balance2 SET closing_debit = NULL WHERE temp_trial_balance2.closing_credit = 0;

    IF(_change_side_when_negative) THEN
        UPDATE temp_trial_balance2 SET previous_debit = temp_trial_balance2.previous_credit * -1, previous_credit = NULL WHERE temp_trial_balance2.previous_credit < 0;
        UPDATE temp_trial_balance2 SET previous_credit = temp_trial_balance2.previous_debit * -1, previous_debit = NULL WHERE temp_trial_balance2.previous_debit < 0;

        UPDATE temp_trial_balance2 SET debit = temp_trial_balance2.credit * -1, credit = NULL WHERE temp_trial_balance2.credit < 0;
        UPDATE temp_trial_balance2 SET credit = temp_trial_balance2.debit * -1, debit = NULL WHERE temp_trial_balance2.debit < 0;

        UPDATE temp_trial_balance2 SET closing_debit = temp_trial_balance2.closing_credit * -1, closing_credit = NULL WHERE temp_trial_balance2.closing_credit < 0;
        UPDATE temp_trial_balance2 SET closing_credit = temp_trial_balance2.closing_debit * -1, closing_debit = NULL WHERE temp_trial_balance2.closing_debit < 0;
    END IF;
    
    RETURN QUERY
    SELECT
        row_number() OVER(ORDER BY temp_trial_balance2.normally_debit DESC, temp_trial_balance2.account_id)::integer AS id,
        temp_trial_balance2.account_id,
        temp_trial_balance2.account_number,
        temp_trial_balance2.account,
        temp_trial_balance2.previous_debit,
        temp_trial_balance2.previous_credit,
        temp_trial_balance2.debit,
        temp_trial_balance2.credit,
        temp_trial_balance2.closing_debit,
        temp_trial_balance2.closing_credit
    FROM temp_trial_balance2;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

