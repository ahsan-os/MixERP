# finance.get_root_account_id function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_root_account_id(_account_id integer, _parent integer DEFAULT 0)
RETURNS integer
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_root_account_id
* Arguments : _account_id integer, _parent integer DEFAULT 0
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_root_account_id(_account_id integer, _parent integer DEFAULT 0)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
    DECLARE _parent_account_id integer;
BEGIN
    SELECT 
        parent_account_id
        INTO _parent_account_id
    FROM finance.accounts
    WHERE finance.accounts.account_id=$1
	AND NOT finance.accounts.deleted;

    

    IF(_parent_account_id IS NULL) THEN
        RETURN $1;
    ELSE
        RETURN finance.get_root_account_id(_parent_account_id, $1);
    END IF; 
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

