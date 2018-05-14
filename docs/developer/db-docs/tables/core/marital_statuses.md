# core.marital_statuses table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | marital_status_id | [ ] | integer | 0 |  |
| 2 | marital_status_code | [ ] | character varying | 12 |  |
| 3 | marital_status_name | [ ] | character varying | 128 |  |
| 4 | is_legally_recognized_marriage | [ ] | boolean | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| marital_statuses_pkey | frapid_db_user | btree | marital_status_id |  |
| marital_statuses_marital_status_code_key | frapid_db_user | btree | marital_status_code |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | marital_status_id | nextval('core.marital_statuses_marital_status_id_seq'::regclass) |
| 4 | is_legally_recognized_marriage | false |
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
