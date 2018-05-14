# hrm.employee_qualification_scrud_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | employee_qualification_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.employee_qualification_scrud_view
 AS
 SELECT employee_qualifications.employee_qualification_id,
    employee_qualifications.employee_id,
    employees.employee_name,
    education_levels.education_level_name,
    employee_qualifications.institution,
    employee_qualifications.majors,
    employee_qualifications.total_years,
    employee_qualifications.score,
    employee_qualifications.started_on,
    employee_qualifications.completed_on
   FROM hrm.employee_qualifications
     JOIN hrm.employees ON employee_qualifications.employee_id = employees.employee_id
     JOIN hrm.education_levels ON employee_qualifications.education_level_id = education_levels.education_level_id
  WHERE NOT employee_qualifications.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

