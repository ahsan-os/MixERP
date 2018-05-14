# hrm.employee_experiences table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | employee_experience_id | [ ] | bigint | 0 |  |
| 2 | employee_id | [ ] | integer | 0 |  |
| 3 | organization_name | [ ] | character varying | 128 |  |
| 4 | title | [ ] | character varying | 128 |  |
| 5 | started_on | [x] | date | 0 |  |
| 6 | ended_on | [x] | date | 0 |  |
| 7 | details | [x] | text | 0 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [employee_id](../hrm/employees.md) | employee_experiences_employee_id_fkey | hrm.employees.employee_id |
| 8 | [audit_user_id](../account/users.md) | employee_experiences_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| employee_experiences_pkey | frapid_db_user | btree | employee_experience_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | employee_experience_id | nextval('hrm.employee_experiences_employee_experience_id_seq'::regclass) |
| 9 | audit_ts | now() |
| 10 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
