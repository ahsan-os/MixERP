# inventory.get_checkout_id_by_transaction_master_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_checkout_id_by_transaction_master_id(_checkout_id bigint)
RETURNS bigint
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_checkout_id_by_transaction_master_id
* Arguments : _checkout_id bigint
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_checkout_id_by_transaction_master_id(_checkout_id bigint)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
BEGIN
        RETURN
        (
            SELECT inventory.checkouts.checkout_id
            FROM inventory.checkouts
            WHERE inventory.checkouts.transaction_master_id=$1
        );
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

