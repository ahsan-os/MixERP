# finance.frequencies table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | frequency_id | [ ] | integer | 0 |  |
| 1 | frequency_id | [ ] | integer | 0 |  |
| 2 | frequency_code | [ ] | character varying | 12 |  |
| 2 | frequency_code | [ ] | character varying | 12 |  |
| 3 | frequency_name | [ ] | character varying | 50 |  |
| 3 | frequency_name | [ ] | character varying | 50 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [audit_user_id](../account/users.md) | frequencies_audit_user_id_fkey | account.users.user_id |
| 4 | [audit_user_id](../account/users.md) | frequencies_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| frequencies_pkey | frapid_db_user | btree | frequency_id |  |
| frequencies_frequency_code_uix | frapid_db_user | btree | upper(frequency_code::text) |  |
| frequencies_frequency_name_uix | frapid_db_user | btree | upper(frequency_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | frequency_id | nextval('finance.frequencies_frequency_id_seq'::regclass) |
| 2 | audit_ts | now() |
| 3 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
