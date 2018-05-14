# config.custom_field_data_types table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | data_type | [ ] | character varying | 50 |  |
| 2 | underlying_type | [ ] | character varying | 500 |  |
| 3 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 3 | [audit_user_id](../account/users.md) | custom_field_data_types_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| custom_field_data_types_pkey | frapid_db_user | btree | data_type |  |



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
