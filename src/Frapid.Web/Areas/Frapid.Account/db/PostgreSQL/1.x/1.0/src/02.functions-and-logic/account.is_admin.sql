DROP FUNCTION IF EXISTS account.is_admin(_user_id integer);

CREATE FUNCTION account.is_admin(_user_id integer)
RETURNS boolean
AS
$$
BEGIN
    RETURN
    (
        SELECT account.roles.is_administrator FROM account.users
        INNER JOIN account.roles
        ON account.users.role_id = account.roles.role_id
        WHERE account.users.user_id=$1
    );
END
$$
LANGUAGE PLPGSQL;

--SELECT * FROM account.is_admin(1);

