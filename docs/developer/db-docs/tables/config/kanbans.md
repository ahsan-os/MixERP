# config.kanbans table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | kanban_id | [ ] | bigint | 0 |  |
| 1 | kanban_id | [ ] | integer | 0 |  |
| 2 | kanban_code | [ ] | character varying | 12 |  |
| 2 | object_name | [ ] | character varying | 128 |  |
| 3 | kanban_name | [ ] | character varying | 100 |  |
| 3 | user_id | [x] | integer | 0 |  |
| 4 | kanban_name | [ ] | character varying | 128 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | description | [x] | text | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [ ] | boolean | 0 |  |
| 6 | audit_user_id | [x] | integer | 0 |  |
| 7 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 8 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 3 | [user_id](../account/users.md) | kanbans_user_id_fkey | account.users.user_id |
| 4 | [audit_user_id](../account/users.md) | kanbans_audit_user_id_fkey | account.users.user_id |
| 6 | [audit_user_id](../account/users.md) | kanbans_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| kanbans_pkey | frapid_db_user | btree | kanban_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | kanban_id | nextval('config.kanbans_kanban_id_seq'::regclass) |
| 1 | kanban_id | nextval('production.kanbans_kanban_id_seq'::regclass) |
| 5 | audit_ts | now() |
| 6 | deleted | false |
| 7 | audit_ts | now() |
| 8 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
