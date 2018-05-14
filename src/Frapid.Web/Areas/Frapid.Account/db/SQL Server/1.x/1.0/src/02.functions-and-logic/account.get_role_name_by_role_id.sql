IF OBJECT_ID('account.get_role_name_by_role_id') IS NOT NULL
DROP FUNCTION account.get_role_name_by_role_id;

GO

CREATE FUNCTION account.get_role_name_by_role_id(@role_id integer)
RETURNS national character varying(100)
AS
BEGIN
    RETURN
    (
        SELECT account.roles.role_name
        FROM account.roles
        WHERE account.roles.role_id = @role_id
    );
END

GO

--SELECT account.get_role_name_by_role_id(9999);