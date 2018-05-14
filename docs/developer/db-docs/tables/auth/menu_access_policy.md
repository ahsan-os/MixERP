# auth.menu_access_policy table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | menu_access_policy_id | [ ] | bigint | 0 |  |
| 2 | office_id | [ ] | integer | 0 |  |
| 3 | menu_id | [ ] | integer | 0 |  |
| 4 | user_id | [x] | integer | 0 |  |
| 5 | allow_access | [x] | boolean | 0 |  |
| 6 | disallow_access | [x] | boolean | 0 |  |
| 7 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [office_id](../core/offices.md) | menu_access_policy_office_id_fkey | core.offices.office_id |
| 3 | [menu_id](../core/menus.md) | menu_access_policy_menu_id_fkey | core.menus.menu_id |
| 4 | [user_id](../account/users.md) | menu_access_policy_user_id_fkey | account.users.user_id |
| 7 | [audit_user_id](../account/users.md) | menu_access_policy_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| menu_access_policy_pkey | frapid_db_user | btree | menu_access_policy_id |  |
| menu_access_policy_uix | frapid_db_user | btree | office_id, menu_id, user_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| menu_access_policy_check CHECK (NOT (allow_access IS TRUE AND disallow_access IS TRUE)) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | menu_access_policy_id | nextval('auth.menu_access_policy_menu_access_policy_id_seq'::regclass) |
| 8 | audit_ts | now() |
| 9 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
