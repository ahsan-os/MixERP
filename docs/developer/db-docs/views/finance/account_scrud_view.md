# finance.account_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | account_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.account_scrud_view
 AS
 SELECT accounts.account_id,
    ((account_masters.account_master_code::text || ' ('::text) || account_masters.account_master_name::text) || ')'::text AS account_master,
    accounts.account_number,
    accounts.external_code,
    ((currencies.currency_code::text || ' ('::text) || currencies.currency_name::text) || ')'::text AS currency,
    accounts.account_name,
    accounts.description,
    accounts.confidential,
    accounts.is_transaction_node,
    accounts.sys_type,
    accounts.account_master_id,
    ((parent_account.account_number::text || ' ('::text) || parent_account.account_name::text) || ')'::text AS parent
   FROM finance.accounts
     JOIN finance.account_masters ON account_masters.account_master_id = accounts.account_master_id
     LEFT JOIN core.currencies ON accounts.currency_code::text = currencies.currency_code::text
     LEFT JOIN finance.accounts parent_account ON parent_account.account_id = accounts.parent_account_id
  WHERE NOT accounts.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

