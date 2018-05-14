# inventory.get_account_id_by_supplier_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_account_id_by_supplier_id(_supplier_id integer)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_account_id_by_supplier_id
* Arguments : _supplier_id integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_account_id_by_supplier_id(_supplier_id integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN
        inventory.suppliers.account_id
    FROM inventory.suppliers
    WHERE inventory.suppliers.supplier_id = _supplier_id
    AND NOT inventory.suppliers.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

