# inventory.inventory_transfer_delivery_details table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | inventory_transfer_delivery_detail_id | [ ] | bigint | 0 |  |
| 2 | inventory_transfer_delivery_id | [ ] | bigint | 0 |  |
| 3 | request_date | [ ] | date | 0 |  |
| 4 | item_id | [ ] | integer | 0 |  |
| 5 | quantity | [ ] | decimal_strict2 | 0 |  |
| 6 | unit_id | [ ] | integer | 0 |  |
| 7 | base_unit_id | [ ] | integer | 0 |  |
| 8 | base_quantity | [ ] | decimal_strict2 | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [inventory_transfer_delivery_id](../inventory/inventory_transfer_deliveries.md) | inventory_transfer_delivery_d_inventory_transfer_delivery__fkey | inventory.inventory_transfer_deliveries.inventory_transfer_delivery_id |
| 4 | [item_id](../inventory/items.md) | inventory_transfer_delivery_details_item_id_fkey | inventory.items.item_id |
| 6 | [unit_id](../inventory/units.md) | inventory_transfer_delivery_details_unit_id_fkey | inventory.units.unit_id |
| 7 | [base_unit_id](../inventory/units.md) | inventory_transfer_delivery_details_base_unit_id_fkey | inventory.units.unit_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| inventory_transfer_delivery_details_pkey | frapid_db_user | btree | inventory_transfer_delivery_detail_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | inventory_transfer_delivery_detail_id | nextval('inventory.inventory_transfer_delivery_d_inventory_transfer_delivery_d_seq'::regclass) |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
