# core.menus table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | menu_id | [ ] | integer | 0 |  |
| 1 | menu_id | [ ] | integer | 0 |  |
| 1 | menu_id | [ ] | integer | 0 |  |
| 2 | menu_name | [x] | character varying | 100 |  |
| 2 | menu_code | [ ] | character varying | 24 |  |
| 2 | sort | [x] | integer | 0 |  |
| 3 | menu_name | [ ] | character varying | 500 |  |
| 3 | description | [x] | text | 0 |  |
| 3 | i18n_key | [ ] | character varying | 200 |  |
| 4 | app_name | [ ] | character varying | 100 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | menu_name | [ ] | character varying | 100 |  |
| 6 | deleted | [x] | boolean | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |
| 6 | url | [x] | text | 0 |  |
| 7 | icon | [x] | character varying | 100 |  |
| 8 | parent_menu_id | [x] | integer | 0 |  |
| 9 | audit_user_id | [x] | integer | 0 |  |
| 10 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 11 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [app_name](../core/apps.md) | menus_app_name_fkey | core.apps.app_name |
| 8 | [parent_menu_id](../core/menus.md) | menus_parent_menu_id_fkey | core.menus.menu_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| menus_pkey | frapid_db_user | btree | menu_id |  |
| menus_app_name_menu_name_uix | frapid_db_user | btree | upper(app_name::text), upper(menu_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | menu_id | nextval('website.menus_menu_id_seq'::regclass) |
| 1 | menu_id | nextval('cafesys.menus_menu_id_seq'::regclass) |
| 1 | menu_id | nextval('core.menus_menu_id_seq'::regclass) |
| 5 | audit_ts | now() |
| 5 | audit_ts | now() |
| 6 | deleted | false |
| 6 | deleted | false |
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
