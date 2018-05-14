# account.fb_sign_in function:

```plpgsql
CREATE OR REPLACE FUNCTION account.fb_sign_in(_fb_user_id text, _email text, _office_id integer, _name text, _token text, _browser text, _ip_address text, _culture text)
RETURNS TABLE(login_id bigint, status boolean, message text)
```
* Schema : [account](../../schemas/account.md)
* Function Name : fb_sign_in
* Arguments : _fb_user_id text, _email text, _office_id integer, _name text, _token text, _browser text, _ip_address text, _culture text
* Owner : frapid_db_user
* Result Type : TABLE(login_id bigint, status boolean, message text)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.fb_sign_in(_fb_user_id text, _email text, _office_id integer, _name text, _token text, _browser text, _ip_address text, _culture text)
 RETURNS TABLE(login_id bigint, status boolean, message text)
 LANGUAGE plpgsql
AS $function$
    DECLARE _user_id                        integer;
    DECLARE _login_id                       bigint;
    DECLARE _auto_register                  boolean = false;
BEGIN
    IF(COALESCE(_office_id, 0) = 0) THEN
        IF(SELECT COUNT(*) = 1 FROM core.offices) THEN
            SELECT office_id INTO _office_id
            FROM core.offices;
        END IF;
    END IF;

    IF account.is_restricted_user(_email) THEN
        --LOGIN IS RESTRICTED TO THIS USER
        RETURN QUERY
        SELECT NULL::bigint, false, 'Access is denied'::text;

        RETURN;
    END IF;

    SELECT user_id INTO _user_id
    FROM account.users
    WHERE LOWER(account.users.email) = LOWER(_email);

    IF NOT account.user_exists(_email) AND account.can_register_with_facebook() THEN
        INSERT INTO account.users(role_id, office_id, email, name)
        SELECT account.get_registration_role_id(_email), account.get_registration_office_id(), _email, _name
        RETURNING user_id INTO _user_id;
    END IF;

    IF NOT account.fb_user_exists(_user_id) THEN
        INSERT INTO account.fb_access_tokens(user_id, fb_user_id, token)
        SELECT COALESCE(_user_id, account.get_user_id_by_email(_email)), _fb_user_id, _token;
    ELSE
        UPDATE account.fb_access_tokens
        SET token = _token
        WHERE user_id = _user_id;    
    END IF;

    IF(_user_id IS NULL) THEN
        SELECT user_id INTO _user_id
        FROM account.users
        WHERE LOWER(account.users.email) = LOWER(_email);
    END IF;
    
	UPDATE account.logins 
	SET is_active = false 
	WHERE user_id=_user_id 
	AND office_id = _office_id 
	AND browser = _browser
	AND ip_address = _ip_address;

    INSERT INTO account.logins(user_id, office_id, browser, ip_address, login_timestamp, culture)
    SELECT _user_id, _office_id, _browser, _ip_address, NOW(), COALESCE(_culture, '')
    RETURNING account.logins.login_id INTO _login_id;
	
    RETURN QUERY
    SELECT _login_id, true, 'Welcome'::text;
    RETURN;    
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

