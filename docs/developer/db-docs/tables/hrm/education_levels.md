# hrm.education_levels table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | education_level_id | [ ] | integer | 0 |  |
| 2 | education_level_name | [ ] | character varying | 50 |  |
| 3 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 3 | [audit_user_id](../account/users.md) | education_levels_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| education_levels_pkey | frapid_db_user | btree | education_level_id |  |
| education_levels_education_level_name_key | frapid_db_user | btree | education_level_name |  |
| education_levels_education_level_name | frapid_db_user | btree | upper(education_level_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | education_level_id | nextval('hrm.education_levels_education_level_id_seq'::regclass) |
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
