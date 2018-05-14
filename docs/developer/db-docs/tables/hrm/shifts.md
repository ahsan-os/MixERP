# hrm.shifts table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | shift_id | [ ] | integer | 0 |  |
| 1 | shift_id | [ ] | integer | 0 |  |
| 2 | shift_code | [ ] | character varying | 12 |  |
| 2 | shift_code | [ ] | character varying | 12 |  |
| 3 | shift_name | [ ] | character varying | 100 |  |
| 3 | shift_name | [ ] | character varying | 100 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 4 | begins_from | [ ] | time without time zone | 0 |  |
| 5 | ends_on | [ ] | time without time zone | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | description | [x] | text | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |
| 7 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [audit_user_id](../account/users.md) | shifts_audit_user_id_fkey | account.users.user_id |
| 7 | [audit_user_id](../account/users.md) | shifts_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| shifts_pkey | frapid_db_user | btree | shift_id |  |
| shifts_shift_code_key | frapid_db_user | btree | shift_code |  |
| shifts_shift_code_uix | frapid_db_user | btree | upper(shift_code::text) |  |
| shifts_shift_name_uix | frapid_db_user | btree | upper(shift_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | shift_id | nextval('hrm.shifts_shift_id_seq'::regclass) |
| 1 | shift_id | nextval('cinesys.shifts_shift_id_seq'::regclass) |
| 5 | audit_ts | now() |
| 6 | description | ''::text |
| 6 | deleted | false |
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
