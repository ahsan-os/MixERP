# finance.frequency_setups table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | frequency_setup_id | [ ] | integer | 0 |  |
| 2 | fiscal_year_code | [ ] | character varying | 12 |  |
| 3 | frequency_setup_code | [ ] | character varying | 12 |  |
| 4 | value_date | [ ] | date | 0 |  |
| 5 | frequency_id | [ ] | integer | 0 |  |
| 6 | office_id | [ ] | integer | 0 |  |
| 7 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [fiscal_year_code](../finance/fiscal_year.md) | frequency_setups_fiscal_year_code_fkey | finance.fiscal_year.fiscal_year_code |
| 5 | [frequency_id](../finance/frequencies.md) | frequency_setups_frequency_id_fkey | finance.frequencies.frequency_id |
| 6 | [office_id](../core/offices.md) | frequency_setups_office_id_fkey | core.offices.office_id |
| 7 | [audit_user_id](../account/users.md) | frequency_setups_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| frequency_setups_pkey | frapid_db_user | btree | frequency_setup_id |  |
| frequency_setups_value_date_key | frapid_db_user | btree | value_date |  |
| frequency_setups_frequency_setup_code_uix | frapid_db_user | btree | upper(frequency_setup_code::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | frequency_setup_id | nextval('finance.frequency_setups_frequency_setup_id_seq'::regclass) |
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
