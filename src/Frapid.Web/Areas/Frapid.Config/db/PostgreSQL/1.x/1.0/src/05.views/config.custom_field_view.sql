DROP VIEW IF EXISTS config.custom_field_view;

CREATE VIEW config.custom_field_view 
AS 
SELECT 
	custom_field_forms.table_name,
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
INNER JOIN config.custom_field_data_types ON custom_field_data_types.data_type = custom_field_setup.data_type
INNER JOIN config.custom_field_forms ON custom_field_forms.form_name = custom_field_setup.form_name
INNER JOIN config.custom_fields ON custom_fields.custom_field_setup_id = custom_field_setup.custom_field_setup_id
WHERE NOT config.custom_field_setup.deleted;
