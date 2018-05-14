# config.email_queue table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | queue_id | [ ] | bigint | 0 |  |
| 2 | from_name | [ ] | character varying | 256 |  |
| 3 | from_email | [ ] | character varying | 256 |  |
| 4 | reply_to | [ ] | character varying | 256 |  |
| 5 | reply_to_name | [ ] | character varying | 256 |  |
| 6 | subject | [ ] | character varying | 256 |  |
| 7 | send_to | [ ] | character varying | 256 |  |
| 8 | attachments | [x] | text | 0 |  |
| 9 | message | [ ] | text | 0 |  |
| 10 | added_on | [ ] | timestamp with time zone | 0 |  |
| 11 | send_on | [ ] | timestamp with time zone | 0 |  |
| 12 | delivered | [ ] | boolean | 0 |  |
| 13 | delivered_on | [x] | timestamp with time zone | 0 |  |
| 14 | canceled | [ ] | boolean | 0 |  |
| 15 | canceled_on | [x] | timestamp with time zone | 0 |  |
| 16 | is_test | [ ] | boolean | 0 |  |
| 17 | audit_user_id | [x] | integer | 0 |  |
| 18 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 19 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 17 | [audit_user_id](../account/users.md) | email_queue_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| email_queue_pkey | frapid_db_user | btree | queue_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | queue_id | nextval('config.email_queue_queue_id_seq'::regclass) |
| 10 | added_on | now() |
| 11 | send_on | now() |
| 12 | delivered | false |
| 14 | canceled | false |
| 16 | is_test | false |
| 18 | audit_ts | now() |
| 19 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
