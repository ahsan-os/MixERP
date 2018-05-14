# finance.auto_verification_policy table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | auto_verification_policy_id | [ ] | integer | 0 |  |
| 2 | user_id | [ ] | integer | 0 |  |
| 3 | office_id | [ ] | integer | 0 |  |
| 4 | verification_limit | [ ] | money_strict2 | 0 |  |
| 5 | effective_from | [ ] | date | 0 |  |
| 6 | ends_on | [ ] | date | 0 |  |
| 7 | is_active | [ ] | boolean | 0 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [user_id](../account/users.md) | auto_verification_policy_user_id_fkey | account.users.user_id |
| 3 | [office_id](../core/offices.md) | auto_verification_policy_office_id_fkey | core.offices.office_id |
| 8 | [audit_user_id](../account/users.md) | auto_verification_policy_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| auto_verification_policy_pkey | frapid_db_user | btree | auto_verification_policy_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | auto_verification_policy_id | nextval('finance.auto_verification_policy_auto_verification_policy_id_seq'::regclass) |
| 4 | verification_limit | 0 |
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
