# inventory.item_variants table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | item_variant_id | [ ] | integer | 0 |  |
| 2 | item_id | [ ] | integer | 0 |  |
| 3 | variant_id | [ ] | integer | 0 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [item_id](../inventory/items.md) | item_variants_item_id_fkey | inventory.items.item_id |
| 3 | [variant_id](../inventory/variants.md) | item_variants_variant_id_fkey | inventory.variants.variant_id |
| 4 | [audit_user_id](../account/users.md) | item_variants_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| item_variants_pkey | frapid_db_user | btree | item_variant_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | item_variant_id | nextval('inventory.item_variants_item_variant_id_seq'::regclass) |
| 5 | audit_ts | now() |
| 6 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
