DROP FUNCTION IF EXISTS account.can_confirm_registration(_token uuid);

CREATE FUNCTION account.can_confirm_registration(_token uuid)
RETURNS boolean
STABLE
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.registrations
        WHERE account.registrations.registration_id = _token
        AND NOT account.registrations.confirmed
		AND NOT account.registrations.deleted
    ) THEN
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;
