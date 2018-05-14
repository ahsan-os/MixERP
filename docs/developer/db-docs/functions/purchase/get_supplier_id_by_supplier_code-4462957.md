# purchase.get_supplier_id_by_supplier_code function:

```plpgsql
CREATE OR REPLACE FUNCTION purchase.get_supplier_id_by_supplier_code(text)
RETURNS bigint
```
* Schema : [purchase](../../schemas/purchase.md)
* Function Name : get_supplier_id_by_supplier_code
* Arguments : text
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION purchase.get_supplier_id_by_supplier_code(text)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN
    (
        SELECT
            supplier_id
        FROM
            inventory.suppliers
        WHERE 
            inventory.suppliers.supplier_code=$1
	AND NOT
	    inventory.suppliers.deleted
    );
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

