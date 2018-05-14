IF OBJECT_ID('account.can_confirm_registration') IS NOT NULL
DROP FUNCTION account.can_confirm_registration;

GO

CREATE FUNCTION account.can_confirm_registration(@token uniqueidentifier)
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.registrations
        WHERE registration_id = @token
        AND confirmed = 0
		AND account.registrations.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;

    RETURN 0;
END;


GO
