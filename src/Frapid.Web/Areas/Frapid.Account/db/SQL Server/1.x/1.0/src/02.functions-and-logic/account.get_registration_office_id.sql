IF OBJECT_ID('account.get_registration_office_id') IS NOT NULL
DROP FUNCTION account.get_registration_office_id;

GO


CREATE FUNCTION account.get_registration_office_id()
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT
			registration_office_id
		FROM account.configuration_profiles
		WHERE is_active = 1
		AND account.configuration_profiles.deleted = 0 
	);
END;


GO