# finance.get_account_id_by_account_name function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_account_id_by_account_name(text)
RETURNS integer
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_account_id_by_account_name
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_account_id_by_account_name(text)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN
		account_id
    FROM finance.accounts
    WHERE finance.accounts.account_name=$1
	AND NOT finance.accounts.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

