DROP VIEW IF EXISTS website.yesterdays_email_subscriptions;

CREATE VIEW website.yesterdays_email_subscriptions
AS
SELECT
    website.email_subscriptions.email,
    website.email_subscriptions.first_name,
    website.email_subscriptions.last_name,
    'subscribed' AS subscription_type
FROM website.email_subscriptions
WHERE website.email_subscriptions.subscribed_on::date = 'yesterday'::date
AND NOT website.email_subscriptions.confirmed_on::date = 'yesterday'::date
AND NOT website.email_subscriptions.deleted
UNION ALL
SELECT
    website.email_subscriptions.email,
    website.email_subscriptions.first_name,
    website.email_subscriptions.last_name,
    'unsubscribed'
FROM website.email_subscriptions
WHERE website.email_subscriptions.unsubscribed_on::date = 'yesterday'::date
AND NOT website.email_subscriptions.deleted
UNION ALL
SELECT
    website.email_subscriptions.email,
    website.email_subscriptions.first_name,
    website.email_subscriptions.last_name,
    'confirmed'
FROM website.email_subscriptions
WHERE website.email_subscriptions.confirmed_on::date = 'yesterday'::date
AND NOT website.email_subscriptions.deleted;