DROP FUNCTION IF EXISTS core.get_office_name_by_office_id(_office_id integer);

CREATE FUNCTION core.get_office_name_by_office_id(_office_id integer)
RETURNS text
AS
$$
BEGIN
    RETURN core.offices.office_name
    FROM core.offices
    WHERE core.offices.office_id = _office_id
	AND NOT core.offices.deleted;
END
$$
LANGUAGE plpgsql;