# social.liked_by table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | feed_id | [ ] | bigint | 0 |  |
| 2 | liked_by | [ ] | integer | 0 |  |
| 3 | liked_on | [ ] | timestamp with time zone | 0 |  |
| 4 | unliked | [ ] | boolean | 0 |  |
| 5 | unliked_on | [x] | timestamp with time zone | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 1 | [feed_id](../social/feeds.md) | liked_by_feed_id_fkey | social.feeds.feed_id |
| 2 | [liked_by](../account/users.md) | liked_by_liked_by_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| liked_by_uix | frapid_db_user | btree | feed_id, liked_by |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 3 | liked_on | now() |
| 4 | unliked | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
