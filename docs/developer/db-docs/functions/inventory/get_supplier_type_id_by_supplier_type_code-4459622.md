# inventory.get_supplier_type_id_by_supplier_type_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_supplier_type_id_by_supplier_type_code(text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_supplier_type_id_by_supplier_type_code
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_supplier_type_id_by_supplier_type_code(text)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN supplier_type_id
    FROM inventory.supplier_types
    WHERE inventory.supplier_types.supplier_type_code=$1
	AND NOT inventory.supplier_types.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

