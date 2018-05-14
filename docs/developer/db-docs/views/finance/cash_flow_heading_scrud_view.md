# finance.cash_flow_heading_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | cash_flow_heading_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.cash_flow_heading_scrud_view
 AS
 SELECT cash_flow_headings.cash_flow_heading_id,
    cash_flow_headings.cash_flow_heading_code,
    cash_flow_headings.cash_flow_heading_name,
    cash_flow_headings.cash_flow_heading_type,
    cash_flow_headings.is_debit,
    cash_flow_headings.is_sales,
    cash_flow_headings.is_purchase
   FROM finance.cash_flow_headings
  WHERE NOT cash_flow_headings.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

