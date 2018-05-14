# hrm.exits table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | exit_id | [ ] | bigint | 0 |  |
| 2 | employee_id | [ ] | integer | 0 |  |
| 3 | forward_to | [x] | integer | 0 |  |
| 4 | change_status_to | [ ] | integer | 0 |  |
| 5 | exit_type_id | [ ] | integer | 0 |  |
| 6 | exit_interview_details | [x] | text | 0 |  |
| 7 | reason | [ ] | character varying | 128 |  |
| 8 | details | [x] | text | 0 |  |
| 9 | verification_status_id | [ ] | smallint | 0 |  |
| 10 | verified_by_user_id | [x] | integer | 0 |  |
| 11 | verified_on | [x] | date | 0 |  |
| 12 | verification_reason | [x] | character varying | 128 |  |
| 13 | service_end_date | [ ] | date | 0 |  |
| 14 | audit_user_id | [x] | integer | 0 |  |
| 15 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 16 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [employee_id](../hrm/employees.md) | exits_employee_id_fkey | hrm.employees.employee_id |
| 3 | [forward_to](../hrm/employees.md) | exits_forward_to_fkey | hrm.employees.employee_id |
| 4 | [change_status_to](../hrm/employment_statuses.md) | exits_change_status_to_fkey | hrm.employment_statuses.employment_status_id |
| 5 | [exit_type_id](../hrm/exit_types.md) | exits_exit_type_id_fkey | hrm.exit_types.exit_type_id |
| 9 | [verification_status_id](../core/verification_statuses.md) | exits_verification_status_id_fkey | core.verification_statuses.verification_status_id |
| 10 | [verified_by_user_id](../account/users.md) | exits_verified_by_user_id_fkey | account.users.user_id |
| 14 | [audit_user_id](../account/users.md) | exits_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| exits_pkey | frapid_db_user | btree | exit_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | exit_id | nextval('hrm.exits_exit_id_seq'::regclass) |
| 15 | audit_ts | now() |
| 16 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |
| hrm.dismiss_employee_trigger | [hrm.dismiss_employee](../../functions/hrm/dismiss_employee-4462574.md) | UPDATE | BEFORE |  | 0 | ROW |  |
| hrm.dismiss_employee_trigger | [hrm.dismiss_employee](../../functions/hrm/dismiss_employee-4462574.md) | INSERT | BEFORE |  | 0 | ROW |  |
| hrm.undismiss_employee_trigger | [hrm.undismiss_employee](../../functions/hrm/undismiss_employee-4462575.md) | DELETE | BEFORE |  | 0 | ROW |  |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
