# inventory.customers table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | customer_id | [ ] | integer | 0 |  |
| 1 | customer_id | [ ] | integer | 0 |  |
| 2 | customer_group_id | [ ] | integer | 0 |  |
| 2 | customer_code | [ ] | character varying | 24 |  |
| 3 | email_address | [ ] | character varying | 1000 |  |
| 3 | customer_name | [ ] | character varying | 500 |  |
| 4 | associated_user_id | [x] | integer | 0 |  |
| 4 | customer_type_id | [ ] | integer | 0 |  |
| 5 | company_name | [x] | character varying | 1000 |  |
| 5 | account_id | [x] | integer | 0 |  |
| 6 | address | [x] | text | 0 |  |
| 6 | currency_code | [ ] | character varying | 12 |  |
| 7 | email | [x] | character varying | 128 |  |
| 7 | country | [x] | character varying | 500 |  |
| 8 | company_name | [x] | character varying | 1000 |  |
| 8 | first_name | [x] | character varying | 1000 |  |
| 9 | company_address_line_1 | [x] | character varying | 128 |  |
| 9 | last_name | [x] | character varying | 1000 |  |
| 10 | zip_code | [x] | character varying | 100 |  |
| 10 | company_address_line_2 | [x] | character varying | 128 |  |
| 11 | company_street | [x] | character varying | 1000 |  |
| 11 | po_box | [x] | character varying | 100 |  |
| 12 | company_city | [x] | character varying | 1000 |  |
| 12 | phone_numbers | [x] | character varying | 1000 |  |
| 13 | company_state | [x] | character varying | 1000 |  |
| 13 | billing_address_line_1 | [x] | character varying | 500 |  |
| 14 | company_country | [x] | character varying | 1000 |  |
| 14 | billing_address_line_2 | [x] | character varying | 500 |  |
| 15 | company_po_box | [x] | character varying | 1000 |  |
| 15 | billing_address_street | [x] | character varying | 500 |  |
| 16 | billing_address_city | [x] | character varying | 500 |  |
| 16 | company_zipcode | [x] | character varying | 1000 |  |
| 17 | company_phone_numbers | [x] | character varying | 1000 |  |
| 17 | billing_address_state | [x] | character varying | 500 |  |
| 18 | billing_address_zip_code | [x] | character varying | 100 |  |
| 18 | company_fax | [x] | character varying | 100 |  |
| 19 | logo | [x] | photo | 0 |  |
| 19 | billing_address_po_box | [x] | character varying | 100 |  |
| 20 | contact_first_name | [x] | character varying | 100 |  |
| 20 | billing_address_country | [x] | character varying | 500 |  |
| 21 | audit_user_id | [x] | integer | 0 |  |
| 21 | contact_middle_name | [x] | character varying | 100 |  |
| 22 | contact_last_name | [x] | character varying | 100 |  |
| 22 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 23 | contact_address_line_1 | [x] | character varying | 128 |  |
| 23 | deleted | [x] | boolean | 0 |  |
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
| 37 | pan_number | [x] | character varying | 10 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [customer_type_id](../inventory/customer_types.md) | customers_customer_type_id_fkey | inventory.customer_types.customer_type_id |
| 5 | [account_id](../finance/accounts.md) | customers_account_id_fkey | finance.accounts.account_id |
| 6 | [currency_code](../core/currencies.md) | customers_currency_code_fkey | core.currencies.currency_code |
| 21 | [audit_user_id](../account/users.md) | customers_audit_user_id_fkey | account.users.user_id |
| 34 | [audit_user_id](../account/users.md) | customers_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| customers_pkey | frapid_db_user | btree | customer_id |  |
| customers_account_id_key | frapid_db_user | btree | account_id |  |
| customers_customer_code_uix | frapid_db_user | btree | upper(customer_code::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| customers_pan_number_check CHECK (length(pan_number::text) = 0 OR length(pan_number::text) = 10) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | customer_id | nextval('helpdesk.customers_customer_id_seq'::regclass) |
| 1 | customer_id | nextval('inventory.customers_customer_id_seq'::regclass) |
| 22 | audit_ts | now() |
| 23 | deleted | false |
| 35 | audit_ts | now() |
| 36 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |
| inventory.customer_after_insert_trigger | [inventory.customer_after_insert_trigger](../../functions/inventory/customer_after_insert_trigger-4459684.md) | INSERT | AFTER |  | 0 | ROW |  |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
