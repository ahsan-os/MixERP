# inventory.inventory_transfer_requests table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | inventory_transfer_request_id | [ ] | bigint | 0 |  |
| 2 | office_id | [ ] | integer | 0 |  |
| 3 | user_id | [ ] | integer | 0 |  |
| 4 | store_id | [ ] | integer | 0 |  |
| 5 | request_date | [ ] | date | 0 |  |
| 6 | transaction_timestamp | [ ] | timestamp with time zone | 0 |  |
| 7 | reference_number | [x] | character varying | 24 |  |
| 8 | statement_reference | [x] | character varying | 500 |  |
| 9 | authorized | [ ] | boolean | 0 |  |
| 10 | authorized_by_user_id | [x] | integer | 0 |  |
| 11 | authorized_on | [x] | timestamp with time zone | 0 |  |
| 12 | authorization_reason | [x] | character varying | 500 |  |
| 13 | rejected | [ ] | boolean | 0 |  |
| 14 | rejected_by_user_id | [x] | integer | 0 |  |
| 15 | rejected_on | [x] | timestamp with time zone | 0 |  |
| 16 | rejection_reason | [x] | character varying | 500 |  |
| 17 | received | [ ] | boolean | 0 |  |
| 18 | received_by_user_id | [x] | integer | 0 |  |
| 19 | received_on | [x] | timestamp with time zone | 0 |  |
| 20 | receipt_memo | [x] | character varying | 500 |  |
| 21 | delivered | [ ] | boolean | 0 |  |
| 22 | delivered_by_user_id | [x] | integer | 0 |  |
| 23 | delivered_on | [x] | timestamp with time zone | 0 |  |
| 24 | delivery_memo | [x] | character varying | 500 |  |
| 25 | audit_user_id | [x] | integer | 0 |  |
| 26 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 27 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [office_id](../core/offices.md) | inventory_transfer_requests_office_id_fkey | core.offices.office_id |
| 3 | [user_id](../account/users.md) | inventory_transfer_requests_user_id_fkey | account.users.user_id |
| 4 | [store_id](../inventory/stores.md) | inventory_transfer_requests_store_id_fkey | inventory.stores.store_id |
| 10 | [authorized_by_user_id](../account/users.md) | inventory_transfer_requests_authorized_by_user_id_fkey | account.users.user_id |
| 14 | [rejected_by_user_id](../account/users.md) | inventory_transfer_requests_rejected_by_user_id_fkey | account.users.user_id |
| 18 | [received_by_user_id](../account/users.md) | inventory_transfer_requests_received_by_user_id_fkey | account.users.user_id |
| 22 | [delivered_by_user_id](../account/users.md) | inventory_transfer_requests_delivered_by_user_id_fkey | account.users.user_id |
| 25 | [audit_user_id](../account/users.md) | inventory_transfer_requests_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| inventory_transfer_requests_pkey | frapid_db_user | btree | inventory_transfer_request_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | inventory_transfer_request_id | nextval('inventory.inventory_transfer_requests_inventory_transfer_request_id_seq'::regclass) |
| 6 | transaction_timestamp | now() |
| 9 | authorized | false |
| 13 | rejected | false |
| 17 | received | false |
| 21 | delivered | false |
| 26 | audit_ts | now() |
| 27 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
