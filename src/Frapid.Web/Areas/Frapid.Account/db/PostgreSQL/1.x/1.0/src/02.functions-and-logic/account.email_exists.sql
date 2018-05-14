DROP FUNCTION IF EXISTS account.email_exists(_email national character varying(100));

CREATE FUNCTION account.email_exists(_email national character varying(100))
RETURNS bool
AS
$$
    DECLARE _count                          integer;
BEGIN
    SELECT COUNT(*) INTO _count
	FROM account.users 
	WHERE LOWER(email) = LOWER(_email)
	AND NOT account.users.deleted;

    IF(COALESCE(_count, 0) =0) THEN
        SELECT COUNT(*) INTO _count 
		FROM account.registrations 
		WHERE LOWER(email) = LOWER(_email)
		AND NOT account.registrations.deleted;
    END IF;
    
    RETURN COALESCE(_count, 0) > 0;
END
$$
LANGUAGE plpgsql;
