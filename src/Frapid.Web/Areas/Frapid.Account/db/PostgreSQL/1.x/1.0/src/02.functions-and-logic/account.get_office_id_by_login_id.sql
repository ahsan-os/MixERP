DROP FUNCTION IF EXISTS account.get_office_id_by_login_id(_login_id bigint);

CREATE FUNCTION account.get_office_id_by_login_id(_login_id bigint)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT account.logins.office_id 
		FROM account.logins
		WHERE account.logins.login_id = _login_id
	);
END;
$$
LANGUAGE plpgsql;

--SELECT account.get_office_id_by_login_id(1);
