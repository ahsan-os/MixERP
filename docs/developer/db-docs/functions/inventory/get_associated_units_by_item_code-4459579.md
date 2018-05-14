# inventory.get_associated_units_by_item_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_associated_units_by_item_code(_item_code text)
RETURNS TABLE(unit_id integer, unit_code text, unit_name text)
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_associated_units_by_item_code
* Arguments : _item_code text
* Owner : frapid_db_user
* Result Type : TABLE(unit_id integer, unit_code text, unit_name text)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_associated_units_by_item_code(_item_code text)
 RETURNS TABLE(unit_id integer, unit_code text, unit_name text)
 LANGUAGE plpgsql
AS $function$
    DECLARE _unit_id integer;
BEGIN
    SELECT inventory.items.unit_id INTO _unit_id
    FROM inventory.items
    WHERE LOWER(item_code) = LOWER(_item_code)
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

