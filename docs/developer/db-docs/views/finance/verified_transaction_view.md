# finance.verified_transaction_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | verified_transaction_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.verified_transaction_view
 AS
 SELECT transaction_view.transaction_master_id,
    transaction_view.transaction_counter,
    transaction_view.transaction_code,
    transaction_view.book,
    transaction_view.value_date,
    transaction_view.transaction_ts,
    transaction_view.login_id,
    transaction_view.user_id,
    transaction_view.office_id,
    transaction_view.cost_center_id,
    transaction_view.reference_number,
    transaction_view.master_statement_reference,
    transaction_view.last_verified_on,
    transaction_view.verified_by_user_id,
    transaction_view.verification_status_id,
    transaction_view.verification_reason,
    transaction_view.transaction_detail_id,
    transaction_view.tran_type,
    transaction_view.account_id,
    transaction_view.account_number,
    transaction_view.account_name,
    transaction_view.normally_debit,
    transaction_view.account_master_code,
    transaction_view.account_master_name,
    transaction_view.account_master_id,
    transaction_view.confidential,
    transaction_view.statement_reference,
    transaction_view.cash_repository_id,
    transaction_view.currency_code,
    transaction_view.amount_in_currency,
    transaction_view.local_currency_code,
    transaction_view.amount_in_local_currency
   FROM finance.transaction_view
  WHERE transaction_view.verification_status_id > 0;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

