# finance.day_operation_routines table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | day_operation_routine_id | [ ] | bigint | 0 |  |
| 2 | day_id | [ ] | bigint | 0 |  |
| 3 | routine_id | [ ] | integer | 0 |  |
| 4 | started_on | [ ] | timestamp with time zone | 0 |  |
| 5 | completed_on | [x] | timestamp with time zone | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [day_id](../finance/day_operation.md) | day_operation_routines_day_id_fkey | finance.day_operation.day_id |
| 3 | [routine_id](../finance/routines.md) | day_operation_routines_routine_id_fkey | finance.routines.routine_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| day_operation_routines_pkey | frapid_db_user | btree | day_operation_routine_id |  |
| day_operation_routines_started_on_inx | frapid_db_user | btree | started_on |  |
| day_operation_routines_completed_on_inx | frapid_db_user | btree | completed_on |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | day_operation_routine_id | nextval('finance.day_operation_routines_day_operation_routine_id_seq'::regclass) |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
