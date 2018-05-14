# finance.shareholders_equity_selector_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | shareholders_equity_selector_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.shareholders_equity_selector_view
 AS
 SELECT account_scrud_view.account_id AS shareholders_equity_id,
    account_scrud_view.account_name AS shareholders_equity_name
   FROM finance.account_scrud_view
  WHERE (account_scrud_view.account_master_id IN ( SELECT get_account_master_ids.get_account_master_ids
           FROM finance.get_account_master_ids(15200) get_account_master_ids(get_account_master_ids)))
  ORDER BY account_scrud_view.account_id;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

