# hrm.leave_application_verification_scrud_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | leave_application_verification_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.leave_application_verification_scrud_view
 AS
 SELECT leave_applications.leave_application_id,
    leave_applications.employee_id,
    ((employees.employee_code::text || ' ('::text) || employees.employee_name::text) || ')'::text AS employee,
    employees.photo,
    ((leave_types.leave_type_code::text || ' ('::text) || leave_types.leave_type_name::text) || ')'::text AS leave_type,
    users.name AS entered_by,
    leave_applications.applied_on,
    leave_applications.reason,
    leave_applications.start_date,
    leave_applications.end_date
   FROM hrm.leave_applications
     JOIN hrm.employees ON employees.employee_id = leave_applications.employee_id
     JOIN hrm.leave_types ON leave_types.leave_type_id = leave_applications.leave_type_id
     JOIN account.users ON users.user_id = leave_applications.entered_by
  WHERE leave_applications.verification_status_id = 0 AND NOT leave_applications.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

