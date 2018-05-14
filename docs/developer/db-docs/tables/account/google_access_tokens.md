# account.google_access_tokens table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | user_id | [ ] | integer | 0 |  |
| 2 | token | [x] | text | 0 |  |
| 3 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 1 | [user_id](../account/users.md) | google_access_tokens_user_id_fkey | account.users.user_id |
| 3 | [audit_user_id](../account/users.md) | google_access_tokens_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| google_access_tokens_pkey | frapid_db_user | btree | user_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 4 | audit_ts | now() |
| 5 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
