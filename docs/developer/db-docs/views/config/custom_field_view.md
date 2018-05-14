# config.custom_field_view view

| Schema | [config](../../schemas/config.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | custom_field_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW config.custom_field_view
 AS
 SELECT custom_field_forms.table_name,
    custom_field_forms.key_name,
    custom_field_setup.custom_field_setup_id,
    custom_field_setup.form_name,
    custom_field_setup.field_order,
    custom_field_setup.field_name,
    custom_field_setup.field_label,
    custom_field_setup.description,
    custom_field_data_types.underlying_type,
    custom_fields.resource_id,
    custom_fields.value
   FROM config.custom_field_setup
     JOIN config.custom_field_data_types ON custom_field_data_types.data_type::text = custom_field_setup.data_type::text
     JOIN config.custom_field_forms ON custom_field_forms.form_name::text = custom_field_setup.form_name::text
     JOIN config.custom_fields ON custom_fields.custom_field_setup_id = custom_field_setup.custom_field_setup_id
  WHERE NOT custom_field_setup.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

