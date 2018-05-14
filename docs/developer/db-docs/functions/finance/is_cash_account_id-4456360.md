# finance.is_cash_account_id function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.is_cash_account_id(_account_id integer)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : is_cash_account_id
* Arguments : _account_id integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.is_cash_account_id(_account_id integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT 1 FROM finance.accounts 
        WHERE account_master_id IN(10101)
        AND account_id=_account_id
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

