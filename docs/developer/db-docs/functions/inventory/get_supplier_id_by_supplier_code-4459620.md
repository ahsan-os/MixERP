# inventory.get_supplier_id_by_supplier_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_supplier_id_by_supplier_code(text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_supplier_id_by_supplier_code
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_supplier_id_by_supplier_code(text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
        RETURN supplier_id
        FROM inventory.suppliers
        WHERE inventory.suppliers.supplier_code=$1
		AND NOT inventory.suppliers.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

