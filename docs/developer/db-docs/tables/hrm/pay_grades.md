# hrm.pay_grades table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | pay_grade_id | [ ] | integer | 0 |  |
| 2 | pay_grade_code | [ ] | character varying | 12 |  |
| 3 | pay_grade_name | [ ] | character varying | 100 |  |
| 4 | minimum_salary | [ ] | numeric | 1966086 |  |
| 5 | maximum_salary | [ ] | numeric | 1966086 |  |
| 6 | description | [x] | text | 0 |  |
| 7 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 7 | [audit_user_id](../account/users.md) | pay_grades_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| pay_grades_pkey | frapid_db_user | btree | pay_grade_id |  |
| pay_grades_pay_grade_code_key | frapid_db_user | btree | pay_grade_code |  |
| pay_grades_pay_grade_code_uix | frapid_db_user | btree | upper(pay_grade_code::text) |  |
| pay_grades_pay_grade_name_uix | frapid_db_user | btree | upper(pay_grade_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| pay_grades_check CHECK (maximum_salary >= minimum_salary) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | pay_grade_id | nextval('hrm.pay_grades_pay_grade_id_seq'::regclass) |
| 6 | description | ''::text |
| 8 | audit_ts | now() |
| 9 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
