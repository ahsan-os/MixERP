# account.get_name_by_user_id function:

```plpgsql
CREATE OR REPLACE FUNCTION account.get_name_by_user_id(_user_id integer)
RETURNS character varying
```
* Schema : [account](../../schemas/account.md)
* Function Name : get_name_by_user_id
* Arguments : _user_id integer
* Owner : frapid_db_user
* Result Type : character varying
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.get_name_by_user_id(_user_id integer)
 RETURNS character varying
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN
        account.users.name
    FROM account.users
    WHERE account.users.user_id = _user_id
	AND NOT account.users.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

