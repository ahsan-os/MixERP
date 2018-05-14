# hrm.attendance_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | attendance_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.attendance_view
 AS
 SELECT attendances.attendance_id,
    attendances.office_id,
    ((offices.office_code::text || ' ('::text) || offices.office_name::text) || ')'::text AS office,
    attendances.employee_id,
    ((employees.employee_code::text || ' ('::text) || employees.employee_name::text) || ')'::text AS employee,
    employees.photo,
    attendances.attendance_date,
    attendances.was_present,
    attendances.check_in_time,
    attendances.check_out_time,
    attendances.overtime_hours,
    attendances.was_absent,
    attendances.reason_for_absenteeism
   FROM hrm.attendances
     JOIN core.offices ON offices.office_id = attendances.office_id
     JOIN hrm.employees ON employees.employee_id = attendances.employee_id AND NOT attendances.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

