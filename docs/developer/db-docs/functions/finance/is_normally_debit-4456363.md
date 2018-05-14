# finance.is_normally_debit function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.is_normally_debit(_account_id integer)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : is_normally_debit
* Arguments : _account_id integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.is_normally_debit(_account_id integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN
        finance.account_masters.normally_debit
    FROM  finance.accounts
    INNER JOIN finance.account_masters
    ON finance.accounts.account_master_id = finance.account_masters.account_master_id
    WHERE finance.accounts.account_id = $1
	AND NOT finance.accounts.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

