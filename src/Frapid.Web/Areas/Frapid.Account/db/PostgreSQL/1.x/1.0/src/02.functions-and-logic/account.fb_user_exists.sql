DROP FUNCTION IF EXISTS account.fb_user_exists(_user_id integer);

CREATE FUNCTION account.fb_user_exists(_user_id integer)
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.fb_access_tokens
        WHERE account.fb_access_tokens.user_id = _user_id
		AND NOT account.fb_access_tokens.deleted
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$$
LANGUAGE plpgsql;
