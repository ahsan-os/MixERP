# finance.account_masters table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | account_master_id | [ ] | smallint | 0 |  |
| 2 | account_master_code | [ ] | character varying | 3 |  |
| 3 | account_master_name | [ ] | character varying | 40 |  |
| 4 | normally_debit | [ ] | boolean | 0 |  |
| 5 | parent_account_master_id | [x] | smallint | 0 |  |
| 6 | audit_user_id | [x] | integer | 0 |  |
| 7 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 8 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 5 | [parent_account_master_id](../finance/account_masters.md) | account_masters_parent_account_master_id_fkey | finance.account_masters.account_master_id |
| 6 | [audit_user_id](../account/users.md) | account_masters_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| account_masters_pkey | frapid_db_user | btree | account_master_id |  |
| account_master_code_uix | frapid_db_user | btree | upper(account_master_code::text) |  |
| account_master_name_uix | frapid_db_user | btree | upper(account_master_name::text) |  |
| account_master_parent_account_master_id_inx | frapid_db_user | btree | parent_account_master_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 4 | normally_debit | false |
| 7 | audit_ts | now() |
| 8 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
