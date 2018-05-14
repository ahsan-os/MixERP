# account.get_user_id_by_email function:

```plpgsql
CREATE OR REPLACE FUNCTION account.get_user_id_by_email(_email character varying)
RETURNS integer
```
* Schema : [account](../../schemas/account.md)
* Function Name : get_user_id_by_email
* Arguments : _email character varying
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.get_user_id_by_email(_email character varying)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN user_id
    FROM account.users
    WHERE LOWER(account.users.email) = LOWER(_email)
	AND NOT account.users.deleted;	
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

