# config.custom_field_setup table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | custom_field_setup_id | [ ] | integer | 0 |  |
| 2 | form_name | [ ] | character varying | 100 |  |
| 3 | before_field | [x] | character varying | 500 |  |
| 4 | field_order | [ ] | integer | 0 |  |
| 5 | after_field | [x] | character varying | 500 |  |
| 6 | field_name | [ ] | character varying | 100 |  |
| 7 | field_label | [ ] | character varying | 200 |  |
| 8 | data_type | [x] | character varying | 50 |  |
| 9 | description | [ ] | text | 0 |  |
| 10 | audit_user_id | [x] | integer | 0 |  |
| 11 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 12 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [form_name](../config/custom_field_forms.md) | custom_field_setup_form_name_fkey | config.custom_field_forms.form_name |
| 8 | [data_type](../config/custom_field_data_types.md) | custom_field_setup_data_type_fkey | config.custom_field_data_types.data_type |
| 10 | [audit_user_id](../account/users.md) | custom_field_setup_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| custom_field_setup_pkey | frapid_db_user | btree | custom_field_setup_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | custom_field_setup_id | nextval('config.custom_field_setup_custom_field_setup_id_seq'::regclass) |
| 4 | field_order | 0 |
| 11 | audit_ts | now() |
| 12 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
