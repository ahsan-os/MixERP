# inventory.get_unit_id_by_unit_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_unit_id_by_unit_code(text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_unit_id_by_unit_code
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_unit_id_by_unit_code(text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
        RETURN inventory.units.unit_id
        FROM inventory.units
        WHERE inventory.units.unit_code=$1
		AND NOT inventory.units.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

