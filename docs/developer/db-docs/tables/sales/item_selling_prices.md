# sales.item_selling_prices table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | item_selling_price_id | [ ] | bigint | 0 |  |
| 2 | item_id | [ ] | integer | 0 |  |
| 3 | unit_id | [ ] | integer | 0 |  |
| 4 | customer_type_id | [x] | integer | 0 |  |
| 5 | price_type_id | [x] | integer | 0 |  |
| 6 | includes_tax | [ ] | boolean | 0 |  |
| 7 | price | [ ] | money_strict | 0 |  |
| 8 | audit_user_id | [x] | integer | 0 |  |
| 9 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 10 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [item_id](../inventory/items.md) | item_selling_prices_item_id_fkey | inventory.items.item_id |
| 3 | [unit_id](../inventory/units.md) | item_selling_prices_unit_id_fkey | inventory.units.unit_id |
| 4 | [customer_type_id](../inventory/customer_types.md) | item_selling_prices_customer_type_id_fkey | inventory.customer_types.customer_type_id |
| 5 | [price_type_id](../sales/price_types.md) | item_selling_prices_price_type_id_fkey | sales.price_types.price_type_id |
| 8 | [audit_user_id](../account/users.md) | item_selling_prices_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| item_selling_prices_pkey | frapid_db_user | btree | item_selling_price_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | item_selling_price_id | nextval('sales.item_selling_prices_item_selling_price_id_seq'::regclass) |
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
