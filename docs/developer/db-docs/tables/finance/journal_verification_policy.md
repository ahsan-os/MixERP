# finance.journal_verification_policy table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | journal_verification_policy_id | [ ] | integer | 0 |  |
| 2 | user_id | [ ] | integer | 0 |  |
| 3 | office_id | [ ] | integer | 0 |  |
| 4 | can_verify | [ ] | boolean | 0 |  |
| 5 | verification_limit | [ ] | money_strict2 | 0 |  |
| 6 | can_self_verify | [ ] | boolean | 0 |  |
| 7 | self_verification_limit | [ ] | money_strict2 | 0 |  |
| 8 | effective_from | [ ] | date | 0 |  |
| 9 | ends_on | [ ] | date | 0 |  |
| 10 | is_active | [ ] | boolean | 0 |  |
| 11 | audit_user_id | [x] | integer | 0 |  |
| 12 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 13 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [user_id](../account/users.md) | journal_verification_policy_user_id_fkey | account.users.user_id |
| 3 | [office_id](../core/offices.md) | journal_verification_policy_office_id_fkey | core.offices.office_id |
| 11 | [audit_user_id](../account/users.md) | journal_verification_policy_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| journal_verification_policy_pkey | frapid_db_user | btree | journal_verification_policy_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | journal_verification_policy_id | nextval('finance.journal_verification_policy_journal_verification_policy_id_seq'::regclass) |
| 4 | can_verify | false |
| 5 | verification_limit | 0 |
| 6 | can_self_verify | false |
| 7 | self_verification_limit | 0 |
| 12 | audit_ts | now() |
| 13 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
