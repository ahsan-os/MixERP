IF OBJECT_ID('core.get_office_id_by_office_name') IS NOT NULL
DROP FUNCTION core.get_office_id_by_office_name;

GO

CREATE FUNCTION core.get_office_id_by_office_name
(
	@office_name national character varying(100)
)
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT core.offices.office_id
		FROM core.offices
		WHERE core.offices.office_name = @office_name
		AND core.offices.deleted = 0
    );
END;

GO