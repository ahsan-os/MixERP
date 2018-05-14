# website.email_subscription_confirmation_trigger function:

```plpgsql
CREATE OR REPLACE FUNCTION website.email_subscription_confirmation_trigger()
RETURNS trigger
```
* Schema : [website](../../schemas/website.md)
* Function Name : email_subscription_confirmation_trigger
* Arguments : 
* Owner : frapid_db_user
* Result Type : trigger
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION website.email_subscription_confirmation_trigger()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF(NEW.confirmed) THEN
        NEW.confirmed_on = NOW();
    END IF;
    
    RETURN NEW;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

