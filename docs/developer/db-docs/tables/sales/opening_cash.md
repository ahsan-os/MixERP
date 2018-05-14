# sales.opening_cash table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | opening_cash_id | [ ] | bigint | 0 |  |
| 2 | user_id | [ ] | integer | 0 |  |
| 3 | transaction_date | [ ] | date | 0 |  |
| 4 | amount | [ ] | numeric | 1966086 |  |
| 5 | provided_by | [ ] | character varying | 1000 |  |
| 6 | memo | [x] | character varying | 4000 |  |
| 7 | closed | [ ] | boolean | 0 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [user_id](../account/users.md) | opening_cash_user_id_fkey | account.users.user_id |
| 8 | [audit_user_id](../account/users.md) | opening_cash_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| opening_cash_pkey | frapid_db_user | btree | opening_cash_id |  |
| opening_cash_transaction_date_user_id_uix | frapid_db_user | btree | user_id, transaction_date |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | opening_cash_id | nextval('sales.opening_cash_opening_cash_id_seq'::regclass) |
| 5 | provided_by | ''::character varying |
| 6 | memo | ''::character varying |
| 7 | closed | false |
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
