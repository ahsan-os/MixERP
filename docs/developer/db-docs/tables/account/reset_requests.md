# account.reset_requests table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | request_id | [ ] | uuid | 0 |  |
| 2 | user_id | [ ] | integer | 0 |  |
| 3 | email | [x] | text | 0 |  |
| 4 | name | [x] | text | 0 |  |
| 5 | requested_on | [ ] | timestamp with time zone | 0 |  |
| 6 | expires_on | [ ] | timestamp with time zone | 0 |  |
| 7 | browser | [x] | text | 0 |  |
| 8 | ip_address | [x] | character varying | 50 |  |
| 9 | confirmed | [x] | boolean | 0 |  |
| 10 | confirmed_on | [x] | timestamp with time zone | 0 |  |
| 11 | audit_user_id | [x] | integer | 0 |  |
| 12 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 13 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [user_id](../account/users.md) | reset_requests_user_id_fkey | account.users.user_id |
| 11 | [audit_user_id](../account/users.md) | reset_requests_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| reset_requests_pkey | frapid_db_user | btree | request_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | request_id | gen_random_uuid() |
| 5 | requested_on | now() |
| 6 | expires_on | (now() + '24:00:00'::interval) |
| 9 | confirmed | false |
| 12 | audit_ts | now() |
| 13 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
