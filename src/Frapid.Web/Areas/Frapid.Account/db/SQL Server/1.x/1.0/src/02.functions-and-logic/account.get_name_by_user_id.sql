IF OBJECT_ID('account.get_name_by_user_id') IS NOT NULL
DROP FUNCTION account.get_name_by_user_id;

GO

CREATE FUNCTION account.get_name_by_user_id(@user_id integer)
RETURNS national character varying(500)
AS
BEGIN
    RETURN
    (
		SELECT
			account.users.name
		FROM account.users
		WHERE account.users.user_id = @user_id
		AND account.users.deleted = 0
	);
END;


GO