# hrm.leave_benefits table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | leave_benefit_id | [ ] | integer | 0 |  |
| 2 | leave_benefit_code | [ ] | character varying | 12 |  |
| 3 | leave_benefit_name | [ ] | character varying | 128 |  |
| 4 | total_days | [ ] | integer_strict | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 5 | [audit_user_id](../account/users.md) | leave_benefits_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| leave_benefits_pkey | frapid_db_user | btree | leave_benefit_id |  |
| leave_benefits_leave_benefit_code_key | frapid_db_user | btree | leave_benefit_code |  |
| leave_benefits_leave_benefit_code_uix | frapid_db_user | btree | upper(leave_benefit_code::text) |  |
| leave_benefits_leave_benefit_name_uix | frapid_db_user | btree | upper(leave_benefit_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | leave_benefit_id | nextval('hrm.leave_benefits_leave_benefit_id_seq'::regclass) |
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
