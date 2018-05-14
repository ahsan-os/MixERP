# inventory.get_base_unit_id_by_unit_name function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_base_unit_id_by_unit_name(text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_base_unit_id_by_unit_name
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_base_unit_id_by_unit_name(text)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
DECLARE _unit_id integer;
BEGIN
    _unit_id := inventory.get_unit_id_by_unit_name($1);

    RETURN inventory.get_root_unit_id(_unit_id);
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

