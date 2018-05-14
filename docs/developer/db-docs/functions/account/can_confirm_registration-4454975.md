# account.can_confirm_registration function:

```plpgsql
CREATE OR REPLACE FUNCTION account.can_confirm_registration(_token uuid)
RETURNS boolean
```
* Schema : [account](../../schemas/account.md)
* Function Name : can_confirm_registration
* Arguments : _token uuid
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.can_confirm_registration(_token uuid)
 RETURNS boolean
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.registrations
        WHERE account.registrations.registration_id = _token
        AND NOT account.registrations.confirmed
		AND NOT account.registrations.deleted
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

