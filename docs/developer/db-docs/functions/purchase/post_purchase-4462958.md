# purchase.post_purchase function:

```plpgsql
CREATE OR REPLACE FUNCTION purchase.post_purchase(_office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _cost_center_id integer, _reference_number character varying, _statement_reference text, _supplier_id integer, _price_type_id integer, _shipper_id integer, _details purchase.purchase_detail_type[])
RETURNS bigint
```
* Schema : [purchase](../../schemas/purchase.md)
* Function Name : post_purchase
* Arguments : _office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _cost_center_id integer, _reference_number character varying, _statement_reference text, _supplier_id integer, _price_type_id integer, _shipper_id integer, _details purchase.purchase_detail_type[]
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION purchase.post_purchase(_office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _cost_center_id integer, _reference_number character varying, _statement_reference text, _supplier_id integer, _price_type_id integer, _shipper_id integer, _details purchase.purchase_detail_type[])
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
    DECLARE _transaction_master_id          bigint;
    DECLARE _checkout_id                    bigint;
    DECLARE _checkout_detail_id             bigint;
    DECLARE _shipping_address_id            integer;
    DECLARE _grand_total                    public.money_strict;
    DECLARE _discount_total                 public.money_strict2;
    DECLARE _payable                        public.money_strict2;
    DECLARE _default_currency_code          national character varying(12);
    DECLARE _is_periodic                    boolean = inventory.is_periodic_inventory(_office_id);
    DECLARE _tran_counter                   integer;
    DECLARE _transaction_code               text;
    DECLARE _tax_total                      public.money_strict2;
    DECLARE _tax_account_id                 integer;
    DECLARE _shipping_charge                public.money_strict2;
    DECLARE _book_name                      national character varying(100) = 'Purchase';
