# hrm.roles table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | role_id | [ ] | integer | 0 |  |
| 1 | role_id | [ ] | integer | 0 |  |
| 2 | role_name | [ ] | character varying | 100 |  |
| 2 | role_code | [ ] | character varying | 12 |  |
| 3 | is_administrator | [ ] | boolean | 0 |  |
| 3 | role_name | [ ] | character varying | 50 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [audit_user_id](../account/users.md) | roles_audit_user_id_fkey | account.users.user_id |
| 4 | [audit_user_id](../account/users.md) | roles_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| roles_pkey | frapid_db_user | btree | role_id |  |
| roles_role_code_uix | frapid_db_user | btree | upper(role_code::text) |  |
| roles_role_name_uix | frapid_db_user | btree | upper(role_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | role_id | nextval('hrm.roles_role_id_seq'::regclass) |
| 3 | is_administrator | false |
| 5 | audit_ts | now() |
| 5 | audit_ts | now() |
| 6 | deleted | false |
| 6 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
