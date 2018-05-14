# inventory.get_item_type_id_by_item_type_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_item_type_id_by_item_type_code(text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_item_type_id_by_item_type_code
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_item_type_id_by_item_type_code(text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
        RETURN item_type_id
        FROM inventory.item_types
        WHERE inventory.item_types.item_type_code=$1
		AND NOT inventory.item_types.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

