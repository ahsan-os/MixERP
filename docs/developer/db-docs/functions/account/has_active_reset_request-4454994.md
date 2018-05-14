# account.has_active_reset_request function:

```plpgsql
CREATE OR REPLACE FUNCTION account.has_active_reset_request(_email text)
RETURNS boolean
```
* Schema : [account](../../schemas/account.md)
* Function Name : has_active_reset_request
* Arguments : _email text
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.has_active_reset_request(_email text)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    DECLARE _expires_on                     TIMESTAMP WITH TIME ZONE = NOW() + INTERVAL '24 Hours';
BEGIN
    IF EXISTS
    (
        SELECT * FROM account.reset_requests
        WHERE LOWER(email) = LOWER(_email)
        AND expires_on <= _expires_on
		AND NOT account.reset_requests.deleted
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

