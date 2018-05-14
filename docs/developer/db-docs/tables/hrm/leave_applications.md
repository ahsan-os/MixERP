# hrm.leave_applications table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | leave_application_id | [ ] | bigint | 0 |  |
| 2 | employee_id | [ ] | integer | 0 |  |
| 3 | leave_type_id | [ ] | integer | 0 |  |
| 4 | entered_by | [ ] | integer | 0 |  |
| 5 | applied_on | [x] | date | 0 |  |
| 6 | reason | [x] | text | 0 |  |
| 7 | start_date | [x] | date | 0 |  |
| 8 | end_date | [x] | date | 0 |  |
| 9 | verification_status_id | [ ] | smallint | 0 |  |
| 10 | verified_by_user_id | [x] | integer | 0 |  |
| 11 | verified_on | [x] | date | 0 |  |
| 12 | verification_reason | [x] | character varying | 128 |  |
| 13 | audit_user_id | [x] | integer | 0 |  |
| 14 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 15 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [employee_id](../hrm/employees.md) | leave_applications_employee_id_fkey | hrm.employees.employee_id |
| 3 | [leave_type_id](../hrm/leave_types.md) | leave_applications_leave_type_id_fkey | hrm.leave_types.leave_type_id |
| 4 | [entered_by](../account/users.md) | leave_applications_entered_by_fkey | account.users.user_id |
| 9 | [verification_status_id](../core/verification_statuses.md) | leave_applications_verification_status_id_fkey | core.verification_statuses.verification_status_id |
| 10 | [verified_by_user_id](../account/users.md) | leave_applications_verified_by_user_id_fkey | account.users.user_id |
| 13 | [audit_user_id](../account/users.md) | leave_applications_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| leave_applications_pkey | frapid_db_user | btree | leave_application_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | leave_application_id | nextval('hrm.leave_applications_leave_application_id_seq'::regclass) |
| 5 | applied_on | now() |
| 14 | audit_ts | now() |
| 15 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
