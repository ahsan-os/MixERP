# purchase.purchase_returns table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | purchase_return_id | [ ] | bigint | 0 |  |
| 2 | purchase_id | [ ] | bigint | 0 |  |
| 3 | checkout_id | [ ] | bigint | 0 |  |
| 4 | supplier_id | [ ] | integer | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [purchase_id](../purchase/purchases.md) | purchase_returns_purchase_id_fkey | purchase.purchases.purchase_id |
| 3 | [checkout_id](../inventory/checkouts.md) | purchase_returns_checkout_id_fkey | inventory.checkouts.checkout_id |
| 4 | [supplier_id](../inventory/suppliers.md) | purchase_returns_supplier_id_fkey | inventory.suppliers.supplier_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| purchase_returns_pkey | frapid_db_user | btree | purchase_return_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | purchase_return_id | nextval('purchase.purchase_returns_purchase_return_id_seq'::regclass) |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
