# core.office_scrud_view view

| Schema | [core](../../schemas/core.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | office_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW core.office_scrud_view
 AS
 SELECT offices.office_id,
    offices.office_code,
    offices.office_name,
    offices.currency_code,
    ((parent_office.office_code::text || ' ('::text) || parent_office.office_name::text) || ')'::text AS parent_office
   FROM core.offices
     LEFT JOIN core.offices parent_office ON parent_office.office_id = offices.parent_office_id
  WHERE NOT offices.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

