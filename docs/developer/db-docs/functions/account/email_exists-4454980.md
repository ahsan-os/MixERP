# account.email_exists function:

```plpgsql
CREATE OR REPLACE FUNCTION account.email_exists(_email character varying)
RETURNS boolean
```
* Schema : [account](../../schemas/account.md)
* Function Name : email_exists
* Arguments : _email character varying
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.email_exists(_email character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    DECLARE _count                          integer;
BEGIN
    SELECT COUNT(*) INTO _count
	FROM account.users 
	WHERE LOWER(email) = LOWER(_email)
	AND NOT account.users.deleted;

    IF(COALESCE(_count, 0) =0) THEN
        SELECT COUNT(*) INTO _count 
		FROM account.registrations 
		WHERE LOWER(email) = LOWER(_email)
		AND NOT account.registrations.deleted;
    END IF;
    
    RETURN COALESCE(_count, 0) > 0;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

