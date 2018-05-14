# account.is_restricted_user function:

```plpgsql
CREATE OR REPLACE FUNCTION account.is_restricted_user(_email character varying)
RETURNS boolean
```
* Schema : [account](../../schemas/account.md)
* Function Name : is_restricted_user
* Arguments : _email character varying
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.is_restricted_user(_email character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.users
        WHERE LOWER(account.users.email) = LOWER(_email)
        AND NOT account.users.status
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