BEGIN
    IF NOT finance.can_post_transaction(_login_id, _user_id, _office_id, _book_name, _value_date) THEN
        RETURN 0;
    END IF;

    _tax_account_id                         := finance.get_sales_tax_account_id_by_office_id(_office_id);

    IF(_supplier_id IS NULL) THEN
        RAISE EXCEPTION '%', 'Invalid supplier';
    END IF;
    
    DROP TABLE IF EXISTS temp_checkout_details CASCADE;
    CREATE TEMPORARY TABLE temp_checkout_details
    (
        id                              	SERIAL PRIMARY KEY,
        checkout_id                     	bigint, 
        store_id                        	integer,
        transaction_type                	national character varying(2),
        item_id                         	integer, 
        quantity                        	public.integer_strict,
        unit_id                         	integer,
        base_quantity                   	decimal(30, 6),
        base_unit_id                    	integer,
        price                           	public.money_strict NOT NULL DEFAULT(0),
        cost_of_goods_sold              	public.money_strict2 NOT NULL DEFAULT(0),
        discount                        	public.money_strict2 NOT NULL DEFAULT(0),
        tax                                 public.money_strict2 NOT NULL DEFAULT(0),
        shipping_charge                     public.money_strict2 NOT NULL DEFAULT(0),
        purchase_account_id             	integer, 
        purchase_discount_account_id    	integer, 
        inventory_account_id            	integer
    ) ON COMMIT DROP;



    INSERT INTO temp_checkout_details(store_id, transaction_type, item_id, quantity, unit_id, price, discount, tax, shipping_charge)
    SELECT store_id, transaction_type, item_id, quantity, unit_id, price, discount, tax, shipping_charge
    FROM explode_array(_details);


    UPDATE temp_checkout_details 
    SET
        base_quantity                   	= inventory.get_base_quantity_by_unit_id(unit_id, quantity),
        base_unit_id                    	= inventory.get_root_unit_id(unit_id),
        purchase_account_id             	= inventory.get_purchase_account_id(item_id),
        purchase_discount_account_id    	= inventory.get_purchase_discount_account_id(item_id),
        inventory_account_id            	= inventory.get_inventory_account_id(item_id);    
    
    IF EXISTS
    (
            SELECT 1 FROM temp_checkout_details AS details
            WHERE inventory.is_valid_unit_id(details.unit_id, details.item_id) = false
            LIMIT 1
    ) THEN
        RAISE EXCEPTION 'Item/unit mismatch.'
        USING ERRCODE='P3201';
    END IF;

    SELECT SUM(COALESCE(discount, 0))                               INTO _discount_total FROM temp_checkout_details;
    SELECT SUM(COALESCE(price, 0) * COALESCE(quantity, 0))          INTO _grand_total FROM temp_checkout_details;
    SELECT SUM(COALESCE(shipping_charge, 0))                        INTO _shipping_charge FROM temp_checkout_details;
   SELECT SUM(COALESCE(tax, 0))                                     INTO _tax_total FROM temp_checkout_details;


    DROP TABLE IF EXISTS temp_transaction_details;
    CREATE TEMPORARY TABLE temp_transaction_details
    (
        transaction_master_id       		BIGINT, 
        tran_type                   		national character varying(4), 
        account_id                  		integer, 
        statement_reference         		text, 
        currency_code               		national character varying(12), 
        amount_in_currency          		public.money_strict, 
        local_currency_code         		national character varying(12), 
        er                          		decimal_strict, 
        amount_in_local_currency    		public.money_strict
    ) ON COMMIT DROP;

    _payable                                := _grand_total - COALESCE(_discount_total, 0) + COALESCE(_shipping_charge, 0) + COALESCE(_tax_total, 0);
    _default_currency_code              	:= core.get_currency_code_by_office_id(_office_id);
    _transaction_master_id  				:= nextval(pg_get_serial_sequence('finance.transaction_master', 'transaction_master_id'));
    _checkout_id            				:= nextval(pg_get_serial_sequence('inventory.checkouts', 'checkout_id'));
    _tran_counter           				:= finance.get_new_transaction_counter(_value_date);
    _transaction_code       				:= finance.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

    IF(_is_periodic = true) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', purchase_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), 1, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
        FROM temp_checkout_details
        GROUP BY purchase_account_id;
    ELSE
        --Perpetutal Inventory Accounting System
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', inventory_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0)), 1, _default_currency_code, SUM(COALESCE(price, 0) * COALESCE(quantity, 0))
        FROM temp_checkout_details
        GROUP BY inventory_account_id;
    END IF;


    IF(_discount_total > 0) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Cr', purchase_discount_account_id, _statement_reference, _default_currency_code, SUM(COALESCE(discount, 0)), 1, _default_currency_code, SUM(COALESCE(discount, 0))
        FROM temp_checkout_details
        GROUP BY purchase_discount_account_id;
    END IF;

    IF(COALESCE(_tax_total, 0) > 0) THEN
        INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
        SELECT 'Dr', _tax_account_id, _statement_reference, _default_currency_code, _tax_total, 1, _default_currency_code, _tax_total;
    END IF;	

    INSERT INTO temp_transaction_details(tran_type, account_id, statement_reference, currency_code, amount_in_currency, er, local_currency_code, amount_in_local_currency)
    SELECT 'Cr', inventory.get_account_id_by_supplier_id(_supplier_id), _statement_reference, _default_currency_code, _payable, 1, _default_currency_code, _payable;

    --RAISE EXCEPTION '%', _BOOK_DATE;



    UPDATE temp_transaction_details     SET transaction_master_id   = _transaction_master_id;
    UPDATE temp_checkout_details           SET checkout_id         = _checkout_id;
    
    INSERT INTO finance.transaction_master(transaction_master_id, transaction_counter, transaction_code, book, value_date, book_date, user_id, login_id, office_id, cost_center_id, reference_number, statement_reference) 
    SELECT _transaction_master_id, _tran_counter, _transaction_code, _book_name, _value_date, _book_date, _user_id, _login_id, _office_id, _cost_center_id, _reference_number, _statement_reference;

    
    INSERT INTO finance.transaction_details(value_date, book_date, office_id, transaction_master_id, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency)
    SELECT _value_date, _book_date, _office_id, transaction_master_id, tran_type, account_id, statement_reference, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency
    FROM temp_transaction_details
    ORDER BY tran_type DESC;


    INSERT INTO inventory.checkouts(value_date, book_date, checkout_id, transaction_master_id, transaction_book, posted_by, shipper_id, office_id)
    SELECT _value_date, _book_date, _checkout_id, _transaction_master_id, _book_name, _user_id, _shipper_id, _office_id;

    INSERT INTO purchase.purchases(checkout_id, supplier_id, price_type_id)
    SELECT _checkout_id, _supplier_id, _price_type_id;

    INSERT INTO inventory.checkout_details(checkout_id, value_date, book_date, store_id, transaction_type, item_id, price, discount, cost_of_goods_sold, tax, shipping_charge, unit_id, quantity, base_unit_id, base_quantity)
    SELECT _checkout_id, _value_date, _book_date, store_id, transaction_type, item_id, price, discount, cost_of_goods_sold, tax, shipping_charge, unit_id, quantity, base_unit_id, base_quantity
    FROM temp_checkout_details;
    
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

