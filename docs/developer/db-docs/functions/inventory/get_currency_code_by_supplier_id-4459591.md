# inventory.get_currency_code_by_supplier_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_currency_code_by_supplier_id(_supplier_id integer)
RETURNS character varying
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_currency_code_by_supplier_id
* Arguments : _supplier_id integer
* Owner : frapid_db_user
* Result Type : character varying
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_currency_code_by_supplier_id(_supplier_id integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN inventory.suppliers.currency_code
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

