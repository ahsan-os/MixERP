DROP VIEW IF EXISTS config.custom_field_definition_view;

CREATE VIEW config.custom_field_definition_view
AS
SELECT
    config.custom_field_forms.table_name,
    config.custom_field_forms.key_name,
    config.custom_field_setup.custom_field_setup_id,
    config.custom_field_setup.form_name,
    config.custom_field_setup.field_order,
    config.custom_field_setup.field_name,
    config.custom_field_setup.field_label,
    config.custom_field_setup.description,
    config.custom_field_data_types.data_type,
    config.custom_field_data_types.underlying_type,
    ''::text AS resource_id,
    ''::text AS value
FROM config.custom_field_setup
INNER JOIN config.custom_field_data_types
ON config.custom_field_data_types.data_type = config.custom_field_setup.data_type
INNER JOIN config.custom_field_forms
ON config.custom_field_forms.form_name = config.custom_field_setup.form_name
WHERE NOT config.custom_field_setup.deleted
ORDER BY field_order;