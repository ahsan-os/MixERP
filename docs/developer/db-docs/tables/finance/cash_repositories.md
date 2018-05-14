# finance.cash_repositories table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | cash_repository_id | [ ] | integer | 0 |  |
| 2 | office_id | [ ] | integer | 0 |  |
| 3 | cash_repository_code | [ ] | character varying | 12 |  |
| 4 | cash_repository_name | [ ] | character varying | 50 |  |
| 5 | parent_cash_repository_id | [x] | integer | 0 |  |
| 6 | description | [x] | character varying | 100 |  |
| 7 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [office_id](../core/offices.md) | cash_repositories_office_id_fkey | core.offices.office_id |
| 5 | [parent_cash_repository_id](../finance/cash_repositories.md) | cash_repositories_parent_cash_repository_id_fkey | finance.cash_repositories.cash_repository_id |
| 7 | [audit_user_id](../account/users.md) | cash_repositories_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| cash_repositories_pkey | frapid_db_user | btree | cash_repository_id |  |
| cash_repositories_cash_repository_code_uix | frapid_db_user | btree | office_id, upper(cash_repository_code::text) |  |
| cash_repositories_cash_repository_name_uix | frapid_db_user | btree | office_id, upper(cash_repository_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | cash_repository_id | nextval('finance.cash_repositories_cash_repository_id_seq'::regclass) |
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
