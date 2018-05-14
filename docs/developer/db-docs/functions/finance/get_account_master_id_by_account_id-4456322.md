# finance.get_account_master_id_by_account_id function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_account_master_id_by_account_id(bigint)
RETURNS integer
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_account_master_id_by_account_id
* Arguments : bigint
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_account_master_id_by_account_id(bigint)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN finance.accounts.account_master_id
    FROM finance.accounts
    WHERE finance.accounts.account_id= $1
	AND NOT finance.accounts.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

