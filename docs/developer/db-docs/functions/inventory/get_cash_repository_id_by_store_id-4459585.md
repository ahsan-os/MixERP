# inventory.get_cash_repository_id_by_store_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_cash_repository_id_by_store_id(_store_id integer)
RETURNS bigint
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_cash_repository_id_by_store_id
* Arguments : _store_id integer
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_cash_repository_id_by_store_id(_store_id integer)
 RETURNS bigint
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN inventory.stores.default_cash_repository_id
    FROM inventory.stores
    WHERE inventory.stores.store_id=_store_id
    AND NOT inventory.stores.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

