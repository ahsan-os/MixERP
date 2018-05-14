# sales.late_fee_postings table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | transaction_master_id | [ ] | bigint | 0 |  |
| 2 | customer_id | [ ] | integer | 0 |  |
| 3 | value_date | [ ] | date | 0 |  |
| 4 | late_fee_tran_id | [ ] | bigint | 0 |  |
| 5 | amount | [x] | money_strict | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 1 | [transaction_master_id](../finance/transaction_master.md) | late_fee_postings_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |
| 2 | [customer_id](../inventory/customers.md) | late_fee_postings_customer_id_fkey | inventory.customers.customer_id |
| 4 | [late_fee_tran_id](../finance/transaction_master.md) | late_fee_postings_late_fee_tran_id_fkey | finance.transaction_master.transaction_master_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| late_fee_postings_pkey | frapid_db_user | btree | transaction_master_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
