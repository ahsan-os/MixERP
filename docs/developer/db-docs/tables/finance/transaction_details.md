# finance.transaction_details table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | transaction_detail_id | [ ] | bigint | 0 |  |
| 2 | transaction_master_id | [ ] | bigint | 0 |  |
| 3 | value_date | [ ] | date | 0 |  |
| 4 | book_date | [ ] | date | 0 |  |
| 5 | tran_type | [ ] | character varying | 4 |  |
| 6 | account_id | [ ] | integer | 0 |  |
| 7 | statement_reference | [x] | text | 0 |  |
| 8 | reconciliation_memo | [x] | text | 0 |  |
| 9 | cash_repository_id | [x] | integer | 0 |  |
| 10 | currency_code | [ ] | character varying | 12 |  |
| 11 | amount_in_currency | [ ] | money_strict | 0 |  |
| 12 | local_currency_code | [ ] | character varying | 12 |  |
| 13 | er | [ ] | decimal_strict | 0 |  |
| 14 | amount_in_local_currency | [ ] | money_strict | 0 |  |
| 15 | office_id | [ ] | integer | 0 |  |
| 16 | audit_user_id | [x] | integer | 0 |  |
| 17 | audit_ts | [x] | timestamp with time zone | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [transaction_master_id](../finance/transaction_master.md) | transaction_details_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |
| 6 | [account_id](../finance/accounts.md) | transaction_details_account_id_fkey | finance.accounts.account_id |
| 9 | [cash_repository_id](../finance/cash_repositories.md) | transaction_details_cash_repository_id_fkey | finance.cash_repositories.cash_repository_id |
| 10 | [currency_code](../core/currencies.md) | transaction_details_currency_code_fkey | core.currencies.currency_code |
| 12 | [local_currency_code](../core/currencies.md) | transaction_details_local_currency_code_fkey | core.currencies.currency_code |
| 15 | [office_id](../core/offices.md) | transaction_details_office_id_fkey | core.offices.office_id |
| 16 | [audit_user_id](../account/users.md) | transaction_details_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| transaction_details_pkey | frapid_db_user | btree | transaction_detail_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| transaction_details_tran_type_check CHECK (tran_type::text = ANY (ARRAY['Dr'::character varying, 'Cr'::character varying]::text[])) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | transaction_detail_id | nextval('finance.transaction_details_transaction_detail_id_seq'::regclass) |
| 17 | audit_ts | now() |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
