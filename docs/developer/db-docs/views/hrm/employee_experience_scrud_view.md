# hrm.employee_experience_scrud_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | employee_experience_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.employee_experience_scrud_view
 AS
 SELECT employee_experiences.employee_experience_id,
    employee_experiences.employee_id,
    employees.employee_name,
    employee_experiences.organization_name,
    employee_experiences.title,
    employee_experiences.started_on,
    employee_experiences.ended_on
   FROM hrm.employee_experiences
     JOIN hrm.employees ON employee_experiences.employee_id = employees.employee_id
  WHERE NOT employee_experiences.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

