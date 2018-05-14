# sales.gift_cards table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | gift_card_id | [ ] | integer | 0 |  |
| 2 | gift_card_number | [ ] | character varying | 100 |  |
| 3 | payable_account_id | [ ] | integer | 0 |  |
| 4 | customer_id | [x] | integer | 0 |  |
| 5 | first_name | [x] | character varying | 100 |  |
| 6 | middle_name | [x] | character varying | 100 |  |
| 7 | last_name | [x] | character varying | 100 |  |
| 8 | address_line_1 | [x] | character varying | 128 |  |
| 9 | address_line_2 | [x] | character varying | 128 |  |
| 10 | street | [x] | character varying | 100 |  |
| 11 | city | [x] | character varying | 100 |  |
| 12 | state | [x] | character varying | 100 |  |
| 13 | country | [x] | character varying | 100 |  |
| 14 | po_box | [x] | character varying | 100 |  |
| 15 | zipcode | [x] | character varying | 100 |  |
| 16 | phone_numbers | [x] | character varying | 100 |  |
| 17 | fax | [x] | character varying | 100 |  |
| 18 | audit_user_id | [x] | integer | 0 |  |
| 19 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 20 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 3 | [payable_account_id](../finance/accounts.md) | gift_cards_payable_account_id_fkey | finance.accounts.account_id |
| 4 | [customer_id](../inventory/customers.md) | gift_cards_customer_id_fkey | inventory.customers.customer_id |
| 18 | [audit_user_id](../account/users.md) | gift_cards_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| gift_cards_pkey | frapid_db_user | btree | gift_card_id |  |
| gift_cards_gift_card_number_uix | frapid_db_user | btree | upper(gift_card_number::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | gift_card_id | nextval('sales.gift_cards_gift_card_id_seq'::regclass) |
| 19 | audit_ts | now() |
| 20 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
