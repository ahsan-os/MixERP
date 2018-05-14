# account.registrations table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | registration_id | [ ] | uuid | 0 |  |
| 2 | name | [x] | character varying | 100 |  |
| 3 | email | [ ] | character varying | 100 |  |
| 4 | phone | [x] | character varying | 100 |  |
| 5 | password | [x] | text | 0 |  |
| 6 | browser | [x] | text | 0 |  |
| 7 | ip_address | [x] | character varying | 50 |  |
| 8 | registered_on | [ ] | timestamp with time zone | 0 |  |
| 9 | confirmed | [x] | boolean | 0 |  |
| 10 | confirmed_on | [x] | timestamp with time zone | 0 |  |
| 11 | audit_user_id | [x] | integer | 0 |  |
| 12 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 13 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| registrations_pkey | frapid_db_user | btree | registration_id |  |
| registrations_email_uix | frapid_db_user | btree | lower(email::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | registration_id | gen_random_uuid() |
| 8 | registered_on | now() |
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
