# audit.disable_update_transaction_log function:

```plpgsql
CREATE OR REPLACE FUNCTION audit.disable_update_transaction_log()
RETURNS trigger
```
* Schema : [audit](../../schemas/audit.md)
* Function Name : disable_update_transaction_log
* Arguments : 
* Owner : frapid_db_user
* Result Type : trigger
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION audit.disable_update_transaction_log()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
BEGIN
    RAISE EXCEPTION 'Cannot modify or delete the transaction log.';
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

