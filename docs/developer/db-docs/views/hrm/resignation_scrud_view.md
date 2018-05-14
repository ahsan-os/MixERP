# hrm.resignation_scrud_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | resignation_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.resignation_scrud_view
 AS
 SELECT resignations.resignation_id,
    users.name AS entered_by,
    resignations.notice_date,
    resignations.desired_resign_date,
    ((employees.employee_code::text || ' ('::text) || employees.employee_name::text) || ')'::text AS employee,
    employees.photo,
    ((forward_to.employee_code::text || ' ('::text) || forward_to.employee_name::text) || ')'::text AS forward_to,
    resignations.reason
   FROM hrm.resignations
     JOIN account.users ON users.user_id = resignations.entered_by
     JOIN hrm.employees ON employees.employee_id = resignations.employee_id
     JOIN hrm.employees forward_to ON forward_to.employee_id = resignations.forward_to
  WHERE NOT resignations.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

