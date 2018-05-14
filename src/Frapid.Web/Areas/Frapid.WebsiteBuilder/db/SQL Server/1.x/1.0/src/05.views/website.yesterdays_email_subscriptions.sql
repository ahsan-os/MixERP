IF OBJECT_ID('website.yesterdays_email_subscriptions') IS NOT NULL
DROP VIEW website.yesterdays_email_subscriptions;

GO


CREATE VIEW website.yesterdays_email_subscriptions
AS
SELECT
    email,
    first_name,
    last_name,
    'subscribed' AS subscription_type
FROM website.email_subscriptions
WHERE CONVERT(date, subscribed_on) = CONVERT(date, DATEADD(d, -1, getutcdate()))
AND NOT CONVERT(date, confirmed_on) = CONVERT(date, DATEADD(d, -1, getutcdate()))
AND website.email_subscriptions.deleted = 0
UNION ALL
SELECT
    email,
    first_name,
    last_name,
    'unsubscribed'
FROM website.email_subscriptions
WHERE CONVERT(date, unsubscribed_on) = CONVERT(date, DATEADD(d, -1, getutcdate()))
AND website.email_subscriptions.deleted = 0
UNION ALL
SELECT
    email,
    first_name,
    last_name,
    'confirmed'
FROM website.email_subscriptions
WHERE CONVERT(date, confirmed_on) = CONVERT(date, DATEADD(d, -1, getutcdate()))
AND website.email_subscriptions.deleted = 0;


GO
