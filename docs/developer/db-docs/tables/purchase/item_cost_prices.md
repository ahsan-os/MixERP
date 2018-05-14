# purchase.item_cost_prices table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | item_cost_price_id | [ ] | bigint | 0 |  |
| 2 | item_id | [ ] | integer | 0 |  |
| 3 | unit_id | [ ] | integer | 0 |  |
| 4 | supplier_id | [x] | integer | 0 |  |
| 5 | lead_time_in_days | [ ] | integer | 0 |  |
| 6 | includes_tax | [ ] | boolean | 0 |  |
| 7 | price | [ ] | money_strict | 0 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [item_id](../inventory/items.md) | item_cost_prices_item_id_fkey | inventory.items.item_id |
| 3 | [unit_id](../inventory/units.md) | item_cost_prices_unit_id_fkey | inventory.units.unit_id |
| 4 | [supplier_id](../inventory/suppliers.md) | item_cost_prices_supplier_id_fkey | inventory.suppliers.supplier_id |
| 8 | [audit_user_id](../account/users.md) | item_cost_prices_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| item_cost_prices_pkey | frapid_db_user | btree | item_cost_price_id |  |
| item_cost_prices_item_id_unit_id_supplier_id_uix | frapid_db_user | btree | item_id, unit_id, supplier_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | item_cost_price_id | nextval('purchase.item_cost_prices_item_cost_price_id_seq'::regclass) |
| 5 | lead_time_in_days | 0 |
| 6 | includes_tax | false |
| 9 | audit_ts | now() |
| 10 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
