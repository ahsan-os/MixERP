# hrm.office_hours table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | office_hour_id | [ ] | integer | 0 |  |
| 2 | office_id | [ ] | integer | 0 |  |
| 3 | shift_id | [ ] | integer | 0 |  |
| 4 | week_day_id | [ ] | integer | 0 |  |
| 5 | begins_from | [ ] | time without time zone | 0 |  |
| 6 | ends_on | [ ] | time without time zone | 0 |  |
| 7 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [office_id](../core/offices.md) | office_hours_office_id_fkey | core.offices.office_id |
| 3 | [shift_id](../hrm/shifts.md) | office_hours_shift_id_fkey | hrm.shifts.shift_id |
| 4 | [week_day_id](../hrm/week_days.md) | office_hours_week_day_id_fkey | hrm.week_days.week_day_id |
| 7 | [audit_user_id](../account/users.md) | office_hours_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| office_hours_pkey | frapid_db_user | btree | office_hour_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | office_hour_id | nextval('hrm.office_hours_office_hour_id_seq'::regclass) |
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
