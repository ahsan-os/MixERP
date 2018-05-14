DROP FUNCTION IF EXISTS account.can_register_with_google();

CREATE FUNCTION account.can_register_with_google()
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 1 
		FROM account.configuration_profiles
        WHERE account.configuration_profiles.is_active
        AND account.configuration_profiles.allow_registration
        AND account.configuration_profiles.allow_google_registration
		AND NOT account.configuration_profiles.deleted
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$$
LANGUAGE plpgsql;
