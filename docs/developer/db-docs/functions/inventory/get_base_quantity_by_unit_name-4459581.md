# inventory.get_base_quantity_by_unit_name function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_base_quantity_by_unit_name(text, numeric)
RETURNS numeric
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_base_quantity_by_unit_name
* Arguments : text, numeric
* Owner : frapid_db_user
* Result Type : numeric
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_base_quantity_by_unit_name(text, numeric)
 RETURNS numeric
 LANGUAGE plpgsql
 STABLE
AS $function$
	DECLARE _unit_id integer;
	DECLARE _root_unit_id integer;
	DECLARE _factor decimal(30, 6);
BEGIN
    _unit_id := inventory.get_unit_id_by_unit_name($1);
    _root_unit_id = inventory.get_root_unit_id(_unit_id);
    _factor = inventory.convert_unit(_unit_id, _root_unit_id);

    RETURN _factor * $2;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

