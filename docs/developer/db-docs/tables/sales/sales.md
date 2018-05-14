# sales.sales table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | sales_id | [ ] | bigint | 0 |  |
| 1 | sales_id | [ ] | bigint | 0 |  |
| 2 | invoice_number | [ ] | bigint | 0 |  |
| 2 | sold_on | [ ] | timestamp with time zone | 0 |  |
| 3 | fiscal_year_code | [ ] | character varying | 12 |  |
| 3 | customer_id | [ ] | integer | 0 |  |
| 4 | cash_repository_id | [x] | integer | 0 |  |
| 4 | product_id | [ ] | bigint | 0 |  |
| 5 | domain_prefix | [x] | character varying | 100 |  |
| 5 | price_type_id | [ ] | integer | 0 |  |
| 6 | coupon_code | [x] | character varying | 100 |  |
| 6 | sales_order_id | [x] | bigint | 0 |  |
| 7 | initial_amount | [x] | money_strict2 | 0 |  |
| 7 | sales_quotation_id | [x] | bigint | 0 |  |
| 8 | recurring_amount | [x] | money_strict | 0 |  |
| 8 | transaction_master_id | [ ] | bigint | 0 |  |
| 9 | billing_cycle_id | [x] | integer | 0 |  |
| 9 | receipt_transaction_master_id | [x] | bigint | 0 |  |
| 10 | cancelled | [ ] | boolean | 0 |  |
| 10 | checkout_id | [ ] | bigint | 0 |  |
| 11 | audit_user_id | [x] | integer | 0 |  |
| 11 | counter_id | [ ] | integer | 0 |  |
| 12 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 12 | customer_id | [x] | integer | 0 |  |
| 13 | deleted | [x] | boolean | 0 |  |
| 13 | salesperson_id | [x] | integer | 0 |  |
| 14 | total_amount | [ ] | money_strict | 0 |  |
| 15 | coupon_id | [x] | integer | 0 |  |
| 16 | is_flat_discount | [x] | boolean | 0 |  |
| 17 | discount | [x] | decimal_strict2 | 0 |  |
| 18 | total_discount_amount | [x] | decimal_strict2 | 0 |  |
| 19 | is_credit | [ ] | boolean | 0 |  |
| 20 | credit_settled | [x] | boolean | 0 |  |
| 21 | payment_term_id | [x] | integer | 0 |  |
| 22 | tender | [ ] | numeric | 1966086 |  |
| 23 | change | [ ] | numeric | 1966086 |  |
| 24 | gift_card_id | [x] | integer | 0 |  |
| 25 | check_number | [x] | character varying | 100 |  |
| 26 | check_date | [x] | date | 0 |  |
| 27 | check_bank_name | [x] | character varying | 1000 |  |
| 28 | check_amount | [x] | money_strict2 | 0 |  |
| 29 | reward_points | [ ] | numeric | 1966086 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 3 | [fiscal_year_code](../finance/fiscal_year.md) | sales_fiscal_year_code_fkey | finance.fiscal_year.fiscal_year_code |
| 3 | [customer_id](../inventory/customers.md) | sales_customer_id_fkey | inventory.customers.customer_id |
| 4 | [cash_repository_id](../finance/cash_repositories.md) | sales_cash_repository_id_fkey | finance.cash_repositories.cash_repository_id |
| 5 | [price_type_id](../sales/price_types.md) | sales_price_type_id_fkey | sales.price_types.price_type_id |
| 6 | [sales_order_id](../sales/orders.md) | sales_sales_order_id_fkey | sales.orders.order_id |
| 7 | [sales_quotation_id](../sales/quotations.md) | sales_sales_quotation_id_fkey | sales.quotations.quotation_id |
| 8 | [transaction_master_id](../finance/transaction_master.md) | sales_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |
| 9 | [receipt_transaction_master_id](../finance/transaction_master.md) | sales_receipt_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |
| 10 | [checkout_id](../inventory/checkouts.md) | sales_checkout_id_fkey | inventory.checkouts.checkout_id |
| 11 | [counter_id](../inventory/counters.md) | sales_counter_id_fkey | inventory.counters.counter_id |
| 12 | [customer_id](../inventory/customers.md) | sales_customer_id_fkey | inventory.customers.customer_id |
| 13 | [salesperson_id](../account/users.md) | sales_salesperson_id_fkey | account.users.user_id |
| 15 | [coupon_id](../sales/coupons.md) | sales_coupon_id_fkey | sales.coupons.coupon_id |
| 21 | [payment_term_id](../sales/payment_terms.md) | sales_payment_term_id_fkey | sales.payment_terms.payment_term_id |
| 24 | [gift_card_id](../sales/gift_cards.md) | sales_gift_card_id_fkey | sales.gift_cards.gift_card_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| sales_pkey | frapid_db_user | btree | sales_id |  |
| sales_invoice_number_fiscal_year_uix | frapid_db_user | btree | upper(fiscal_year_code::text), invoice_number |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | sales_id | nextval('sales.sales_sales_id_seq'::regclass) |
| 1 | sales_id | nextval('helpdesk.sales_sales_id_seq'::regclass) |
| 2 | sold_on | now() |
| 10 | cancelled | false |
| 12 | audit_ts | now() |
| 13 | deleted | false |
| 19 | is_credit | false |
| 29 | reward_points | 0 |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
