# inventory.get_shipper_id_by_shipper_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_shipper_id_by_shipper_code(_shipper_code character varying)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_shipper_id_by_shipper_code
* Arguments : _shipper_code character varying
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_shipper_id_by_shipper_code(_shipper_code character varying)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN inventory.shippers.shipper_id
    FROM inventory.shippers
    WHERE inventory.shippers.shipper_code = _shipper_code;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

