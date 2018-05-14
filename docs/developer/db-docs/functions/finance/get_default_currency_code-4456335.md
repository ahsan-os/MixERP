# finance.get_default_currency_code function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_default_currency_code(cash_repository_id integer)
RETURNS character varying
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_default_currency_code
* Arguments : cash_repository_id integer
* Owner : frapid_db_user
* Result Type : character varying
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_default_currency_code(cash_repository_id integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN
    (
        SELECT core.offices.currency_code 
        FROM finance.cash_repositories
        INNER JOIN core.offices
        ON core.offices.office_id = finance.cash_repositories.office_id
        WHERE finance.cash_repositories.cash_repository_id=$1
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

