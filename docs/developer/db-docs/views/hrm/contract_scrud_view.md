# hrm.contract_scrud_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | contract_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.contract_scrud_view
 AS
 SELECT contracts.contract_id,
    employees.employee_id,
    ((employees.employee_code::text || ' ('::text) || employees.employee_name::text) || ')'::text AS employee,
    employees.photo,
    ((offices.office_code::text || ' ('::text) || offices.office_name::text) || ')'::text AS office,
    ((departments.department_code::text || ' ('::text) || departments.department_name::text) || ')'::text AS department,
    ((roles.role_code::text || ' ('::text) || roles.role_name::text) || ')'::text AS role,
    ((leave_benefits.leave_benefit_code::text || ' ('::text) || leave_benefits.leave_benefit_name::text) || ')'::text AS leave_benefit,
    ((employment_status_codes.status_code::text || ' ('::text) || employment_status_codes.status_code_name::text) || ')'::text AS employment_status_code,
    contracts.began_on,
    contracts.ended_on
   FROM hrm.contracts
     JOIN hrm.employees ON employees.employee_id = contracts.employee_id
     JOIN core.offices ON offices.office_id = contracts.office_id
     JOIN hrm.departments ON departments.department_id = contracts.department_id
     JOIN hrm.roles ON roles.role_id = contracts.role_id
     JOIN hrm.employment_status_codes ON employment_status_codes.employment_status_code_id = contracts.employment_status_code_id
     LEFT JOIN hrm.leave_benefits ON leave_benefits.leave_benefit_id = contracts.leave_benefit_id
  WHERE NOT contracts.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

