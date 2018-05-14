DROP FUNCTION IF EXISTS account.get_registration_office_id();

CREATE FUNCTION account.get_registration_office_id()
RETURNS integer
AS
$$
BEGIN
    RETURN account.configuration_profiles.registration_office_id
    FROM account.configuration_profiles
    WHERE account.configuration_profiles.is_active
	AND NOT account.configuration_profiles.deleted;
END
$$
LANGUAGE plpgsql;