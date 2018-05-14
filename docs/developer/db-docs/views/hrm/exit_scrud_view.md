# hrm.exit_scrud_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | exit_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.exit_scrud_view
 AS
 SELECT exits.exit_id,
    exits.employee_id,
    ((employees.employee_code::text || ' ('::text) || employees.employee_name::text) || ')'::text AS employee,
    employees.photo,
    exits.reason,
    ((forwarded_to.employee_code::text || ' ('::text) || forwarded_to.employee_name::text) || ' )'::text AS forward_to,
    ((employment_statuses.employment_status_code::text || ' ('::text) || employment_statuses.employment_status_name::text) || ')'::text AS employment_status,
    ((exit_types.exit_type_code::text || ' ('::text) || exit_types.exit_type_name::text) || ')'::text AS exit_type,
    exits.details,
    exits.exit_interview_details
   FROM hrm.exits
     JOIN hrm.employees ON employees.employee_id = exits.employee_id
     JOIN hrm.employment_statuses ON employment_statuses.employment_status_id = exits.change_status_to
     JOIN hrm.exit_types ON exit_types.exit_type_id = exits.exit_type_id
     JOIN hrm.employees forwarded_to ON forwarded_to.employee_id = exits.forward_to AND NOT exits.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

