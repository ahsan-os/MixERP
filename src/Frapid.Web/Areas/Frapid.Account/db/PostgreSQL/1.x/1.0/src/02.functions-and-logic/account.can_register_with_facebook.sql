DROP FUNCTION IF EXISTS account.can_register_with_facebook();

CREATE FUNCTION account.can_register_with_facebook()
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
        AND account.configuration_profiles.allow_facebook_registration
		AND NOT account.configuration_profiles.deleted
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$$
LANGUAGE plpgsql;
