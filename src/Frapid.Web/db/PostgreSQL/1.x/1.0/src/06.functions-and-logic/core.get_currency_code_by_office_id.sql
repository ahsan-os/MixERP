DROP FUNCTION IF EXISTS core.get_currency_code_by_office_id(_office_id integer);

CREATE FUNCTION core.get_currency_code_by_office_id(_office_id integer)
RETURNS national character varying(50)
AS
$$
BEGIN
    RETURN currency_code 
    FROM core.offices
    WHERE core.offices.office_id = _office_id
	AND NOT core.offices.deleted;
END
$$
LANGUAGE plpgsql;