# inventory.get_item_id_by_item_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_item_id_by_item_code(_item_code text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_item_id_by_item_code
* Arguments : _item_code text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_item_id_by_item_code(_item_code text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN item_id
    FROM inventory.items
    WHERE inventory.items.item_code = _item_code
	AND NOT inventory.items.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

