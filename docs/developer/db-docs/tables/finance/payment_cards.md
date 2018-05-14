# finance.payment_cards table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | payment_card_id | [ ] | integer | 0 |  |
| 2 | payment_card_code | [ ] | character varying | 12 |  |
| 3 | payment_card_name | [ ] | character varying | 100 |  |
| 4 | card_type_id | [ ] | integer | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [card_type_id](../finance/card_types.md) | payment_cards_card_type_id_fkey | finance.card_types.card_type_id |
| 5 | [audit_user_id](../account/users.md) | payment_cards_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| payment_cards_pkey | frapid_db_user | btree | payment_card_id |  |
| payment_cards_payment_card_code_uix | frapid_db_user | btree | upper(payment_card_code::text) |  |
| payment_cards_payment_card_name_uix | frapid_db_user | btree | upper(payment_card_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | payment_card_id | nextval('finance.payment_cards_payment_card_id_seq'::regclass) |
| 6 | audit_ts | now() |
| 7 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
