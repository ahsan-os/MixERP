# inventory.suppliers table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | supplier_id | [ ] | integer | 0 |  |
| 2 | supplier_code | [ ] | character varying | 24 |  |
| 3 | supplier_name | [ ] | character varying | 500 |  |
| 4 | supplier_type_id | [ ] | integer | 0 |  |
| 5 | account_id | [x] | integer | 0 |  |
| 6 | email | [x] | character varying | 128 |  |
| 7 | currency_code | [ ] | character varying | 12 |  |
| 8 | company_name | [x] | character varying | 1000 |  |
| 9 | company_address_line_1 | [x] | character varying | 128 |  |
| 10 | company_address_line_2 | [x] | character varying | 128 |  |
| 11 | company_street | [x] | character varying | 1000 |  |
| 12 | company_city | [x] | character varying | 1000 |  |
| 13 | company_state | [x] | character varying | 1000 |  |
| 14 | company_country | [x] | character varying | 1000 |  |
| 15 | company_po_box | [x] | character varying | 1000 |  |
| 16 | company_zipcode | [x] | character varying | 1000 |  |
| 17 | company_phone_numbers | [x] | character varying | 1000 |  |
| 18 | company_fax | [x] | character varying | 100 |  |
| 19 | logo | [x] | photo | 0 |  |
| 20 | contact_first_name | [x] | character varying | 100 |  |
| 21 | contact_middle_name | [x] | character varying | 100 |  |
| 22 | contact_last_name | [x] | character varying | 100 |  |
| 23 | contact_address_line_1 | [x] | character varying | 128 |  |
| 24 | contact_address_line_2 | [x] | character varying | 128 |  |
| 25 | contact_street | [x] | character varying | 100 |  |
| 26 | contact_city | [x] | character varying | 100 |  |
| 27 | contact_state | [x] | character varying | 100 |  |
| 28 | contact_country | [x] | character varying | 100 |  |
| 29 | contact_po_box | [x] | character varying | 100 |  |
| 30 | contact_zipcode | [x] | character varying | 100 |  |
| 31 | contact_phone_numbers | [x] | character varying | 100 |  |
| 32 | contact_fax | [x] | character varying | 100 |  |
| 33 | photo | [x] | photo | 0 |  |
| 34 | audit_user_id | [x] | integer | 0 |  |
| 35 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 36 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [supplier_type_id](../inventory/supplier_types.md) | suppliers_supplier_type_id_fkey | inventory.supplier_types.supplier_type_id |
| 5 | [account_id](../finance/accounts.md) | suppliers_account_id_fkey | finance.accounts.account_id |
| 7 | [currency_code](../core/currencies.md) | suppliers_currency_code_fkey | core.currencies.currency_code |
| 34 | [audit_user_id](../account/users.md) | suppliers_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| suppliers_pkey | frapid_db_user | btree | supplier_id |  |
| suppliers_account_id_key | frapid_db_user | btree | account_id |  |
| suppliers_supplier_code_uix | frapid_db_user | btree | upper(supplier_code::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | supplier_id | nextval('inventory.suppliers_supplier_id_seq'::regclass) |
| 35 | audit_ts | now() |
| 36 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |
| inventory.supplier_after_insert_trigger | [inventory.supplier_after_insert_trigger](../../functions/inventory/supplier_after_insert_trigger-4459688.md) | INSERT | AFTER |  | 0 | ROW |  |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
