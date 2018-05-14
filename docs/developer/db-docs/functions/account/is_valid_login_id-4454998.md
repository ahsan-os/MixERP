# account.is_valid_login_id function:

```plpgsql
CREATE OR REPLACE FUNCTION account.is_valid_login_id(bigint)
RETURNS boolean
```
* Schema : [account](../../schemas/account.md)
* Function Name : is_valid_login_id
* Arguments : bigint
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.is_valid_login_id(bigint)
 RETURNS boolean
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    IF EXISTS(SELECT 1 FROM account.logins WHERE login_id=$1) THEN
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

