DROP VIEW IF EXISTS website.email_subscription_scrud_view;

CREATE VIEW website.email_subscription_scrud_view
AS
SELECT
    website.email_subscriptions.email_subscription_id,
    website.email_subscriptions.first_name,
    website.email_subscriptions.last_name,
    website.email_subscriptions.email,
    website.email_subscriptions.confirmed,
    website.email_subscriptions.confirmed_on,
    website.email_subscriptions.unsubscribed,
    website.email_subscriptions.unsubscribed_on
FROM website.email_subscriptions
WHERE NOT website.email_subscriptions.deleted;