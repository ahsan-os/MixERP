# finance.is_periodic_inventory function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.is_periodic_inventory(_office_id integer)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : is_periodic_inventory
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.is_periodic_inventory(_office_id integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN inventory.is_periodic_inventory(_office_id);
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

