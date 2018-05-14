IF OBJECT_ID('account.is_admin') IS NOT NULL
DROP FUNCTION account.is_admin;

GO

CREATE FUNCTION account.is_admin(@user_id integer)
RETURNS bit
AS
BEGIN
    RETURN
    (
        SELECT account.roles.is_administrator FROM account.users
        INNER JOIN account.roles
        ON account.users.role_id = account.roles.role_id
        WHERE account.users.user_id=@user_id
    );
END;

GO

--SELECT account.is_admin(1);

