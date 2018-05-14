# hrm.employee_types table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | employee_type_id | [ ] | integer | 0 |  |
| 2 | employee_type_code | [ ] | character varying | 12 |  |
| 3 | employee_type_name | [ ] | character varying | 128 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [audit_user_id](../account/users.md) | employee_types_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| employee_types_pkey | frapid_db_user | btree | employee_type_id |  |
| employee_types_employee_type_code_key | frapid_db_user | btree | employee_type_code |  |
| employee_types_employee_type_code_uix | frapid_db_user | btree | upper(employee_type_code::text) |  |
| employee_types_employee_type_name_uix | frapid_db_user | btree | upper(employee_type_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | employee_type_id | nextval('hrm.employee_types_employee_type_id_seq'::regclass) |
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
