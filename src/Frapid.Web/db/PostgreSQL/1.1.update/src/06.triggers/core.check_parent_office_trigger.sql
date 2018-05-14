DROP FUNCTION IF EXISTS core.check_parent_office_trigger() CASCADE;

CREATE FUNCTION core.check_parent_office_trigger()
RETURNS TRIGGER
AS
$$
BEGIN
	IF(NEW.office_id = NEW.parent_office_id) THEN
		RAISE EXCEPTION 'Same office cannot be a parent office';
	END IF;

	RETURN NEW;
END
$$

LANGUAGE plpgSql;

CREATE TRIGGER check_parent_office
AFTER INSERT OR UPDATE
ON core.offices
FOR EACH ROW
EXECUTE PROCEDURE core.check_parent_office_trigger();
