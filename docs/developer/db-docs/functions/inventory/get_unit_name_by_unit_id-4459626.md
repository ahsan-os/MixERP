# inventory.get_unit_name_by_unit_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_unit_name_by_unit_id(_unit_id integer)
RETURNS character varying
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_unit_name_by_unit_id
* Arguments : _unit_id integer
* Owner : frapid_db_user
* Result Type : character varying
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_unit_name_by_unit_id(_unit_id integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN unit_name
    FROM inventory.units
    WHERE inventory.units.unit_id = _unit_id
	AND NOT inventory.units.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

