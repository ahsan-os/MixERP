DROP FUNCTION IF EXISTS website.add_email_subscription
(
    _email                                  text
);


CREATE FUNCTION website.add_email_subscription
(
    _email                                  text
)
RETURNS boolean
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT * 
		FROM website.email_subscriptions
        WHERE website.email_subscriptions.email = _email
		AND NOT website.email_subscriptions.deleted
    ) THEN
        INSERT INTO website.email_subscriptions(email)
        SELECT _email;
        
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;