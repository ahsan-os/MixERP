# hrm.employee_identification_details table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | employee_identification_detail_id | [ ] | bigint | 0 |  |
| 2 | employee_id | [ ] | integer | 0 |  |
| 3 | identification_type_id | [ ] | integer | 0 |  |
| 4 | identification_number | [ ] | character varying | 128 |  |
| 5 | expires_on | [x] | date | 0 |  |
| 6 | audit_user_id | [x] | integer | 0 |  |
| 7 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 8 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [employee_id](../hrm/employees.md) | employee_identification_details_employee_id_fkey | hrm.employees.employee_id |
| 3 | [identification_type_id](../hrm/identification_types.md) | employee_identification_details_identification_type_id_fkey | hrm.identification_types.identification_type_id |
| 6 | [audit_user_id](../account/users.md) | employee_identification_details_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| employee_identification_details_pkey | frapid_db_user | btree | employee_identification_detail_id |  |
| employee_identification_details_employee_id_itc_uix | frapid_db_user | btree | employee_id, identification_type_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | employee_identification_detail_id | nextval('hrm.employee_identification_detai_employee_identification_detai_seq'::regclass) |
| 7 | audit_ts | now() |
| 8 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
