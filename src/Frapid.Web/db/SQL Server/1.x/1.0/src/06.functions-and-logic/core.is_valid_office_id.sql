IF OBJECT_ID('core.is_valid_office_id') IS NOT NULL
DROP FUNCTION core.is_valid_office_id;

GO

CREATE FUNCTION core.is_valid_office_id(@office_id integer)
RETURNS bit
AS
BEGIN
    IF EXISTS(SELECT 1 FROM core.offices WHERE office_id=@office_id)
	BEGIN
        RETURN 1;
    END;

    RETURN 0;
END;

GO

--SELECT core.is_valid_office_id(1);