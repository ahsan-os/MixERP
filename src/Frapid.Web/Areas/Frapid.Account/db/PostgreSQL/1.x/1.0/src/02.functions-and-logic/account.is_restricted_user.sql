DROP FUNCTION IF EXISTS account.is_restricted_user(_email national character varying(100));

CREATE FUNCTION account.is_restricted_user(_email national character varying(100))
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.users
        WHERE LOWER(account.users.email) = LOWER(_email)
        AND NOT account.users.status
		AND NOT account.users.deleted
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$$
LANGUAGE plpgsql;
