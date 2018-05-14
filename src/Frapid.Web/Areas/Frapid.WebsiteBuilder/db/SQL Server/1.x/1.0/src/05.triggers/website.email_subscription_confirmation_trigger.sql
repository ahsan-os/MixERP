IF OBJECT_ID('website.email_subscription_confirmation_trigger') IS NOT NULL
DROP TRIGGER website.email_subscription_confirmation_trigger;

GO

CREATE TRIGGER website.email_subscription_confirmation_trigger
ON website.email_subscriptions 
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
    SET XACT_ABORT ON;
	
	IF @@NESTLEVEL > 1
	BEGIN
		RETURN;
	END;
	
	DECLARE @inserted TABLE
	(
		email_subscription_id				uniqueidentifier,
		confirmed							bit
	);


	INSERT INTO @inserted
	SELECT email_subscription_id, confirmed
	FROM INSERTED
	WHERE confirmed = 1;
	
	UPDATE website.email_subscriptions
	SET confirmed_on = getutcdate()
	WHERE email_subscription_id IN
	(
		SELECT email_subscription_id
		FROM @inserted
	);    
END;


GO
