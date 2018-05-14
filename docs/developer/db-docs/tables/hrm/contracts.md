# hrm.contracts table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | contract_id | [ ] | bigint | 0 |  |
| 2 | employee_id | [ ] | integer | 0 |  |
| 3 | office_id | [ ] | integer | 0 |  |
| 4 | department_id | [ ] | integer | 0 |  |
| 5 | role_id | [x] | integer | 0 |  |
| 6 | leave_benefit_id | [x] | integer | 0 |  |
| 7 | began_on | [x] | date | 0 |  |
| 8 | ended_on | [x] | date | 0 |  |
| 9 | employment_status_code_id | [ ] | integer | 0 |  |
| 10 | verification_status_id | [ ] | smallint | 0 |  |
| 11 | verified_by_user_id | [x] | integer | 0 |  |
| 12 | verified_on | [x] | date | 0 |  |
| 13 | verification_reason | [x] | character varying | 128 |  |
| 14 | audit_user_id | [x] | integer | 0 |  |
| 15 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 16 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [employee_id](../hrm/employees.md) | contracts_employee_id_fkey | hrm.employees.employee_id |
| 3 | [office_id](../core/offices.md) | contracts_office_id_fkey | core.offices.office_id |
| 4 | [department_id](../hrm/departments.md) | contracts_department_id_fkey | hrm.departments.department_id |
| 5 | [role_id](../hrm/roles.md) | contracts_role_id_fkey | hrm.roles.role_id |
| 6 | [leave_benefit_id](../hrm/leave_benefits.md) | contracts_leave_benefit_id_fkey | hrm.leave_benefits.leave_benefit_id |
| 9 | [employment_status_code_id](../hrm/employment_status_codes.md) | contracts_employment_status_code_id_fkey | hrm.employment_status_codes.employment_status_code_id |
| 10 | [verification_status_id](../core/verification_statuses.md) | contracts_verification_status_id_fkey | core.verification_statuses.verification_status_id |
| 11 | [verified_by_user_id](../account/users.md) | contracts_verified_by_user_id_fkey | account.users.user_id |
| 14 | [audit_user_id](../account/users.md) | contracts_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| contracts_pkey | frapid_db_user | btree | contract_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | contract_id | nextval('hrm.contracts_contract_id_seq'::regclass) |
| 15 | audit_ts | now() |
| 16 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
