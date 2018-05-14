DROP FUNCTION IF EXISTS account.has_active_reset_request(_email text);

CREATE FUNCTION account.has_active_reset_request(_email text)
RETURNS boolean
AS
$$
    DECLARE _expires_on                     TIMESTAMP WITH TIME ZONE = NOW() + INTERVAL '24 Hours';
BEGIN
    IF EXISTS
    (
        SELECT * FROM account.reset_requests
        WHERE LOWER(email) = LOWER(_email)
        AND expires_on <= _expires_on
		AND NOT account.reset_requests.deleted
    ) THEN        
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;