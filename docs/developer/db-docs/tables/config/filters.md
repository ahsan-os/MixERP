# config.filters table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | filter_id | [ ] | bigint | 0 |  |
| 2 | object_name | [ ] | text | 0 |  |
| 3 | filter_name | [ ] | text | 0 |  |
| 4 | is_default | [ ] | boolean | 0 |  |
| 5 | is_default_admin | [ ] | boolean | 0 |  |
| 6 | filter_statement | [ ] | character varying | 12 |  |
| 7 | column_name | [ ] | text | 0 |  |
| 8 | data_type | [ ] | text | 0 |  |
| 9 | filter_condition | [ ] | integer | 0 |  |
| 10 | filter_value | [x] | text | 0 |  |
| 11 | filter_and_value | [x] | text | 0 |  |
| 12 | audit_user_id | [x] | integer | 0 |  |
| 13 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 14 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 12 | [audit_user_id](../account/users.md) | filters_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| filters_pkey | frapid_db_user | btree | filter_id |  |
| filters_object_name_inx | frapid_db_user | btree | object_name |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | filter_id | nextval('config.filters_filter_id_seq'::regclass) |
| 4 | is_default | false |
| 5 | is_default_admin | false |
| 6 | filter_statement | 'WHERE'::character varying |
| 8 | data_type | ''::text |
| 13 | audit_ts | now() |
| 14 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
