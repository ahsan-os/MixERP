# inventory.post_adjustment function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.post_adjustment(_office_id integer, _user_id integer, _login_id bigint, _store_id integer, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.adjustment_type[])
RETURNS bigint
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : post_adjustment
* Arguments : _office_id integer, _user_id integer, _login_id bigint, _store_id integer, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.adjustment_type[]
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.post_adjustment(_office_id integer, _user_id integer, _login_id bigint, _store_id integer, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.adjustment_type[])
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
    DECLARE _transaction_master_id          bigint;
    DECLARE _checkout_id                    bigint;
    DECLARE _book_name                      text='Inventory Adjustment';
    DECLARE _is_periodic                    boolean = inventory.is_periodic_inventory(_office_id);
    DECLARE _default_currency_code          national character varying(12);
BEGIN
    IF NOT finance.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) THEN
        return 0;
    END IF;
    
    CREATE TEMPORARY TABLE IF NOT EXISTS temp_stock_details
    (
        tran_type                       national character varying(2),
        store_id                        integer,
        item_id                         integer,
        item_code                       national character varying(12),
        unit_id                         integer,
        base_unit_id                    integer,
        unit_name                       national character varying(50),
        quantity                        public.decimal_strict,
        base_quantity                   public.decimal_strict,                
        price                           money_strict,
        cost_of_goods_sold              money_strict2 DEFAULT(0),
        inventory_account_id            integer,
        cost_of_goods_sold_account_id   integer
    ) 
    ON COMMIT DROP; 

    DROP TABLE IF EXISTS temp_transaction_details;
    CREATE TEMPORARY TABLE temp_transaction_details
    (
        tran_type                   national character varying(2), 
        account_id                  integer, 
        statement_reference         text, 
        cash_repository_id          integer, 
        currency_code               national character varying(12), 
        amount_in_currency          money_strict, 
        local_currency_code         national character varying(12), 
        er                          decimal_strict, 
        amount_in_local_currency    money_strict
    ) ON COMMIT DROP;

    INSERT INTO temp_stock_details(tran_type, store_id, item_code, unit_name, quantity)
    SELECT tran_type, _store_id, item_code, unit_name, quantity FROM explode_array(_details);

    IF EXISTS
    (
        SELECT * FROM temp_stock_details
        WHERE tran_type = 'Dr'
    ) THEN
        RAISE EXCEPTION 'A stock adjustment entry can not contain debit item(s).'
        USING ERRCODE='P5201';
    END IF;

    IF EXISTS
    (
        SELECT 1 FROM temp_stock_details
        GROUP BY item_code
        HAVING COUNT(item_code) <> 1
    ) THEN
        RAISE EXCEPTION 'An item can appear only once in a store.'
        USING ERRCODE='P5202';
    END IF;

    UPDATE temp_stock_details 
    SET 
        item_id         = inventory.get_item_id_by_item_code(item_code),
        unit_id         = inventory.get_unit_id_by_unit_name(unit_name);

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
        tran_type                       = 'Cr',
        base_quantity                   = inventory.get_base_quantity_by_unit_id(unit_id, quantity),
        base_unit_id                    = inventory.get_root_unit_id(unit_id),
        price                           = inventory.get_item_cost_price(item_id, unit_id),
        inventory_account_id            = inventory.get_inventory_account_id(item_id),
        cost_of_goods_sold_account_id   = inventory.get_cost_of_goods_sold_account_id(item_id);


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
        RAISE EXCEPTION E'Access is denied!\nA stock adjustment transaction cannot references multiple branches.'
        USING ERRCODE='P9012';
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

    --No accounting treatment is needed for periodic accounting system.
    IF(_is_periodic = false) THEN
        _default_currency_code  := core.get_currency_code_by_office_id(_office_id);

        UPDATE temp_stock_details 
        SET 
            cost_of_goods_sold = inventory.get_cost_of_goods_sold(item_id, unit_id, store_id, quantity);
    
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', cost_of_goods_sold_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
        FROM temp_stock_details
        GROUP BY cost_of_goods_sold_account_id;

        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Cr', inventory_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
        FROM temp_stock_details
        GROUP BY inventory_account_id;
    END IF;
    
    _transaction_master_id  := nextval(pg_get_serial_sequence('finance.transaction_master', 'transaction_master_id'));

    INSERT INTO finance.transaction_master
    (
            transaction_master_id,
            transaction_counter,
            transaction_code,
            book,
            value_date,
            book_date,
            login_id,
            user_id,
            office_id,
            reference_number,
            statement_reference
    )
    SELECT
            _transaction_master_id, 
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

    INSERT INTO finance.transaction_details(office_id, value_date, book_date, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency)
    SELECT _office_id, _value_date, _book_date, _transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency
    FROM temp_transaction_details
    ORDER BY tran_type DESC;

    INSERT INTO inventory.checkouts(checkout_id, transaction_master_id, value_date, book_date, transaction_book, posted_by, office_id)
    SELECT nextval(pg_get_serial_sequence('inventory.checkouts', 'checkout_id')), _transaction_master_id, _value_date, _book_date, _book_name, _user_id, _office_id;

    _checkout_id                                := currval(pg_get_serial_sequence('inventory.checkouts', 'checkout_id'));

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

