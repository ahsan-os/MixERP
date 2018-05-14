# hrm.nationalities table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | nationality_id | [ ] | integer | 0 |  |
| 2 | nationality_code | [x] | character varying | 12 |  |
| 3 | nationality_name | [ ] | character varying | 50 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [audit_user_id](../account/users.md) | nationalities_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| nationalities_pkey | frapid_db_user | btree | nationality_id |  |
| nationalities_nationality_code_uix | frapid_db_user | btree | upper(nationality_code::text) |  |
| nationalities_nationality_name_uix | frapid_db_user | btree | upper(nationality_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | nationality_id | nextval('hrm.nationalities_nationality_id_seq'::regclass) |
| 5 | audit_ts | now() |
| 6 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
