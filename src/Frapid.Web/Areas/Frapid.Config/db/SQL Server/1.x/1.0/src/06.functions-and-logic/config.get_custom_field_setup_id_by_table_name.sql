IF OBJECT_ID('config.get_custom_field_setup_id_by_table_name') IS NOT NULL
DROP FUNCTION config.get_custom_field_setup_id_by_table_name;

GO


CREATE FUNCTION config.get_custom_field_setup_id_by_table_name
(
    @schema_name						national character varying(100), 
    @table_name							national character varying(100),
    @field_name							national character varying(100)
)
RETURNS integer
AS
BEGIN
    RETURN 
    (
		SELECT custom_field_setup_id
		FROM config.custom_field_setup
		WHERE form_name = config.get_custom_field_form_name(@schema_name + '.' + @table_name)
		AND field_name = @field_name
		AND config.custom_field_setup.deleted = 0
	);
END;

GO