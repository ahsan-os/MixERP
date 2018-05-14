﻿DROP FUNCTION IF EXISTS account.sign_in
(
    _email                                  text,
    _office_id                              integer,
    _browser                                text,
    _ip_address                             text,
    _culture                                text
);

CREATE FUNCTION account.sign_in
(
    _email                                  text,
    _office_id                              integer,
    _browser                                text,
    _ip_address                             text,
    _culture                                text
)
RETURNS TABLE
(
    login_id                                bigint,
    status                                  boolean,
    message                                 text
)
AS
$$
    DECLARE _login_id                       bigint;
    DECLARE _user_id                        integer;
BEGIN
    IF(COALESCE(_office_id, 0) = 0) THEN
        IF(SELECT COUNT(*) = 1 FROM core.offices) THEN
            SELECT office_id INTO _office_id
            FROM core.offices;
        END IF;
    END IF;

    IF account.is_restricted_user(_email) THEN
        RETURN QUERY
        SELECT NULL::bigint, false, 'Access is denied'::text;

        RETURN;
    END IF;

    SELECT user_id INTO _user_id
    FROM account.users
    WHERE email = _email;

	UPDATE account.logins 
	SET is_active = false 
	WHERE user_id=_user_id 
	AND office_id = _office_id 
	AND browser = _browser
	AND ip_address = _ip_address;

    INSERT INTO account.logins(user_id, office_id, browser, ip_address, login_timestamp, culture)
    SELECT _user_id, _office_id, _browser, _ip_address, NOW(), COALESCE(_culture, '')
    RETURNING account.logins.login_id INTO _login_id;
    
    RETURN QUERY
    SELECT _login_id, true, 'Welcome'::text;
    RETURN;    
END
$$
LANGUAGE plpgsql;