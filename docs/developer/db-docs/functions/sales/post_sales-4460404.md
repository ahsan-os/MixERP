# sales.post_sales function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.post_sales(_office_id integer, _user_id integer, _login_id bigint, _counter_id integer, _value_date date, _book_date date, _cost_center_id integer, _reference_number character varying, _statement_reference text, _tender money_strict2, _change money_strict2, _payment_term_id integer, _check_amount money_strict2, _check_bank_name character varying, _check_number character varying, _check_date date, _gift_card_number character varying, _customer_id integer, _price_type_id integer, _shipper_id integer, _store_id integer, _coupon_code character varying, _is_flat_discount boolean, _discount money_strict2, _details sales.sales_detail_type[], _sales_quotation_id bigint, _sales_order_id bigint)
RETURNS bigint
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : post_sales
* Arguments : _office_id integer, _user_id integer, _login_id bigint, _counter_id integer, _value_date date, _book_date date, _cost_center_id integer, _reference_number character varying, _statement_reference text, _tender money_strict2, _change money_strict2, _payment_term_id integer, _check_amount money_strict2, _check_bank_name character varying, _check_number character varying, _check_date date, _gift_card_number character varying, _customer_id integer, _price_type_id integer, _shipper_id integer, _store_id integer, _coupon_code character varying, _is_flat_discount boolean, _discount money_strict2, _details sales.sales_detail_type[], _sales_quotation_id bigint, _sales_order_id bigint
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.post_sales(_office_id integer, _user_id integer, _login_id bigint, _counter_id integer, _value_date date, _book_date date, _cost_center_id integer, _reference_number character varying, _statement_reference text, _tender money_strict2, _change money_strict2, _payment_term_id integer, _check_amount money_strict2, _check_bank_name character varying, _check_number character varying, _check_date date, _gift_card_number character varying, _customer_id integer, _price_type_id integer, _shipper_id integer, _store_id integer, _coupon_code character varying, _is_flat_discount boolean, _discount money_strict2, _details sales.sales_detail_type[], _sales_quotation_id bigint, _sales_order_id bigint)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
    DECLARE _book_name                      national character varying(48) = 'Sales Entry';
    DECLARE _transaction_master_id          bigint;
    DECLARE _checkout_id                    bigint;
    DECLARE _grand_total                    public.money_strict;
    DECLARE _discount_total                 public.money_strict2;
    DECLARE _receivable                     public.money_strict2;
    DECLARE _default_currency_code          national character varying(12);
    DECLARE _is_periodic                    boolean = inventory.is_periodic_inventory(_office_id);
    DECLARE _cost_of_goods                  public.money_strict;
    DECLARE _tran_counter                   integer;
    DECLARE _transaction_code               text;
    DECLARE _tax_total                      public.money_strict2;
    DECLARE _shipping_charge                public.money_strict2;
    DECLARE _cash_repository_id             integer;
    DECLARE _cash_account_id                integer;
    DECLARE _is_cash                        boolean = false;
    DECLARE _is_credit                      boolean = false;
    DECLARE _gift_card_id                   integer;
    DECLARE _gift_card_balance              numeric(30, 6);
    DECLARE _coupon_id                      integer;
    DECLARE _coupon_discount                numeric(30, 6); 
    DECLARE _default_discount_account_id    integer;
    DECLARE _fiscal_year_code               national character varying(12);
    DECLARE _invoice_number                 bigint;
    DECLARE _tax_account_id                 integer;
    DECLARE _receipt_transaction_master_id  bigint;
    DECLARE this                            RECORD;
