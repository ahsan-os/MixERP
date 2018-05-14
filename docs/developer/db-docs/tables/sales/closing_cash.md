# sales.closing_cash table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | closing_cash_id | [ ] | bigint | 0 |  |
| 2 | user_id | [ ] | integer | 0 |  |
| 3 | transaction_date | [ ] | date | 0 |  |
| 4 | opening_cash | [ ] | numeric | 1966086 |  |
| 5 | total_cash_sales | [ ] | numeric | 1966086 |  |
| 6 | submitted_to | [ ] | character varying | 1000 |  |
| 7 | memo | [ ] | character varying | 4000 |  |
| 8 | deno_1000 | [x] | integer | 0 |  |
| 9 | deno_500 | [x] | integer | 0 |  |
| 10 | deno_250 | [x] | integer | 0 |  |
| 11 | deno_200 | [x] | integer | 0 |  |
| 12 | deno_100 | [x] | integer | 0 |  |
| 13 | deno_50 | [x] | integer | 0 |  |
| 14 | deno_25 | [x] | integer | 0 |  |
| 15 | deno_20 | [x] | integer | 0 |  |
| 16 | deno_10 | [x] | integer | 0 |  |
| 17 | deno_5 | [x] | integer | 0 |  |
| 18 | deno_2 | [x] | integer | 0 |  |
| 19 | deno_1 | [x] | integer | 0 |  |
| 20 | coins | [x] | numeric | 1966086 |  |
| 21 | submitted_cash | [ ] | numeric | 1966086 |  |
| 22 | approved_by | [x] | integer | 0 |  |
| 23 | approval_memo | [x] | character varying | 4000 |  |
| 24 | audit_user_id | [x] | integer | 0 |  |
| 25 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 26 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [user_id](../account/users.md) | closing_cash_user_id_fkey | account.users.user_id |
| 22 | [approved_by](../account/users.md) | closing_cash_approved_by_fkey | account.users.user_id |
| 24 | [audit_user_id](../account/users.md) | closing_cash_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| closing_cash_pkey | frapid_db_user | btree | closing_cash_id |  |
| closing_cash_transaction_date_user_id_uix | frapid_db_user | btree | user_id, transaction_date |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | closing_cash_id | nextval('sales.closing_cash_closing_cash_id_seq'::regclass) |
| 6 | submitted_to | ''::character varying |
| 7 | memo | ''::character varying |
| 8 | deno_1000 | 0 |
| 9 | deno_500 | 0 |
| 10 | deno_250 | 0 |
| 11 | deno_200 | 0 |
| 12 | deno_100 | 0 |
| 13 | deno_50 | 0 |
| 14 | deno_25 | 0 |
| 15 | deno_20 | 0 |
| 16 | deno_10 | 0 |
| 17 | deno_5 | 0 |
| 18 | deno_2 | 0 |
| 19 | deno_1 | 0 |
| 20 | coins | 0 |
| 25 | audit_ts | now() |
| 26 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
