# public schema

**Tables**

| # | Table Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |



**Sequences**

| # | Sequence Name | Owner | Data Type | Start Value | Increment | Description |
| --- | --- | --- | --- | --- | --- | --- |


**Views**

| # | View Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |



**Materialized Views**

| # | Matview Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |



**Functions**

| # | Function | Owner | Description |
| --- | --- | --- | --- |
| 1 | [explode_array(in_array anyarray)RETURNS SETOF anyelement](../functions/public/explode_array-4454484.md) | frapid_db_user |  |
| 2 | [get_app_data_type(_nullable text, _db_data_type text)RETURNS text](../functions/public/get_app_data_type-4454480.md) | frapid_db_user |  |
| 3 | [parse_default(text)RETURNS text](../functions/public/parse_default-4454483.md) | frapid_db_user |  |
| 4 | [poco_get_table_function_annotation(_schema_name text, _table_name text)RETURNS TABLE(id integer, column_name text, nullable text, db_data_type text, value text, max_length integer, primary_key text)](../functions/public/poco_get_table_function_annotation-4454479.md) | frapid_db_user |  |
| 5 | [poco_get_table_function_definition(_schema character varying, _name character varying)RETURNS TABLE(id bigint, column_name text, nullable text, db_data_type text, value text, max_length integer, primary_key text, data_type text)](../functions/public/poco_get_table_function_definition-4454481.md) | frapid_db_user |  |
| 6 | [poco_get_tables(_schema text)RETURNS TABLE(table_schema name, table_name name, table_type text, has_duplicate boolean)](../functions/public/poco_get_tables-4454482.md) | frapid_db_user |  |
| 7 | [text_to_bigint(text)RETURNS bigint](../functions/public/text_to_bigint-4454476.md) | frapid_db_user |  |
| 8 | [text_to_int_array(_input text, _remove_nulls boolean DEFAULT true)RETURNS integer[]](../functions/public/text_to_int_array-4454478.md) | frapid_db_user |  |



**Triggers**

| # | Trigger | Owner | Description |
| --- | --- | --- | --- |



**Types**

| # | Type | Base Type | Owner | Collation | Default | Type | StoreType | NotNull | Description |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 1 | [color](../types/public/color.md) | text | frapid_db_user | default |  | Domain | Compressed Inline/Seconary | False |  |
| 2 | [decimal_strict](../types/public/decimal_strict.md) | numeric | frapid_db_user |  |  | Domain | Compressed Inline | False |  |
| 3 | [decimal_strict2](../types/public/decimal_strict2.md) | numeric | frapid_db_user |  |  | Domain | Compressed Inline | False |  |
| 4 | [ghstore](../types/public/ghstore.md) | - | postgres |  |  | Base Type | Plain | False |  |
| 5 | [hstore](../types/public/hstore.md) | - | postgres |  |  | Base Type | Compressed Inline/Seconary | False |  |
| 6 | [html](../types/public/html.md) | text | frapid_db_user | default |  | Domain | Compressed Inline/Seconary | False |  |
| 7 | [integer_strict](../types/public/integer_strict.md) | integer | frapid_db_user |  |  | Domain | Plain | False |  |
| 8 | [integer_strict2](../types/public/integer_strict2.md) | integer | frapid_db_user |  |  | Domain | Plain | False |  |
| 9 | [money_strict](../types/public/money_strict.md) | numeric | frapid_db_user |  |  | Domain | Compressed Inline | False |  |
| 10 | [money_strict2](../types/public/money_strict2.md) | numeric | frapid_db_user |  |  | Domain | Compressed Inline | False |  |
| 11 | [password](../types/public/password.md) | text | frapid_db_user | default |  | Domain | Compressed Inline/Seconary | False |  |
| 12 | [photo](../types/public/photo.md) | text | frapid_db_user | default |  | Domain | Compressed Inline/Seconary | False |  |
| 13 | [smallint_strict](../types/public/smallint_strict.md) | smallint | frapid_db_user |  |  | Domain | Plain | False |  |
| 14 | [smallint_strict2](../types/public/smallint_strict2.md) | smallint | frapid_db_user |  |  | Domain | Plain | False |  |


### Related Contents
* [Schema List](../schemas.md)
* [Table of Contents](../../README.md)