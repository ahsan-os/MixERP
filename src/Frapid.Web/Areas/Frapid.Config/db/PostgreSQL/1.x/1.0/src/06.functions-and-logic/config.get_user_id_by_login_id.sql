DROP FUNCTION IF EXISTS config.get_user_id_by_login_id(_login_id bigint);

CREATE FUNCTION config.get_user_id_by_login_id(_login_id bigint)
RETURNS integer
AS
$$
BEGIN
    RETURN 
    user_id
    FROM account.logins
    WHERE account.logins.login_id = _login_id
	AND NOT account.logins.deleted;
END
$$
LANGUAGE plpgsql;
