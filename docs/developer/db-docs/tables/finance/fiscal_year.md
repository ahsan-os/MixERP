# finance.fiscal_year table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | fiscal_year_id | [ ] | integer | 0 |  |
| 2 | fiscal_year_code | [ ] | character varying | 12 |  |
| 3 | fiscal_year_name | [ ] | character varying | 50 |  |
| 4 | starts_from | [ ] | date | 0 |  |
| 5 | ends_on | [ ] | date | 0 |  |
| 6 | eod_required | [ ] | boolean | 0 |  |
| 7 | office_id | [ ] | integer | 0 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 7 | [office_id](../core/offices.md) | fiscal_year_office_id_fkey | core.offices.office_id |
| 8 | [audit_user_id](../account/users.md) | fiscal_year_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| fiscal_year_pkey | frapid_db_user | btree | fiscal_year_code |  |
| fiscal_year_fiscal_year_id_key | frapid_db_user | btree | fiscal_year_id |  |
| fiscal_year_fiscal_year_name_uix | frapid_db_user | btree | upper(fiscal_year_name::text) |  |
| fiscal_year_starts_from_uix | frapid_db_user | btree | starts_from |  |
| fiscal_year_ends_on_uix | frapid_db_user | btree | ends_on |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | fiscal_year_id | nextval('finance.fiscal_year_fiscal_year_id_seq'::regclass) |
| 6 | eod_required | true |
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
