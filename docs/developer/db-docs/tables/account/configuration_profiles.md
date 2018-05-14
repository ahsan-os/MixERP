# account.configuration_profiles table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | configuration_profile_id | [ ] | integer | 0 |  |
| 2 | profile_name | [ ] | character varying | 100 |  |
| 3 | is_active | [ ] | boolean | 0 |  |
| 4 | allow_registration | [ ] | boolean | 0 |  |
| 5 | registration_office_id | [ ] | integer | 0 |  |
| 6 | registration_role_id | [ ] | integer | 0 |  |
| 7 | allow_facebook_registration | [ ] | boolean | 0 |  |
| 8 | allow_google_registration | [ ] | boolean | 0 |  |
| 9 | google_signin_client_id | [x] | text | 0 |  |
| 10 | google_signin_scope | [x] | text | 0 |  |
| 11 | facebook_app_id | [x] | text | 0 |  |
| 12 | facebook_scope | [x] | text | 0 |  |
| 13 | audit_user_id | [x] | integer | 0 |  |
| 14 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 15 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 5 | [registration_office_id](../core/offices.md) | configuration_profiles_registration_office_id_fkey | core.offices.office_id |
| 6 | [registration_role_id](../account/roles.md) | configuration_profiles_registration_role_id_fkey | account.roles.role_id |
| 13 | [audit_user_id](../account/users.md) | configuration_profiles_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| configuration_profiles_pkey | frapid_db_user | btree | configuration_profile_id |  |
| configuration_profiles_profile_name_key | frapid_db_user | btree | profile_name |  |
| configuration_profile_uix | frapid_db_user | btree | is_active |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | configuration_profile_id | nextval('account.configuration_profiles_configuration_profile_id_seq'::regclass) |
| 3 | is_active | true |
| 4 | allow_registration | true |
| 7 | allow_facebook_registration | true |
| 8 | allow_google_registration | true |
| 14 | audit_ts | now() |
| 15 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
