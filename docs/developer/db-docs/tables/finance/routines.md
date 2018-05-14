# finance.routines table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | specific_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 1 | routine_id | [ ] | integer | 0 |  |
| 2 | order | [ ] | integer | 0 |  |
| 2 | specific_schema | [x] | information_schema.sql_identifier | 0 |  |
| 3 | specific_name | [x] | information_schema.sql_identifier | 0 |  |
| 3 | routine_code | [ ] | character varying | 12 |  |
| 4 | routine_name | [ ] | regproc | 0 |  |
| 4 | routine_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 5 | status | [ ] | boolean | 0 |  |
| 5 | routine_schema | [x] | information_schema.sql_identifier | 0 |  |
| 6 | routine_name | [x] | information_schema.sql_identifier | 0 |  |
| 7 | routine_type | [x] | information_schema.character_data | 0 |  |
| 8 | module_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 9 | module_schema | [x] | information_schema.sql_identifier | 0 |  |
| 10 | module_name | [x] | information_schema.sql_identifier | 0 |  |
| 11 | udt_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 12 | udt_schema | [x] | information_schema.sql_identifier | 0 |  |
| 13 | udt_name | [x] | information_schema.sql_identifier | 0 |  |
| 14 | data_type | [x] | information_schema.character_data | 0 |  |
| 15 | character_maximum_length | [x] | information_schema.cardinal_number | 0 |  |
| 16 | character_octet_length | [x] | information_schema.cardinal_number | 0 |  |
| 17 | character_set_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 18 | character_set_schema | [x] | information_schema.sql_identifier | 0 |  |
| 19 | character_set_name | [x] | information_schema.sql_identifier | 0 |  |
| 20 | collation_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 21 | collation_schema | [x] | information_schema.sql_identifier | 0 |  |
| 22 | collation_name | [x] | information_schema.sql_identifier | 0 |  |
| 23 | numeric_precision | [x] | information_schema.cardinal_number | 0 |  |
| 24 | numeric_precision_radix | [x] | information_schema.cardinal_number | 0 |  |
| 25 | numeric_scale | [x] | information_schema.cardinal_number | 0 |  |
| 26 | datetime_precision | [x] | information_schema.cardinal_number | 0 |  |
| 27 | interval_type | [x] | information_schema.character_data | 0 |  |
| 28 | interval_precision | [x] | information_schema.cardinal_number | 0 |  |
| 29 | type_udt_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 30 | type_udt_schema | [x] | information_schema.sql_identifier | 0 |  |
| 31 | type_udt_name | [x] | information_schema.sql_identifier | 0 |  |
| 32 | scope_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 33 | scope_schema | [x] | information_schema.sql_identifier | 0 |  |
| 34 | scope_name | [x] | information_schema.sql_identifier | 0 |  |
| 35 | maximum_cardinality | [x] | information_schema.cardinal_number | 0 |  |
| 36 | dtd_identifier | [x] | information_schema.sql_identifier | 0 |  |
| 37 | routine_body | [x] | information_schema.character_data | 0 |  |
| 38 | routine_definition | [x] | information_schema.character_data | 0 |  |
| 39 | external_name | [x] | information_schema.character_data | 0 |  |
| 40 | external_language | [x] | information_schema.character_data | 0 |  |
| 41 | parameter_style | [x] | information_schema.character_data | 0 |  |
| 42 | is_deterministic | [x] | information_schema.yes_or_no | 0 |  |
| 43 | sql_data_access | [x] | information_schema.character_data | 0 |  |
| 44 | is_null_call | [x] | information_schema.yes_or_no | 0 |  |
| 45 | sql_path | [x] | information_schema.character_data | 0 |  |
| 46 | schema_level_routine | [x] | information_schema.yes_or_no | 0 |  |
| 47 | max_dynamic_result_sets | [x] | information_schema.cardinal_number | 0 |  |
| 48 | is_user_defined_cast | [x] | information_schema.yes_or_no | 0 |  |
| 49 | is_implicitly_invocable | [x] | information_schema.yes_or_no | 0 |  |
| 50 | security_type | [x] | information_schema.character_data | 0 |  |
| 51 | to_sql_specific_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 52 | to_sql_specific_schema | [x] | information_schema.sql_identifier | 0 |  |
| 53 | to_sql_specific_name | [x] | information_schema.sql_identifier | 0 |  |
| 54 | as_locator | [x] | information_schema.yes_or_no | 0 |  |
| 55 | created | [x] | information_schema.time_stamp | 0 |  |
| 56 | last_altered | [x] | information_schema.time_stamp | 0 |  |
| 57 | new_savepoint_level | [x] | information_schema.yes_or_no | 0 |  |
| 58 | is_udt_dependent | [x] | information_schema.yes_or_no | 0 |  |
| 59 | result_cast_from_data_type | [x] | information_schema.character_data | 0 |  |
| 60 | result_cast_as_locator | [x] | information_schema.yes_or_no | 0 |  |
| 61 | result_cast_char_max_length | [x] | information_schema.cardinal_number | 0 |  |
| 62 | result_cast_char_octet_length | [x] | information_schema.cardinal_number | 0 |  |
| 63 | result_cast_char_set_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 64 | result_cast_char_set_schema | [x] | information_schema.sql_identifier | 0 |  |
| 65 | result_cast_character_set_name | [x] | information_schema.sql_identifier | 0 |  |
| 66 | result_cast_collation_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 67 | result_cast_collation_schema | [x] | information_schema.sql_identifier | 0 |  |
| 68 | result_cast_collation_name | [x] | information_schema.sql_identifier | 0 |  |
| 69 | result_cast_numeric_precision | [x] | information_schema.cardinal_number | 0 |  |
| 70 | result_cast_numeric_precision_radix | [x] | information_schema.cardinal_number | 0 |  |
| 71 | result_cast_numeric_scale | [x] | information_schema.cardinal_number | 0 |  |
| 72 | result_cast_datetime_precision | [x] | information_schema.cardinal_number | 0 |  |
| 73 | result_cast_interval_type | [x] | information_schema.character_data | 0 |  |
| 74 | result_cast_interval_precision | [x] | information_schema.cardinal_number | 0 |  |
| 75 | result_cast_type_udt_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 76 | result_cast_type_udt_schema | [x] | information_schema.sql_identifier | 0 |  |
| 77 | result_cast_type_udt_name | [x] | information_schema.sql_identifier | 0 |  |
| 78 | result_cast_scope_catalog | [x] | information_schema.sql_identifier | 0 |  |
| 79 | result_cast_scope_schema | [x] | information_schema.sql_identifier | 0 |  |
| 80 | result_cast_scope_name | [x] | information_schema.sql_identifier | 0 |  |
| 81 | result_cast_maximum_cardinality | [x] | information_schema.cardinal_number | 0 |  |
| 82 | result_cast_dtd_identifier | [x] | information_schema.sql_identifier | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| routines_pkey | frapid_db_user | btree | routine_id |  |
| routines_routine_name_key | frapid_db_user | btree | routine_name |  |
| routines_routine_code_uix | frapid_db_user | btree | lower(routine_code::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | routine_id | nextval('finance.routines_routine_id_seq'::regclass) |
| 5 | status | true |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
