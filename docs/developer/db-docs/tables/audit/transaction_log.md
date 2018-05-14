# audit.transaction_log table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | log_id | [ ] | bigint | 0 |  |
| 2 | user_id | [ ] | integer | 0 |  |
| 3 | tran_type | [x] | character varying | 20 |  |
| 4 | action | [x] | character varying | 20 |  |
| 5 | invoice_number | [x] | bigint | 0 |  |
| 6 | tran_date | [ ] | date | 0 |  |
| 7 | amount | [x] | numeric | 1966086 |  |
| 8 | remarks | [x] | character varying | 4000 |  |
| 9 | audit_ts | [ ] | timestamp with time zone | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [user_id](../account/users.md) | transaction_log_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| transaction_log_pkey | frapid_db_user | btree | log_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | log_id | nextval('audit.transaction_log_log_id_seq'::regclass) |
| 9 | audit_ts | now() |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |
| audit.disable_update_transaction_log | [audit.disable_update_transaction_log](../../functions/audit/disable_update_transaction_log-4458111.md) | UPDATE | AFTER |  | 0 | ROW |  |
| audit.disable_update_transaction_log | [audit.disable_update_transaction_log](../../functions/audit/disable_update_transaction_log-4458111.md) | DELETE | AFTER |  | 0 | ROW |  |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
