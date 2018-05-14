# finance.account_selector_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | account_selector_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.account_selector_view
 AS
 SELECT accounts.account_id,
    accounts.account_number AS account_code,
    accounts.account_name
   FROM finance.accounts
  WHERE NOT accounts.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

