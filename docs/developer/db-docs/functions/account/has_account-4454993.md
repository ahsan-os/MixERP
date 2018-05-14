# account.has_account function:

```plpgsql
CREATE OR REPLACE FUNCTION account.has_account(_email character varying)
RETURNS boolean
```
* Schema : [account](../../schemas/account.md)
* Function Name : has_account
* Arguments : _email character varying
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.has_account(_email character varying)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    DECLARE _count                          integer;
BEGIN
    SELECT COUNT(*) INTO _count 
	FROM account.users 
	WHERE lower(email) = LOWER(_email)
	AND NOT account.users.deleted;
	
    RETURN COALESCE(_count, 0) = 1;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

