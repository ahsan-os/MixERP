DROP FUNCTION IF EXISTS account.get_name_by_user_id(_user_id integer);

CREATE FUNCTION account.get_name_by_user_id(_user_id integer)
RETURNS national character varying(100)
STABLE
AS
$$
BEGIN
    RETURN
        account.users.name
    FROM account.users
    WHERE account.users.user_id = _user_id
	AND NOT account.users.deleted;
END
$$
LANGUAGE plpgsql;


