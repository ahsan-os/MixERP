# finance.frequency_date_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | frequency_date_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.frequency_date_view
 AS
 SELECT offices.office_id,
    finance.get_value_date(offices.office_id) AS today,
    finance.is_new_day_started(offices.office_id) AS new_day_started,
    finance.get_month_start_date(offices.office_id) AS month_start_date,
    finance.get_month_end_date(offices.office_id) AS month_end_date,
    finance.get_quarter_start_date(offices.office_id) AS quarter_start_date,
    finance.get_quarter_end_date(offices.office_id) AS quarter_end_date,
    finance.get_fiscal_half_start_date(offices.office_id) AS fiscal_half_start_date,
    finance.get_fiscal_half_end_date(offices.office_id) AS fiscal_half_end_date,
    finance.get_fiscal_year_start_date(offices.office_id) AS fiscal_year_start_date,
    finance.get_fiscal_year_end_date(offices.office_id) AS fiscal_year_end_date
   FROM core.offices;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

