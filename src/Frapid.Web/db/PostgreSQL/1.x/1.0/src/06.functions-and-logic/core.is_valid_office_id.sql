DROP FUNCTION IF EXISTS core.is_valid_office_id(integer);

CREATE FUNCTION core.is_valid_office_id(integer)
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS(SELECT 1 FROM core.offices WHERE office_id=$1) THEN
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;

SELECT core.is_valid_office_id(1);