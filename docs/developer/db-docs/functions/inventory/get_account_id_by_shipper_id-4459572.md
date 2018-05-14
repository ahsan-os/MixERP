# inventory.get_account_id_by_shipper_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_account_id_by_shipper_id(_shipper_id integer)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_account_id_by_shipper_id
* Arguments : _shipper_id integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_account_id_by_shipper_id(_shipper_id integer)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN inventory.shippers.account_id
    FROM inventory.shippers
    WHERE inventory.shippers.shipper_id=_shipper_id
    AND NOT inventory.shippers.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

