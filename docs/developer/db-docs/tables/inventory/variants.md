# inventory.variants table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | variant_id | [ ] | integer | 0 |  |
| 2 | variant_code | [ ] | character varying | 12 |  |
| 3 | variant_name | [ ] | character varying | 100 |  |
| 4 | attribute_id | [ ] | integer | 0 |  |
| 5 | attribute_value | [ ] | character varying | 200 |  |
| 6 | audit_user_id | [x] | integer | 0 |  |
| 7 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 8 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [attribute_id](../inventory/attributes.md) | variants_attribute_id_fkey | inventory.attributes.attribute_id |
| 6 | [audit_user_id](../account/users.md) | variants_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| variants_pkey | frapid_db_user | btree | variant_id |  |
| variants_variant_code_key | frapid_db_user | btree | variant_code |  |
| variants_variant_code_uix | frapid_db_user | btree | upper(variant_code::text) |  |
| variants_variant_name_uix | frapid_db_user | btree | upper(variant_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | variant_id | nextval('inventory.variants_variant_id_seq'::regclass) |
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
