# sales.orders table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | order_id | [ ] | bigint | 0 |  |
| 1 | order_id | [ ] | bigint | 0 |  |
| 1 | order_id | [ ] | bigint | 0 |  |
| 2 | quotation_id | [x] | bigint | 0 |  |
| 2 | office_id | [ ] | integer | 0 |  |
| 2 | quotation_id | [x] | bigint | 0 |  |
| 3 | description | [ ] | character varying | 100 |  |
| 3 | value_date | [ ] | date | 0 |  |
| 3 | value_date | [ ] | date | 0 |  |
| 4 | expected_delivery_date | [ ] | date | 0 |  |
| 4 | expected_delivery_date | [ ] | date | 0 |  |
| 4 | order_date | [ ] | date | 0 |  |
| 5 | transaction_timestamp | [ ] | timestamp with time zone | 0 |  |
| 5 | scheduled_on | [x] | timestamp without time zone | 0 |  |
| 5 | transaction_timestamp | [ ] | timestamp with time zone | 0 |  |
| 6 | supplier_id | [ ] | integer | 0 |  |
| 6 | due_date | [ ] | date | 0 |  |
| 6 | customer_id | [ ] | integer | 0 |  |
| 7 | price_type_id | [ ] | integer | 0 |  |
| 7 | priority_id | [ ] | integer | 0 |  |
| 7 | price_type_id | [ ] | integer | 0 |  |
| 8 | shipper_id | [x] | integer | 0 |  |
| 8 | order_type_id | [ ] | integer | 0 |  |
| 8 | shipper_id | [x] | integer | 0 |  |
| 9 | user_id | [ ] | integer | 0 |  |
| 9 | reference_number | [ ] | character varying | 24 |  |
| 9 | user_id | [ ] | integer | 0 |  |
| 10 | raw_material_store_id | [ ] | integer | 0 |  |
| 10 | office_id | [ ] | integer | 0 |  |
| 10 | office_id | [ ] | integer | 0 |  |
| 11 | reference_number | [x] | character varying | 24 |  |
| 11 | reference_number | [x] | character varying | 24 |  |
| 11 | work_in_progress_store_id | [ ] | integer | 0 |  |
| 12 | wip_account_id | [ ] | integer | 0 |  |
| 12 | terms | [x] | character varying | 500 |  |
| 12 | terms | [x] | character varying | 500 |  |
| 13 | internal_memo | [x] | character varying | 500 |  |
| 13 | internal_memo | [x] | character varying | 500 |  |
| 13 | finished_good_store_id | [ ] | integer | 0 |  |
| 14 | audit_user_id | [x] | integer | 0 |  |
| 14 | order_action_id | [ ] | integer | 0 |  |
| 14 | audit_user_id | [x] | integer | 0 |  |
| 15 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 15 | bom_id | [ ] | integer | 0 |  |
| 15 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 16 | deleted | [x] | boolean | 0 |  |
| 16 | routing_id | [x] | integer | 0 |  |
| 16 | deleted | [x] | boolean | 0 |  |
| 17 | plant_id | [ ] | integer | 0 |  |
| 18 | customer_id | [x] | integer | 0 |  |
| 19 | sales_order_id | [x] | bigint | 0 |  |
| 20 | managed_by | [ ] | integer | 0 |  |
| 21 | supervised_by | [ ] | integer | 0 |  |
| 22 | cost_center_id | [ ] | integer | 0 |  |
| 23 | kanban_id | [x] | integer | 0 |  |
| 24 | verification_status_id | [ ] | integer | 0 |  |
| 25 | verified_by_user_id | [x] | integer | 0 |  |
| 26 | verified_on | [x] | date | 0 |  |
| 27 | verification_reason | [x] | character varying | 128 |  |
| 28 | audit_user_id | [x] | integer | 0 |  |
| 29 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 30 | deleted | [ ] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [quotation_id](../sales/quotations.md) | orders_quotation_id_fkey | sales.quotations.quotation_id |
| 2 | [office_id](../core/offices.md) | orders_office_id_fkey | core.offices.office_id |
| 2 | [quotation_id](../sales/quotations.md) | orders_quotation_id_fkey | sales.quotations.quotation_id |
| 6 | [customer_id](../inventory/customers.md) | orders_customer_id_fkey | inventory.customers.customer_id |
| 7 | [price_type_id](../sales/price_types.md) | orders_price_type_id_fkey | sales.price_types.price_type_id |
| 7 | [price_type_id](../sales/price_types.md) | orders_price_type_id_fkey | sales.price_types.price_type_id |
| 8 | [shipper_id](../inventory/shippers.md) | orders_shipper_id_fkey | inventory.shippers.shipper_id |
| 8 | [shipper_id](../inventory/shippers.md) | orders_shipper_id_fkey | inventory.shippers.shipper_id |
| 9 | [user_id](../account/users.md) | orders_user_id_fkey | account.users.user_id |
| 9 | [user_id](../account/users.md) | orders_user_id_fkey | account.users.user_id |
| 10 | [office_id](../core/offices.md) | orders_office_id_fkey | core.offices.office_id |
| 10 | [office_id](../core/offices.md) | orders_office_id_fkey | core.offices.office_id |
| 14 | [audit_user_id](../account/users.md) | orders_audit_user_id_fkey | account.users.user_id |
| 14 | [audit_user_id](../account/users.md) | orders_audit_user_id_fkey | account.users.user_id |
| 18 | [customer_id](../inventory/customers.md) | orders_customer_id_fkey | inventory.customers.customer_id |
| 28 | [audit_user_id](../account/users.md) | orders_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| orders_pkey | frapid_db_user | btree | order_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | order_id | nextval('production.orders_order_id_seq'::regclass) |
| 1 | order_id | nextval('sales.orders_order_id_seq'::regclass) |
| 1 | order_id | nextval('purchase.orders_order_id_seq'::regclass) |
| 3 | description | ''::character varying |
| 5 | transaction_timestamp | now() |
| 5 | transaction_timestamp | now() |
| 9 | reference_number | ''::character varying |
| 15 | audit_ts | now() |
| 15 | audit_ts | now() |
| 16 | deleted | false |
| 16 | deleted | false |
| 29 | audit_ts | now() |
| 30 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
