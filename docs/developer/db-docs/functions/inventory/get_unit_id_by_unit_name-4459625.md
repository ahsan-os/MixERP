# inventory.get_unit_id_by_unit_name function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_unit_id_by_unit_name(_unit_name text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_unit_id_by_unit_name
* Arguments : _unit_name text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_unit_id_by_unit_name(_unit_name text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN unit_id
    FROM inventory.units
    WHERE inventory.units.unit_name = _unit_name
	AND NOT inventory.units.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

