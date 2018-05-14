# finance.tax_setups table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | tax_setup_id | [ ] | integer | 0 |  |
| 2 | office_id | [ ] | integer | 0 |  |
| 3 | income_tax_rate | [ ] | decimal_strict | 0 |  |
| 4 | income_tax_account_id | [ ] | integer | 0 |  |
| 5 | sales_tax_rate | [ ] | decimal_strict | 0 |  |
| 6 | sales_tax_account_id | [ ] | integer | 0 |  |
| 7 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [office_id](../core/offices.md) | tax_setups_office_id_fkey | core.offices.office_id |
| 4 | [income_tax_account_id](../finance/accounts.md) | tax_setups_income_tax_account_id_fkey | finance.accounts.account_id |
| 6 | [sales_tax_account_id](../finance/accounts.md) | tax_setups_sales_tax_account_id_fkey | finance.accounts.account_id |
| 7 | [audit_user_id](../account/users.md) | tax_setups_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| tax_setups_pkey | frapid_db_user | btree | tax_setup_id |  |
| tax_setup_office_id_uix | frapid_db_user | btree | office_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | tax_setup_id | nextval('finance.tax_setups_tax_setup_id_seq'::regclass) |
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
