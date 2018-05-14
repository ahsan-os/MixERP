# website.configurations table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | configuration_id | [ ] | integer | 0 |  |
| 2 | domain_name | [ ] | character varying | 500 |  |
| 3 | website_name | [ ] | character varying | 500 |  |
| 4 | description | [x] | text | 0 |  |
| 5 | blog_title | [x] | character varying | 500 |  |
| 6 | blog_description | [x] | text | 0 |  |
| 7 | is_default | [ ] | boolean | 0 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 8 | [audit_user_id](../account/users.md) | configurations_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| configurations_pkey | frapid_db_user | btree | configuration_id |  |
| configuration_domain_name_uix | frapid_db_user | btree | lower(domain_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | configuration_id | nextval('website.configurations_configuration_id_seq'::regclass) |
| 7 | is_default | true |
| 9 | audit_ts | now() |
| 10 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
