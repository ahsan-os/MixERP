# audit.logged_actions table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | event_id | [ ] | bigint | 0 |  |
| 2 | schema_name | [ ] | character varying | 1000 |  |
| 3 | table_name | [ ] | character varying | 1000 |  |
| 4 | relid | [ ] | character varying | 1000 |  |
| 5 | session_user_name | [x] | character varying | 1000 |  |
| 6 | action_tstamp_tx | [ ] | timestamp with time zone | 0 |  |
| 7 | action_tstamp_stm | [ ] | timestamp with time zone | 0 |  |
| 8 | action_tstamp_clk | [ ] | timestamp with time zone | 0 |  |
| 9 | transaction_id | [x] | bigint | 0 |  |
| 10 | application_name | [x] | character varying | 1000 |  |
| 11 | client_addr | [x] | character varying | 1000 |  |
| 12 | client_port | [x] | integer | 0 |  |
| 13 | client_query | [ ] | text | 0 |  |
| 14 | action | [ ] | character | 1 |  |
| 15 | row_data | [x] | text | 0 |  |
| 16 | changed_fields | [x] | text | 0 |  |
| 17 | statement_only | [ ] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| logged_actions_pkey | frapid_db_user | btree | event_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| logged_actions_action_check CHECK (action = ANY (ARRAY['I'::bpchar, 'D'::bpchar, 'U'::bpchar, 'T'::bpchar])) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
