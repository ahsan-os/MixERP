# finance.day_operation table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | day_id | [ ] | bigint | 0 |  |
| 2 | office_id | [ ] | integer | 0 |  |
| 3 | value_date | [ ] | date | 0 |  |
| 4 | started_on | [ ] | timestamp with time zone | 0 |  |
| 5 | started_by | [ ] | integer | 0 |  |
| 6 | completed_on | [x] | timestamp with time zone | 0 |  |
| 7 | completed_by | [x] | integer | 0 |  |
| 8 | completed | [ ] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [office_id](../core/offices.md) | day_operation_office_id_fkey | core.offices.office_id |
| 5 | [started_by](../account/users.md) | day_operation_started_by_fkey | account.users.user_id |
| 7 | [completed_by](../account/users.md) | day_operation_completed_by_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| day_operation_pkey | frapid_db_user | btree | day_id |  |
| day_operation_value_date_uix | frapid_db_user | btree | value_date |  |
| day_operation_completed_on_inx | frapid_db_user | btree | completed_on |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| day_operation_completed_chk CHECK (completed OR completed_on IS NOT NULL OR NOT completed OR completed_on IS NULL) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | day_id | nextval('finance.day_operation_day_id_seq'::regclass) |
| 8 | completed | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
