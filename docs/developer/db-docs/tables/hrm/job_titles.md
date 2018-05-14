# hrm.job_titles table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | job_title_id | [ ] | integer | 0 |  |
| 2 | job_title_code | [ ] | character varying | 12 |  |
| 3 | job_title_name | [ ] | character varying | 100 |  |
| 4 | description | [x] | text | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 5 | [audit_user_id](../account/users.md) | job_titles_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| job_titles_pkey | frapid_db_user | btree | job_title_id |  |
| job_titles_job_title_code_key | frapid_db_user | btree | job_title_code |  |
| job_titles_job_title_code_uix | frapid_db_user | btree | upper(job_title_code::text) |  |
| job_titles_job_title_name_uix | frapid_db_user | btree | upper(job_title_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | job_title_id | nextval('hrm.job_titles_job_title_id_seq'::regclass) |
| 4 | description | ''::text |
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
