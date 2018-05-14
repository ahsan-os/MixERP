# account.complete_reset function:

```plpgsql
CREATE OR REPLACE FUNCTION account.complete_reset(_request_id uuid, _password text)
RETURNS void
```
* Schema : [account](../../schemas/account.md)
* Function Name : complete_reset
* Arguments : _request_id uuid, _password text
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.complete_reset(_request_id uuid, _password text)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
    DECLARE _user_id                integer;
    DECLARE _email                  text;
BEGIN
    SELECT
        account.users.user_id,
        account.users.email
    INTO
        _user_id,
        _email
    FROM account.reset_requests
    INNER JOIN account.users
    ON account.users.user_id = account.reset_requests.user_id
    WHERE account.reset_requests.request_id = _request_id
    AND expires_on >= NOW();
    
    UPDATE account.users
    SET
        password = _password
    WHERE user_id = _user_id;

    UPDATE account.reset_requests
    SET confirmed = true, confirmed_on = NOW();
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

