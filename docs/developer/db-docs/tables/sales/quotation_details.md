# sales.quotation_details table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | quotation_detail_id | [ ] | bigint | 0 |  |
| 1 | quotation_detail_id | [ ] | bigint | 0 |  |
| 2 | quotation_id | [ ] | bigint | 0 |  |
| 2 | quotation_id | [ ] | bigint | 0 |  |
| 3 | value_date | [ ] | date | 0 |  |
| 3 | value_date | [ ] | date | 0 |  |
| 4 | item_id | [ ] | integer | 0 |  |
| 4 | item_id | [ ] | integer | 0 |  |
| 5 | price | [ ] | money_strict | 0 |  |
| 5 | price | [ ] | money_strict | 0 |  |
| 6 | discount_rate | [ ] | decimal_strict2 | 0 |  |
| 6 | discount_rate | [ ] | decimal_strict2 | 0 |  |
| 7 | tax | [ ] | money_strict2 | 0 |  |
| 7 | tax | [ ] | money_strict2 | 0 |  |
| 8 | shipping_charge | [ ] | money_strict2 | 0 |  |
| 8 | shipping_charge | [ ] | money_strict2 | 0 |  |
| 9 | unit_id | [ ] | integer | 0 |  |
| 9 | unit_id | [ ] | integer | 0 |  |
| 10 | quantity | [ ] | decimal_strict2 | 0 |  |
| 10 | quantity | [ ] | decimal_strict2 | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [quotation_id](../sales/quotations.md) | quotation_details_quotation_id_fkey | sales.quotations.quotation_id |
| 2 | [quotation_id](../sales/quotations.md) | quotation_details_quotation_id_fkey | sales.quotations.quotation_id |
| 4 | [item_id](../inventory/items.md) | quotation_details_item_id_fkey | inventory.items.item_id |
| 4 | [item_id](../inventory/items.md) | quotation_details_item_id_fkey | inventory.items.item_id |
| 9 | [unit_id](../inventory/units.md) | quotation_details_unit_id_fkey | inventory.units.unit_id |
| 9 | [unit_id](../inventory/units.md) | quotation_details_unit_id_fkey | inventory.units.unit_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| quotation_details_pkey | frapid_db_user | btree | quotation_detail_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | quotation_detail_id | nextval('purchase.quotation_details_quotation_detail_id_seq'::regclass) |
| 1 | quotation_detail_id | nextval('sales.quotation_details_quotation_detail_id_seq'::regclass) |
| 6 | discount_rate | 0 |
| 6 | discount_rate | 0 |
| 7 | tax | 0 |
| 7 | tax | 0 |
| 8 | shipping_charge | 0 |
| 8 | shipping_charge | 0 |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
