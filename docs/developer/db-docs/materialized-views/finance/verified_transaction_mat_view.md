# finance.verified_transaction_mat_view materialized view

| Schema | [finance](../../schemas/finance.md) |
| Materialized View Name | verified_transaction_mat_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

```sql
 CREATE MATERIALIZED VIEW finance.verified_transaction_mat_view
 AS
 SELECT verified_transaction_view.transaction_master_id,
    verified_transaction_view.transaction_counter,
    verified_transaction_view.transaction_code,
    verified_transaction_view.book,
    verified_transaction_view.value_date,
    verified_transaction_view.transaction_ts,
    verified_transaction_view.login_id,
    verified_transaction_view.user_id,
    verified_transaction_view.office_id,
    verified_transaction_view.cost_center_id,
    verified_transaction_view.reference_number,
    verified_transaction_view.master_statement_reference,
    verified_transaction_view.last_verified_on,
    verified_transaction_view.verified_by_user_id,
    verified_transaction_view.verification_status_id,
    verified_transaction_view.verification_reason,
    verified_transaction_view.transaction_detail_id,
    verified_transaction_view.tran_type,
    verified_transaction_view.account_id,
    verified_transaction_view.account_number,
    verified_transaction_view.account_name,
    verified_transaction_view.normally_debit,
    verified_transaction_view.account_master_code,
    verified_transaction_view.account_master_name,
    verified_transaction_view.account_master_id,
    verified_transaction_view.confidential,
    verified_transaction_view.statement_reference,
    verified_transaction_view.cash_repository_id,
    verified_transaction_view.currency_code,
    verified_transaction_view.amount_in_currency,
    verified_transaction_view.local_currency_code,
    verified_transaction_view.amount_in_local_currency
   FROM finance.verified_transaction_view;
```

### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)
