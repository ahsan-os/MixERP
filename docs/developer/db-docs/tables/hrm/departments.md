# hrm.departments table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | department_id | [ ] | integer | 0 |  |
| 1 | department_id | [ ] | integer | 0 |  |
| 2 | department_name | [ ] | character varying | 500 |  |
| 2 | department_code | [ ] | character varying | 12 |  |
| 3 | description | [x] | text | 0 |  |
| 3 | department_name | [ ] | character varying | 50 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [audit_user_id](../account/users.md) | departments_audit_user_id_fkey | account.users.user_id |
| 4 | [audit_user_id](../account/users.md) | departments_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| departments_pkey | frapid_db_user | btree | department_id |  |
| departments_department_code_uix | frapid_db_user | btree | upper(department_code::text) |  |
| departments_department_name_uix | frapid_db_user | btree | upper(department_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | department_id | nextval('hrm.departments_department_id_seq'::regclass) |
| 1 | department_id | nextval('helpdesk.departments_department_id_seq'::regclass) |
| 5 | audit_ts | now() |
| 5 | audit_ts | now() |
| 6 | deleted | false |
| 6 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
