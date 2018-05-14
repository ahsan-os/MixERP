# inventory.supplier_types table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | supplier_type_id | [ ] | integer | 0 |  |
| 2 | supplier_type_code | [ ] | character varying | 24 |  |
| 3 | supplier_type_name | [ ] | character varying | 500 |  |
| 4 | account_id | [ ] | integer | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [account_id](../finance/accounts.md) | supplier_types_account_id_fkey | finance.accounts.account_id |
| 5 | [audit_user_id](../account/users.md) | supplier_types_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| supplier_types_pkey | frapid_db_user | btree | supplier_type_id |  |
| supplier_types_supplier_type_code_uix | frapid_db_user | btree | upper(supplier_type_code::text) |  |
| supplier_types_supplier_type_name_uix | frapid_db_user | btree | upper(supplier_type_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | supplier_type_id | nextval('inventory.supplier_types_supplier_type_id_seq'::regclass) |
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
