# hrm.employee_qualifications table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | employee_qualification_id | [ ] | bigint | 0 |  |
| 2 | employee_id | [ ] | integer | 0 |  |
| 3 | education_level_id | [ ] | integer | 0 |  |
| 4 | institution | [ ] | character varying | 128 |  |
| 5 | majors | [ ] | character varying | 128 |  |
| 6 | total_years | [x] | integer | 0 |  |
| 7 | score | [x] | numeric | 1966086 |  |
| 8 | started_on | [x] | date | 0 |  |
| 9 | completed_on | [x] | date | 0 |  |
| 10 | details | [x] | text | 0 |  |
| 11 | audit_user_id | [x] | integer | 0 |  |
| 12 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 13 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [employee_id](../hrm/employees.md) | employee_qualifications_employee_id_fkey | hrm.employees.employee_id |
| 3 | [education_level_id](../hrm/education_levels.md) | employee_qualifications_education_level_id_fkey | hrm.education_levels.education_level_id |
| 11 | [audit_user_id](../account/users.md) | employee_qualifications_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| employee_qualifications_pkey | frapid_db_user | btree | employee_qualification_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | employee_qualification_id | nextval('hrm.employee_qualifications_employee_qualification_id_seq'::regclass) |
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
