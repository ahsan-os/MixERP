# sales.get_avaiable_coupons_to_print function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.get_avaiable_coupons_to_print(_tran_id bigint)
RETURNS TABLE(coupon_id integer)
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : get_avaiable_coupons_to_print
* Arguments : _tran_id bigint
* Owner : frapid_db_user
* Result Type : TABLE(coupon_id integer)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.get_avaiable_coupons_to_print(_tran_id bigint)
 RETURNS TABLE(coupon_id integer)
 LANGUAGE plpgsql
AS $function$
    DECLARE _price_type_id                  integer;
    DECLARE _total_amount                   public.money_strict;
    DECLARE _customer_id                    integer;
BEGIN
    DROP TABLE IF EXISTS temp_coupons;
    CREATE TEMPORARY TABLE temp_coupons
    (
        coupon_id                           integer,
        maximum_usage                       public.integer_strict,
        total_used                          integer
    ) ON COMMIT DROP;
    
    SELECT
        sales.sales.price_type_id,
        sales.sales.total_amount,
        sales.sales.customer_id
    INTO
        _price_type_id,
        _total_amount,
        _customer_id
    FROM sales.sales
    WHERE sales.sales.transaction_master_id = _tran_id;


    INSERT INTO temp_coupons
    SELECT sales.coupons.coupon_id, sales.coupons.maximum_usage
    FROM sales.coupons
    WHERE NOT sales.coupons.deleted
    AND sales.coupons.enable_ticket_printing = true
    AND (sales.coupons.begins_from IS NULL OR sales.coupons.begins_from >= NOW()::date)
    AND (sales.coupons.expires_on IS NULL OR sales.coupons.expires_on <= NOW()::date)
    AND sales.coupons.for_ticket_of_price_type_id IS NULL
    AND COALESCE(sales.coupons.for_ticket_having_minimum_amount, 0) = 0
    AND COALESCE(sales.coupons.for_ticket_having_maximum_amount, 0) = 0
    AND sales.coupons.for_ticket_of_unknown_customers_only IS NULL;

    INSERT INTO temp_coupons
    SELECT sales.coupons.coupon_id, sales.coupons.maximum_usage
    FROM sales.coupons
    WHERE NOT sales.coupons.deleted
    AND sales.coupons.enable_ticket_printing = true
    AND (sales.coupons.begins_from IS NULL OR sales.coupons.begins_from >= NOW()::date)
    AND (sales.coupons.expires_on IS NULL OR sales.coupons.expires_on <= NOW()::date)
    AND (sales.coupons.for_ticket_of_price_type_id IS NULL OR for_ticket_of_price_type_id = _price_type_id)
    AND (sales.coupons.for_ticket_having_minimum_amount IS NULL OR sales.coupons.for_ticket_having_minimum_amount <= _total_amount)
    AND (sales.coupons.for_ticket_having_maximum_amount IS NULL OR sales.coupons.for_ticket_having_maximum_amount >= _total_amount)
    AND sales.coupons.for_ticket_of_unknown_customers_only IS NULL;

    IF(COALESCE(_customer_id, 0) > 0) THEN
        INSERT INTO temp_coupons
        SELECT sales.coupons.coupon_id, sales.coupons.maximum_usage
        FROM sales.coupons
        WHERE NOT sales.coupons.deleted
        AND sales.coupons.enable_ticket_printing = true
        AND (sales.coupons.begins_from IS NULL OR sales.coupons.begins_from >= NOW()::date)
        AND (sales.coupons.expires_on IS NULL OR sales.coupons.expires_on <= NOW()::date)
        AND (sales.coupons.for_ticket_of_price_type_id IS NULL OR for_ticket_of_price_type_id = _price_type_id)
        AND (sales.coupons.for_ticket_having_minimum_amount IS NULL OR sales.coupons.for_ticket_having_minimum_amount <= _total_amount)
        AND (sales.coupons.for_ticket_having_maximum_amount IS NULL OR sales.coupons.for_ticket_having_maximum_amount >= _total_amount)
        AND NOT sales.coupons.for_ticket_of_unknown_customers_only;
    ELSE
        INSERT INTO temp_coupons
        SELECT sales.coupons.coupon_id, sales.coupons.maximum_usage
        FROM sales.coupons
        WHERE NOT sales.coupons.deleted
        AND sales.coupons.enable_ticket_printing = true
        AND (sales.coupons.begins_from IS NULL OR sales.coupons.begins_from >= NOW()::date)
        AND (sales.coupons.expires_on IS NULL OR sales.coupons.expires_on <= NOW()::date)
        AND (sales.coupons.for_ticket_of_price_type_id IS NULL OR for_ticket_of_price_type_id = _price_type_id)
        AND (sales.coupons.for_ticket_having_minimum_amount IS NULL OR sales.coupons.for_ticket_having_minimum_amount <= _total_amount)
        AND (sales.coupons.for_ticket_having_maximum_amount IS NULL OR sales.coupons.for_ticket_having_maximum_amount >= _total_amount)
        AND sales.coupons.for_ticket_of_unknown_customers_only;    
    END IF;

    UPDATE temp_coupons
    SET total_used = 
    (
        SELECT COUNT(*)
        FROM sales.sales
        WHERE sales.sales.coupon_id = temp_coupons.coupon_id 
    );

    DELETE FROM temp_coupons WHERE total_used > maximum_usage;
    
    RETURN QUERY
    SELECT temp_coupons.coupon_id FROM temp_coupons;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

