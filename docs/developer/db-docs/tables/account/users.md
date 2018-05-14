# account.users table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | user_id | [ ] | integer | 0 |  |
| 2 | email | [ ] | character varying | 100 |  |
| 3 | password | [x] | text | 0 |  |
| 4 | office_id | [ ] | integer | 0 |  |
| 5 | role_id | [ ] | integer | 0 |  |
| 6 | name | [x] | character varying | 100 |  |
| 7 | phone | [x] | character varying | 100 |  |
| 8 | status | [x] | boolean | 0 |  |
| 9 | created_on | [ ] | timestamp with time zone | 0 |  |
| 10 | last_seen_on | [x] | timestamp with time zone | 0 |  |
| 11 | last_ip | [x] | text | 0 |  |
| 12 | last_browser | [x] | text | 0 |  |
| 13 | audit_user_id | [x] | integer | 0 |  |
| 14 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 15 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [office_id](../core/offices.md) | users_office_id_fkey | core.offices.office_id |
| 5 | [role_id](../account/roles.md) | users_role_id_fkey | account.roles.role_id |
| 13 | [audit_user_id](../account/users.md) | users_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| users_pkey | frapid_db_user | btree | user_id |  |
| users_email_uix | frapid_db_user | btree | lower(email::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | user_id | nextval('account.users_user_id_seq'::regclass) |
| 8 | status | true |
| 9 | created_on | now() |
| 14 | audit_ts | now() |
| 15 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
