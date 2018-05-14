# inventory.get_cost_of_good_method function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_cost_of_good_method(_office_id integer)
RETURNS text
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_cost_of_good_method
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : text
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_cost_of_good_method(_office_id integer)
 RETURNS text
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN inventory.inventory_setup.cogs_calculation_method
    FROM inventory.inventory_setup
    WHERE inventory.inventory_setup.office_id=$1;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

