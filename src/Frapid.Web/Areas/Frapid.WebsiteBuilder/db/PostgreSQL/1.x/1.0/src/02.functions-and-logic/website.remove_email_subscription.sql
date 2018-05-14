DROP FUNCTION IF EXISTS website.remove_email_subscription
(
    _email                                  text
);

CREATE FUNCTION website.remove_email_subscription
(
    _email                                  text
)
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT * FROM website.email_subscriptions
        WHERE email = _email
        AND NOT unsubscribed
    ) THEN
        UPDATE website.email_subscriptions
        SET
            unsubscribed = true,
            unsubscribed_on = NOW()
        WHERE email = _email;

        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;