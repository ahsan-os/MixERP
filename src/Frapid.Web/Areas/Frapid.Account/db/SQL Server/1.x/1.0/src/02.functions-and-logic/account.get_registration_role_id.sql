IF OBJECT_ID('account.get_registration_role_id') IS NOT NULL
DROP FUNCTION account.get_registration_role_id;

GO

CREATE FUNCTION account.get_registration_role_id(@email national character varying(500))
RETURNS integer
AS
BEGIN
    DECLARE @is_admin               bit = 0;
    DECLARE @role_id                integer;

    IF EXISTS
    (
        SELECT * FROM account.installed_domains
        WHERE admin_email = @email
		AND account.installed_domains.deleted = 0
    )
    BEGIN
        SET @is_admin = 1;
    END;
   
    IF(@is_admin = 1)
    BEGIN
        SELECT
        TOP 1
            @role_id = role_id            
        FROM account.roles
        WHERE is_administrator = 1
		AND account.roles.deleted = 0;
    END
    ELSE
    BEGIN
        SELECT 
            @role_id = registration_role_id
        FROM account.configuration_profiles
        WHERE is_active = 1
		AND account.configuration_profiles.deleted = 0;
    END;

    RETURN @role_id;
END;

GO