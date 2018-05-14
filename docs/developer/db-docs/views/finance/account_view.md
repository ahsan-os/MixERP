# finance.account_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | account_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.account_view
 AS
 SELECT accounts.account_id,
    ((accounts.account_number::text || ' ('::text) || accounts.account_name::text) || ')'::text AS account,
    accounts.account_number,
    accounts.account_name,
    accounts.description,
    accounts.external_code,
    accounts.currency_code,
    accounts.confidential,
    account_masters.normally_debit,
    accounts.is_transaction_node,
    accounts.sys_type,
    accounts.parent_account_id,
    parent_accounts.account_number AS parent_account_number,
    parent_accounts.account_name AS parent_account_name,
    ((parent_accounts.account_number::text || ' ('::text) || parent_accounts.account_name::text) || ')'::text AS parent_account,
    account_masters.account_master_id,
    account_masters.account_master_code,
    account_masters.account_master_name,
    finance.has_child_accounts(accounts.account_id::bigint) AS has_child
   FROM finance.account_masters
     JOIN finance.accounts ON account_masters.account_master_id = accounts.account_master_id
     LEFT JOIN finance.accounts parent_accounts ON accounts.parent_account_id = parent_accounts.account_id
  WHERE NOT account_masters.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

