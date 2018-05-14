DROP FUNCTION IF EXISTS core.get_office_id_by_office_name(_office_name text);

CREATE FUNCTION core.get_office_id_by_office_name(_office_name text)
RETURNS integer
AS
$$
BEGIN
    RETURN core.offices.office_id
    FROM core.offices
    WHERE core.offices.office_name = _office_name
	AND NOT core.offices.deleted;
END
$$
LANGUAGE plpgsql;