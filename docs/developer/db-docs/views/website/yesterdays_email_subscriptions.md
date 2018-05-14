# website.yesterdays_email_subscriptions view

| Schema | [website](../../schemas/website.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | yesterdays_email_subscriptions |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW website.yesterdays_email_subscriptions
 AS
 SELECT email_subscriptions.email,
    email_subscriptions.first_name,
    email_subscriptions.last_name,
    'subscribed'::text AS subscription_type
   FROM website.email_subscriptions
  WHERE email_subscriptions.subscribed_on::date = '2017-01-27'::date AND NOT email_subscriptions.confirmed_on::date = '2017-01-27'::date AND NOT email_subscriptions.deleted
UNION ALL
 SELECT email_subscriptions.email,
    email_subscriptions.first_name,
    email_subscriptions.last_name,
    'unsubscribed'::text AS subscription_type
   FROM website.email_subscriptions
  WHERE email_subscriptions.unsubscribed_on::date = '2017-01-27'::date AND NOT email_subscriptions.deleted
UNION ALL
 SELECT email_subscriptions.email,
    email_subscriptions.first_name,
    email_subscriptions.last_name,
    'confirmed'::text AS subscription_type
   FROM website.email_subscriptions
  WHERE email_subscriptions.confirmed_on::date = '2017-01-27'::date AND NOT email_subscriptions.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

