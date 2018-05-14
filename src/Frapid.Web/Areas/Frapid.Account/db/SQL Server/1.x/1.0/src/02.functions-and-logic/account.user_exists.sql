IF OBJECT_ID('account.user_exists') IS NOT NULL
DROP FUNCTION account.user_exists;

GO

CREATE FUNCTION account.user_exists(@email national character varying(100))
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.users
        WHERE account.users.email = @email
		AND account.users.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;
    
    RETURN 0;
END;

GO