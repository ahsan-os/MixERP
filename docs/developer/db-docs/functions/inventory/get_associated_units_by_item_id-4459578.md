# inventory.get_associated_units_by_item_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_associated_units_by_item_id(_item_id integer)
RETURNS TABLE(unit_id integer, unit_code text, unit_name text)
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_associated_units_by_item_id
* Arguments : _item_id integer
* Owner : frapid_db_user
* Result Type : TABLE(unit_id integer, unit_code text, unit_name text)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_associated_units_by_item_id(_item_id integer)
 RETURNS TABLE(unit_id integer, unit_code text, unit_name text)
 LANGUAGE plpgsql
AS $function$
    DECLARE _unit_id integer;
BEGIN
    SELECT inventory.items.unit_id INTO _unit_id
    FROM inventory.items
    WHERE inventory.items.item_id = _item_id
	AND NOT inventory.items.deleted;

    RETURN QUERY
    SELECT * FROM inventory.get_associated_units(_unit_id);
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

