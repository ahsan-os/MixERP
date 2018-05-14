# inventory.get_office_id_by_store_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_office_id_by_store_id(integer)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_office_id_by_store_id
* Arguments : integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_office_id_by_store_id(integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN inventory.stores.office_id
    FROM inventory.stores
    WHERE inventory.stores.store_id=$1
	AND NOT inventory.stores.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

