IF OBJECT_ID('account.is_restricted_user') IS NOT NULL
DROP FUNCTION account.is_restricted_user;

GO

CREATE FUNCTION account.is_restricted_user(@email national character varying(100))
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.users
        WHERE account.users.email = @email
        AND account.users.status = 0
		AND account.users.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;
    
    RETURN 0;
END;

GO
