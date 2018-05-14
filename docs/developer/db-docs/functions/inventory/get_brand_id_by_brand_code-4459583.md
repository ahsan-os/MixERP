# inventory.get_brand_id_by_brand_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_brand_id_by_brand_code(text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_brand_id_by_brand_code
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_brand_id_by_brand_code(text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
        RETURN brand_id
        FROM inventory.brands
        WHERE inventory.brands.brand_code=$1
		AND NOT inventory.brands.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

