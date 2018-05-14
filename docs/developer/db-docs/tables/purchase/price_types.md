# purchase.price_types table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | price_type_id | [ ] | integer | 0 |  |
| 1 | price_type_id | [ ] | integer | 0 |  |
| 2 | price_type_code | [ ] | character varying | 24 |  |
| 2 | price_type_code | [ ] | character varying | 24 |  |
| 3 | price_type_name | [ ] | character varying | 500 |  |
| 3 | price_type_name | [ ] | character varying | 500 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [audit_user_id](../account/users.md) | price_types_audit_user_id_fkey | account.users.user_id |
| 4 | [audit_user_id](../account/users.md) | price_types_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| price_types_pkey | frapid_db_user | btree | price_type_id |  |
| price_types_price_type_code_uix | frapid_db_user | btree | upper(price_type_code::text) |  |
| price_types_price_type_name_uix | frapid_db_user | btree | upper(price_type_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | price_type_id | nextval('purchase.price_types_price_type_id_seq'::regclass) |
| 1 | price_type_id | nextval('sales.price_types_price_type_id_seq'::regclass) |
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
