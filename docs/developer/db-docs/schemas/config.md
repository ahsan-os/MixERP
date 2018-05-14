# config schema

**Tables**

| # | Table Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [custom_field_data_types](../tables/config/custom_field_data_types.md) | frapid_db_user | DEFAULT |  |
| 2 | [custom_field_forms](../tables/config/custom_field_forms.md) | frapid_db_user | DEFAULT |  |
| 3 | [custom_field_setup](../tables/config/custom_field_setup.md) | frapid_db_user | DEFAULT |  |
| 4 | [custom_fields](../tables/config/custom_fields.md) | frapid_db_user | DEFAULT |  |
| 5 | [email_queue](../tables/config/email_queue.md) | frapid_db_user | DEFAULT |  |
| 6 | [filters](../tables/config/filters.md) | frapid_db_user | DEFAULT |  |
| 7 | [kanban_details](../tables/config/kanban_details.md) | frapid_db_user | DEFAULT |  |
| 8 | [kanbans](../tables/config/kanbans.md) | frapid_db_user | DEFAULT |  |
| 9 | [smtp_configs](../tables/config/smtp_configs.md) | frapid_db_user | DEFAULT |  |



**Sequences**

| # | Sequence Name | Owner | Data Type | Start Value | Increment | Description |
| --- | --- | --- | --- | --- | --- | --- |
| 1 | custom_field_setup_custom_field_setup_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 2 | custom_fields_custom_field_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 3 | email_queue_queue_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 4 | filters_filter_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 5 | kanban_details_kanban_detail_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 6 | kanbans_kanban_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 7 | smtp_configs_smtp_config_id_seq | frapid_db_user | bigint | 1 | 1 |  |


**Views**

| # | View Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [custom_field_definition_view](../views/config/custom_field_definition_view.md) | frapid_db_user | DEFAULT |  |
| 2 | [custom_field_view](../views/config/custom_field_view.md) | frapid_db_user | DEFAULT |  |
| 3 | [filter_name_view](../views/config/filter_name_view.md) | frapid_db_user | DEFAULT |  |
| 4 | [smtp_config_scrud_view](../views/config/smtp_config_scrud_view.md) | frapid_db_user | DEFAULT |  |



**Materialized Views**

| # | Matview Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |



**Functions**

| # | Function | Owner | Description |
| --- | --- | --- | --- |
| 1 | [create_custom_field(_form_name character varying, _before_field character varying, _field_order integer, _after_field character varying, _field_name character varying, _field_label character varying, _data_type character varying, _description character varying)RETURNS void](../functions/config/create_custom_field-4455403.md) | frapid_db_user |  |
| 2 | [get_custom_field_form_name(_table_name character varying)RETURNS character varying](../functions/config/get_custom_field_form_name-4455404.md) | frapid_db_user |  |
| 3 | [get_custom_field_setup_id_by_table_name(_schema_name character varying, _table_name character varying, _field_name character varying)RETURNS integer](../functions/config/get_custom_field_setup_id_by_table_name-4455405.md) | frapid_db_user |  |
| 4 | [get_server_timezone()RETURNS character varying](../functions/config/get_server_timezone-4455406.md) | frapid_db_user |  |
| 5 | [get_user_id_by_login_id(_login_id bigint)RETURNS integer](../functions/config/get_user_id_by_login_id-4455407.md) | frapid_db_user |  |



**Triggers**

| # | Trigger | Owner | Description |
| --- | --- | --- | --- |



**Types**

| # | Type | Base Type | Owner | Collation | Default | Type | StoreType | NotNull | Description |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../schemas.md)
* [Table of Contents](../../README.md)