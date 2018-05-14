# website.menu_items table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | menu_item_id | [ ] | integer | 0 |  |
| 2 | menu_id | [x] | integer | 0 |  |
| 3 | sort | [ ] | integer | 0 |  |
| 4 | title | [ ] | character varying | 100 |  |
| 5 | url | [x] | character varying | 500 |  |
| 6 | target | [x] | character varying | 20 |  |
| 7 | content_id | [x] | integer | 0 |  |
| 8 | parent_menu_item_id | [x] | integer | 0 |  |
| 9 | audit_user_id | [x] | integer | 0 |  |
| 10 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 11 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [menu_id](../website/menus.md) | menu_items_menu_id_fkey | website.menus.menu_id |
| 7 | [content_id](../website/contents.md) | menu_items_content_id_fkey | website.contents.content_id |
| 8 | [parent_menu_item_id](../website/menu_items.md) | menu_items_parent_menu_item_id_fkey | website.menu_items.menu_item_id |
| 9 | [audit_user_id](../account/users.md) | menu_items_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| menu_items_pkey | frapid_db_user | btree | menu_item_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | menu_item_id | nextval('website.menu_items_menu_item_id_seq'::regclass) |
| 3 | sort | 0 |
| 10 | audit_ts | now() |
| 11 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
