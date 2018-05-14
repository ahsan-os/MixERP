DROP FUNCTION IF EXISTS account.is_valid_login_id(bigint);

CREATE FUNCTION account.is_valid_login_id(bigint)
RETURNS boolean
STABLE
AS
$$
BEGIN
    IF EXISTS(SELECT 1 FROM account.logins WHERE login_id=$1) THEN
            RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;

--SELECT account.is_valid_login_id(1);