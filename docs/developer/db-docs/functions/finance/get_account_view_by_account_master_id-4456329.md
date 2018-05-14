# finance.get_account_view_by_account_master_id function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_account_view_by_account_master_id(_account_master_id integer, _row_number integer)
RETURNS TABLE(id bigint, account_id integer, account_name text)
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_account_view_by_account_master_id
* Arguments : _account_master_id integer, _row_number integer
* Owner : frapid_db_user
* Result Type : TABLE(id bigint, account_id integer, account_name text)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_account_view_by_account_master_id(_account_master_id integer, _row_number integer)
 RETURNS TABLE(id bigint, account_id integer, account_name text)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    SELECT ROW_NUMBER() OVER (ORDER BY accounts.account_id) +_row_number, * FROM 
    (
        SELECT finance.accounts.account_id, finance.get_account_name_by_account_id(finance.accounts.account_id)
        FROM finance.accounts
        WHERE finance.accounts.account_master_id = _account_master_id
    ) AS accounts;    
END;
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

