# inventory.item_types table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | item_type_id | [ ] | integer | 0 |  |
| 2 | item_type_code | [ ] | character varying | 12 |  |
| 3 | item_type_name | [ ] | character varying | 50 |  |
| 4 | is_component | [ ] | boolean | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 5 | [audit_user_id](../account/users.md) | item_types_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| item_types_pkey | frapid_db_user | btree | item_type_id |  |
| item_type_item_type_code_uix | frapid_db_user | btree | upper(item_type_code::text) |  |
| item_type_item_type_name_uix | frapid_db_user | btree | upper(item_type_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | item_type_id | nextval('inventory.item_types_item_type_id_seq'::regclass) |
| 4 | is_component | false |
| 6 | audit_ts | now() |
| 7 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
