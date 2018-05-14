# inventory.get_item_code_by_item_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_item_code_by_item_id(item_id_ integer)
RETURNS character varying
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_item_code_by_item_id
* Arguments : item_id_ integer
* Owner : frapid_db_user
* Result Type : character varying
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_item_code_by_item_id(item_id_ integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN item_code
    FROM inventory.items
    WHERE inventory.items.item_id = item_id_
    AND NOT inventory.items.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

