IF OBJECT_ID('account.google_user_exists') IS NOT NULL
DROP FUNCTION account.google_user_exists;

GO

CREATE FUNCTION account.google_user_exists(@user_id integer)
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.google_access_tokens
        WHERE account.google_access_tokens.user_id = @user_id
		AND account.google_access_tokens.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;
    
    RETURN 0;
END;


GO
