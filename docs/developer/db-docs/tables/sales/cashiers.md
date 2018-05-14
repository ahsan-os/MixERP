# sales.cashiers table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | cashier_id | [ ] | integer | 0 |  |
| 1 | cashier_id | [ ] | integer | 0 |  |
| 1 | cashier_id | [ ] | integer | 0 |  |
| 2 | cashier_code | [ ] | character varying | 12 |  |
| 2 | cashier_code | [ ] | character varying | 12 |  |
| 2 | cashier_code | [ ] | character varying | 12 |  |
| 3 | pin_code | [ ] | character varying | 8 |  |
| 3 | pin_code | [ ] | character varying | 8 |  |
| 3 | pin_code | [ ] | character varying | 8 |  |
| 4 | associated_user_id | [ ] | integer | 0 |  |
| 4 | associated_user_id | [ ] | integer | 0 |  |
| 4 | associated_user_id | [ ] | integer | 0 |  |
| 5 | counter_id | [ ] | integer | 0 |  |
| 5 | counter_id | [ ] | integer | 0 |  |
| 5 | counter_id | [ ] | integer | 0 |  |
| 6 | valid_from | [ ] | date | 0 |  |
| 6 | valid_from | [ ] | date | 0 |  |
| 6 | valid_from | [ ] | date | 0 |  |
| 7 | valid_till | [ ] | date | 0 |  |
| 7 | valid_till | [ ] | date | 0 |  |
| 7 | valid_till | [ ] | date | 0 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [associated_user_id](../account/users.md) | cashiers_associated_user_id_fkey | account.users.user_id |
| 4 | [associated_user_id](../account/users.md) | cashiers_associated_user_id_fkey | account.users.user_id |
| 4 | [associated_user_id](../account/users.md) | cashiers_associated_user_id_fkey | account.users.user_id |
| 5 | [counter_id](../inventory/counters.md) | cashiers_counter_id_fkey | inventory.counters.counter_id |
| 5 | [counter_id](../inventory/counters.md) | cashiers_counter_id_fkey | inventory.counters.counter_id |
| 5 | [counter_id](../inventory/counters.md) | cashiers_counter_id_fkey | inventory.counters.counter_id |
| 8 | [audit_user_id](../account/users.md) | cashiers_audit_user_id_fkey | account.users.user_id |
| 8 | [audit_user_id](../account/users.md) | cashiers_audit_user_id_fkey | account.users.user_id |
| 8 | [audit_user_id](../account/users.md) | cashiers_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| cashiers_pkey | frapid_db_user | btree | cashier_id |  |
| cashiers_cashier_code_uix | frapid_db_user | btree | upper(cashier_code::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| cashiers_check CHECK (valid_till >= valid_from) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | cashier_id | nextval('sales.cashiers_cashier_id_seq'::regclass) |
| 1 | cashier_id | nextval('cinesys.cashiers_cashier_id_seq'::regclass) |
| 1 | cashier_id | nextval('foodcourt.cashiers_cashier_id_seq'::regclass) |
| 9 | audit_ts | now() |
| 9 | audit_ts | now() |
| 9 | audit_ts | now() |
| 10 | deleted | false |
| 10 | deleted | false |
| 10 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
