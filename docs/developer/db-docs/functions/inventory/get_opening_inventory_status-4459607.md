# inventory.get_opening_inventory_status function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_opening_inventory_status(_office_id integer)
RETURNS TABLE(office_id integer, multiple_inventory_allowed boolean, has_opening_inventory boolean)
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_opening_inventory_status
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : TABLE(office_id integer, multiple_inventory_allowed boolean, has_opening_inventory boolean)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_opening_inventory_status(_office_id integer)
 RETURNS TABLE(office_id integer, multiple_inventory_allowed boolean, has_opening_inventory boolean)
 LANGUAGE plpgsql
 STABLE
AS $function$
    DECLARE _multiple_inventory_allowed             boolean;
    DECLARE _has_opening_inventory                  boolean = false;

BEGIN
    SELECT inventory.inventory_setup.allow_multiple_opening_inventory 
    INTO _multiple_inventory_allowed    
    FROM inventory.inventory_setup
    WHERE inventory.inventory_setup.office_id = _office_id;

    IF EXISTS
    (
        SELECT 1
        FROM finance.transaction_master
        WHERE finance.transaction_master.book = 'Opening Inventory'
        AND finance.transaction_master.office_id = _office_id
    ) THEN
        _has_opening_inventory                      := true;
    END IF;
    
    RETURN QUERY
    SELECT _office_id, _multiple_inventory_allowed, _has_opening_inventory;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

