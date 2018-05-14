# hrm.resignations table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | resignation_id | [ ] | integer | 0 |  |
| 2 | entered_by | [ ] | integer | 0 |  |
| 3 | notice_date | [ ] | date | 0 |  |
| 4 | desired_resign_date | [ ] | date | 0 |  |
| 5 | employee_id | [ ] | integer | 0 |  |
| 6 | forward_to | [x] | integer | 0 |  |
| 7 | reason | [ ] | character varying | 128 |  |
| 8 | details | [x] | text | 0 |  |
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
| 2 | [entered_by](../account/users.md) | resignations_entered_by_fkey | account.users.user_id |
| 5 | [employee_id](../hrm/employees.md) | resignations_employee_id_fkey | hrm.employees.employee_id |
| 6 | [forward_to](../hrm/employees.md) | resignations_forward_to_fkey | hrm.employees.employee_id |
| 9 | [verification_status_id](../core/verification_statuses.md) | resignations_verification_status_id_fkey | core.verification_statuses.verification_status_id |
| 10 | [verified_by_user_id](../account/users.md) | resignations_verified_by_user_id_fkey | account.users.user_id |
| 13 | [audit_user_id](../account/users.md) | resignations_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| resignations_pkey | frapid_db_user | btree | resignation_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | resignation_id | nextval('hrm.resignations_resignation_id_seq'::regclass) |
| 14 | audit_ts | now() |
| 15 | deleted | false |


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
