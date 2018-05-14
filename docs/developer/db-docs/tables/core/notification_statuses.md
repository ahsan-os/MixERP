# core.notification_statuses table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | notification_status_id | [ ] | uuid | 0 |  |
| 2 | notification_id | [ ] | uuid | 0 |  |
| 3 | last_seen_on | [ ] | timestamp with time zone | 0 |  |
| 4 | seen_by | [ ] | integer | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [notification_id](../core/notifications.md) | notification_statuses_notification_id_fkey | core.notifications.notification_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| notification_statuses_pkey | frapid_db_user | btree | notification_status_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | notification_status_id | gen_random_uuid() |
| 3 | last_seen_on | now() |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
