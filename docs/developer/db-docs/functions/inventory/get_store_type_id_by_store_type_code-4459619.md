# inventory.get_store_type_id_by_store_type_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_store_type_id_by_store_type_code(text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_store_type_id_by_store_type_code
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_store_type_id_by_store_type_code(text)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN store_type_id
    FROM inventory.store_types
    WHERE inventory.store_types.store_type_code=$1
	AND NOT inventory.store_types.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

