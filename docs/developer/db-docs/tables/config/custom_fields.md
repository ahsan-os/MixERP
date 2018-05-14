# config.custom_fields table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | custom_field_id | [ ] | integer | 0 |  |
| 2 | custom_field_setup_id | [ ] | integer | 0 |  |
| 3 | resource_id | [ ] | character varying | 500 |  |
| 4 | value | [x] | text | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [custom_field_setup_id](../config/custom_field_setup.md) | custom_fields_custom_field_setup_id_fkey | config.custom_field_setup.custom_field_setup_id |
| 5 | [audit_user_id](../account/users.md) | custom_fields_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| custom_fields_pkey | frapid_db_user | btree | custom_field_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | custom_field_id | nextval('config.custom_fields_custom_field_id_seq'::regclass) |
| 6 | audit_ts | now() |
| 7 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
