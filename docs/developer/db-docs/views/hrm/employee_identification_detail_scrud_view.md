# hrm.employee_identification_detail_scrud_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | employee_identification_detail_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.employee_identification_detail_scrud_view
 AS
 SELECT employee_identification_details.employee_identification_detail_id,
    employee_identification_details.employee_id,
    employees.employee_name,
    employee_identification_details.identification_type_id,
    identification_types.identification_type_code,
    identification_types.identification_type_name,
    employee_identification_details.identification_number,
    employee_identification_details.expires_on
   FROM hrm.employee_identification_details
     JOIN hrm.employees ON employee_identification_details.employee_id = employees.employee_id
     JOIN hrm.identification_types ON employee_identification_details.identification_type_id = identification_types.identification_type_id
  WHERE NOT employee_identification_details.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

