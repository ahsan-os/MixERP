# social schema

**Tables**

| # | Table Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [feeds](../tables/social/feeds.md) | frapid_db_user | DEFAULT |  |
| 2 | [liked_by](../tables/social/liked_by.md) | frapid_db_user | DEFAULT |  |
| 3 | [shared_with](../tables/social/shared_with.md) | frapid_db_user | DEFAULT |  |



**Sequences**

| # | Sequence Name | Owner | Data Type | Start Value | Increment | Description |
| --- | --- | --- | --- | --- | --- | --- |
| 1 | feeds_feed_id_seq | frapid_db_user | bigint | 1 | 1 |  |


**Views**

| # | View Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [liked_by_view](../views/social/liked_by_view.md) | frapid_db_user | DEFAULT |  |



**Materialized Views**

| # | Matview Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |



**Functions**

| # | Function | Owner | Description |
| --- | --- | --- | --- |
| 1 | [count_comments(_feed_id bigint)RETURNS bigint](../functions/social/count_comments-4464318.md) | frapid_db_user |  |
| 2 | [get_followers(_feed_id bigint, _me integer)RETURNS text](../functions/social/get_followers-4464319.md) | frapid_db_user |  |
| 3 | [get_next_top_feeds(_user_id integer, _last_feed_id bigint, _parent_feed_id bigint)RETURNS TABLE(row_number bigint, feed_id bigint, event_timestamp timestamp with time zone, audit_ts timestamp with time zone, formatted_text character varying, created_by integer, created_by_name character varying, attachments text, scope character varying, is_public boolean, parent_feed_id bigint, child_count bigint)](../functions/social/get_next_top_feeds-4464320.md) | frapid_db_user |  |
| 4 | [get_root_feed_id(_feed_id bigint)RETURNS bigint](../functions/social/get_root_feed_id-4464327.md) | frapid_db_user |  |
| 5 | [like(_user_id integer, _feed_id bigint)RETURNS void](../functions/social/like-4464328.md) | frapid_db_user |  |
| 6 | [unlike(_user_id integer, _feed_id bigint)RETURNS void](../functions/social/unlike-4464329.md) | frapid_db_user |  |



**Triggers**

| # | Trigger | Owner | Description |
| --- | --- | --- | --- |
| 1 | [update_audit_timestamp_trigger()RETURNS TRIGGER](../functions/social/update_audit_timestamp_trigger-4464330.md) | frapid_db_user |  |



**Types**

| # | Type | Base Type | Owner | Collation | Default | Type | StoreType | NotNull | Description |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../schemas.md)
* [Table of Contents](../../README.md)