# account.logins table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | login_id | [ ] | bigint | 0 |  |
| 2 | user_id | [x] | integer | 0 |  |
| 3 | office_id | [x] | integer | 0 |  |
| 4 | browser | [x] | text | 0 |  |
| 5 | ip_address | [x] | character varying | 50 |  |
| 6 | is_active | [ ] | boolean | 0 |  |
| 7 | login_timestamp | [ ] | timestamp with time zone | 0 |  |
| 8 | culture | [ ] | character varying | 12 |  |
| 9 | audit_user_id | [x] | integer | 0 |  |
| 10 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 11 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [user_id](../account/users.md) | logins_user_id_fkey | account.users.user_id |
| 3 | [office_id](../core/offices.md) | logins_office_id_fkey | core.offices.office_id |
| 9 | [audit_user_id](../account/users.md) | logins_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| logins_pkey | frapid_db_user | btree | login_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | login_id | nextval('account.logins_login_id_seq'::regclass) |
| 6 | is_active | true |
| 7 | login_timestamp | now() |
| 10 | audit_ts | now() |
| 11 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
