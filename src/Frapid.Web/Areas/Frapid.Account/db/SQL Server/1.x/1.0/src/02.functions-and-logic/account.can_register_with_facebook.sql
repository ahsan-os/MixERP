IF OBJECT_ID('account.can_register_with_facebook') IS NOT NULL
DROP FUNCTION account.can_register_with_facebook;

GO


CREATE FUNCTION account.can_register_with_facebook()
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT 1 FROM account.configuration_profiles
        WHERE is_active = 1
        AND allow_registration = 1
        AND allow_facebook_registration = 1
		AND account.configuration_profiles.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;
    
    RETURN 0;
END;


GO