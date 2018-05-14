# sales.payment_terms table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | payment_term_id | [ ] | integer | 0 |  |
| 2 | payment_term_code | [ ] | character varying | 24 |  |
| 3 | payment_term_name | [ ] | character varying | 500 |  |
| 4 | due_on_date | [ ] | boolean | 0 |  |
| 5 | due_days | [ ] | integer_strict2 | 0 |  |
| 6 | due_frequency_id | [x] | integer | 0 |  |
| 7 | grace_period | [ ] | integer | 0 |  |
| 8 | late_fee_id | [x] | integer | 0 |  |
| 9 | late_fee_posting_frequency_id | [x] | integer | 0 |  |
| 10 | audit_user_id | [x] | integer | 0 |  |
| 11 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 12 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 6 | [due_frequency_id](../finance/frequencies.md) | payment_terms_due_frequency_id_fkey | finance.frequencies.frequency_id |
| 8 | [late_fee_id](../sales/late_fee.md) | payment_terms_late_fee_id_fkey | sales.late_fee.late_fee_id |
| 9 | [late_fee_posting_frequency_id](../finance/frequencies.md) | payment_terms_late_fee_posting_frequency_id_fkey | finance.frequencies.frequency_id |
| 10 | [audit_user_id](../account/users.md) | payment_terms_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| payment_terms_pkey | frapid_db_user | btree | payment_term_id |  |
| payment_terms_payment_term_code_uix | frapid_db_user | btree | upper(payment_term_code::text) |  |
| payment_terms_payment_term_name_uix | frapid_db_user | btree | upper(payment_term_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | payment_term_id | nextval('sales.payment_terms_payment_term_id_seq'::regclass) |
| 4 | due_on_date | false |
| 5 | due_days | 0 |
| 7 | grace_period | 0 |
| 11 | audit_ts | now() |
| 12 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
