# sales.customer_receipts table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | receipt_id | [ ] | bigint | 0 |  |
| 2 | transaction_master_id | [ ] | bigint | 0 |  |
| 3 | customer_id | [ ] | integer | 0 |  |
| 4 | currency_code | [ ] | character varying | 12 |  |
| 5 | er_debit | [ ] | decimal_strict | 0 |  |
| 6 | er_credit | [ ] | decimal_strict | 0 |  |
| 7 | cash_repository_id | [x] | integer | 0 |  |
| 8 | posted_date | [x] | date | 0 |  |
| 9 | tender | [x] | money_strict2 | 0 |  |
| 10 | change | [x] | money_strict2 | 0 |  |
| 11 | amount | [x] | money_strict2 | 0 |  |
| 12 | collected_on_bank_id | [x] | integer | 0 |  |
| 13 | collected_bank_instrument_code | [x] | character varying | 500 |  |
| 14 | collected_bank_transaction_code | [x] | character varying | 500 |  |
| 15 | check_number | [x] | character varying | 100 |  |
| 16 | check_date | [x] | date | 0 |  |
| 17 | check_bank_name | [x] | character varying | 1000 |  |
| 18 | check_amount | [x] | money_strict2 | 0 |  |
| 19 | check_cleared | [x] | boolean | 0 |  |
| 20 | check_clear_date | [x] | date | 0 |  |
| 21 | check_clearing_memo | [x] | character varying | 1000 |  |
| 22 | check_clearing_transaction_master_id | [x] | bigint | 0 |  |
| 23 | gift_card_number | [x] | character varying | 100 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [transaction_master_id](../finance/transaction_master.md) | customer_receipts_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |
| 3 | [customer_id](../inventory/customers.md) | customer_receipts_customer_id_fkey | inventory.customers.customer_id |
| 4 | [currency_code](../core/currencies.md) | customer_receipts_currency_code_fkey | core.currencies.currency_code |
| 7 | [cash_repository_id](../finance/cash_repositories.md) | customer_receipts_cash_repository_id_fkey | finance.cash_repositories.cash_repository_id |
| 12 | [collected_on_bank_id](../finance/bank_accounts.md) | customer_receipts_collected_on_bank_id_fkey | finance.bank_accounts.bank_account_id |
| 22 | [check_clearing_transaction_master_id](../finance/transaction_master.md) | customer_receipts_check_clearing_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| customer_receipts_pkey | frapid_db_user | btree | receipt_id |  |
| customer_receipts_transaction_master_id_inx | frapid_db_user | btree | transaction_master_id |  |
| customer_receipts_customer_id_inx | frapid_db_user | btree | customer_id |  |
| customer_receipts_currency_code_inx | frapid_db_user | btree | currency_code |  |
| customer_receipts_cash_repository_id_inx | frapid_db_user | btree | cash_repository_id |  |
| customer_receipts_posted_date_inx | frapid_db_user | btree | posted_date |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | receipt_id | nextval('sales.customer_receipts_receipt_id_seq'::regclass) |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
