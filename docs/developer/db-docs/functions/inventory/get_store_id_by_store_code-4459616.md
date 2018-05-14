# inventory.get_store_id_by_store_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_store_id_by_store_code(_store_code text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_store_id_by_store_code
* Arguments : _store_code text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_store_id_by_store_code(_store_code text)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN
    (
        SELECT inventory.stores.store_id
        FROM inventory.stores
        WHERE inventory.stores.store_code=_store_code 
        AND NOT inventory.stores.deleted
    );
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

