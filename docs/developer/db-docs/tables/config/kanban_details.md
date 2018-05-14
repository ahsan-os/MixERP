# config.kanban_details table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | kanban_detail_id | [ ] | bigint | 0 |  |
| 2 | kanban_id | [ ] | bigint | 0 |  |
| 3 | rating | [x] | smallint | 0 |  |
| 4 | resource_id | [ ] | character varying | 128 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [kanban_id](../config/kanbans.md) | kanban_details_kanban_id_fkey | config.kanbans.kanban_id |
| 5 | [audit_user_id](../account/users.md) | kanban_details_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| kanban_details_pkey | frapid_db_user | btree | kanban_detail_id |  |
| kanban_details_kanban_id_resource_id_uix | frapid_db_user | btree | kanban_id, resource_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| kanban_details_rating_check CHECK (rating >= 0 AND rating <= 5) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | kanban_detail_id | nextval('config.kanban_details_kanban_detail_id_seq'::regclass) |
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
