IF OBJECT_ID('website.remove_email_subscription') IS NOT NULL
DROP PROCEDURE website.remove_email_subscription;

GO


CREATE PROCEDURE website.remove_email_subscription
(
    @email                                  national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF EXISTS
    (
        SELECT * FROM website.email_subscriptions
        WHERE email = @email
        AND unsubscribed = 0
		AND website.email_subscriptions.deleted = 0
    ) 
    BEGIN
        UPDATE website.email_subscriptions
        SET
            unsubscribed = 1,
            unsubscribed_on = getutcdate()
        WHERE email = @email;

        RETURN 1;
    END;

    RETURN 0;
END;

GO
