# website.email_subscriptions table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | email_subscription_id | [ ] | uuid | 0 |  |
| 2 | first_name | [x] | character varying | 100 |  |
| 3 | last_name | [x] | character varying | 100 |  |
| 4 | email | [ ] | character varying | 100 |  |
| 5 | browser | [x] | text | 0 |  |
| 6 | ip_address | [x] | character varying | 50 |  |
| 7 | confirmed | [x] | boolean | 0 |  |
| 8 | confirmed_on | [x] | timestamp with time zone | 0 |  |
| 9 | unsubscribed | [x] | boolean | 0 |  |
| 10 | subscribed_on | [x] | timestamp with time zone | 0 |  |
| 11 | unsubscribed_on | [x] | timestamp with time zone | 0 |  |
| 12 | audit_user_id | [x] | integer | 0 |  |
| 13 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 14 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 12 | [audit_user_id](../account/users.md) | email_subscriptions_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| email_subscriptions_pkey | frapid_db_user | btree | email_subscription_id |  |
| email_subscriptions_email_key | frapid_db_user | btree | email |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | email_subscription_id | gen_random_uuid() |
| 7 | confirmed | false |
| 9 | unsubscribed | false |
| 10 | subscribed_on | now() |
| 13 | audit_ts | now() |
| 14 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |
| website.email_subscription_confirmation_trigger | [website.email_subscription_confirmation_trigger](../../functions/website/email_subscription_confirmation_trigger-4455603.md) | UPDATE | BEFORE |  | 0 | ROW |  |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
