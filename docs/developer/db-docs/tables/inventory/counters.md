# inventory.counters table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | counter_id | [ ] | integer | 0 |  |
| 1 | counter_id | [ ] | integer | 0 |  |
| 1 | counter_id | [ ] | integer | 0 |  |
| 2 | counter_code | [ ] | character varying | 12 |  |
| 2 | counter_code | [ ] | character varying | 12 |  |
| 2 | counter_code | [ ] | character varying | 12 |  |
| 3 | counter_name | [ ] | character varying | 100 |  |
| 3 | counter_name | [ ] | character varying | 100 |  |
| 3 | counter_name | [ ] | character varying | 100 |  |
| 4 | associated_screen_id | [ ] | integer | 0 |  |
| 4 | pos_area_id | [ ] | integer | 0 |  |
| 4 | store_id | [ ] | integer | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [store_id](../inventory/stores.md) | counters_store_id_fkey | inventory.stores.store_id |
| 5 | [audit_user_id](../account/users.md) | counters_audit_user_id_fkey | account.users.user_id |
| 5 | [audit_user_id](../account/users.md) | counters_audit_user_id_fkey | account.users.user_id |
| 5 | [audit_user_id](../account/users.md) | counters_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| counters_pkey | frapid_db_user | btree | counter_id |  |
| counters_counter_code_uix | frapid_db_user | btree | upper(counter_code::text) |  |
| counters_counter_name_uix | frapid_db_user | btree | upper(counter_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | counter_id | nextval('foodcourt.counters_counter_id_seq'::regclass) |
| 1 | counter_id | nextval('cinesys.counters_counter_id_seq'::regclass) |
| 1 | counter_id | nextval('inventory.counters_counter_id_seq'::regclass) |
| 6 | audit_ts | now() |
| 6 | audit_ts | now() |
| 6 | audit_ts | now() |
| 7 | deleted | false |
| 7 | deleted | false |
| 7 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
