# account.token_auto_expiry_trigger function:

```plpgsql
CREATE OR REPLACE FUNCTION account.token_auto_expiry_trigger()
RETURNS trigger
```
* Schema : [account](../../schemas/account.md)
* Function Name : token_auto_expiry_trigger
* Arguments : 
* Owner : frapid_db_user
* Result Type : trigger
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.token_auto_expiry_trigger()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
BEGIN
    UPDATE account.access_tokens
    SET 
        revoked = true,
        revoked_on = NOW()
    WHERE ip_address = NEW.ip_address
    AND user_agent = NEW.user_agent;

    RETURN NEW;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

