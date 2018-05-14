# finance.cash_flow_setup table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | cash_flow_setup_id | [ ] | integer | 0 |  |
| 2 | cash_flow_heading_id | [ ] | integer | 0 |  |
| 3 | account_master_id | [ ] | smallint | 0 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [cash_flow_heading_id](../finance/cash_flow_headings.md) | cash_flow_setup_cash_flow_heading_id_fkey | finance.cash_flow_headings.cash_flow_heading_id |
| 3 | [account_master_id](../finance/account_masters.md) | cash_flow_setup_account_master_id_fkey | finance.account_masters.account_master_id |
| 4 | [audit_user_id](../account/users.md) | cash_flow_setup_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| cash_flow_setup_pkey | frapid_db_user | btree | cash_flow_setup_id |  |
| cash_flow_setup_cash_flow_heading_id_inx | frapid_db_user | btree | cash_flow_heading_id |  |
| cash_flow_setup_account_master_id_inx | frapid_db_user | btree | account_master_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | cash_flow_setup_id | nextval('finance.cash_flow_setup_cash_flow_setup_id_seq'::regclass) |
| 5 | audit_ts | now() |
| 6 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
