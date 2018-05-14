DROP FUNCTION IF EXISTS account.get_registration_role_id(_email text);

CREATE FUNCTION account.get_registration_role_id(_email text)
RETURNS integer
STABLE
AS
$$
    DECLARE _is_admin               boolean = false;
    DECLARE _role_id                integer;
BEGIN
    IF EXISTS
    (
        SELECT * 
		FROM account.installed_domains
        WHERE account.installed_domains.admin_email = _email
		AND NOT account.installed_domains.deleted
    ) THEN
        _is_admin = true;
    END IF;
   
    IF(_is_admin) THEN
        SELECT
            account.roles.role_id
        INTO
            _role_id
        FROM account.roles
        WHERE account.roles.is_administrator
		AND NOT account.roles.deleted
        LIMIT 1;
    ELSE
        SELECT 
            account.configuration_profiles.registration_role_id
        INTO
            _role_id
        FROM account.configuration_profiles
        WHERE account.configuration_profiles.is_active
		AND NOT account.configuration_profiles.deleted;
    END IF;

    RETURN _role_id;
END
$$
LANGUAGE plpgsql;