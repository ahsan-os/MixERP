# purchase.purchases table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | purchase_id | [ ] | integer | 0 |  |
| 1 | purchase_id | [ ] | bigint | 0 |  |
| 2 | checkout_id | [ ] | bigint | 0 |  |
| 2 | invoice_number | [ ] | character varying | 12 |  |
| 3 | invoice_date | [ ] | date | 0 |  |
| 3 | supplier_id | [ ] | integer | 0 |  |
| 4 | price_type_id | [ ] | integer | 0 |  |
| 4 | dealer_id | [ ] | integer | 0 |  |
| 5 | is_vat_included | [ ] | boolean | 0 |  |
| 6 | audit_user_id | [x] | integer | 0 |  |
| 7 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 8 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [checkout_id](../inventory/checkouts.md) | purchases_checkout_id_fkey | inventory.checkouts.checkout_id |
| 3 | [supplier_id](../inventory/suppliers.md) | purchases_supplier_id_fkey | inventory.suppliers.supplier_id |
| 4 | [price_type_id](../purchase/price_types.md) | purchases_price_type_id_fkey | purchase.price_types.price_type_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| purchases_pkey | frapid_db_user | btree | purchase_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | purchase_id | nextval('foodcourt.purchases_purchase_id_seq'::regclass) |
| 1 | purchase_id | nextval('purchase.purchases_purchase_id_seq'::regclass) |
| 3 | invoice_date | now() |
| 5 | is_vat_included | false |
| 7 | audit_ts | now() |
| 8 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
