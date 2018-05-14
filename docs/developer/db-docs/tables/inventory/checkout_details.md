# inventory.checkout_details table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | checkout_detail_id | [ ] | bigint | 0 |  |
| 1 | checkout_detail_id | [ ] | bigint | 0 |  |
| 1 | checkout_detail_id | [ ] | bigint | 0 |  |
| 2 | checkout_id | [ ] | bigint | 0 |  |
| 2 | checkout_id | [ ] | bigint | 0 |  |
| 2 | checkout_id | [ ] | bigint | 0 |  |
| 3 | store_id | [ ] | integer | 0 |  |
| 3 | item_id | [ ] | bigint | 0 |  |
| 3 | food_course_id | [ ] | integer | 0 |  |
| 4 | dining_option_id | [ ] | integer | 0 |  |
| 4 | value_date | [ ] | date | 0 |  |
| 4 | rate | [ ] | numeric | 1966086 |  |
| 5 | quantity | [ ] | integer | 0 |  |
| 5 | chair | [x] | character varying | 20 |  |
| 5 | book_date | [ ] | date | 0 |  |
| 6 | transaction_type | [ ] | character varying | 2 |  |
| 6 | audit_user_id | [x] | integer | 0 |  |
| 6 | item_id | [ ] | integer | 0 |  |
| 7 | unit_id | [ ] | integer | 0 |  |
| 7 | item_id | [ ] | integer | 0 |  |
| 7 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 8 | price | [ ] | money_strict | 0 |  |
| 8 | rate | [ ] | money_strict2 | 0 |  |
| 8 | deleted | [x] | boolean | 0 |  |
| 9 | quantity | [ ] | decimal_strict | 0 |  |
| 9 | discount | [ ] | money_strict2 | 0 |  |
| 10 | cost_of_goods_sold | [ ] | money_strict2 | 0 |  |
| 10 | discount_rate | [ ] | decimal_strict2 | 0 |  |
| 11 | service_charge | [ ] | money_strict2 | 0 |  |
| 11 | tax | [ ] | money_strict2 | 0 |  |
| 12 | shipping_charge | [ ] | money_strict2 | 0 |  |
| 12 | tax | [ ] | money_strict2 | 0 |  |
| 13 | unit_id | [ ] | integer | 0 |  |
| 13 | special_instructions | [x] | character varying | 2000 |  |
| 14 | quantity | [ ] | decimal_strict | 0 |  |
| 15 | base_unit_id | [ ] | integer | 0 |  |
| 16 | base_quantity | [ ] | numeric | 1966086 |  |
| 17 | audit_ts | [x] | timestamp with time zone | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [checkout_id](../inventory/checkouts.md) | checkout_details_checkout_id_fkey | inventory.checkouts.checkout_id |
| 2 | [checkout_id](../inventory/checkouts.md) | checkout_details_checkout_id_fkey | inventory.checkouts.checkout_id |
| 2 | [checkout_id](../inventory/checkouts.md) | checkout_details_checkout_id_fkey | inventory.checkouts.checkout_id |
| 3 | [store_id](../inventory/stores.md) | checkout_details_store_id_fkey | inventory.stores.store_id |
| 3 | [item_id](../inventory/items.md) | checkout_details_item_id_fkey | inventory.items.item_id |
| 6 | [item_id](../inventory/items.md) | checkout_details_item_id_fkey | inventory.items.item_id |
| 7 | [unit_id](../inventory/units.md) | checkout_details_unit_id_fkey | inventory.units.unit_id |
| 7 | [item_id](../inventory/items.md) | checkout_details_item_id_fkey | inventory.items.item_id |
| 13 | [unit_id](../inventory/units.md) | checkout_details_unit_id_fkey | inventory.units.unit_id |
| 15 | [base_unit_id](../inventory/units.md) | checkout_details_base_unit_id_fkey | inventory.units.unit_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| checkout_details_pkey | frapid_db_user | btree | checkout_detail_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| checkout_details_transaction_type_check CHECK (transaction_type::text = ANY (ARRAY['Dr'::character varying, 'Cr'::character varying]::text[])) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | checkout_detail_id | nextval('cafesys.checkout_details_checkout_detail_id_seq'::regclass) |
| 1 | checkout_detail_id | nextval('inventory.checkout_details_checkout_detail_id_seq'::regclass) |
| 1 | checkout_detail_id | nextval('foodcourt.checkout_details_checkout_detail_id_seq'::regclass) |
| 7 | audit_ts | now() |
| 8 | deleted | false |
| 9 | discount | 0 |
| 10 | cost_of_goods_sold | 0 |
| 10 | discount_rate | 0 |
| 11 | service_charge | 0 |
| 11 | tax | 0 |
| 12 | shipping_charge | 0 |
| 12 | tax | 0 |
| 17 | audit_ts | now() |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
