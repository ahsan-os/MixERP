IF OBJECT_ID('core.get_office_code_by_office_id') IS NOT NULL
DROP FUNCTION core.get_office_code_by_office_id;

GO

CREATE FUNCTION core.get_office_code_by_office_id(@office_id integer)
RETURNS national character varying(12)
AS
BEGIN
    RETURN 
	(
		SELECT core.offices.office_code
		FROM core.offices
		WHERE core.offices.office_id = @office_id
		AND core.offices.deleted = 0
	);
END;

GO

SELECT core.get_office_code_by_office_id(1);