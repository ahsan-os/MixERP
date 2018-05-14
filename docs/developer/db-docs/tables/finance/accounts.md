# finance.accounts table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | account_id | [ ] | integer | 0 |  |
| 2 | account_master_id | [ ] | smallint | 0 |  |
| 3 | account_number | [ ] | character varying | 24 |  |
| 4 | external_code | [x] | character varying | 24 |  |
| 5 | currency_code | [ ] | character varying | 12 |  |
| 6 | account_name | [ ] | character varying | 500 |  |
| 7 | description | [x] | character varying | 1000 |  |
| 8 | confidential | [ ] | boolean | 0 |  |
| 9 | is_transaction_node | [ ] | boolean | 0 |  |
| 10 | sys_type | [ ] | boolean | 0 |  |
| 11 | parent_account_id | [x] | integer | 0 |  |
| 12 | audit_user_id | [x] | integer | 0 |  |
| 13 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 14 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [account_master_id](../finance/account_masters.md) | accounts_account_master_id_fkey | finance.account_masters.account_master_id |
| 5 | [currency_code](../core/currencies.md) | accounts_currency_code_fkey | core.currencies.currency_code |
| 11 | [parent_account_id](../finance/accounts.md) | accounts_parent_account_id_fkey | finance.accounts.account_id |
| 12 | [audit_user_id](../account/users.md) | accounts_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| accounts_pkey | frapid_db_user | btree | account_id |  |
| accounts_account_number_uix | frapid_db_user | btree | upper(account_number::text) |  |
| accounts_name_uix | frapid_db_user | btree | upper(account_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | account_id | nextval('finance.accounts_account_id_seq'::regclass) |
| 4 | external_code | ''::character varying |
| 8 | confidential | false |
| 9 | is_transaction_node | true |
| 10 | sys_type | false |
| 13 | audit_ts | now() |
| 14 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
