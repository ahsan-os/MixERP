# inventory.get_root_unit_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_root_unit_id(_any_unit_id integer)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_root_unit_id
* Arguments : _any_unit_id integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_root_unit_id(_any_unit_id integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
    DECLARE root_unit_id integer;
BEGIN
    SELECT base_unit_id INTO root_unit_id
    FROM inventory.compound_units
    WHERE inventory.compound_units.compare_unit_id=_any_unit_id
	AND NOT inventory.compound_units.deleted;

    IF(root_unit_id IS NULL) THEN
        RETURN _any_unit_id;
    ELSE
        RETURN inventory.get_root_unit_id(root_unit_id);
    END IF; 
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

