# inventory.get_account_statement function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_account_statement(_value_date_from date, _value_date_to date, _user_id integer, _item_id integer, _store_id integer)
RETURNS TABLE(id integer, value_date date, book_date date, store_name text, tran_code text, statement_reference text, debit numeric, credit numeric, balance numeric, book text, item_id integer, item_code text, item_name text, posted_on timestamp with time zone, posted_by text, approved_by text, verification_status integer)
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_account_statement
* Arguments : _value_date_from date, _value_date_to date, _user_id integer, _item_id integer, _store_id integer
* Owner : frapid_db_user
* Result Type : TABLE(id integer, value_date date, book_date date, store_name text, tran_code text, statement_reference text, debit numeric, credit numeric, balance numeric, book text, item_id integer, item_code text, item_name text, posted_on timestamp with time zone, posted_by text, approved_by text, verification_status integer)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_account_statement(_value_date_from date, _value_date_to date, _user_id integer, _item_id integer, _store_id integer)
 RETURNS TABLE(id integer, value_date date, book_date date, store_name text, tran_code text, statement_reference text, debit numeric, credit numeric, balance numeric, book text, item_id integer, item_code text, item_name text, posted_on timestamp with time zone, posted_by text, approved_by text, verification_status integer)
 LANGUAGE plpgsql
AS $function$
BEGIN

    DROP TABLE IF EXISTS temp_account_statement;
    CREATE TEMPORARY TABLE temp_account_statement
    (
        id                      SERIAL,
        value_date              date,
        book_date               date,
        store_name              text,
        tran_code               text,
        statement_reference     text,
        debit                   numeric(30, 6),
        credit                  numeric(30, 6),
        balance                 numeric(30, 6),
        book                    text,
        item_id                 integer,
        item_code               text,
        item_name               text,
        posted_on               TIMESTAMP WITH TIME ZONE,
        posted_by               text,
        approved_by             text,
        verification_status     integer
    ) ON COMMIT DROP;

    INSERT INTO temp_account_statement(value_date, book_date, store_name, statement_reference, debit, item_id)
    SELECT 
        _value_date_from, 
        _value_date_from, 
        '',
        'Opening Balance',
        SUM
        (
            CASE inventory.checkout_details.transaction_type
            WHEN 'Dr' THEN base_quantity
            ELSE base_quantity * -1 
            END            
        ) as debit,
        _item_id
    FROM inventory.checkout_details
    INNER JOIN inventory.checkouts
    ON inventory.checkout_details.checkout_id = inventory.checkouts.checkout_id
    INNER JOIN finance.transaction_master
    ON inventory.checkouts.transaction_master_id = finance.transaction_master.transaction_master_id
    WHERE finance.transaction_master.verification_status_id > 0
    AND finance.transaction_master.value_date < _value_date_from
    AND (_store_id IS NULL OR inventory.checkout_details.store_id = _store_id)
    AND inventory.checkout_details.item_id = _item_id;

    DELETE FROM temp_account_statement
    WHERE COALESCE(temp_account_statement.debit, 0) = 0
    AND COALESCE(temp_account_statement.credit, 0) = 0;

    UPDATE temp_account_statement SET 
    debit = temp_account_statement.credit * -1,
    credit = 0
    WHERE temp_account_statement.credit < 0;

    INSERT INTO temp_account_statement(value_date, book_date, store_name, tran_code, statement_reference, debit, credit, book, item_id, posted_on, posted_by, approved_by, verification_status)
    SELECT
        finance.transaction_master.value_date,
        finance.transaction_master.book_date,
        inventory.get_store_name_by_store_id(inventory.checkout_details.store_id),
        finance.transaction_master.transaction_code,
        finance.transaction_master.statement_reference,
        CASE inventory.checkout_details.transaction_type
        WHEN 'Dr' THEN base_quantity
        ELSE 0 END AS debit,
        CASE inventory.checkout_details.transaction_type
        WHEN 'Cr' THEN base_quantity
        ELSE 0 END AS credit,
        finance.transaction_master.book,
        inventory.checkout_details.item_id,
        finance.transaction_master.transaction_ts AS posted_on,
        account.get_name_by_user_id(finance.transaction_master.user_id),
        account.get_name_by_user_id(finance.transaction_master.verified_by_user_id),
        finance.transaction_master.verification_status_id
    FROM finance.transaction_master
    INNER JOIN inventory.checkouts
    ON finance.transaction_master.transaction_master_id = inventory.checkouts.transaction_master_Id
    INNER JOIN inventory.checkout_details
    ON inventory.checkouts.checkout_id = inventory.checkout_details.checkout_id
    WHERE finance.transaction_master.verification_status_id > 0
    AND finance.transaction_master.value_date >= _value_date_from
    AND finance.transaction_master.value_date <= _value_date_to
    AND (_store_id IS NULL OR inventory.checkout_details.store_id = _store_id)
    AND inventory.checkout_details.item_id = _item_id
    ORDER BY 
        finance.transaction_master.value_date,
        finance.transaction_master.last_verified_on;
    
    UPDATE temp_account_statement
    SET balance = c.balance
    FROM
    (
        SELECT
            temp_account_statement.id, 
            SUM(COALESCE(c.debit, 0)) 
            - 
            SUM(COALESCE(c.credit,0)) As balance
        FROM temp_account_statement
        LEFT JOIN temp_account_statement AS c 
            ON (c.id <= temp_account_statement.id)
        GROUP BY temp_account_statement.id
        ORDER BY temp_account_statement.id
    ) AS c
    WHERE temp_account_statement.id = c.id;

    UPDATE temp_account_statement SET 
        item_code = inventory.items.item_code,
        item_name = inventory.items.item_name
    FROM inventory.items
    WHERE temp_account_statement.item_id = inventory.items.item_id;

        
    RETURN QUERY
    SELECT * FROM temp_account_statement;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

