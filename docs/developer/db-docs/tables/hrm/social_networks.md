# hrm.social_networks table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | social_network_id | [ ] | integer | 0 |  |
| 2 | social_network_name | [ ] | character varying | 128 |  |
| 3 | icon_css_class | [x] | character varying | 128 |  |
| 4 | base_url | [x] | character varying | 128 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 5 | [audit_user_id](../account/users.md) | social_networks_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| social_networks_pkey | frapid_db_user | btree | social_network_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | social_network_id | nextval('hrm.social_networks_social_network_id_seq'::regclass) |
| 4 | base_url | ''::character varying |
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
