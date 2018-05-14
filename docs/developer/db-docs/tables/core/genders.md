# core.genders table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | gender_code | [ ] | character varying | 4 |  |
| 2 | gender_name | [ ] | character varying | 50 |  |
| 3 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| genders_pkey | frapid_db_user | btree | gender_code |  |
| genders_gender_name_key | frapid_db_user | btree | gender_name |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 4 | audit_ts | now() |
| 5 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
