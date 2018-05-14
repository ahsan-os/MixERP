# sales.late_fee table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | late_fee_id | [ ] | integer | 0 |  |
| 2 | late_fee_code | [ ] | character varying | 24 |  |
| 3 | late_fee_name | [ ] | character varying | 500 |  |
| 4 | is_flat_amount | [ ] | boolean | 0 |  |
| 5 | rate | [ ] | numeric | 1966086 |  |
| 6 | account_id | [ ] | integer | 0 |  |
| 7 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 6 | [account_id](../finance/accounts.md) | late_fee_account_id_fkey | finance.accounts.account_id |
| 7 | [audit_user_id](../account/users.md) | late_fee_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| late_fee_pkey | frapid_db_user | btree | late_fee_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | late_fee_id | nextval('sales.late_fee_late_fee_id_seq'::regclass) |
| 4 | is_flat_amount | false |
| 8 | audit_ts | now() |
| 9 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
