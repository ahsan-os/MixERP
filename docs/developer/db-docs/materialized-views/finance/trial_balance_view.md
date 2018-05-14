# finance.trial_balance_view materialized view

| Schema | [finance](../../schemas/finance.md) |
| Materialized View Name | trial_balance_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

```sql
 CREATE MATERIALIZED VIEW finance.trial_balance_view
 AS
 SELECT finance.get_account_name_by_account_id(verified_transaction_view.account_id) AS get_account_name_by_account_id,
    sum(
        CASE verified_transaction_view.tran_type
            WHEN 'Dr'::text THEN verified_transaction_view.amount_in_local_currency::numeric
            ELSE NULL::numeric
        END) AS debit,
    sum(
        CASE verified_transaction_view.tran_type
            WHEN 'Cr'::text THEN verified_transaction_view.amount_in_local_currency::numeric
            ELSE NULL::numeric
        END) AS credit
   FROM finance.verified_transaction_view
  GROUP BY verified_transaction_view.account_id;
```

### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)
