# hrm.termination_verification_scrud_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | termination_verification_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.termination_verification_scrud_view
 AS
 SELECT terminations.termination_id,
    ((employees.employee_code::text || ' ('::text) || employees.employee_name::text) || ')'::text AS employee,
    employees.photo,
    terminations.notice_date,
    terminations.service_end_date,
    ((forwarded_to.employee_code::text || ' ('::text) || forwarded_to.employee_name::text) || ' )'::text AS forward_to,
    ((employment_statuses.employment_status_code::text || ' ('::text) || employment_statuses.employment_status_name::text) || ')'::text AS employment_status,
    terminations.reason,
    terminations.details
   FROM hrm.terminations
     JOIN hrm.employees ON employees.employee_id = terminations.employee_id
     JOIN hrm.employment_statuses ON employment_statuses.employment_status_id = terminations.change_status_to
     JOIN hrm.employees forwarded_to ON forwarded_to.employee_id = terminations.forward_to
  WHERE terminations.verification_status_id = 0 AND NOT terminations.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

