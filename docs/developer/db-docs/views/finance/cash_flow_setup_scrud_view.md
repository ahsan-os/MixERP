# finance.cash_flow_setup_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | cash_flow_setup_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.cash_flow_setup_scrud_view
 AS
 SELECT cash_flow_setup.cash_flow_setup_id,
    ((cash_flow_headings.cash_flow_heading_code::text || '('::text) || cash_flow_headings.cash_flow_heading_name::text) || ')'::text AS cash_flow_heading,
    ((account_masters.account_master_code::text || '('::text) || account_masters.account_master_name::text) || ')'::text AS account_master
   FROM finance.cash_flow_setup
     JOIN finance.cash_flow_headings ON cash_flow_setup.cash_flow_heading_id = cash_flow_headings.cash_flow_heading_id
     JOIN finance.account_masters ON cash_flow_setup.account_master_id = account_masters.account_master_id
  WHERE NOT cash_flow_setup.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