BEGIN        
    IF NOT finance.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) THEN
        RETURN 0;
    END IF;

    _tax_account_id                         := finance.get_sales_tax_account_id_by_office_id(_office_id);
    _default_currency_code                  := core.get_currency_code_by_office_id(_office_id);
    _cash_account_id                        := inventory.get_cash_account_id_by_store_id(_store_id);
    _cash_repository_id                     := inventory.get_cash_repository_id_by_store_id(_store_id);
    _is_cash                                := finance.is_cash_account_id(_cash_account_id);    

    _coupon_id                              := sales.get_active_coupon_id_by_coupon_code(_coupon_code);
    _gift_card_id                           := sales.get_gift_card_id_by_gift_card_number(_gift_card_number);
    _gift_card_balance                      := sales.get_gift_card_balance(_gift_card_id, _value_date);


    SELECT finance.fiscal_year.fiscal_year_code INTO _fiscal_year_code
    FROM finance.fiscal_year
    WHERE _value_date BETWEEN finance.fiscal_year.starts_from AND finance.fiscal_year.ends_on
    LIMIT 1;

    IF(COALESCE(_customer_id, 0) = 0) THEN
        RAISE EXCEPTION 'Please select a customer.';
    END IF;

    IF(COALESCE(_coupon_code, '') != '' AND COALESCE(_discount, 0) > 0) THEN
        RAISE EXCEPTION 'Please do not specify discount rate when you mention coupon code.';
    END IF;
    --TODO: VALIDATE COUPON CODE AND POST DISCOUNT

    IF(COALESCE(_payment_term_id, 0) > 0) THEN
        _is_credit                          := true;
    END IF;

    IF(NOT _is_credit AND NOT _is_cash) THEN
        RAISE EXCEPTION 'Cannot post sales. Invalid cash account mapping on store.'
        USING ERRCODE='P1302';
    END IF;

   
    IF(NOT _is_cash) THEN
        _cash_repository_id                 := NULL;
    END IF;

    DROP TABLE IF EXISTS temp_checkout_details CASCADE;
    CREATE TEMPORARY TABLE temp_checkout_details
    (
        id                              SERIAL PRIMARY KEY,
        checkout_id                     bigint, 
        tran_type                       national character varying(2), 
        store_id                        integer,
        item_id                         integer, 
        quantity                        public.decimal_strict,        
        unit_id                         integer,
        base_quantity                   decimal(30, 6),
        base_unit_id                    integer,                
        price                           public.money_strict,
        cost_of_goods_sold              public.money_strict2 DEFAULT(0),
        discount_rate                   public.decimal_strict2,
        discount                        public.money_strict2,
        tax                             public.money_strict2,
        shipping_charge                 public.money_strict2,
        sales_account_id                integer,
        sales_discount_account_id       integer,
        inventory_account_id            integer,
        cost_of_goods_sold_account_id   integer
    ) ON COMMIT DROP;

    INSERT INTO temp_checkout_details(store_id, item_id, quantity, unit_id, price, discount_rate, tax, shipping_charge)
    SELECT store_id, item_id, quantity, unit_id, price, discount_rate, tax, shipping_charge
    FROM explode_array(_details);

    
    UPDATE temp_checkout_details 
    SET
        tran_type                       = 'Cr',
        base_quantity                   = inventory.get_base_quantity_by_unit_id(unit_id, quantity),
        base_unit_id                    = inventory.get_root_unit_id(unit_id),
        discount                        = ROUND((price * quantity) * (discount_rate / 100), 2);


    UPDATE temp_checkout_details
    SET
        sales_account_id                = inventory.get_sales_account_id(item_id),
        sales_discount_account_id       = inventory.get_sales_discount_account_id(item_id),
        inventory_account_id            = inventory.get_inventory_account_id(item_id),
        cost_of_goods_sold_account_id   = inventory.get_cost_of_goods_sold_account_id(item_id);

    DROP TABLE IF EXISTS item_quantities_temp;
    CREATE TEMPORARY TABLE item_quantities_temp
    (
        item_id             integer,
        base_unit_id        integer,
        store_id            integer,
        total_sales         numeric(30, 6),
        in_stock            numeric(30, 6),
        maintain_inventory      boolean
    ) ON COMMIT DROP;

    INSERT INTO item_quantities_temp(item_id, base_unit_id, store_id, total_sales)
    SELECT item_id, base_unit_id, store_id, SUM(base_quantity)
    FROM temp_checkout_details
    GROUP BY item_id, base_unit_id, store_id;

    UPDATE item_quantities_temp
    SET maintain_inventory = inventory.items.maintain_inventory
    FROM inventory.items
    WHERE item_quantities_temp.item_id = inventory.items.item_id;
    
    UPDATE item_quantities_temp
    SET in_stock = inventory.count_item_in_stock(item_quantities_temp.item_id, item_quantities_temp.base_unit_id, item_quantities_temp.store_id)
    WHERE maintain_inventory;


    IF EXISTS
    (
        SELECT 0 FROM item_quantities_temp
        WHERE total_sales > in_stock
        AND maintain_inventory
        LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Insufficient item quantity'
        USING ERRCODE='P5500';
    END IF;
    
    IF EXISTS
    (
        SELECT 1 FROM temp_checkout_details AS details
        WHERE inventory.is_valid_unit_id(details.unit_id, details.item_id) = false
        LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Item/unit mismatch.'
        USING ERRCODE='P3201';
    END IF;

    SELECT ROUND(SUM(COALESCE(discount, 0)), 2)                 INTO _discount_total FROM temp_checkout_details;
    SELECT SUM(COALESCE(price, 0) * COALESCE(quantity, 0))      INTO _grand_total FROM temp_checkout_details;
    SELECT SUM(COALESCE(shipping_charge, 0))                    INTO _shipping_charge FROM temp_checkout_details;
    SELECT ROUND(SUM(COALESCE(tax, 0)), 2)                      INTO _tax_total FROM temp_checkout_details;

     
     _receivable                    := COALESCE(_grand_total, 0) - COALESCE(_discount_total, 0) + COALESCE(_tax_total, 0) + COALESCE(_shipping_charge, 0);
        
    IF(_is_flat_discount AND _discount > _receivable) THEN
        RAISE EXCEPTION 'The discount amount cannot be greater than total amount.';
    ELSIF(NOT _is_flat_discount AND _discount > 100) THEN
        RAISE EXCEPTION 'The discount rate cannot be greater than 100.';    
    END IF;

    _coupon_discount                := ROUND(_discount, 2);

    IF(NOT _is_flat_discount AND COALESCE(_discount, 0) > 0) THEN
        _coupon_discount            := ROUND(_receivable * (_discount/100), 2);
    END IF;

    IF(COALESCE(_coupon_discount, 0) > 0) THEN
        _discount_total             := _discount_total + _coupon_discount;
        _receivable                 := _receivable - _coupon_discount;
    END IF;

    IF(_tender > 0) THEN
        IF(_tender < _receivable ) THEN
            RAISE EXCEPTION 'The tender amount must be greater than or equal to %.', _receivable;
        END IF;
    ELSIF(_check_amount > 0) THEN
        IF(_check_amount < _receivable ) THEN
            RAISE EXCEPTION 'The check amount must be greater than or equal to %.', _receivable;
        END IF;
    ELSIF(COALESCE(_gift_card_number, '') != '') THEN
        IF(_gift_card_balance < _receivable ) THEN
            RAISE EXCEPTION 'The gift card must have a balance of at least %.', _receivable;
        END IF;
    END IF;
    
    DROP TABLE IF EXISTS temp_transaction_details;
    CREATE TEMPORARY TABLE temp_transaction_details
    (
        transaction_master_id       BIGINT, 
        tran_type                   national character varying(2), 
        account_id                  integer NOT NULL, 
        statement_reference         text, 
        cash_repository_id          integer, 
        currency_code               national character varying(12), 
        amount_in_currency          money_strict NOT NULL, 
        local_currency_code         national character varying(12), 
        er                          decimal_strict, 
        amount_in_local_currency    money_strict
    ) ON COMMIT DROP;


    INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
    SELECT 'Cr', sales_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), 1, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
    FROM temp_checkout_details
    GROUP BY sales_account_id;

    IF(NOT _is_periodic) THEN
        --Perpetutal Inventory Accounting System

        UPDATE temp_checkout_details SET cost_of_goods_sold = inventory.get_cost_of_goods_sold(item_id, unit_id, store_id, quantity);
        
        SELECT SUM(cost_of_goods_sold) INTO _cost_of_goods
        FROM temp_checkout_details;


        IF(_cost_of_goods > 0) THEN
            INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
            SELECT 'Dr', cost_of_goods_sold_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
            FROM temp_checkout_details
            GROUP BY cost_of_goods_sold_account_id;

            INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
            SELECT 'Cr', inventory_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0)), 1, _default_currency_code, SUM(COALESCE(cost_of_goods_sold, 0))
            FROM temp_checkout_details
            GROUP BY inventory_account_id;
        END IF;
    END IF;

    IF(COALESCE(_tax_total, 0) > 0) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Cr', _tax_account_id, _statement_reference, _default_currency_code, _tax_total, 1, _default_currency_code, _tax_total;
    END IF;

    IF(COALESCE(_shipping_charge, 0) > 0) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Cr', inventory.get_account_id_by_shipper_id(_shipper_id), _statement_reference, _default_currency_code, _shipping_charge, 1, _default_currency_code, _shipping_charge;                
    END IF;


    IF(_discount_total > 0) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', sales_discount_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(discount, 0)), 1, _default_currency_code, SUM(COALESCE(discount, 0))
        FROM temp_checkout_details
        GROUP BY sales_discount_account_id
        HAVING SUM(COALESCE(discount, 0)) > 0;
    END IF;


    IF(_coupon_discount > 0) THEN
        SELECT inventory.inventory_setup.default_discount_account_id INTO _default_discount_account_id
        FROM inventory.inventory_setup
        WHERE inventory.inventory_setup.office_id = _office_id;

        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', _default_discount_account_id, _statement_reference, _default_currency_code, _coupon_discount, 1, _default_currency_code, _coupon_discount;
    END IF;



    INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
    SELECT 'Dr', inventory.get_account_id_by_customer_id(_customer_id), _statement_reference, _default_currency_code, _receivable, 1, _default_currency_code, _receivable;

    
    _transaction_master_id  := nextval(pg_get_serial_sequence('finance.transaction_master', 'transaction_master_id'));
    _checkout_id        := nextval(pg_get_serial_sequence('inventory.checkouts', 'checkout_id'));    
    _tran_counter           := finance.get_new_transaction_counter(_value_date);
    _transaction_code       := finance.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

    UPDATE temp_transaction_details     SET transaction_master_id   = _transaction_master_id;
    UPDATE temp_checkout_details           SET checkout_id         = _checkout_id;
    
    INSERT INTO finance.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, book_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference) 
    SELECT _transaction_master_id, _tran_counter, _transaction_code, _book_name, _value_date, _book_date, _user_id, _login_id, _office_id, _cost_center_id, _reference_number, _statement_reference;


    INSERT INTO finance.transaction_details(value_date, book_date, office_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency)
    SELECT _value_date, _book_date, _office_id, transaction_master_id, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency
    FROM temp_transaction_details
    ORDER BY tran_type DESC;

    INSERT INTO inventory.checkouts(transaction_book, value_date, book_date, checkout_id, transaction_master_id, shipper_id, posted_by, office_id, discount)
    SELECT _book_name, _value_date, _book_date, _checkout_id, _transaction_master_id, _shipper_id, _user_id, _office_id, _coupon_discount;

    INSERT INTO inventory.checkout_details(value_date, book_date, checkout_id, transaction_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, cost_of_goods_sold, discount, tax, shipping_charge)
    SELECT _value_date, _book_date, checkout_id, tran_type, store_id, item_id, quantity, unit_id, base_quantity, base_unit_id, price, COALESCE(cost_of_goods_sold, 0), discount, tax, shipping_charge 
    FROM temp_checkout_details;

    SELECT
        COALESCE(MAX(invoice_number), 0) + 1
    INTO
        _invoice_number
    FROM sales.sales
    WHERE sales.sales.fiscal_year_code = _fiscal_year_code;
    

    IF(NOT _is_credit) THEN
        SELECT sales.post_receipt
        (
            _user_id, 
            _office_id, 
            _login_id,
            _customer_id,
            _default_currency_code, 
            1.0, 
            1.0,
            _reference_number, 
            _statement_reference, 
            _cost_center_id,
            _cash_account_id,
            _cash_repository_id,
            _value_date,
            _book_date,
            _receivable,
            _tender,
            _change,
            _check_amount,
            _check_bank_name,
            _check_number,
            _check_date,
            _gift_card_number,
            _store_id,
            _transaction_master_id
        ) INTO _receipt_transaction_master_id;

        PERFORM finance.auto_verify(_receipt_transaction_master_id, _office_id);        
    ELSE
        PERFORM sales.settle_customer_due(_customer_id, _office_id);
    END IF;

    INSERT INTO sales.sales(fiscal_year_code, invoice_number, price_type_id, counter_id, total_amount, cash_repository_id, sales_order_id, sales_quotation_id, transaction_master_id, checkout_id, customer_id, salesperson_id, coupon_id, is_flat_discount, discount, total_discount_amount, is_credit, payment_term_id, tender, change, check_number, check_date, check_bank_name, check_amount, gift_card_id, receipt_transaction_master_id)
    SELECT _fiscal_year_code, _invoice_number, _price_type_id, _counter_id, _receivable, _cash_repository_id, _sales_order_id, _sales_quotation_id, _transaction_master_id, _checkout_id, _customer_id, _user_id, _coupon_id, _is_flat_discount, _discount, _discount_total, _is_credit, _payment_term_id, _tender, _change, _check_number, _check_date, _check_bank_name, _check_amount, _gift_card_id, _receipt_transaction_master_id;
    
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

