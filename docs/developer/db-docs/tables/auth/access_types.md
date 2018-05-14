# auth.access_types table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | access_type_id | [ ] | integer | 0 |  |
| 2 | access_type_name | [ ] | character varying | 48 |  |
| 3 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 3 | [audit_user_id](../account/users.md) | access_types_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| access_types_pkey | frapid_db_user | btree | access_type_id |  |
| access_types_uix | frapid_db_user | btree | upper(access_type_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 4 | audit_ts | now() |
| 5 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
