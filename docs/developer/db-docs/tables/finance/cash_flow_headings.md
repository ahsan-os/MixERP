# finance.cash_flow_headings table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | cash_flow_heading_id | [ ] | integer | 0 |  |
| 2 | cash_flow_heading_code | [ ] | character varying | 12 |  |
| 3 | cash_flow_heading_name | [ ] | character varying | 100 |  |
| 4 | cash_flow_heading_type | [ ] | character | 1 |  |
| 5 | is_debit | [ ] | boolean | 0 |  |
| 6 | is_sales | [ ] | boolean | 0 |  |
| 7 | is_purchase | [ ] | boolean | 0 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 8 | [audit_user_id](../account/users.md) | cash_flow_headings_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| cash_flow_headings_pkey | frapid_db_user | btree | cash_flow_heading_id |  |
| cash_flow_headings_cash_flow_heading_code_uix | frapid_db_user | btree | upper(cash_flow_heading_code::text) |  |
| cash_flow_headings_cash_flow_heading_name_uix | frapid_db_user | btree | upper(cash_flow_heading_code::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| cash_flow_heading_cash_flow_heading_type_chk CHECK (cash_flow_heading_type = ANY (ARRAY['O'::bpchar, 'I'::bpchar, 'F'::bpchar])) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 5 | is_debit | false |
| 6 | is_sales | false |
| 7 | is_purchase | false |
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
