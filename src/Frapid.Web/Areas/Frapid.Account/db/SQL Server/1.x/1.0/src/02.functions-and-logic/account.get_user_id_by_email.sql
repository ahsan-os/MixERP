IF OBJECT_ID('account.get_user_id_by_email') IS NOT NULL
DROP FUNCTION account.get_user_id_by_email;

GO

CREATE FUNCTION account.get_user_id_by_email(@email national character varying(100))
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT
		user_id
		FROM account.users
		WHERE account.users.email = @email
		AND account.users.deleted = 0 
	);
END;


GO