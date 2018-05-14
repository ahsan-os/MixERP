# inventory.is_purchase function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.is_purchase(_transaction_master_id bigint)
RETURNS boolean
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : is_purchase
* Arguments : _transaction_master_id bigint
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.is_purchase(_transaction_master_id bigint)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT * FROM finance.transaction_master
        WHERE finance.transaction_master.transaction_master_id = $1
        AND book IN ('Purchase')
    ) THEN
            RETURN true;
    END IF;

    RETURN false;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

