# finance.has_child_accounts function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.has_child_accounts(bigint)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : has_child_accounts
* Arguments : bigint
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.has_child_accounts(bigint)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS(SELECT 0 FROM finance.accounts WHERE parent_account_id=$1 LIMIT 1) THEN
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

