
IF OBJECT_ID('config.get_custom_field_definition') IS NOT NULL
DROP PROCEDURE config.get_custom_field_definition;

GO


CREATE PROCEDURE config.get_custom_field_definition
(
    @table_name             national character varying(500),
    @resource_id            national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	DECLARE @result TABLE
	(
		table_name              national character varying(100),
		key_name                national character varying(100),
		custom_field_setup_id   integer,
		form_name               national character varying(100),
		field_order             integer,
		field_name              national character varying(100),
		field_label             national character varying(100),
		description             national character varying(500),
		data_type               national character varying(50),
		underlying_type			national character varying(100),
		resource_id             national character varying(500),
		value                   national character varying(500)
	);


    
    INSERT INTO @result
    SELECT * FROM config.custom_field_definition_view
    WHERE config.custom_field_definition_view.table_name = @table_name
    ORDER BY field_order;

    UPDATE @result
    SET resource_id = @resource_id;

    UPDATE @result
    SET value = config.custom_fields.value
    FROM @result result
    INNER JOIN config.custom_fields
    ON result.custom_field_setup_id = config.custom_fields.custom_field_setup_id
    WHERE config.custom_fields.resource_id = @resource_id;
    
    SELECT * FROM @result;
    RETURN;
END;

GO
