# hrm.employee_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | employee_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.employee_view
 AS
 SELECT employees.employee_id,
    employees.first_name,
    employees.middle_name,
    employees.last_name,
    employees.employee_code,
    employees.employee_name,
    employees.gender_code,
    genders.gender_name,
    ((marital_statuses.marital_status_code::text || ' ('::text) || marital_statuses.marital_status_name::text) || ')'::text AS marital_status,
    employees.joined_on,
    employees.office_id,
    ((offices.office_code::text || ' ('::text) || offices.office_name::text) || ')'::text AS office,
    employees.user_id,
    users.name,
    employees.employee_type_id,
    ((employee_types.employee_type_code::text || ' ('::text) || employee_types.employee_type_name::text) || ')'::text AS employee_type,
    employees.current_department_id,
    ((departments.department_code::text || ' ('::text) || departments.department_name::text) || ')'::text AS current_department,
    employees.current_role_id,
    ((roles.role_code::text || ' ('::text) || roles.role_name::text) || ')'::text AS role,
    employees.current_employment_status_id,
    ((employment_statuses.employment_status_code::text || ' ('::text) || employment_statuses.employment_status_name::text) || ')'::text AS employment_status,
    employees.current_job_title_id,
    ((job_titles.job_title_code::text || ' ('::text) || job_titles.job_title_name::text) || ')'::text AS job_title,
    employees.current_pay_grade_id,
    ((pay_grades.pay_grade_code::text || ' ('::text) || pay_grades.pay_grade_name::text) || ')'::text AS pay_grade,
    employees.current_shift_id,
    ((shifts.shift_code::text || ' ('::text) || shifts.shift_name::text) || ')'::text AS shift,
    employees.nationality_id,
    ((nationalities.nationality_code::text || ' ('::text) || nationalities.nationality_name::text) || ')'::text AS nationality,
    employees.date_of_birth,
    employees.photo,
    employees.zip_code,
    employees.address_line_1,
    employees.address_line_2,
    employees.street,
    employees.city,
    employees.state,
    employees.country_code,
    countries.country_name AS country,
    employees.phone_home,
    employees.phone_cell,
    employees.phone_office_extension,
    employees.phone_emergency,
    employees.phone_emergency_2,
    employees.email_address,
    employees.website,
    employees.blog,
    employees.is_smoker,
    employees.is_alcoholic,
    employees.with_disabilities,
    employees.low_vision,
    employees.uses_wheelchair,
    employees.hard_of_hearing,
    employees.is_aphonic,
    employees.is_cognitively_disabled,
    employees.is_autistic
   FROM hrm.employees
     JOIN core.genders ON employees.gender_code::text = genders.gender_code::text
     JOIN core.marital_statuses ON employees.marital_status_id = marital_statuses.marital_status_id
     JOIN core.offices ON employees.office_id = offices.office_id
     JOIN hrm.departments ON employees.current_department_id = departments.department_id
     JOIN hrm.employee_types ON employee_types.employee_type_id = employees.employee_type_id
     JOIN hrm.employment_statuses ON employees.current_employment_status_id = employment_statuses.employment_status_id
     JOIN hrm.job_titles ON employees.current_job_title_id = job_titles.job_title_id
     JOIN hrm.pay_grades ON employees.current_pay_grade_id = pay_grades.pay_grade_id
     JOIN hrm.shifts ON employees.current_shift_id = shifts.shift_id
     LEFT JOIN account.users ON employees.user_id = users.user_id
     LEFT JOIN hrm.roles ON employees.current_role_id = roles.role_id
     LEFT JOIN hrm.nationalities ON employees.nationality_id = nationalities.nationality_id
     LEFT JOIN core.countries ON employees.country_code::text = countries.country_code::text
  WHERE (employees.service_ended_on IS NULL OR COALESCE(employees.service_ended_on, 'infinity'::date) >= now()) AND NOT employees.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

