# finance.transaction_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | transaction_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.transaction_view
 AS
 SELECT transaction_master.transaction_master_id,
    transaction_master.transaction_counter,
    transaction_master.transaction_code,
    transaction_master.book,
    transaction_master.value_date,
    transaction_master.transaction_ts,
    transaction_master.login_id,
    transaction_master.user_id,
    transaction_master.office_id,
    transaction_master.cost_center_id,
    transaction_master.reference_number,
    transaction_master.statement_reference AS master_statement_reference,
    transaction_master.last_verified_on,
    transaction_master.verified_by_user_id,
    transaction_master.verification_status_id,
    transaction_master.verification_reason,
    transaction_details.transaction_detail_id,
    transaction_details.tran_type,
    transaction_details.account_id,
    accounts.account_number,
    accounts.account_name,
    account_masters.normally_debit,
    account_masters.account_master_code,
    account_masters.account_master_name,
    accounts.account_master_id,
    accounts.confidential,
    transaction_details.statement_reference,
    transaction_details.cash_repository_id,
    transaction_details.currency_code,
    transaction_details.amount_in_currency,
    transaction_details.local_currency_code,
    transaction_details.amount_in_local_currency
   FROM finance.transaction_master
     JOIN finance.transaction_details ON transaction_master.transaction_master_id = transaction_details.transaction_master_id
     JOIN finance.accounts ON transaction_details.account_id = accounts.account_id
     JOIN finance.account_masters ON accounts.account_master_id = account_masters.account_master_id
  WHERE NOT transaction_master.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

