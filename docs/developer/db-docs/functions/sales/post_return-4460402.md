# sales.post_return function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.post_return(_transaction_master_id bigint, _office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _store_id integer, _counter_id integer, _customer_id integer, _price_type_id integer, _reference_number character varying, _statement_reference text, _details sales.sales_detail_type[])
RETURNS bigint
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : post_return
* Arguments : _transaction_master_id bigint, _office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _store_id integer, _counter_id integer, _customer_id integer, _price_type_id integer, _reference_number character varying, _statement_reference text, _details sales.sales_detail_type[]
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.post_return(_transaction_master_id bigint, _office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _store_id integer, _counter_id integer, _customer_id integer, _price_type_id integer, _reference_number character varying, _statement_reference text, _details sales.sales_detail_type[])
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
    DECLARE _book_name              national character varying(50) = 'Sales Return';
    DECLARE _cost_center_id         bigint;
    DECLARE _tran_master_id         bigint;
    DECLARE _tran_counter           integer;
    DECLARE _tran_code              text;
    DECLARE _checkout_id            bigint;
    DECLARE _grand_total            money_strict;
    DECLARE _discount_total         money_strict2;
    DECLARE _is_credit              boolean;
    DECLARE _default_currency_code  national character varying(12);
    DECLARE _cost_of_goods_sold     money_strict2;
    DECLARE _ck_id                  bigint;
    DECLARE _sales_id               bigint;
    DECLARE _tax_total              public.money_strict2;
    DECLARE _tax_account_id         integer;
    DECLARE _original_store_id      integer;
    DECLARE this                    RECORD;
