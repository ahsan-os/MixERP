# hrm.status_code_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | status_code_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.status_code_view
 AS
 SELECT employment_status_codes.employment_status_code_id AS status_code_id,
    employment_status_codes.status_code,
    employment_status_codes.status_code_name
   FROM hrm.employment_status_codes
  WHERE NOT employment_status_codes.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

