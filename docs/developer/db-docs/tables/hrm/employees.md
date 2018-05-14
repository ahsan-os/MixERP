# hrm.employees table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | employee_id | [ ] | integer | 0 |  |
| 2 | employee_code | [ ] | character varying | 12 |  |
| 3 | first_name | [ ] | character varying | 50 |  |
| 4 | middle_name | [x] | character varying | 50 |  |
| 5 | last_name | [x] | character varying | 50 |  |
| 6 | employee_name | [ ] | character varying | 160 |  |
| 7 | gender_code | [ ] | character varying | 4 |  |
| 8 | marital_status_id | [ ] | integer | 0 |  |
| 9 | joined_on | [x] | date | 0 |  |
| 10 | office_id | [ ] | integer | 0 |  |
| 11 | user_id | [x] | integer | 0 |  |
| 12 | employee_type_id | [ ] | integer | 0 |  |
| 13 | current_department_id | [ ] | integer | 0 |  |
| 14 | current_role_id | [x] | integer | 0 |  |
| 15 | current_employment_status_id | [ ] | integer | 0 |  |
| 16 | current_job_title_id | [ ] | integer | 0 |  |
| 17 | current_pay_grade_id | [ ] | integer | 0 |  |
| 18 | current_shift_id | [ ] | integer | 0 |  |
| 19 | nationality_id | [x] | integer | 0 |  |
| 20 | date_of_birth | [x] | date | 0 |  |
| 21 | photo | [x] | photo | 0 |  |
| 22 | bank_account_number | [x] | character varying | 128 |  |
| 23 | bank_name | [x] | character varying | 128 |  |
| 24 | bank_branch_name | [x] | character varying | 128 |  |
| 25 | bank_reference_number | [x] | character varying | 128 |  |
| 26 | zip_code | [x] | character varying | 128 |  |
| 27 | address_line_1 | [x] | character varying | 128 |  |
| 28 | address_line_2 | [x] | character varying | 128 |  |
| 29 | street | [x] | character varying | 128 |  |
| 30 | city | [x] | character varying | 128 |  |
| 31 | state | [x] | character varying | 128 |  |
| 32 | country_code | [x] | character varying | 12 |  |
| 33 | phone_home | [x] | character varying | 128 |  |
| 34 | phone_cell | [x] | character varying | 128 |  |
| 35 | phone_office_extension | [x] | character varying | 128 |  |
| 36 | phone_emergency | [x] | character varying | 128 |  |
| 37 | phone_emergency_2 | [x] | character varying | 128 |  |
| 38 | email_address | [x] | character varying | 128 |  |
| 39 | website | [x] | character varying | 128 |  |
| 40 | blog | [x] | character varying | 128 |  |
| 41 | is_smoker | [x] | boolean | 0 |  |
| 42 | is_alcoholic | [x] | boolean | 0 |  |
| 43 | with_disabilities | [x] | boolean | 0 |  |
| 44 | low_vision | [x] | boolean | 0 |  |
| 45 | uses_wheelchair | [x] | boolean | 0 |  |
| 46 | hard_of_hearing | [x] | boolean | 0 |  |
| 47 | is_aphonic | [x] | boolean | 0 |  |
| 48 | is_cognitively_disabled | [x] | boolean | 0 |  |
| 49 | is_autistic | [x] | boolean | 0 |  |
| 50 | service_ended_on | [x] | date | 0 |  |
| 51 | audit_user_id | [x] | integer | 0 |  |
| 52 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 53 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 7 | [gender_code](../core/genders.md) | employees_gender_code_fkey | core.genders.gender_code |
| 8 | [marital_status_id](../core/marital_statuses.md) | employees_marital_status_id_fkey | core.marital_statuses.marital_status_id |
| 10 | [office_id](../core/offices.md) | employees_office_id_fkey | core.offices.office_id |
| 11 | [user_id](../account/users.md) | employees_user_id_fkey | account.users.user_id |
| 12 | [employee_type_id](../hrm/employee_types.md) | employees_employee_type_id_fkey | hrm.employee_types.employee_type_id |
| 13 | [current_department_id](../hrm/departments.md) | employees_current_department_id_fkey | hrm.departments.department_id |
| 14 | [current_role_id](../hrm/roles.md) | employees_current_role_id_fkey | hrm.roles.role_id |
| 15 | [current_employment_status_id](../hrm/employment_statuses.md) | employees_current_employment_status_id_fkey | hrm.employment_statuses.employment_status_id |
| 16 | [current_job_title_id](../hrm/job_titles.md) | employees_current_job_title_id_fkey | hrm.job_titles.job_title_id |
| 17 | [current_pay_grade_id](../hrm/pay_grades.md) | employees_current_pay_grade_id_fkey | hrm.pay_grades.pay_grade_id |
| 18 | [current_shift_id](../hrm/shifts.md) | employees_current_shift_id_fkey | hrm.shifts.shift_id |
| 19 | [nationality_id](../hrm/nationalities.md) | employees_nationality_id_fkey | hrm.nationalities.nationality_id |
| 32 | [country_code](../core/countries.md) | employees_country_code_fkey | core.countries.country_code |
| 51 | [audit_user_id](../account/users.md) | employees_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| employees_pkey | frapid_db_user | btree | employee_id |  |
| employees_employee_code_key | frapid_db_user | btree | employee_code |  |
| employees_employee_code_uix | frapid_db_user | btree | upper(employee_code::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | employee_id | nextval('hrm.employees_employee_id_seq'::regclass) |
| 4 | middle_name | ''::character varying |
| 5 | last_name | ''::character varying |
| 22 | bank_account_number | ''::character varying |
| 23 | bank_name | ''::character varying |
| 24 | bank_branch_name | ''::character varying |
| 25 | bank_reference_number | ''::character varying |
| 26 | zip_code | ''::character varying |
| 27 | address_line_1 | ''::character varying |
| 28 | address_line_2 | ''::character varying |
| 29 | street | ''::character varying |
| 30 | city | ''::character varying |
| 31 | state | ''::character varying |
| 33 | phone_home | ''::character varying |
| 34 | phone_cell | ''::character varying |
| 35 | phone_office_extension | ''::character varying |
| 36 | phone_emergency | ''::character varying |
| 37 | phone_emergency_2 | ''::character varying |
| 38 | email_address | ''::character varying |
| 39 | website | ''::character varying |
| 40 | blog | ''::character varying |
| 52 | audit_ts | now() |
| 53 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
