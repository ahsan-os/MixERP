# sales.returns table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | return_id | [ ] | bigint | 0 |  |
| 2 | sales_id | [ ] | bigint | 0 |  |
| 3 | checkout_id | [ ] | bigint | 0 |  |
| 4 | transaction_master_id | [ ] | bigint | 0 |  |
| 5 | return_transaction_master_id | [ ] | bigint | 0 |  |
| 6 | counter_id | [ ] | integer | 0 |  |
| 7 | customer_id | [x] | integer | 0 |  |
| 8 | price_type_id | [ ] | integer | 0 |  |
| 9 | is_credit | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [sales_id](../sales/sales.md) | returns_sales_id_fkey | sales.sales.sales_id |
| 3 | [checkout_id](../inventory/checkouts.md) | returns_checkout_id_fkey | inventory.checkouts.checkout_id |
| 4 | [transaction_master_id](../finance/transaction_master.md) | returns_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |
| 5 | [return_transaction_master_id](../finance/transaction_master.md) | returns_return_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |
| 6 | [counter_id](../inventory/counters.md) | returns_counter_id_fkey | inventory.counters.counter_id |
| 7 | [customer_id](../inventory/customers.md) | returns_customer_id_fkey | inventory.customers.customer_id |
| 8 | [price_type_id](../sales/price_types.md) | returns_price_type_id_fkey | sales.price_types.price_type_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| returns_pkey | frapid_db_user | btree | return_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | return_id | nextval('sales.returns_return_id_seq'::regclass) |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
