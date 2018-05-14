# hrm.attendances table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | attendance_id | [ ] | bigint | 0 |  |
| 2 | office_id | [ ] | integer | 0 |  |
| 3 | employee_id | [ ] | integer | 0 |  |
| 4 | attendance_date | [ ] | date | 0 |  |
| 5 | was_present | [ ] | boolean | 0 |  |
| 6 | check_in_time | [x] | time without time zone | 0 |  |
| 7 | check_out_time | [x] | time without time zone | 0 |  |
| 8 | overtime_hours | [ ] | numeric | 1966086 |  |
| 9 | was_absent | [ ] | boolean | 0 |  |
| 10 | reason_for_absenteeism | [x] | text | 0 |  |
| 11 | audit_user_id | [x] | integer | 0 |  |
| 12 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 13 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [office_id](../core/offices.md) | attendances_office_id_fkey | core.offices.office_id |
| 3 | [employee_id](../hrm/employees.md) | attendances_employee_id_fkey | hrm.employees.employee_id |
| 11 | [audit_user_id](../account/users.md) | attendances_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| attendances_pkey | frapid_db_user | btree | attendance_id |  |
| attendance_date_employee_id_uix | frapid_db_user | btree | attendance_date, employee_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| attendances_check CHECK (was_absent <> was_present) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | attendance_id | nextval('hrm.attendances_attendance_id_seq'::regclass) |
| 12 | audit_ts | now() |
| 13 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
