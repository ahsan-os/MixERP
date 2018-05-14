# sales.get_active_coupon_id_by_coupon_code function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.get_active_coupon_id_by_coupon_code(_coupon_code character varying)
RETURNS integer
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : get_active_coupon_id_by_coupon_code
* Arguments : _coupon_code character varying
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.get_active_coupon_id_by_coupon_code(_coupon_code character varying)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN sales.coupons.coupon_id
    FROM sales.coupons
    WHERE sales.coupons.coupon_code = _coupon_code
    AND COALESCE(sales.coupons.begins_from, NOW()::date) >= NOW()::date
    AND COALESCE(sales.coupons.expires_on, NOW()::date) <= NOW()::date
    AND NOT sales.coupons.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

