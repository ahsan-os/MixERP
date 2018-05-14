# hrm.identification_types table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | identification_type_id | [ ] | integer | 0 |  |
| 2 | identification_type_code | [ ] | character varying | 12 |  |
| 3 | identification_type_name | [ ] | character varying | 100 |  |
| 4 | can_expire | [ ] | boolean | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 5 | [audit_user_id](../account/users.md) | identification_types_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| identification_types_pkey | frapid_db_user | btree | identification_type_id |  |
| identification_types_identification_type_name_key | frapid_db_user | btree | identification_type_name |  |
| identification_types_identification_type_code_uix | frapid_db_user | btree | upper(identification_type_code::text) |  |
| identification_types_identification_type_name_uix | frapid_db_user | btree | upper(identification_type_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | identification_type_id | nextval('hrm.identification_types_identification_type_id_seq'::regclass) |
| 4 | can_expire | false |
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
