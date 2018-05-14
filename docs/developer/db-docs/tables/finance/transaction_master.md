# finance.transaction_master table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | transaction_master_id | [ ] | bigint | 0 |  |
| 2 | transaction_counter | [ ] | integer | 0 |  |
| 3 | transaction_code | [ ] | character varying | 50 |  |
| 4 | book | [ ] | character varying | 50 |  |
| 5 | value_date | [ ] | date | 0 |  |
| 6 | book_date | [ ] | date | 0 |  |
| 7 | transaction_ts | [ ] | timestamp with time zone | 0 |  |
| 8 | login_id | [ ] | bigint | 0 |  |
| 9 | user_id | [ ] | integer | 0 |  |
| 10 | office_id | [ ] | integer | 0 |  |
| 11 | cost_center_id | [x] | integer | 0 |  |
| 12 | reference_number | [x] | character varying | 24 |  |
| 13 | statement_reference | [x] | text | 0 |  |
| 14 | last_verified_on | [x] | timestamp with time zone | 0 |  |
| 15 | verified_by_user_id | [x] | integer | 0 |  |
| 16 | verification_status_id | [ ] | smallint | 0 |  |
| 17 | verification_reason | [ ] | character varying | 128 |  |
| 18 | cascading_tran_id | [x] | bigint | 0 |  |
| 19 | audit_user_id | [x] | integer | 0 |  |
| 20 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 21 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 8 | [login_id](../account/logins.md) | transaction_master_login_id_fkey | account.logins.login_id |
| 9 | [user_id](../account/users.md) | transaction_master_user_id_fkey | account.users.user_id |
| 10 | [office_id](../core/offices.md) | transaction_master_office_id_fkey | core.offices.office_id |
| 11 | [cost_center_id](../finance/cost_centers.md) | transaction_master_cost_center_id_fkey | finance.cost_centers.cost_center_id |
| 15 | [verified_by_user_id](../account/users.md) | transaction_master_verified_by_user_id_fkey | account.users.user_id |
| 16 | [verification_status_id](../core/verification_statuses.md) | transaction_master_verification_status_id_fkey | core.verification_statuses.verification_status_id |
| 18 | [cascading_tran_id](../finance/transaction_master.md) | transaction_master_cascading_tran_id_fkey | finance.transaction_master.transaction_master_id |
| 19 | [audit_user_id](../account/users.md) | transaction_master_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| transaction_master_pkey | frapid_db_user | btree | transaction_master_id |  |
| transaction_master_transaction_code_uix | frapid_db_user | btree | upper(transaction_code::text) |  |
| transaction_master_cascading_tran_id_inx | frapid_db_user | btree | cascading_tran_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | transaction_master_id | nextval('finance.transaction_master_transaction_master_id_seq'::regclass) |
| 7 | transaction_ts | now() |
| 16 | verification_status_id | 0 |
| 17 | verification_reason | ''::character varying |
| 20 | audit_ts | now() |
| 21 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |
| finance.update_transaction_meta | [finance.update_transaction_meta](../../functions/finance/update_transaction_meta-4456382.md) | INSERT | AFTER |  | 0 | ROW |  |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
