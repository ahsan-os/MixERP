# config.smtp_configs table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | smtp_config_id | [ ] | integer | 0 |  |
| 2 | configuration_name | [ ] | character varying | 256 |  |
| 3 | enabled | [ ] | boolean | 0 |  |
| 4 | is_default | [ ] | boolean | 0 |  |
| 5 | from_display_name | [ ] | character varying | 256 |  |
| 6 | from_email_address | [ ] | character varying | 256 |  |
| 7 | smtp_host | [ ] | character varying | 256 |  |
| 8 | smtp_enable_ssl | [ ] | boolean | 0 |  |
| 9 | smtp_username | [ ] | character varying | 256 |  |
| 10 | smtp_password | [ ] | character varying | 256 |  |
| 11 | smtp_port | [ ] | integer | 0 |  |
| 12 | audit_user_id | [x] | integer | 0 |  |
| 13 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 14 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 12 | [audit_user_id](../account/users.md) | smtp_configs_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| smtp_configs_pkey | frapid_db_user | btree | smtp_config_id |  |
| smtp_configs_configuration_name_key | frapid_db_user | btree | configuration_name |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | smtp_config_id | nextval('config.smtp_configs_smtp_config_id_seq'::regclass) |
| 3 | enabled | false |
| 4 | is_default | false |
| 8 | smtp_enable_ssl | true |
| 11 | smtp_port | 587 |
| 13 | audit_ts | now() |
| 14 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
