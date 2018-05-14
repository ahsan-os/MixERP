# purchase.get_price_type_id_by_price_type_name function:

```plpgsql
CREATE OR REPLACE FUNCTION purchase.get_price_type_id_by_price_type_name(_price_type_name character varying)
RETURNS integer
```
* Schema : [purchase](../../schemas/purchase.md)
* Function Name : get_price_type_id_by_price_type_name
* Arguments : _price_type_name character varying
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION purchase.get_price_type_id_by_price_type_name(_price_type_name character varying)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN purchase.price_types.price_type_id
    FROM purchase.price_types
    WHERE purchase.price_types.price_type_name = _price_type_name;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

