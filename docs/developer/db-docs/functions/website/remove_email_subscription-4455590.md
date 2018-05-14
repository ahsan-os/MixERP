# website.remove_email_subscription function:

```plpgsql
CREATE OR REPLACE FUNCTION website.remove_email_subscription(_email text)
RETURNS boolean
```
* Schema : [website](../../schemas/website.md)
* Function Name : remove_email_subscription
* Arguments : _email text
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION website.remove_email_subscription(_email text)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT * FROM website.email_subscriptions
        WHERE email = _email
        AND NOT unsubscribed
    ) THEN
        UPDATE website.email_subscriptions
        SET
            unsubscribed = true,
            unsubscribed_on = NOW()
        WHERE email = _email;

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

