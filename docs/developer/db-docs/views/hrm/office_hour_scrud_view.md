# hrm.office_hour_scrud_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | office_hour_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.office_hour_scrud_view
 AS
 SELECT office_hours.office_hour_id,
    ((offices.office_code::text || ' ('::text) || offices.office_name::text) || ')'::text AS office,
    offices.logo AS photo,
    ((shifts.shift_code::text || ' ('::text) || shifts.shift_name::text) || ')'::text AS shift,
    ((week_days.week_day_code::text || ' ('::text) || week_days.week_day_name::text) || ')'::text AS week_day,
    office_hours.begins_from,
    office_hours.ends_on
   FROM hrm.office_hours
     LEFT JOIN core.offices ON offices.office_id = office_hours.office_id
     LEFT JOIN hrm.shifts ON shifts.shift_id = office_hours.shift_id
     LEFT JOIN hrm.week_days ON week_days.week_day_id = office_hours.week_day_id
  WHERE NOT office_hours.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

