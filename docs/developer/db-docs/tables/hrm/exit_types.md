# hrm.exit_types table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | exit_type_id | [ ] | integer | 0 |  |
| 2 | exit_type_code | [ ] | character varying | 12 |  |
| 3 | exit_type_name | [ ] | character varying | 128 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [audit_user_id](../account/users.md) | exit_types_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| exit_types_pkey | frapid_db_user | btree | exit_type_id |  |
| exit_types_exit_type_code_key | frapid_db_user | btree | exit_type_code |  |
| exit_types_exit_type_code_uix | frapid_db_user | btree | exit_type_code |  |
| exit_types_exit_type_name_uix | frapid_db_user | btree | exit_type_name |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | exit_type_id | nextval('hrm.exit_types_exit_type_id_seq'::regclass) |
| 5 | audit_ts | now() |
| 6 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
