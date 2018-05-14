DROP FUNCTION IF EXISTS account.get_email_by_user_id(_user_id integer);

CREATE FUNCTION account.get_email_by_user_id(_user_id integer)
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN
        account.users.email
    FROM account.users
    WHERE account.users.user_id = _user_id
	AND NOT account.users.deleted;
END
$$
LANGUAGE plpgsql;
