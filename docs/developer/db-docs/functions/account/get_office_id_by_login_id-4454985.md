# account.get_office_id_by_login_id function:

```plpgsql
CREATE OR REPLACE FUNCTION account.get_office_id_by_login_id(_login_id bigint)
RETURNS integer
```
* Schema : [account](../../schemas/account.md)
* Function Name : get_office_id_by_login_id
* Arguments : _login_id bigint
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.get_office_id_by_login_id(_login_id bigint)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN
	(
		SELECT account.logins.office_id 
		FROM account.logins
		WHERE account.logins.login_id = _login_id
	);
END;
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

