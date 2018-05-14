# social.feeds table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | feed_id | [ ] | bigint | 0 |  |
| 2 | event_timestamp | [ ] | timestamp with time zone | 0 |  |
| 3 | formatted_text | [ ] | character varying | 4000 |  |
| 4 | created_by | [ ] | integer | 0 |  |
| 5 | attachments | [x] | text | 0 |  |
| 6 | scope | [x] | character varying | 100 |  |
| 7 | is_public | [ ] | boolean | 0 |  |
| 8 | parent_feed_id | [x] | bigint | 0 |  |
| 9 | audit_ts | [ ] | timestamp with time zone | 0 |  |
| 10 | deleted | [ ] | boolean | 0 |  |
| 11 | deleted_on | [x] | timestamp with time zone | 0 |  |
| 12 | deleted_by | [x] | integer | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [created_by](../account/users.md) | feeds_created_by_fkey | account.users.user_id |
| 8 | [parent_feed_id](../social/feeds.md) | feeds_parent_feed_id_fkey | social.feeds.feed_id |
| 12 | [deleted_by](../account/users.md) | feeds_deleted_by_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| feeds_pkey | frapid_db_user | btree | feed_id |  |
| feeds_scope_inx | frapid_db_user | btree | lower(scope::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | feed_id | nextval('social.feeds_feed_id_seq'::regclass) |
| 2 | event_timestamp | now() |
| 7 | is_public | true |
| 9 | audit_ts | now() |
| 10 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |
| social.update_audit_timestamp_trigger | [social.update_audit_timestamp_trigger](../../functions/social/update_audit_timestamp_trigger-4464330.md) | INSERT | AFTER |  | 0 | ROW |  |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
