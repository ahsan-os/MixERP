# inventory.attributes table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | attribute_id | [ ] | integer | 0 |  |
| 1 | udt_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 2 | udt_schema | [x] | information_schema.sql_identifier | 0 |  |
| 2 | attribute_code | [ ] | character varying | 12 |  |
| 3 | attribute_name | [ ] | character varying | 100 |  |
| 3 | udt_name | [x] | information_schema.sql_identifier | 0 |  |
| 4 | attribute_name | [x] | information_schema.sql_identifier | 0 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | ordinal_position | [x] | information_schema.cardinal_number | 0 |  |
| 6 | attribute_default | [x] | information_schema.character_data | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |
| 7 | is_nullable | [x] | information_schema.yes_or_no | 0 |  |
| 8 | data_type | [x] | information_schema.character_data | 0 |  |
| 9 | character_maximum_length | [x] | information_schema.cardinal_number | 0 |  |
| 10 | character_octet_length | [x] | information_schema.cardinal_number | 0 |  |
| 11 | character_set_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 12 | character_set_schema | [x] | information_schema.sql_identifier | 0 |  |
| 13 | character_set_name | [x] | information_schema.sql_identifier | 0 |  |
| 14 | collation_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 15 | collation_schema | [x] | information_schema.sql_identifier | 0 |  |
| 16 | collation_name | [x] | information_schema.sql_identifier | 0 |  |
| 17 | numeric_precision | [x] | information_schema.cardinal_number | 0 |  |
| 18 | numeric_precision_radix | [x] | information_schema.cardinal_number | 0 |  |
| 19 | numeric_scale | [x] | information_schema.cardinal_number | 0 |  |
| 20 | datetime_precision | [x] | information_schema.cardinal_number | 0 |  |
| 21 | interval_type | [x] | information_schema.character_data | 0 |  |
| 22 | interval_precision | [x] | information_schema.cardinal_number | 0 |  |
| 23 | attribute_udt_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 24 | attribute_udt_schema | [x] | information_schema.sql_identifier | 0 |  |
| 25 | attribute_udt_name | [x] | information_schema.sql_identifier | 0 |  |
| 26 | scope_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 27 | scope_schema | [x] | information_schema.sql_identifier | 0 |  |
| 28 | scope_name | [x] | information_schema.sql_identifier | 0 |  |
| 29 | maximum_cardinality | [x] | information_schema.cardinal_number | 0 |  |
| 30 | dtd_identifier | [x] | information_schema.sql_identifier | 0 |  |
| 31 | is_derived_reference_attribute | [x] | information_schema.yes_or_no | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [audit_user_id](../account/users.md) | attributes_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| attributes_pkey | frapid_db_user | btree | attribute_id |  |
| attributes_attribute_code_key | frapid_db_user | btree | attribute_code |  |
| attributes_attribute_code_uix | frapid_db_user | btree | upper(attribute_code::text) |  |
| attributes_attribute_name_uix | frapid_db_user | btree | upper(attribute_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | attribute_id | nextval('inventory.attributes_attribute_id_seq'::regclass) |
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