BEGIN
    IF NOT finance.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) THEN
        RETURN 0;
    END IF;

    IF(_original_store_id != _store_id) THEN
        RAISE EXCEPTION 'Invalid store.'
        USING ERRCODE='P3012';
    END IF;

    _tax_account_id                         := finance.get_sales_tax_account_id_by_office_id(_office_id);

    IF(NOT sales.validate_items_for_return(_transaction_master_id, _details)) THEN
        RETURN 0;
    END IF;

    _default_currency_code          := core.get_currency_code_by_office_id(_office_id);

    SELECT sales.sales.sales_id 
    INTO _sales_id
    FROM sales.sales
    WHERE sales.sales.transaction_master_id = +transaction_master_id;
    
    SELECT cost_center_id   INTO _cost_center_id    
    FROM finance.transaction_master 
    WHERE finance.transaction_master.transaction_master_id = _transaction_master_id;

    SELECT 
        is_credit,
        checkout_id
    INTO 
        _is_credit,
        _ck_id
    FROM sales.sales
    WHERE transaction_master_id = _transaction_master_id;

    DROP TABLE IF EXISTS temp_checkout_details CASCADE;
    CREATE TEMPORARY TABLE temp_checkout_details
    (
        id                              SERIAL PRIMARY KEY,
        checkout_id                     bigint, 
        tran_type                       national character varying(2), 
        store_id                        integer,
        item_id                         integer, 
        quantity                        integer_strict,        
        unit_id                         integer,
        base_quantity                   decimal(30, 6),
        base_unit_id                    integer,                
        price                           public.money_strict,
        cost_of_goods_sold              public.money_strict2 DEFAULT(0),
        discount                        public.money_strict2 DEFAULT(0),
        discount_rate                   public.decimal_strict2,
        tax                             public.money_strict2,
        shipping_charge                 public.money_strict2,
        sales_account_id                integer,
        sales_discount_account_id       integer,
        sales_return_account_id         integer,
        inventory_account_id            integer,
        cost_of_goods_sold_account_id   integer
    ) ON COMMIT DROP;
        
    INSERT INTO temp_checkout_details(store_id, item_id, quantity, unit_id, price, discount_rate, tax, shipping_charge)
    SELECT store_id, item_id, quantity, unit_id, price, discount_rate, tax, shipping_charge
    FROM explode_array(_details);

    UPDATE temp_checkout_details 
    SET
        tran_type                   = 'Dr',
        base_quantity               = inventory.get_base_quantity_by_unit_id(unit_id, quantity),
        base_unit_id                = inventory.get_root_unit_id(unit_id);

    UPDATE temp_checkout_details
    SET
        sales_account_id                = inventory.get_sales_account_id(item_id),
        sales_discount_account_id       = inventory.get_sales_discount_account_id(item_id),
        sales_return_account_id         = inventory.get_sales_return_account_id(item_id),        
        inventory_account_id            = inventory.get_inventory_account_id(item_id),
        cost_of_goods_sold_account_id   = inventory.get_cost_of_goods_sold_account_id(item_id);
    
    IF EXISTS
    (
            SELECT 1 FROM temp_checkout_details AS details
            WHERE inventory.is_valid_unit_id(details.unit_id, details.item_id) = false
            LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Item/unit mismatch.'
        USING ERRCODE='P3201';
    END IF;


    _tran_master_id             := nextval(pg_get_serial_sequence('finance.transaction_master', 'transaction_master_id'));
    _checkout_id                := nextval(pg_get_serial_sequence('inventory.checkouts', 'checkout_id'));
    _tran_counter               := finance.get_new_transaction_counter(_value_date);
    _tran_code                  := finance.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

    INSERT INTO finance.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, book_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference)
    SELECT _tran_master_id, _tran_counter, _tran_code, _book_name, _value_date, _book_date, _user_id, _login_id, _office_id, _cost_center_id, _reference_number, _statement_reference;
        
    SELECT SUM(COALESCE(tax, 0))                                INTO _tax_total FROM temp_checkout_details;
    SELECT SUM(COALESCE(discount, 0))                           INTO _discount_total FROM temp_checkout_details;
    SELECT SUM(COALESCE(price, 0) * COALESCE(quantity, 0))      INTO _grand_total FROM temp_checkout_details;



    UPDATE temp_checkout_details
    SET cost_of_goods_sold = COALESCE(inventory.get_write_off_cost_of_goods_sold(_ck_id, item_id, unit_id, quantity), 0);


    SELECT SUM(cost_of_goods_sold) INTO _cost_of_goods_sold FROM temp_checkout_details;


    IF(_cost_of_goods_sold > 0) THEN
        INSERT INTO finance.transaction_details(transaction_master_id, office_id, value_date, book_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT _tran_master_id, _office_id, _value_date, _book_date, 'Dr', inventory_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
        FROM temp_checkout_details
        GROUP BY inventory_account_id;


        INSERT INTO finance.transaction_details(transaction_master_id, office_id, value_date, book_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT _tran_master_id, _office_id, _value_date, _book_date, 'Cr', cost_of_goods_sold_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
        FROM temp_checkout_details
        GROUP BY cost_of_goods_sold_account_id;
    END IF;


    INSERT INTO finance.transaction_details(transaction_master_id, office_id, value_date, book_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er,amount_in_local_currency) 
    SELECT _tran_master_id, _office_id, _value_date, _book_date, 'Dr', sales_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), _default_currency_code, 1, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
    FROM temp_checkout_details
    GROUP BY sales_account_id;


    IF(_discount_total IS NOT NULL AND _discount_total > 0) THEN
        INSERT INTO finance.transaction_details(transaction_master_id, office_id, value_date, book_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
        SELECT _tran_master_id, _office_id, _value_date, _book_date, 'Cr', sales_discount_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(discount, 0)), _default_currency_code, 1, SUM(COALESCE(discount, 0))
        FROM temp_checkout_details
        GROUP BY sales_discount_account_id;
    END IF;

    IF(COALESCE(_tax_total, 0) > 0) THEN
        INSERT INTO finance.transaction_details(transaction_master_id, office_id, value_date, book_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
        SELECT _tran_master_id, _office_id, _value_date, _book_date, 'Dr', _tax_account_id, _statement_reference, _default_currency_code, _tax_total, _default_currency_code, 1, _tax_total;
    END IF;	

    IF(_is_credit) THEN
        INSERT INTO finance.transaction_details(transaction_master_id, office_id, value_date, book_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
        SELECT _tran_master_id, _office_id, _value_date, _book_date, 'Cr',  inventory.get_account_id_by_customer_id(_customer_id), _statement_reference, _default_currency_code, _grand_total - _discount_total, _default_currency_code, 1, _grand_total - _discount_total;
    ELSE
        INSERT INTO finance.transaction_details(transaction_master_id, office_id, value_date, book_date, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency) 
        SELECT _tran_master_id, _office_id, _value_date, _book_date, 'Cr',  sales_return_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)) - SUM(COALESCE(discount, 0)), _default_currency_code, 1, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)) - SUM(COALESCE(discount, 0)) + SUM(COALESCE(tax, 0))
        FROM temp_checkout_details
        GROUP BY sales_return_account_id;
    END IF;



    INSERT INTO inventory.checkouts(checkout_id, transaction_book, value_date, book_date, transaction_master_id, office_id, posted_by) 
    SELECT _checkout_id, _book_name, _value_date, _book_date, _tran_master_id, _office_id, _user_id;


    INSERT INTO inventory.checkout_details(value_date, book_date, checkout_id, transaction_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, tax, cost_of_goods_sold, discount)
    SELECT _value_date, _book_date, _checkout_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, tax, cost_of_goods_sold, discount FROM temp_checkout_details;

    INSERT INTO sales.returns(sales_id, checkout_id, counter_id, transaction_master_id, return_transaction_master_id, customer_id, price_type_id, is_credit)
    SELECT _sales_id, _checkout_id, _counter_id, _transaction_master_id, _tran_master_id, _customer_id, _price_type_id, false;

    PERFORM finance.auto_verify(_tran_master_id, _office_id);
    RETURN _tran_master_id;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

