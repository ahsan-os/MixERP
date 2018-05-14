# inventory.stores table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | store_id | [ ] | integer | 0 |  |
| 2 | store_code | [ ] | character varying | 24 |  |
| 3 | store_name | [ ] | character varying | 500 |  |
| 4 | store_type_id | [ ] | integer | 0 |  |
| 5 | office_id | [ ] | integer | 0 |  |
| 6 | default_account_id_for_checks | [ ] | integer | 0 |  |
| 7 | default_cash_account_id | [ ] | integer | 0 |  |
| 8 | default_cash_repository_id | [ ] | integer | 0 |  |
| 9 | address_line_1 | [x] | character varying | 128 |  |
| 10 | address_line_2 | [x] | character varying | 128 |  |
| 11 | street | [x] | character varying | 50 |  |
| 12 | city | [x] | character varying | 50 |  |
| 13 | state | [x] | character varying | 50 |  |
| 14 | country | [x] | character varying | 50 |  |
| 15 | phone | [x] | character varying | 50 |  |
| 16 | fax | [x] | character varying | 50 |  |
| 17 | cell | [x] | character varying | 50 |  |
| 18 | allow_sales | [ ] | boolean | 0 |  |
| 19 | audit_user_id | [x] | integer | 0 |  |
| 20 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 21 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [store_type_id](../inventory/store_types.md) | stores_store_type_id_fkey | inventory.store_types.store_type_id |
| 5 | [office_id](../core/offices.md) | stores_office_id_fkey | core.offices.office_id |
| 6 | [default_account_id_for_checks](../finance/accounts.md) | stores_default_account_id_for_checks_fkey | finance.accounts.account_id |
| 7 | [default_cash_account_id](../finance/accounts.md) | stores_default_cash_account_id_fkey | finance.accounts.account_id |
| 8 | [default_cash_repository_id](../finance/cash_repositories.md) | stores_default_cash_repository_id_fkey | finance.cash_repositories.cash_repository_id |
| 19 | [audit_user_id](../account/users.md) | stores_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| stores_pkey | frapid_db_user | btree | store_id |  |
| stores_store_code_uix | frapid_db_user | btree | upper(store_code::text) |  |
| stores_store_name_uix | frapid_db_user | btree | upper(store_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | store_id | nextval('inventory.stores_store_id_seq'::regclass) |
| 18 | allow_sales | true |
| 20 | audit_ts | now() |
| 21 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
