# inventory.checkouts table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | checkout_id | [ ] | bigint | 0 |  |
| 1 | checkout_id | [ ] | bigint | 0 |  |
| 1 | checkout_id | [ ] | bigint | 0 |  |
| 1 | checkout_id | [ ] | bigint | 0 |  |
| 2 | started_on | [ ] | timestamp with time zone | 0 |  |
| 2 | value_date | [ ] | date | 0 |  |
| 2 | fiscal_year_name | [ ] | character varying | 50 |  |
| 2 | fiscal_year_name | [ ] | character varying | 50 |  |
| 3 | book_date | [ ] | date | 0 |  |
| 3 | invoice_number | [ ] | bigint | 0 |  |
| 3 | invoice_number | [ ] | bigint | 0 |  |
| 3 | table_id | [ ] | bigint | 0 |  |
| 4 | payment_mode | [ ] | character varying | 100 |  |
| 4 | payment_mode | [ ] | character varying | 100 |  |
| 4 | transaction_master_id | [ ] | bigint | 0 |  |
| 4 | price_type_id | [x] | integer | 0 |  |
| 5 | transaction_timestamp | [ ] | timestamp with time zone | 0 |  |
| 5 | customer_name | [x] | character varying | 1000 |  |
| 5 | store_id | [x] | integer | 0 |  |
| 5 | transaction_timestamp | [ ] | timestamp with time zone | 0 |  |
| 6 | transaction_date | [ ] | date | 0 |  |
| 6 | counter_id | [x] | integer | 0 |  |
| 6 | customer_pan | [x] | character varying | 9 |  |
| 6 | transaction_book | [ ] | character varying | 100 |  |
| 7 | address | [x] | text | 0 |  |
| 7 | discount | [x] | decimal_strict2 | 0 |  |
| 7 | customer_name | [x] | character varying | 1000 |  |
| 7 | customer_id | [x] | integer | 0 |  |
| 8 | customer_code | [x] | character varying | 100 |  |
| 8 | transaction_timestamp | [ ] | timestamp with time zone | 0 |  |
| 8 | posted_by | [ ] | integer | 0 |  |
| 8 | customer_pan | [x] | character varying | 9 |  |
| 9 | transaction_date | [ ] | date | 0 |  |
| 9 | office_id | [ ] | integer | 0 |  |
| 9 | address | [x] | text | 0 |  |
| 9 | coupon_code | [x] | character varying | 100 |  |
| 10 | posted_by | [ ] | integer | 0 |  |
| 10 | cancelled | [ ] | boolean | 0 |  |
| 10 | payment_term_id | [x] | integer | 0 |  |
| 10 | posted_by | [ ] | integer | 0 |  |
| 11 | cancellation_reason | [x] | text | 0 |  |
| 11 | counter_id | [ ] | integer | 0 |  |
| 11 | counter_id | [ ] | integer | 0 |  |
| 11 | tender | [x] | money_strict2 | 0 |  |
| 12 | change | [x] | money_strict2 | 0 |  |
| 12 | shipper_id | [x] | integer | 0 |  |
| 12 | cancelled | [ ] | boolean | 0 |  |
| 12 | cancelled | [ ] | boolean | 0 |  |
| 13 | cancellation_reason | [x] | character varying | 1000 |  |
| 13 | audit_user_id | [x] | integer | 0 |  |
| 13 | cancellation_reason | [x] | character varying | 1000 |  |
| 13 | check_amount | [x] | money_strict2 | 0 |  |
| 14 | printed | [ ] | boolean | 0 |  |
| 14 | printed | [ ] | boolean | 0 |  |
| 14 | check_bank_name | [x] | character varying | 1000 |  |
| 14 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 15 | total_prints | [ ] | integer | 0 |  |
| 15 | check_number | [x] | character varying | 1000 |  |
| 15 | total_prints | [ ] | integer | 0 |  |
| 15 | deleted | [x] | boolean | 0 |  |
| 16 | check_date | [x] | date | 0 |  |
| 16 | movie_date | [ ] | date | 0 |  |
| 16 | amount | [ ] | numeric | 1966086 |  |
| 17 | discount | [ ] | numeric | 1966086 |  |
| 17 | gift_card_number | [x] | character varying | 500 |  |
| 17 | show_id | [ ] | bigint | 0 |  |
| 18 | vat | [ ] | numeric | 1966086 |  |
| 18 | discount_type | [x] | character varying | 20 |  |
| 18 | screen_id | [ ] | integer | 0 |  |
| 19 | tender | [ ] | numeric | 1966086 |  |
| 19 | all_seats | [x] | text | 0 |  |
| 19 | discount_rate | [x] | decimal_strict2 | 0 |  |
| 20 | seat | [ ] | character varying | 100 |  |
| 20 | change | [ ] | numeric | 1966086 |  |
| 20 | value_date | [x] | date | 0 |  |
| 21 | book_date | [x] | date | 0 |  |
| 21 | amount | [ ] | numeric | 1966086 |  |
| 21 | audit_user_id | [x] | integer | 0 |  |
| 22 | cost_center_id | [x] | integer | 0 |  |
| 22 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 22 | discount | [ ] | numeric | 1966086 |  |
| 23 | fdt | [ ] | numeric | 1966086 |  |
| 23 | shipper_id | [x] | integer | 0 |  |
| 24 | vat | [ ] | numeric | 1966086 |  |
| 24 | reference_number | [x] | character varying | 100 |  |
| 25 | statement_reference | [x] | character varying | 2000 |  |
| 25 | tender | [ ] | numeric | 1966086 |  |
| 26 | sales_id | [x] | bigint | 0 |  |
| 26 | batch_total | [ ] | numeric | 1966086 |  |
| 27 | batch_discount | [ ] | numeric | 1966086 |  |
| 27 | transaction_master_id | [x] | bigint | 0 |  |
| 28 | change | [ ] | numeric | 1966086 |  |
| 28 | inventory_checkout_id | [x] | bigint | 0 |  |
| 29 | audit_user_id | [x] | integer | 0 |  |
| 29 | closed | [ ] | boolean | 0 |  |
| 30 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 30 | closed_by | [x] | integer | 0 |  |
| 31 | closed_on | [x] | timestamp with time zone | 0 |  |
| 32 | cancelled | [ ] | boolean | 0 |  |
| 33 | cancelled_by | [x] | integer | 0 |  |
| 34 | cancelled_on | [x] | timestamp with time zone | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [transaction_master_id](../finance/transaction_master.md) | checkouts_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |
| 8 | [posted_by](../account/users.md) | checkouts_posted_by_fkey | account.users.user_id |
| 9 | [office_id](../core/offices.md) | checkouts_office_id_fkey | core.offices.office_id |
| 10 | [posted_by](../account/users.md) | checkouts_posted_by_fkey | account.users.user_id |
| 10 | [posted_by](../account/users.md) | checkouts_posted_by_fkey | account.users.user_id |
| 12 | [shipper_id](../inventory/shippers.md) | checkouts_shipper_id_fkey | inventory.shippers.shipper_id |
| 13 | [audit_user_id](../account/users.md) | checkouts_audit_user_id_fkey | account.users.user_id |
| 21 | [audit_user_id](../account/users.md) | checkouts_audit_user_id_fkey | account.users.user_id |
| 23 | [shipper_id](../inventory/shippers.md) | checkouts_shipper_id_fkey | inventory.shippers.shipper_id |
| 27 | [transaction_master_id](../finance/transaction_master.md) | checkouts_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |
| 29 | [audit_user_id](../account/users.md) | checkouts_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| checkouts_pkey | frapid_db_user | btree | checkout_id |  |
| checkouts_transaction_master_id_inx | frapid_db_user | btree | transaction_master_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | checkout_id | nextval('foodcourt.checkouts_checkout_id_seq'::regclass) |
| 1 | checkout_id | nextval('inventory.checkouts_checkout_id_seq'::regclass) |
| 1 | checkout_id | nextval('cafesys.checkouts_checkout_id_seq'::regclass) |
| 1 | checkout_id | nextval('cinesys.checkouts_checkout_id_seq'::regclass) |
| 2 | started_on | now() |
| 4 | payment_mode | 'Cash'::character varying |
| 4 | payment_mode | 'Cash'::character varying |
| 5 | transaction_timestamp | now() |
| 5 | transaction_timestamp | now() |
| 7 | discount | 0 |
| 8 | transaction_timestamp | now() |
| 10 | cancelled | false |
| 12 | cancelled | false |
| 12 | cancelled | false |
| 14 | printed | false |
| 14 | printed | false |
| 14 | audit_ts | now() |
| 15 | total_prints | 0 |
| 15 | total_prints | 0 |
| 15 | deleted | false |
| 17 | discount | 0 |
| 22 | audit_ts | now() |
| 22 | discount | 0 |
| 23 | fdt | 0 |
| 27 | batch_discount | 0 |
| 29 | closed | false |
| 30 | audit_ts | now() |
| 32 | cancelled | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
