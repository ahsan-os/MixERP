# inventory.post_opening_inventory function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.post_opening_inventory(_office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.opening_stock_type[])
RETURNS bigint
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : post_opening_inventory
* Arguments : _office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.opening_stock_type[]
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.post_opening_inventory(_office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.opening_stock_type[])
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
    DECLARE _book_name                      text = 'Opening Inventory';
    DECLARE _transaction_master_id          bigint;
    DECLARE _checkout_id                bigint;
    DECLARE _tran_counter                   integer;
    DECLARE _transaction_code               text;
BEGIN
    IF NOT finance.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) THEN
        return 0;
    END IF;

    DROP TABLE IF EXISTS temp_stock_details;
    
    CREATE TEMPORARY TABLE temp_stock_details
    (
        id                              SERIAL PRIMARY KEY,
        tran_type                       national character varying(2),
        store_id                        integer,
        item_id                         integer, 
        quantity                        integer_strict,
        unit_id                         integer,
        base_quantity                   decimal(30, 6),
        base_unit_id                    integer,                
        price                           money_strict
    ) ON COMMIT DROP;

    INSERT INTO temp_stock_details(store_id, item_id, quantity, unit_id, price)
    SELECT store_id, item_id, quantity, unit_id, price
    FROM explode_array(_details);

    UPDATE temp_stock_details 
    SET
        tran_type                       = 'Dr',
        base_quantity                   = inventory.get_base_quantity_by_unit_id(unit_id, quantity),
        base_unit_id                    = inventory.get_root_unit_id(unit_id);

    IF EXISTS
    (
        SELECT * FROM temp_stock_details
        WHERE store_id IS NULL
        OR item_id IS NULL
        OR unit_id IS NULL
    ) THEN
        RAISE EXCEPTION 'Access is denied. Invalid values supplied.'
        USING ERRCODE='P9011';
    END IF;

    IF EXISTS
    (
            SELECT 1 FROM temp_stock_details AS details
            WHERE inventory.is_valid_unit_id(details.unit_id, details.item_id) = false
            LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Item/unit mismatch.'
        USING ERRCODE='P3201';
    END IF;

    
    _transaction_master_id  := nextval(pg_get_serial_sequence('finance.transaction_master', 'transaction_master_id'));
    _checkout_id            := nextval(pg_get_serial_sequence('inventory.checkouts', 'checkout_id'));
    _tran_counter           := finance.get_new_transaction_counter(_value_date);
    _transaction_code       := finance.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

    INSERT INTO finance.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, book_date, user_id, login_id, office_id, reference_number, statement_reference) 
    SELECT _transaction_master_id, _tran_counter, _transaction_code, _book_name, _value_date, _book_date, _user_id, _login_id, _office_id, _reference_number, _statement_reference;

    INSERT INTO inventory.checkouts(transaction_book, value_date, book_date, checkout_id, transaction_master_id, posted_by, office_id)
    SELECT _book_name, _value_date, _book_date, _checkout_id, _transaction_master_id, _user_id, _office_id;

    INSERT INTO inventory.checkout_details(value_date, book_date, checkout_id, transaction_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price)
    SELECT _value_date, _book_date, _checkout_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price
    FROM temp_stock_details;
    
    PERFORM finance.auto_verify(_transaction_master_id, _office_id);    
    RETURN _transaction_master_id;
END;
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

