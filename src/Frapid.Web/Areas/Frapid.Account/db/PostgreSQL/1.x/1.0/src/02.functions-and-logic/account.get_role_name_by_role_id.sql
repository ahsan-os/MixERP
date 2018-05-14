DROP FUNCTION IF EXISTS account.get_role_name_by_role_id(_role_id integer);

CREATE FUNCTION account.get_role_name_by_role_id(_role_id integer)
RETURNS national character varying(100)
AS
$$
BEGIN
    RETURN
    (
        SELECT account.roles.role_name
        FROM account.roles
        WHERE account.roles.role_id = _role_id
    );
END
$$
LANGUAGE plpgsql;
