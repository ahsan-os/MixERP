# inventory.items table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | item_id | [ ] | integer | 0 |  |
| 1 | item_id | [ ] | bigint | 0 |  |
| 2 | item_code | [ ] | character varying | 24 |  |
| 2 | item_code | [ ] | character varying | 24 |  |
| 3 | item_name | [ ] | character varying | 500 |  |
| 3 | item_name | [ ] | character varying | 200 |  |
| 4 | barcode | [x] | character varying | 100 |  |
| 4 | item_group_id | [ ] | integer | 0 |  |
| 5 | brand_id | [x] | integer | 0 |  |
| 5 | item_group_id | [ ] | integer | 0 |  |
| 6 | item_type_id | [ ] | integer | 0 |  |
| 6 | barcode | [ ] | character varying | 100 |  |
| 7 | photo | [x] | photo | 0 |  |
| 7 | brand_id | [x] | integer | 0 |  |
| 8 | preferred_supplier_id | [x] | integer | 0 |  |
| 8 | selling_price | [ ] | numeric | 1966086 |  |
| 9 | lead_time_in_days | [x] | integer | 0 |  |
| 9 | selling_price_includes_vat | [ ] | boolean | 0 |  |
| 10 | audit_user_id | [x] | integer | 0 |  |
| 10 | unit_id | [ ] | integer | 0 |  |
| 11 | hot_item | [ ] | boolean | 0 |  |
| 11 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 12 | deleted | [x] | boolean | 0 |  |
| 12 | is_taxable_item | [ ] | boolean | 0 |  |
| 13 | cost_price | [x] | decimal_strict2 | 0 |  |
| 14 | cost_price_includes_tax | [ ] | boolean | 0 |  |
| 15 | selling_price | [x] | decimal_strict2 | 0 |  |
| 16 | selling_price_includes_tax | [ ] | boolean | 0 |  |
| 17 | reorder_level | [ ] | integer_strict2 | 0 |  |
| 18 | reorder_quantity | [ ] | integer_strict2 | 0 |  |
| 19 | reorder_unit_id | [ ] | integer | 0 |  |
| 20 | maintain_inventory | [ ] | boolean | 0 |  |
| 21 | photo | [x] | photo | 0 |  |
| 22 | allow_sales | [ ] | boolean | 0 |  |
| 23 | allow_purchase | [ ] | boolean | 0 |  |
| 24 | is_variant_of | [x] | integer | 0 |  |
| 25 | audit_user_id | [x] | integer | 0 |  |
| 26 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 27 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [item_group_id](../inventory/item_groups.md) | items_item_group_id_fkey | inventory.item_groups.item_group_id |
| 5 | [brand_id](../inventory/brands.md) | items_brand_id_fkey | inventory.brands.brand_id |
| 5 | [item_group_id](../inventory/item_groups.md) | items_item_group_id_fkey | inventory.item_groups.item_group_id |
| 6 | [item_type_id](../inventory/item_types.md) | items_item_type_id_fkey | inventory.item_types.item_type_id |
| 7 | [brand_id](../inventory/brands.md) | items_brand_id_fkey | inventory.brands.brand_id |
| 8 | [preferred_supplier_id](../inventory/suppliers.md) | items_preferred_supplier_id_fkey | inventory.suppliers.supplier_id |
| 10 | [audit_user_id](../account/users.md) | items_audit_user_id_fkey | account.users.user_id |
| 10 | [unit_id](../inventory/units.md) | items_unit_id_fkey | inventory.units.unit_id |
| 19 | [reorder_unit_id](../inventory/units.md) | items_reorder_unit_id_fkey | inventory.units.unit_id |
| 24 | [is_variant_of](../inventory/items.md) | items_is_variant_of_fkey | inventory.items.item_id |
| 25 | [audit_user_id](../account/users.md) | items_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| items_pkey | frapid_db_user | btree | item_id |  |
| items_item_code_uix | frapid_db_user | btree | upper(item_code::text) |  |
| items_item_name_uix | frapid_db_user | btree | upper(item_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | item_id | nextval('inventory.items_item_id_seq'::regclass) |
| 1 | item_id | nextval('foodcourt.items_item_id_seq'::regclass) |
| 9 | selling_price_includes_vat | false |
| 11 | hot_item | false |
| 11 | audit_ts | now() |
| 12 | deleted | false |
| 12 | is_taxable_item | true |
| 14 | cost_price_includes_tax | false |
| 16 | selling_price_includes_tax | false |
| 17 | reorder_level | 0 |
| 18 | reorder_quantity | 0 |
| 20 | maintain_inventory | true |
| 22 | allow_sales | true |
| 23 | allow_purchase | true |
| 26 | audit_ts | now() |
| 27 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |
| inventory.items_unit_check_trigger | [inventory.items_unit_check_trigger](../../functions/inventory/items_unit_check_trigger-4459686.md) | UPDATE | AFTER |  | 0 | ROW |  |
| inventory.items_unit_check_trigger | [inventory.items_unit_check_trigger](../../functions/inventory/items_unit_check_trigger-4459686.md) | INSERT | AFTER |  | 0 | ROW |  |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
