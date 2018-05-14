# account.get_registration_office_id function:

```plpgsql
CREATE OR REPLACE FUNCTION account.get_registration_office_id()
RETURNS integer
```
* Schema : [account](../../schemas/account.md)
* Function Name : get_registration_office_id
* Arguments : 
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.get_registration_office_id()
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN account.configuration_profiles.registration_office_id
    FROM account.configuration_profiles
    WHERE account.configuration_profiles.is_active
	AND NOT account.configuration_profiles.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

