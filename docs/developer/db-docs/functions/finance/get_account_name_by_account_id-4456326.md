# finance.get_account_name_by_account_id function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_account_name_by_account_id(_account_id integer)
RETURNS text
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_account_name_by_account_id
* Arguments : _account_id integer
* Owner : frapid_db_user
* Result Type : text
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_account_name_by_account_id(_account_id integer)
 RETURNS text
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN account_name
    FROM finance.accounts
    WHERE finance.accounts.account_id=_account_id
	AND NOT finance.accounts.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

