# finance.get_cash_repository_id_by_cash_repository_name function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_cash_repository_id_by_cash_repository_name(text)
RETURNS integer
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_cash_repository_id_by_cash_repository_name
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_cash_repository_id_by_cash_repository_name(text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN
    (
        SELECT cash_repository_id
        FROM finance.cash_repositories
        WHERE finance.cash_repositories.cash_repository_name=$1
		AND NOT finance.cash_repositories.deleted
    );
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

