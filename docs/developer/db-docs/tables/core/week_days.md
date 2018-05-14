# core.week_days table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | week_day_id | [ ] | integer | 0 |  |
| 1 | week_day_id | [ ] | smallint | 0 |  |
| 1 | week_day_id | [ ] | integer | 0 |  |
| 2 | week_day_code | [ ] | character varying | 12 |  |
| 2 | week_day_code | [ ] | character varying | 12 |  |
| 2 | week_day_name | [ ] | character varying | 12 |  |
| 3 | week_day_name | [ ] | character varying | 50 |  |
| 3 | week_day_name | [ ] | character varying | 50 |  |
| 3 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | deleted | [x] | boolean | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| week_days_pkey | frapid_db_user | btree | week_day_id |  |
| week_days_week_day_code_key | frapid_db_user | btree | week_day_code |  |
| week_days_week_day_name_key | frapid_db_user | btree | week_day_name |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| week_days_week_day_id_check CHECK (week_day_id >= 1 AND week_day_id <= 7) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 4 | audit_ts | now() |
| 5 | deleted | false |
| 5 | audit_ts | now() |
| 5 | audit_ts | now() |
| 6 | deleted | false |
| 6 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
