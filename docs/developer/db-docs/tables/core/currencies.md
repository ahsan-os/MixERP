# core.currencies table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | currency_id | [ ] | integer | 0 |  |
| 2 | currency_code | [ ] | character varying | 12 |  |
| 3 | currency_symbol | [ ] | character varying | 12 |  |
| 4 | currency_name | [ ] | character varying | 48 |  |
| 5 | hundredth_name | [ ] | character varying | 48 |  |
| 6 | audit_user_id | [x] | integer | 0 |  |
| 7 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 8 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| currencies_pkey | frapid_db_user | btree | currency_code |  |
| currencies_currency_name_key | frapid_db_user | btree | currency_name |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | currency_id | nextval('core.currencies_currency_id_seq'::regclass) |
| 7 | audit_ts | now() |
| 8 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
