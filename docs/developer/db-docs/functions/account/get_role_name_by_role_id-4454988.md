# account.get_role_name_by_role_id function:

```plpgsql
CREATE OR REPLACE FUNCTION account.get_role_name_by_role_id(_role_id integer)
RETURNS character varying
```
* Schema : [account](../../schemas/account.md)
* Function Name : get_role_name_by_role_id
* Arguments : _role_id integer
* Owner : frapid_db_user
* Result Type : character varying
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.get_role_name_by_role_id(_role_id integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN
    (
        SELECT account.roles.role_name
        FROM account.roles
        WHERE account.roles.role_id = _role_id
    );
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

