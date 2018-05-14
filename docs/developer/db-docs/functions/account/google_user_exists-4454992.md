# account.google_user_exists function:

```plpgsql
CREATE OR REPLACE FUNCTION account.google_user_exists(_user_id integer)
RETURNS boolean
```
* Schema : [account](../../schemas/account.md)
* Function Name : google_user_exists
* Arguments : _user_id integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.google_user_exists(_user_id integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.google_access_tokens
        WHERE account.google_access_tokens.user_id = _user_id
		AND NOT account.google_access_tokens.deleted		
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

