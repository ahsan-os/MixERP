# auth.group_menu_access_policy table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | group_menu_access_policy_id | [ ] | bigint | 0 |  |
| 2 | office_id | [ ] | integer | 0 |  |
| 3 | menu_id | [ ] | integer | 0 |  |
| 4 | role_id | [x] | integer | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [office_id](../core/offices.md) | group_menu_access_policy_office_id_fkey | core.offices.office_id |
| 3 | [menu_id](../core/menus.md) | group_menu_access_policy_menu_id_fkey | core.menus.menu_id |
| 4 | [role_id](../account/roles.md) | group_menu_access_policy_role_id_fkey | account.roles.role_id |
| 5 | [audit_user_id](../account/users.md) | group_menu_access_policy_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| group_menu_access_policy_pkey | frapid_db_user | btree | group_menu_access_policy_id |  |
| menu_access_uix | frapid_db_user | btree | office_id, menu_id, role_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | group_menu_access_policy_id | nextval('auth.group_menu_access_policy_group_menu_access_policy_id_seq'::regclass) |
| 6 | audit_ts | now() |
| 7 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
