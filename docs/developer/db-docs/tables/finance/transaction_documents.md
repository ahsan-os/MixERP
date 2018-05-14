# finance.transaction_documents table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | document_id | [ ] | bigint | 0 |  |
| 2 | transaction_master_id | [ ] | bigint | 0 |  |
| 3 | original_file_name | [ ] | character varying | 500 |  |
| 4 | file_extension | [x] | character varying | 50 |  |
| 5 | file_path | [ ] | character varying | 2000 |  |
| 6 | memo | [x] | character varying | 2000 |  |
| 7 | audit_user_id | [x] | integer | 0 |  |
| 8 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 9 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [transaction_master_id](../finance/transaction_master.md) | transaction_documents_transaction_master_id_fkey | finance.transaction_master.transaction_master_id |
| 7 | [audit_user_id](../account/users.md) | transaction_documents_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| transaction_documents_pkey | frapid_db_user | btree | document_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | document_id | nextval('finance.transaction_documents_document_id_seq'::regclass) |
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
