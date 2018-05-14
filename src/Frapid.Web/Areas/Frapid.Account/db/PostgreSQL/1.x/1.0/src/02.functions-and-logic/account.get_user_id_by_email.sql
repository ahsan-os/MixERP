DROP FUNCTION IF EXISTS account.get_user_id_by_email(_email national character varying(100));

CREATE FUNCTION account.get_user_id_by_email(_email national character varying(100))
RETURNS integer
AS
$$
BEGIN
    RETURN user_id
    FROM account.users
    WHERE LOWER(account.users.email) = LOWER(_email)
	AND NOT account.users.deleted;	
END
$$
LANGUAGE plpgsql;
