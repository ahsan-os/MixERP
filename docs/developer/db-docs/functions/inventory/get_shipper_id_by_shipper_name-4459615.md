# inventory.get_shipper_id_by_shipper_name function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_shipper_id_by_shipper_name(_shipper_name character varying)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_shipper_id_by_shipper_name
* Arguments : _shipper_name character varying
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_shipper_id_by_shipper_name(_shipper_name character varying)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN inventory.shippers.shipper_id
    FROM inventory.shippers
    WHERE inventory.shippers.shipper_name = _shipper_name;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

