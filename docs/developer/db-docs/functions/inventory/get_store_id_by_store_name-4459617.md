# inventory.get_store_id_by_store_name function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_store_id_by_store_name(_store_name text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_store_id_by_store_name
* Arguments : _store_name text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_store_id_by_store_name(_store_name text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN store_id
    FROM inventory.stores
    WHERE inventory.stores.store_name = _store_name
	AND NOT inventory.stores.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

