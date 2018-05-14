# sales.quotations table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | quotation_id | [ ] | bigint | 0 |  |
| 1 | quotation_id | [ ] | bigint | 0 |  |
| 2 | value_date | [ ] | date | 0 |  |
| 2 | value_date | [ ] | date | 0 |  |
| 3 | expected_delivery_date | [ ] | date | 0 |  |
| 3 | expected_delivery_date | [ ] | date | 0 |  |
| 4 | transaction_timestamp | [ ] | timestamp with time zone | 0 |  |
| 4 | transaction_timestamp | [ ] | timestamp with time zone | 0 |  |
| 5 | customer_id | [ ] | integer | 0 |  |
| 5 | supplier_id | [ ] | integer | 0 |  |
| 6 | price_type_id | [ ] | integer | 0 |  |
| 6 | price_type_id | [ ] | integer | 0 |  |
| 7 | shipper_id | [x] | integer | 0 |  |
| 7 | shipper_id | [x] | integer | 0 |  |
| 8 | user_id | [ ] | integer | 0 |  |
| 8 | user_id | [ ] | integer | 0 |  |
| 9 | office_id | [ ] | integer | 0 |  |
| 9 | office_id | [ ] | integer | 0 |  |
| 10 | reference_number | [x] | character varying | 24 |  |
| 10 | reference_number | [x] | character varying | 24 |  |
| 11 | terms | [x] | character varying | 500 |  |
| 11 | terms | [x] | character varying | 500 |  |
| 12 | internal_memo | [x] | character varying | 500 |  |
| 12 | internal_memo | [x] | character varying | 500 |  |
| 13 | audit_user_id | [x] | integer | 0 |  |
| 13 | audit_user_id | [x] | integer | 0 |  |
| 14 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 14 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 15 | deleted | [x] | boolean | 0 |  |
| 15 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 5 | [customer_id](../inventory/customers.md) | quotations_customer_id_fkey | inventory.customers.customer_id |
| 6 | [price_type_id](../sales/price_types.md) | quotations_price_type_id_fkey | sales.price_types.price_type_id |
| 6 | [price_type_id](../sales/price_types.md) | quotations_price_type_id_fkey | sales.price_types.price_type_id |
| 7 | [shipper_id](../inventory/shippers.md) | quotations_shipper_id_fkey | inventory.shippers.shipper_id |
| 7 | [shipper_id](../inventory/shippers.md) | quotations_shipper_id_fkey | inventory.shippers.shipper_id |
| 8 | [user_id](../account/users.md) | quotations_user_id_fkey | account.users.user_id |
| 8 | [user_id](../account/users.md) | quotations_user_id_fkey | account.users.user_id |
| 9 | [office_id](../core/offices.md) | quotations_office_id_fkey | core.offices.office_id |
| 9 | [office_id](../core/offices.md) | quotations_office_id_fkey | core.offices.office_id |
| 13 | [audit_user_id](../account/users.md) | quotations_audit_user_id_fkey | account.users.user_id |
| 13 | [audit_user_id](../account/users.md) | quotations_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| quotations_pkey | frapid_db_user | btree | quotation_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | quotation_id | nextval('sales.quotations_quotation_id_seq'::regclass) |
| 1 | quotation_id | nextval('purchase.quotations_quotation_id_seq'::regclass) |
| 4 | transaction_timestamp | now() |
| 4 | transaction_timestamp | now() |
| 14 | audit_ts | now() |
| 14 | audit_ts | now() |
| 15 | deleted | false |
| 15 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
