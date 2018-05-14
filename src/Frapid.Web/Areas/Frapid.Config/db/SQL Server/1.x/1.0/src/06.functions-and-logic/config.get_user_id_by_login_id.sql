IF OBJECT_ID('config.get_user_id_by_login_id') IS NOT NULL
DROP FUNCTION config.get_user_id_by_login_id;

GO

CREATE FUNCTION config.get_user_id_by_login_id(@login_id bigint)
RETURNS integer
AS
BEGIN
    RETURN
    ( 
		SELECT
		user_id
		FROM account.logins
		WHERE login_id = @login_id
		AND account.logins.deleted = 0
	);
END;

GO
