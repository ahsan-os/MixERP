# website.email_subscription_scrud_view view

| Schema | [website](../../schemas/website.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | email_subscription_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW website.email_subscription_scrud_view
 AS
 SELECT email_subscriptions.email_subscription_id,
    email_subscriptions.first_name,
    email_subscriptions.last_name,
    email_subscriptions.email,
    email_subscriptions.confirmed,
    email_subscriptions.confirmed_on,
    email_subscriptions.unsubscribed,
    email_subscriptions.unsubscribed_on
   FROM website.email_subscriptions
  WHERE NOT email_subscriptions.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

