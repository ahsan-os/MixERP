# account.can_register_with_facebook function:

```plpgsql
CREATE OR REPLACE FUNCTION account.can_register_with_facebook()
RETURNS boolean
```
* Schema : [account](../../schemas/account.md)
* Function Name : can_register_with_facebook
* Arguments : 
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.can_register_with_facebook()
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT 1 
		FROM account.configuration_profiles
        WHERE account.configuration_profiles.is_active
        AND account.configuration_profiles.allow_registration
        AND account.configuration_profiles.allow_facebook_registration
		AND NOT account.configuration_profiles.deleted
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

