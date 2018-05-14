# account.is_valid_client_token function:

```plpgsql
CREATE OR REPLACE FUNCTION account.is_valid_client_token(_client_token text, _ip_address text, _user_agent text)
RETURNS boolean
```
* Schema : [account](../../schemas/account.md)
* Function Name : is_valid_client_token
* Arguments : _client_token text, _ip_address text, _user_agent text
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.is_valid_client_token(_client_token text, _ip_address text, _user_agent text)
 RETURNS boolean
 LANGUAGE plpgsql
 STABLE
AS $function$
    DECLARE _created_on TIMESTAMP WITH TIME ZONE;
    DECLARE _expires_on TIMESTAMP WITH TIME ZONE;
    DECLARE _revoked boolean;
BEGIN
    IF(COALESCE(_client_token, '') = '') THEN
        RETURN false;
    END IF;

    SELECT
        created_on,
        expires_on,
        revoked
    INTO
        _created_on,
        _expires_on,
        _revoked    
    FROM account.access_tokens
    WHERE client_token = _client_token
    AND ip_address = _ip_address
    AND user_agent = _user_agent;
    
    IF(COALESCE(_revoked, true)) THEN
        RETURN false;
    END IF;

    IF(_created_on > NOW()) THEN
        RETURN false;
    END IF;

    IF(COALESCE(_expires_on, NOW()) <= NOW()) THEN
        RETURN false;
    END IF;
    
    RETURN true;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

