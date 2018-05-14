IF OBJECT_ID('website.email_subscription_insert_view') IS NOT NULL
DROP VIEW website.email_subscription_insert_view;
GO

CREATE VIEW website.email_subscription_insert_view
AS
SELECT * FROM website.email_subscriptions
WHERE 1 = 0;

GO

CREATE TRIGGER log_subscriptions 
ON website.email_subscription_insert_view
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON;
    SET XACT_ABORT ON;
	
	INSERT INTO website.email_subscriptions
	(
		email, 
		browser, 
		ip_address, 
		unsubscribed, 
		subscribed_on, 
		unsubscribed_on, 
		first_name, 
		last_name, 
		confirmed
	)
	SELECT
		email, 
		browser, 
		ip_address, 
		unsubscribed, 
		COALESCE(subscribed_on, getutcdate()), 
		unsubscribed_on, 
		first_name, 
		last_name, 
		confirmed
	FROM INSERTED
	WHERE NOT EXISTS
	(
		SELECT 1 
		FROM website.email_subscriptions
		WHERE email IN 
		(		
			SELECT email
			FROM INSERTED
		)
	);
END;


GO
