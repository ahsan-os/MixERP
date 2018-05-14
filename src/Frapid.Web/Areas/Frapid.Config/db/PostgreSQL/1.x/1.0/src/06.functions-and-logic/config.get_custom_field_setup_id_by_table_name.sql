DROP FUNCTION IF EXISTS config.get_custom_field_setup_id_by_table_name
(
    _schema_name national character varying(100), 
    _table_name national character varying(100),
    _field_name national character varying(100)
);

CREATE FUNCTION config.get_custom_field_setup_id_by_table_name
(
    _schema_name national character varying(100), 
    _table_name national character varying(100),
    _field_name national character varying(100)
)
RETURNS integer
AS
$$
BEGIN
    RETURN custom_field_setup_id
    FROM config.custom_field_setup
    WHERE config.custom_field_setup.form_name = config.get_custom_field_form_name(_schema_name, _table_name)
    AND config.custom_field_setup.field_name = _field_name
	AND NOT config.custom_field_setup.deleted;
END
$$
LANGUAGE plpgsql;
