# account.applications table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | application_id | [ ] | uuid | 0 |  |
| 2 | application_name | [ ] | character varying | 100 |  |
| 3 | display_name | [x] | character varying | 100 |  |
| 4 | version_number | [x] | character varying | 100 |  |
| 5 | publisher | [ ] | character varying | 100 |  |
| 6 | published_on | [x] | date | 0 |  |
| 7 | application_url | [x] | character varying | 500 |  |
| 8 | description | [x] | text | 0 |  |
| 9 | browser_based_app | [ ] | boolean | 0 |  |
| 10 | privacy_policy_url | [x] | character varying | 500 |  |
| 11 | terms_of_service_url | [x] | character varying | 500 |  |
| 12 | support_email | [x] | character varying | 100 |  |
| 13 | culture | [x] | character varying | 12 |  |
| 14 | redirect_url | [x] | character varying | 500 |  |
| 15 | app_secret | [x] | text | 0 |  |
| 16 | audit_user_id | [x] | integer | 0 |  |
| 17 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 18 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 16 | [audit_user_id](../account/users.md) | applications_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| applications_pkey | frapid_db_user | btree | application_id |  |
| applications_app_secret_key | frapid_db_user | btree | app_secret |  |
| applications_app_name_uix | frapid_db_user | btree | lower(application_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | application_id | gen_random_uuid() |
| 17 | audit_ts | now() |
| 18 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
