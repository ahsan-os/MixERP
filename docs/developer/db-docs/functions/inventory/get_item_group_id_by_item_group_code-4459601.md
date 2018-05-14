# inventory.get_item_group_id_by_item_group_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_item_group_id_by_item_group_code(text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_item_group_id_by_item_group_code
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_item_group_id_by_item_group_code(text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
        RETURN item_group_id
        FROM inventory.item_groups
        WHERE inventory.item_groups.item_group_code=$1
		AND NOT inventory.item_groups.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

