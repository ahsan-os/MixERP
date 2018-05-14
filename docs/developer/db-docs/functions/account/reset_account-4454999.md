# account.reset_account function:

```plpgsql
CREATE OR REPLACE FUNCTION account.reset_account(_email text, _browser text, _ip_address text)
RETURNS SETOF account.reset_requests
```
* Schema : [account](../../schemas/account.md)
* Function Name : reset_account
* Arguments : _email text, _browser text, _ip_address text
* Owner : frapid_db_user
* Result Type : SETOF account.reset_requests
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.reset_account(_email text, _browser text, _ip_address text)
 RETURNS SETOF account.reset_requests
 LANGUAGE plpgsql
AS $function$
    DECLARE _request_id                     uuid;
    DECLARE _user_id                        integer;
    DECLARE _name                           text;
    DECLARE _expires_on                     TIMESTAMP WITH TIME ZONE = NOW() + INTERVAL '24 Hours';
BEGIN
    IF(NOT account.user_exists(_email) OR account.is_restricted_user(_email)) THEN
        RETURN;
    END IF;

    SELECT
        user_id,
        name
    INTO
        _user_id,
        _name
    FROM account.users
    WHERE LOWER(email) = LOWER(_email);

    IF account.has_active_reset_request(_email) THEN
        RETURN QUERY
        SELECT * FROM account.reset_requests
        WHERE LOWER(email) = LOWER(_email)
        AND expires_on <= _expires_on
        LIMIT 1;
        
        RETURN;
    END IF;

    INSERT INTO account.reset_requests(user_id, email, name, browser, ip_address, expires_on)
    SELECT _user_id, _email, _name, _browser, _ip_address, _expires_on
    RETURNING request_id INTO _request_id;

    RETURN QUERY
    SELECT *
    FROM account.reset_requests
    WHERE request_id = _request_id;

    RETURN;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

