# inventory.inventory_transfer_deliveries table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | inventory_transfer_delivery_id | [ ] | bigint | 0 |  |
| 2 | inventory_transfer_request_id | [ ] | bigint | 0 |  |
| 3 | office_id | [ ] | integer | 0 |  |
| 4 | user_id | [ ] | integer | 0 |  |
| 5 | destination_store_id | [ ] | integer | 0 |  |
| 6 | delivery_date | [ ] | date | 0 |  |
| 7 | transaction_timestamp | [ ] | timestamp with time zone | 0 |  |
| 8 | reference_number | [x] | character varying | 24 |  |
| 9 | statement_reference | [x] | character varying | 500 |  |
| 10 | audit_user_id | [x] | integer | 0 |  |
| 11 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 12 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [inventory_transfer_request_id](../inventory/inventory_transfer_requests.md) | inventory_transfer_deliveries_inventory_transfer_request_i_fkey | inventory.inventory_transfer_requests.inventory_transfer_request_id |
| 3 | [office_id](../core/offices.md) | inventory_transfer_deliveries_office_id_fkey | core.offices.office_id |
| 4 | [user_id](../account/users.md) | inventory_transfer_deliveries_user_id_fkey | account.users.user_id |
| 5 | [destination_store_id](../inventory/stores.md) | inventory_transfer_deliveries_destination_store_id_fkey | inventory.stores.store_id |
| 10 | [audit_user_id](../account/users.md) | inventory_transfer_deliveries_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| inventory_transfer_deliveries_pkey | frapid_db_user | btree | inventory_transfer_delivery_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | inventory_transfer_delivery_id | nextval('inventory.inventory_transfer_deliveries_inventory_transfer_delivery_i_seq'::regclass) |
| 7 | transaction_timestamp | now() |
| 11 | audit_ts | now() |
| 12 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
