# inventory.items_unit_check_trigger function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.items_unit_check_trigger()
RETURNS trigger
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : items_unit_check_trigger
* Arguments : 
* Owner : frapid_db_user
* Result Type : trigger
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.items_unit_check_trigger()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$        
BEGIN
    IF(inventory.get_root_unit_id(NEW.unit_id) != inventory.get_root_unit_id(NEW.reorder_unit_id)) THEN
        RAISE EXCEPTION 'The reorder unit is incompatible with the base unit.'
        USING ERRCODE='P3054';
    END IF;
    RETURN NEW;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

