# inventory.post_transfer function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.post_transfer(_office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.transfer_type[])
RETURNS bigint
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : post_transfer
* Arguments : _office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.transfer_type[]
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.post_transfer(_office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.transfer_type[])
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
    DECLARE _transaction_master_id          bigint;
    DECLARE _checkout_id                    bigint;
    DECLARE _book_name                      text='Inventory Transfer';
BEGIN
    IF NOT finance.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) THEN
        RETURN 0;
    END IF;

    CREATE TEMPORARY TABLE IF NOT EXISTS temp_stock_details
    (
        tran_type       national character varying(2),
        store_id        integer,
        store_name      national character varying(500),
        item_id         integer,
        item_code       national character varying(24),
        unit_id         integer,
        base_unit_id    integer,
        unit_name       national character varying(500),
        quantity        public.decimal_strict,
        base_quantity   public.decimal_strict,                
        price           money_strict
    ) 
    ON COMMIT DROP; 

    INSERT INTO temp_stock_details(tran_type, store_name, item_code, unit_name, quantity, price)
    SELECT tran_type, store_name, item_code, unit_name, quantity, rate * quantity
    FROM explode_array(_details);

    IF EXISTS
    (
        SELECT 1 FROM temp_stock_details
        GROUP BY item_code, store_name
        HAVING COUNT(item_code) <> 1
    ) THEN
        RAISE EXCEPTION 'An item can appear only once in a store.'
        USING ERRCODE='P5202';
    END IF;

    UPDATE temp_stock_details SET 
    item_id         = inventory.get_item_id_by_item_code(item_code),
    unit_id         = inventory.get_unit_id_by_unit_name(unit_name),
    store_id        = inventory.get_store_id_by_store_name(store_name);

    IF EXISTS
    (
        SELECT * FROM temp_stock_details
        WHERE item_id IS NULL OR unit_id IS NULL OR store_id IS NULL
    ) THEN
        RAISE EXCEPTION 'Invalid data supplied.'
        USING ERRCODE='P3000';
    END IF;

    UPDATE temp_stock_details 
    SET
        base_unit_id    = inventory.get_root_unit_id(unit_id),
        base_quantity   = inventory.get_base_quantity_by_unit_id(unit_id, quantity);

    UPDATE temp_stock_details 
    SET
        price           = inventory.get_item_cost_price(item_id, unit_id)
    WHERE temp_stock_details.price IS NULL;

    IF EXISTS
    (
        SELECT item_code FROM temp_stock_details
        GROUP BY item_code
        HAVING SUM(CASE WHEN tran_type='Dr' THEN base_quantity ELSE base_quantity *-1 END) <> 0
    ) THEN
        RAISE EXCEPTION 'Referencing sides are not equal.'
        USING ERRCODE='P5000';        
    END IF;


    IF EXISTS
    (
            SELECT 1
            FROM 
            inventory.stores
            WHERE inventory.stores.store_id
            IN
            (
                SELECT temp_stock_details.store_id
                FROM temp_stock_details
            )
            HAVING COUNT(DISTINCT inventory.stores.office_id) > 1

    ) THEN
        RAISE EXCEPTION E'Access is denied!\nA stock journal transaction cannot references multiple branches.'
        USING ERRCODE='P9013';
    END IF;

    IF EXISTS
    (
            SELECT 1
            FROM 
            temp_stock_details
            WHERE tran_type = 'Cr'
            AND quantity > inventory.count_item_in_stock(item_id, unit_id, store_id)
    ) THEN
        RAISE EXCEPTION 'Negative stock is not allowed.'
        USING ERRCODE='P5001';
    END IF;

    INSERT INTO finance.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, book_date, login_id, user_id, office_id, reference_number, statement_reference)
    SELECT
            nextval(pg_get_serial_sequence('finance.transaction_master', 'transaction_master_id')), 
            finance.get_new_transaction_counter(_value_date), 
            finance.get_transaction_code(_value_date, _office_id, _user_id, _login_id),
            _book_name,
            _value_date,
            _book_date,
            _login_id,
            _user_id,
            _office_id,
            _reference_number,
            _statement_reference;


    _transaction_master_id  := currval(pg_get_serial_sequence('finance.transaction_master', 'transaction_master_id'));


    INSERT INTO inventory.checkouts(checkout_id, transaction_master_id, transaction_book, value_date, book_date, posted_by, office_id)
    SELECT nextval(pg_get_serial_sequence('inventory.checkouts', 'checkout_id')), _transaction_master_id, _book_name, _value_date, _book_date, _user_id, _office_id;

    _checkout_id  := currval(pg_get_serial_sequence('inventory.checkouts', 'checkout_id'));

    INSERT INTO inventory.checkout_details(checkout_id, value_date, book_date, transaction_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price)
    SELECT _checkout_id, _value_date, _book_date, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price
    FROM temp_stock_details;
    
    
    PERFORM finance.auto_verify(_transaction_master_id, _office_id);
    RETURN _transaction_master_id;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

