# sales.cashier_login_info table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | cashier_login_info_id | [ ] | uuid | 0 |  |
| 1 | cashier_login_info_id | [ ] | uuid | 0 |  |
| 1 | cashier_login_info_id | [ ] | uuid | 0 |  |
| 2 | counter_id | [x] | integer | 0 |  |
| 2 | counter_id | [x] | integer | 0 |  |
| 2 | counter_id | [x] | integer | 0 |  |
| 3 | cashier_id | [x] | integer | 0 |  |
| 3 | cashier_id | [x] | integer | 0 |  |
| 3 | cashier_id | [x] | integer | 0 |  |
| 4 | login_date | [ ] | timestamp with time zone | 0 |  |
| 4 | login_date | [ ] | timestamp with time zone | 0 |  |
| 4 | login_date | [ ] | timestamp with time zone | 0 |  |
| 5 | success | [ ] | boolean | 0 |  |
| 5 | success | [ ] | boolean | 0 |  |
| 5 | success | [ ] | boolean | 0 |  |
| 6 | attempted_by | [ ] | integer | 0 |  |
| 6 | attempted_by | [ ] | integer | 0 |  |
| 6 | attempted_by | [ ] | integer | 0 |  |
| 7 | browser | [x] | text | 0 |  |
| 7 | browser | [x] | character varying | 1000 |  |
| 7 | browser | [x] | text | 0 |  |
| 8 | ip_address | [x] | text | 0 |  |
| 8 | ip_address | [x] | text | 0 |  |
| 8 | ip_address | [x] | character varying | 1000 |  |
| 9 | user_agent | [x] | text | 0 |  |
| 9 | user_agent | [x] | character varying | 1000 |  |
| 9 | user_agent | [x] | text | 0 |  |
| 10 | audit_user_id | [x] | integer | 0 |  |
| 10 | audit_user_id | [x] | integer | 0 |  |
| 10 | audit_user_id | [x] | integer | 0 |  |
| 11 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 11 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 11 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 12 | deleted | [x] | boolean | 0 |  |
| 12 | deleted | [x] | boolean | 0 |  |
| 12 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [counter_id](../inventory/counters.md) | cashier_login_info_counter_id_fkey | inventory.counters.counter_id |
| 2 | [counter_id](../inventory/counters.md) | cashier_login_info_counter_id_fkey | inventory.counters.counter_id |
| 2 | [counter_id](../inventory/counters.md) | cashier_login_info_counter_id_fkey | inventory.counters.counter_id |
| 3 | [cashier_id](../sales/cashiers.md) | cashier_login_info_cashier_id_fkey | sales.cashiers.cashier_id |
| 3 | [cashier_id](../sales/cashiers.md) | cashier_login_info_cashier_id_fkey | sales.cashiers.cashier_id |
| 3 | [cashier_id](../sales/cashiers.md) | cashier_login_info_cashier_id_fkey | sales.cashiers.cashier_id |
| 6 | [attempted_by](../account/users.md) | cashier_login_info_attempted_by_fkey | account.users.user_id |
| 6 | [attempted_by](../account/users.md) | cashier_login_info_attempted_by_fkey | account.users.user_id |
| 6 | [attempted_by](../account/users.md) | cashier_login_info_attempted_by_fkey | account.users.user_id |
| 10 | [audit_user_id](../account/users.md) | cashier_login_info_audit_user_id_fkey | account.users.user_id |
| 10 | [audit_user_id](../account/users.md) | cashier_login_info_audit_user_id_fkey | account.users.user_id |
| 10 | [audit_user_id](../account/users.md) | cashier_login_info_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| cashier_login_info_pkey | frapid_db_user | btree | cashier_login_info_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | cashier_login_info_id | gen_random_uuid() |
| 1 | cashier_login_info_id | gen_random_uuid() |
| 1 | cashier_login_info_id | gen_random_uuid() |
| 11 | audit_ts | now() |
| 11 | audit_ts | now() |
| 11 | audit_ts | now() |
| 12 | deleted | false |
| 12 | deleted | false |
| 12 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
