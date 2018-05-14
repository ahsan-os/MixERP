# inventory.is_periodic_inventory function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.is_periodic_inventory(_office_id integer)
RETURNS boolean
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : is_periodic_inventory
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.is_periodic_inventory(_office_id integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS(SELECT * FROM inventory.inventory_setup WHERE inventory_system = 'Periodic' AND office_id = _office_id) THEN
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

