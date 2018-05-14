# social.shared_with table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | feed_id | [ ] | bigint | 0 |  |
| 2 | user_id | [x] | integer | 0 |  |
| 3 | role_id | [x] | integer | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 1 | [feed_id](../social/feeds.md) | shared_with_feed_id_fkey | social.feeds.feed_id |
| 2 | [user_id](../account/users.md) | shared_with_user_id_fkey | account.users.user_id |
| 3 | [role_id](../account/roles.md) | shared_with_role_id_fkey | account.roles.role_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| shared_with_user_id_uix | frapid_db_user | btree | feed_id, user_id |  |
| shared_with_role_id_uix | frapid_db_user | btree | feed_id, role_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| shared_with_check CHECK (user_id IS NOT NULL AND role_id IS NULL OR user_id IS NULL AND role_id IS NOT NULL) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
