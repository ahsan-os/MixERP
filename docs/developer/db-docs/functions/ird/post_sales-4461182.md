# ird.post_sales function:

```plpgsql
CREATE OR REPLACE FUNCTION ird.post_sales(_office_id integer, _user_id integer, _login_id bigint, _counter_id integer, _value_date date, _book_date date, _cost_center_id integer, _reference_number character varying, _statement_reference text, _tender money_strict2, _change money_strict2, _payment_term_id integer, _check_amount money_strict2, _check_bank_name character varying, _check_number character varying, _check_date date, _gift_card_number character varying, _customer_id integer, _price_type_id integer, _shipper_id integer, _store_id integer, _coupon_code character varying, _is_flat_discount boolean, _discount money_strict2, _details sales.sales_detail_type[], _sales_quotation_id bigint, _sales_order_id bigint)
RETURNS bigint
```
* Schema : [ird](../../schemas/ird.md)
* Function Name : post_sales
* Arguments : _office_id integer, _user_id integer, _login_id bigint, _counter_id integer, _value_date date, _book_date date, _cost_center_id integer, _reference_number character varying, _statement_reference text, _tender money_strict2, _change money_strict2, _payment_term_id integer, _check_amount money_strict2, _check_bank_name character varying, _check_number character varying, _check_date date, _gift_card_number character varying, _customer_id integer, _price_type_id integer, _shipper_id integer, _store_id integer, _coupon_code character varying, _is_flat_discount boolean, _discount money_strict2, _details sales.sales_detail_type[], _sales_quotation_id bigint, _sales_order_id bigint
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION ird.post_sales(_office_id integer, _user_id integer, _login_id bigint, _counter_id integer, _value_date date, _book_date date, _cost_center_id integer, _reference_number character varying, _statement_reference text, _tender money_strict2, _change money_strict2, _payment_term_id integer, _check_amount money_strict2, _check_bank_name character varying, _check_number character varying, _check_date date, _gift_card_number character varying, _customer_id integer, _price_type_id integer, _shipper_id integer, _store_id integer, _coupon_code character varying, _is_flat_discount boolean, _discount money_strict2, _details sales.sales_detail_type[], _sales_quotation_id bigint, _sales_order_id bigint)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
    DECLARE _vat_rate                       public.decimal_strict = 13;
    DECLARE _transaction_master_id          bigint;
BEGIN        
    DROP TABLE IF EXISTS temp_ird_checkout_details CASCADE;
    CREATE TEMPORARY TABLE temp_ird_checkout_details
    (
        store_id                        integer,
        item_id                         integer,
        is_taxable_item                 boolean,
        quantity                        public.decimal_strict,        
        unit_id                         integer,
        price                           public.money_strict,
        discount_rate                   public.decimal_strict2,
        tax                             public.money_strict2,
        shipping_charge                 public.money_strict2
    ) ON COMMIT DROP;

    INSERT INTO temp_ird_checkout_details(store_id, item_id, quantity, unit_id, price, discount_rate, tax, shipping_charge)
    SELECT store_id, item_id, quantity, unit_id, price, discount_rate, tax, shipping_charge
    FROM explode_array(_details);

    UPDATE temp_ird_checkout_details
    SET is_taxable_item = inventory.items.is_taxable_item
    FROM inventory.items
    WHERE inventory.items.item_id = temp_ird_checkout_details.item_id;

    UPDATE temp_ird_checkout_details
    SET price = ROUND(price*((100-discount_rate)/100)*(100/(100 + _vat_rate))*(100/(100 - discount_rate)), 2)
    WHERE is_taxable_item;

    SELECT ARRAY(SELECT ROW(store_id, '', item_id, quantity, unit_id, price, discount_rate, tax, shipping_charge)::sales.sales_detail_type FROM temp_ird_checkout_details)
    INTO _details;

    --RAISE EXCEPTION '%', _details;
    
    _transaction_master_id := sales.post_sales
    (
        _office_id,
        _user_id,
        _login_id,
        _counter_id,
        _value_date,
        _book_date,
        _cost_center_id,
        _reference_number,
        _statement_reference,
        _tender,
        _change,
        _payment_term_id,
        _check_amount,
        _check_bank_name,
        _check_number,
        _check_date,
        _gift_card_number,
        _customer_id,
        _price_type_id,
        _shipper_id,
        _store_id,
        _coupon_code,
        _is_flat_discount,
        _discount,
        _details,
        _sales_quotation_id,
        _sales_order_id
    );

    RETURN _transaction_master_id;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

