# inventory.shippers table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | shipper_id | [ ] | integer | 0 |  |
| 2 | shipper_code | [x] | character varying | 24 |  |
| 3 | company_name | [ ] | character varying | 128 |  |
| 4 | shipper_name | [x] | character varying | 150 |  |
| 5 | po_box | [x] | character varying | 128 |  |
| 6 | address_line_1 | [x] | character varying | 128 |  |
| 7 | address_line_2 | [x] | character varying | 128 |  |
| 8 | street | [x] | character varying | 50 |  |
| 9 | city | [x] | character varying | 50 |  |
| 10 | state | [x] | character varying | 50 |  |
| 11 | country | [x] | character varying | 50 |  |
| 12 | phone | [x] | character varying | 50 |  |
| 13 | fax | [x] | character varying | 50 |  |
| 14 | cell | [x] | character varying | 50 |  |
| 15 | email | [x] | character varying | 128 |  |
| 16 | url | [x] | character varying | 50 |  |
| 17 | contact_person | [x] | character varying | 50 |  |
| 18 | contact_po_box | [x] | character varying | 128 |  |
| 19 | contact_address_line_1 | [x] | character varying | 128 |  |
| 20 | contact_address_line_2 | [x] | character varying | 128 |  |
| 21 | contact_street | [x] | character varying | 50 |  |
| 22 | contact_city | [x] | character varying | 50 |  |
| 23 | contact_state | [x] | character varying | 50 |  |
| 24 | contact_country | [x] | character varying | 50 |  |
| 25 | contact_email | [x] | character varying | 128 |  |
| 26 | contact_phone | [x] | character varying | 50 |  |
| 27 | contact_cell | [x] | character varying | 50 |  |
| 28 | factory_address | [x] | character varying | 250 |  |
| 29 | pan_number | [x] | character varying | 50 |  |
| 30 | sst_number | [x] | character varying | 50 |  |
| 31 | cst_number | [x] | character varying | 50 |  |
| 32 | account_id | [ ] | integer | 0 |  |
| 33 | audit_user_id | [x] | integer | 0 |  |
| 34 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 35 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 32 | [account_id](../finance/accounts.md) | shippers_account_id_fkey | finance.accounts.account_id |
| 33 | [audit_user_id](../account/users.md) | shippers_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| shippers_pkey | frapid_db_user | btree | shipper_id |  |
| shippers_shipper_code_uix | frapid_db_user | btree | upper(shipper_code::text) |  |
| shippers_shipper_name_uix | frapid_db_user | btree | upper(shipper_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | shipper_id | nextval('inventory.shippers_shipper_id_seq'::regclass) |
| 34 | audit_ts | now() |
| 35 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
