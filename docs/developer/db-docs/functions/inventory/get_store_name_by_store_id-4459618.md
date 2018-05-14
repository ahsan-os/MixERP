# inventory.get_store_name_by_store_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_store_name_by_store_id(integer)
RETURNS text
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_store_name_by_store_id
* Arguments : integer
* Owner : frapid_db_user
* Result Type : text
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_store_name_by_store_id(integer)
 RETURNS text
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN store_name
    FROM inventory.stores
    WHERE inventory.stores.store_id = $1
	AND NOT inventory.stores.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

