# finance.get_account_master_id_by_account_master_code function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_account_master_id_by_account_master_code(_account_master_code text)
RETURNS integer
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_account_master_id_by_account_master_code
* Arguments : _account_master_code text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_account_master_id_by_account_master_code(_account_master_code text)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN finance.account_masters.account_master_id
    FROM finance.account_masters
    WHERE finance.account_masters.account_master_code = $1
	AND NOT finance.account_masters.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

