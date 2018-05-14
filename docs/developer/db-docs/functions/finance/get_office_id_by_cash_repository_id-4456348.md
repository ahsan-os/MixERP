# finance.get_office_id_by_cash_repository_id function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_office_id_by_cash_repository_id(integer)
RETURNS integer
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_office_id_by_cash_repository_id
* Arguments : integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_office_id_by_cash_repository_id(integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
        RETURN office_id
        FROM finance.cash_repositories
        WHERE finance.cash_repositories.cash_repository_id=$1
		AND NOT finance.cash_repositories.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

