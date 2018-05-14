# hrm.employment_statuses table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | employment_status_id | [ ] | integer | 0 |  |
| 2 | employment_status_code | [ ] | character varying | 12 |  |
| 3 | employment_status_name | [ ] | character varying | 100 |  |
| 4 | is_contract | [ ] | boolean | 0 |  |
| 5 | default_employment_status_code_id | [ ] | integer | 0 |  |
| 6 | description | [x] | text | 0 |  |
| 7 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 5 | [default_employment_status_code_id](../hrm/employment_status_codes.md) | employment_statuses_default_employment_status_code_id_fkey | hrm.employment_status_codes.employment_status_code_id |
| 7 | [audit_user_id](../account/users.md) | employment_statuses_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| employment_statuses_pkey | frapid_db_user | btree | employment_status_id |  |
| employment_statuses_employment_status_code_key | frapid_db_user | btree | employment_status_code |  |
| employment_statuses_employment_status_code_uix | frapid_db_user | btree | upper(employment_status_code::text) |  |
| employment_statuses_employment_status_name_uix | frapid_db_user | btree | upper(employment_status_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | employment_status_id | nextval('hrm.employment_statuses_employment_status_id_seq'::regclass) |
| 4 | is_contract | false |
| 6 | description | ''::text |
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
