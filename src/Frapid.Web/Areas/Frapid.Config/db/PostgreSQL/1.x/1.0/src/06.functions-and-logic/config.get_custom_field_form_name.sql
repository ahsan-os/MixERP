DROP FUNCTION IF EXISTS config.get_custom_field_form_name
(
    _table_name character varying
);

CREATE FUNCTION config.get_custom_field_form_name
(
    _table_name character varying
)
RETURNS national character varying(500)
STABLE
AS
$$
BEGIN
    RETURN form_name 
    FROM config.custom_field_forms
    WHERE config.custom_field_forms.table_name = _table_name
	AND NOT config.custom_field_forms.deleted;
END
$$
LANGUAGE plpgsql;