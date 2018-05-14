IF OBJECT_ID('core.get_currency_code_by_office_id') IS NOT NULL
DROP FUNCTION core.get_currency_code_by_office_id;

GO

CREATE FUNCTION core.get_currency_code_by_office_id(@office_id integer)
RETURNS national character varying(50)
AS
BEGIN
    RETURN 
	(
		SELECT currency_code 
		FROM core.offices
		WHERE core.offices.office_id = @office_id
		AND core.offices.deleted = 0
	);
END;

GO