# inventory.is_valid_unit_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.is_valid_unit_id(_unit_id integer, _item_id integer)
RETURNS boolean
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : is_valid_unit_id
* Arguments : _unit_id integer, _item_id integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.is_valid_unit_id(_unit_id integer, _item_id integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT 1
        FROM inventory.items
        WHERE inventory.items.item_id = $2
        AND inventory.get_root_unit_id($1) = inventory.get_root_unit_id(unit_id)
		AND NOT inventory.items.deleted
    ) THEN
        RETURN true;
    END IF;

    RETURN false;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

