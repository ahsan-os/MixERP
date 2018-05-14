# auth.group_entity_access_policy table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | group_entity_access_policy_id | [ ] | integer | 0 |  |
| 2 | entity_name | [x] | character varying | 500 |  |
| 3 | office_id | [ ] | integer | 0 |  |
| 4 | role_id | [ ] | integer | 0 |  |
| 5 | access_type_id | [x] | integer | 0 |  |
| 6 | allow_access | [ ] | boolean | 0 |  |
| 7 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 3 | [office_id](../core/offices.md) | group_entity_access_policy_office_id_fkey | core.offices.office_id |
| 4 | [role_id](../account/roles.md) | group_entity_access_policy_role_id_fkey | account.roles.role_id |
| 5 | [access_type_id](../auth/access_types.md) | group_entity_access_policy_access_type_id_fkey | auth.access_types.access_type_id |
| 7 | [audit_user_id](../account/users.md) | group_entity_access_policy_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| group_entity_access_policy_pkey | frapid_db_user | btree | group_entity_access_policy_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | group_entity_access_policy_id | nextval('auth.group_entity_access_policy_group_entity_access_policy_id_seq'::regclass) |
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
