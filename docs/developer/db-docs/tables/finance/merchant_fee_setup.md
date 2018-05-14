# finance.merchant_fee_setup table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | merchant_fee_setup_id | [ ] | integer | 0 |  |
| 2 | merchant_account_id | [ ] | integer | 0 |  |
| 3 | payment_card_id | [ ] | integer | 0 |  |
| 4 | rate | [ ] | decimal_strict | 0 |  |
| 5 | customer_pays_fee | [ ] | boolean | 0 |  |
| 6 | account_id | [ ] | integer | 0 |  |
| 7 | statement_reference | [ ] | character varying | 128 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [merchant_account_id](../finance/bank_accounts.md) | merchant_fee_setup_merchant_account_id_fkey | finance.bank_accounts.bank_account_id |
| 3 | [payment_card_id](../finance/payment_cards.md) | merchant_fee_setup_payment_card_id_fkey | finance.payment_cards.payment_card_id |
| 6 | [account_id](../finance/accounts.md) | merchant_fee_setup_account_id_fkey | finance.accounts.account_id |
| 8 | [audit_user_id](../account/users.md) | merchant_fee_setup_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| merchant_fee_setup_pkey | frapid_db_user | btree | merchant_fee_setup_id |  |
| merchant_fee_setup_merchant_account_id_payment_card_id_uix | frapid_db_user | btree | merchant_account_id, payment_card_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | merchant_fee_setup_id | nextval('finance.merchant_fee_setup_merchant_fee_setup_id_seq'::regclass) |
| 5 | customer_pays_fee | false |
| 7 | statement_reference | ''::character varying |
| 9 | audit_ts | now() |
| 10 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
