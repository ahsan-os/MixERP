# sales.get_receivable_account_for_check_receipts function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.get_receivable_account_for_check_receipts(_store_id integer)
RETURNS integer
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : get_receivable_account_for_check_receipts
* Arguments : _store_id integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.get_receivable_account_for_check_receipts(_store_id integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN inventory.stores.default_account_id_for_checks
    FROM inventory.stores
    WHERE inventory.stores.store_id = _store_id
    AND NOT inventory.stores.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

