# sales.gift_card_transactions table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | transaction_id | [ ] | bigint | 0 |  |
| 2 | gift_card_id | [ ] | integer | 0 |  |
| 3 | value_date | [x] | date | 0 |  |
| 4 | book_date | [x] | date | 0 |  |
| 5 | transaction_master_id | [ ] | bigint | 0 |  |
| 6 | transaction_type | [ ] | character varying | 2 |  |
| 7 | amount | [x] | money_strict | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [gift_card_id](../sales/gift_cards.md) | gift_card_transactions_gift_card_id_fkey | sales.gift_cards.gift_card_id |
| 5 | [transaction_master_id](../finance/transaction_master.md) | gift_card_transactions_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| gift_card_transactions_pkey | frapid_db_user | btree | transaction_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| gift_card_transactions_transaction_type_check CHECK (transaction_type::text = ANY (ARRAY['Dr'::character varying, 'Cr'::character varying]::text[])) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | transaction_id | nextval('sales.gift_card_transactions_transaction_id_seq'::regclass) |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
