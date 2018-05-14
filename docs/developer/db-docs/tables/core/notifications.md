# core.notifications table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | notification_id | [ ] | uuid | 0 |  |
| 2 | event_timestamp | [ ] | timestamp with time zone | 0 |  |
| 3 | tenant | [x] | character varying | 1000 |  |
| 4 | office_id | [x] | integer | 0 |  |
| 5 | associated_app | [ ] | character varying | 100 |  |
| 6 | associated_menu_id | [x] | integer | 0 |  |
| 7 | to_user_id | [x] | integer | 0 |  |
| 8 | to_role_id | [x] | integer | 0 |  |
| 9 | to_login_id | [x] | bigint | 0 |  |
| 10 | url | [x] | character varying | 2000 |  |
| 11 | formatted_text | [x] | character varying | 4000 |  |
| 12 | icon | [x] | character varying | 100 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [office_id](../core/offices.md) | notifications_office_id_fkey | core.offices.office_id |
| 5 | [associated_app](../core/apps.md) | notifications_associated_app_fkey | core.apps.app_name |
| 6 | [associated_menu_id](../core/menus.md) | notifications_associated_menu_id_fkey | core.menus.menu_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| notifications_pkey | frapid_db_user | btree | notification_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | notification_id | gen_random_uuid() |
| 2 | event_timestamp | now() |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
