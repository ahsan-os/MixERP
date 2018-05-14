# finance.fiscal_year_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | fiscal_year_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.fiscal_year_scrud_view
 AS
 SELECT fiscal_year.fiscal_year_id,
    fiscal_year.fiscal_year_code,
    fiscal_year.fiscal_year_name,
    fiscal_year.starts_from,
    fiscal_year.ends_on,
    fiscal_year.eod_required,
    core.get_office_name_by_office_id(fiscal_year.office_id) AS office
   FROM finance.fiscal_year
  WHERE NOT fiscal_year.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

