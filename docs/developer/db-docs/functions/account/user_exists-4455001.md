# account.user_exists function:

```plpgsql
CREATE OR REPLACE FUNCTION account.user_exists(_email character varying)
RETURNS boolean
```
* Schema : [account](../../schemas/account.md)
* Function Name : user_exists
* Arguments : _email character varying
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.user_exists(_email character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.users
        WHERE LOWER(account.users.email) = LOWER(_email)
		AND NOT account.users.deleted
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

