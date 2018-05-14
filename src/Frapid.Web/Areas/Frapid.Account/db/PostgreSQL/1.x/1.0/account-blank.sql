-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
DROP SCHEMA IF EXISTS account CASCADE;
CREATE SCHEMA account;

CREATE TABLE account.roles
(
    role_id                                 integer PRIMARY KEY,
    role_name                               national character varying(100) NOT NULL UNIQUE,
    is_administrator                        boolean NOT NULL DEFAULT(false),
    audit_user_id                           integer,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)    
);

CREATE TABLE account.registrations
(
    registration_id                         uuid PRIMARY KEY DEFAULT(gen_random_uuid()),
    name                                    national character varying(100),
    email                                   national character varying(100) NOT NULL,
    phone                                   national character varying(100),
    password                                text,
    browser                                 text,
    ip_address                              national character varying(50),
    registered_on                           TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
    confirmed                               boolean DEFAULT(false),
    confirmed_on                            TIMESTAMP WITH TIME ZONE,
    audit_user_id                           integer,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

CREATE UNIQUE INDEX registrations_email_uix
ON account.registrations(LOWER(email))
WHERE NOT deleted;

CREATE TABLE account.users
(
    user_id                                 SERIAL PRIMARY KEY,
    email                                   national character varying(100) NOT NULL,
    password                                text,
    office_id                               integer NOT NULL REFERENCES core.offices,
    role_id                                 integer NOT NULL REFERENCES account.roles,
    name                                    national character varying(100),
    phone                                   national character varying(100),
    status                                  boolean DEFAULT(true),
    created_on                              TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
	last_seen_on							TIMESTAMP WITH TIME ZONE,
	last_ip									text,
	last_browser							text,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)    
);


CREATE UNIQUE INDEX users_email_uix
ON account.users(LOWER(email))
WHERE NOT deleted;

CREATE TABLE account.installed_domains
(
    domain_id                               SERIAL NOT NULL PRIMARY KEY,
    domain_name                             national character varying(500),
    admin_email                             national character varying(500),
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

CREATE UNIQUE INDEX installed_domains_domain_name_uix
ON account.installed_domains(LOWER(domain_name))
WHERE NOT deleted;


CREATE TABLE account.configuration_profiles
(
    configuration_profile_id                SERIAL PRIMARY KEY,
    profile_name                            national character varying(100) NOT NULL UNIQUE,
    is_active                               boolean NOT NULL DEFAULT(true),    
    allow_registration                      boolean NOT NULL DEFAULT(true),
    registration_office_id                  integer NOT NULL REFERENCES core.offices,
    registration_role_id                    integer NOT NULL REFERENCES account.roles,
    allow_facebook_registration             boolean NOT NULL DEFAULT(true),
    allow_google_registration               boolean NOT NULL DEFAULT(true),
    google_signin_client_id                 text,
    google_signin_scope                     text,
    facebook_app_id                         text,
    facebook_scope                          text,
    audit_user_id                           integer,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)    
);


CREATE UNIQUE INDEX configuration_profile_uix
ON account.configuration_profiles(is_active)
WHERE is_active
AND NOT deleted;


ALTER TABLE account.configuration_profiles
ADD FOREIGN KEY(audit_user_id) REFERENCES account.users;

ALTER TABLE account.roles
ADD FOREIGN KEY(audit_user_id) REFERENCES account.users;

CREATE TABLE account.reset_requests
(
    request_id                              uuid PRIMARY KEY DEFAULT(gen_random_uuid()),
    user_id                                 integer NOT NULL REFERENCES account.users,
    email                                   text,
    name                                    text,
    requested_on                            TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
    expires_on                              TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW() + INTERVAL '24 hours'),
    browser                                 text,
    ip_address                              national character varying(50),
    confirmed                               boolean DEFAULT(false),
    confirmed_on                            TIMESTAMP WITH TIME ZONE,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);




CREATE TABLE account.fb_access_tokens
(
    user_id                                 integer PRIMARY KEY REFERENCES account.users,
    fb_user_id                              text,
    token                                   text,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);


CREATE TABLE account.google_access_tokens
(
    user_id                                 integer PRIMARY KEY REFERENCES account.users,
    token                                   text,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

CREATE TABLE account.logins
(
    login_id                                BIGSERIAL PRIMARY KEY,
    user_id                                 integer REFERENCES account.users,
    office_id                               integer REFERENCES core.offices,
    browser                                 text,
    ip_address                              national character varying(50),
    is_active                               boolean NOT NULL DEFAULT(true),
    login_timestamp                         TIMESTAMP WITH TIME ZONE NOT NULL 
                                            DEFAULT(NOW()),
    culture                                 national character varying(12) NOT NULL,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

DROP TABLE IF EXISTS account.access_tokens;
DROP TABLE IF EXISTS account.applications;

CREATE TABLE account.applications
(
    application_id                              uuid DEFAULT(gen_random_uuid()) PRIMARY KEY,
    application_name                            national character varying(100) NOT NULL,
    display_name                                national character varying(100),
    version_number                              national character varying(100),
    publisher                                   national character varying(100) NOT NULL,
    published_on                                date,
    application_url                             national character varying(500),
    description                                 text,
    browser_based_app                           boolean NOT NULL,
    privacy_policy_url                          national character varying(500),
    terms_of_service_url                        national character varying(500),
    support_email                               national character varying(100),
    culture                                     national character varying(12),
    redirect_url                                national character varying(500),
    app_secret                                  text UNIQUE,
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)        
);

CREATE UNIQUE INDEX applications_app_name_uix
ON account.applications(LOWER(application_name))
WHERE NOT deleted;

CREATE TABLE account.access_tokens
(
    access_token_id                             uuid DEFAULT(gen_random_uuid()) PRIMARY KEY,
    issued_by                                   text NOT NULL,
    audience                                    text NOT NULL,
    ip_address                                  text,
    user_agent                                  text,
    header                                      text,
    subject                                     text,
    token_id                                    text,
    application_id                              uuid NULL REFERENCES account.applications,
    login_id                                    bigint NOT NULL REFERENCES account.logins,
    client_token                                text NOT NULL UNIQUE,
    claims                                      text,
    created_on                                  TIMESTAMP WITH TIME ZONE NOT NULL,
    expires_on                                  TIMESTAMP WITH TIME ZONE NOT NULL,
    revoked                                     boolean NOT NULL DEFAULT(false),
    revoked_by                                  integer REFERENCES account.users,
    revoked_on                                  TIMESTAMP WITH TIME ZONE,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

CREATE INDEX access_tokens_token_info_inx
ON account.access_tokens(client_token, ip_address, user_agent);


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.add_installed_domain.sql --<--<--
DROP FUNCTION IF EXISTS account.add_installed_domain
(
    _domain_name                                text,
    _admin_email                                text
);

CREATE FUNCTION account.add_installed_domain
(
    _domain_name                                text,
    _admin_email                                text
)
RETURNS void
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT * 
		FROM account.installed_domains
        WHERE account.installed_domains.domain_name = _domain_name        
    ) THEN
        UPDATE account.installed_domains
        SET admin_email = _admin_email
        WHERE domain_name = _domain_name;
        
        RETURN;
    END IF;

    INSERT INTO account.installed_domains(domain_name, admin_email)
    SELECT _domain_name, _admin_email;
END
$$
LANGUAGE plpgsql;

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.can_confirm_registration.sql --<--<--
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


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.can_register_with_facebook.sql --<--<--
DROP FUNCTION IF EXISTS account.can_register_with_facebook();

CREATE FUNCTION account.can_register_with_facebook()
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 1 
		FROM account.configuration_profiles
        WHERE account.configuration_profiles.is_active
        AND account.configuration_profiles.allow_registration
        AND account.configuration_profiles.allow_facebook_registration
		AND NOT account.configuration_profiles.deleted
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.can_register_with_google.sql --<--<--
DROP FUNCTION IF EXISTS account.can_register_with_google();

CREATE FUNCTION account.can_register_with_google()
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 1 
		FROM account.configuration_profiles
        WHERE account.configuration_profiles.is_active
        AND account.configuration_profiles.allow_registration
        AND account.configuration_profiles.allow_google_registration
		AND NOT account.configuration_profiles.deleted
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.complete_reset.sql --<--<--
DROP FUNCTION IF EXISTS account.complete_reset
(
    _request_id                     uuid,
    _password                       text
);

CREATE FUNCTION account.complete_reset
(
    _request_id                     uuid,
    _password                       text
)
RETURNS void
AS
$$
    DECLARE _user_id                integer;
    DECLARE _email                  text;
BEGIN
    SELECT
        account.users.user_id,
        account.users.email
    INTO
        _user_id,
        _email
    FROM account.reset_requests
    INNER JOIN account.users
    ON account.users.user_id = account.reset_requests.user_id
    WHERE account.reset_requests.request_id = _request_id
    AND expires_on >= NOW();
    
    UPDATE account.users
    SET
        password = _password
    WHERE user_id = _user_id;

    UPDATE account.reset_requests
    SET confirmed = true, confirmed_on = NOW()
    WHERE user_id = _user_id;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.confirm_registration.sql --<--<--
DROP FUNCTION IF EXISTS account.confirm_registration(_token uuid);

CREATE FUNCTION account.confirm_registration(_token uuid)
RETURNS boolean
AS
$$
    DECLARE _can_confirm        boolean;
    DECLARE _office_id          integer;
    DECLARE _role_id            integer;
BEGIN
    _can_confirm := account.can_confirm_registration(_token);

    IF(NOT _can_confirm) THEN
        RETURN false;
    END IF;

    SELECT
        registration_office_id
    INTO
        _office_id
    FROM account.configuration_profiles
    WHERE is_active
    LIMIT 1;

    INSERT INTO account.users(email, password, office_id, role_id, name, phone)
    SELECT email, password, _office_id, account.get_registration_role_id(email), name, phone
    FROM account.registrations
    WHERE registration_id = _token
	AND NOT confirmed;

    UPDATE account.registrations
    SET 
        confirmed = true, 
        confirmed_on = NOW()
    WHERE registration_id = _token;
    
    RETURN true;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.email_exists.sql --<--<--
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


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.fb_sign_in.sql --<--<--
DROP FUNCTION IF EXISTS account.fb_sign_in
(
    _fb_user_id                             text,
    _email                                  text,
    _office_id                              integer,
    _name                                   text,
    _token                                  text,
    _browser                                text,
    _ip_address                             text,
    _culture                                text
);

CREATE FUNCTION account.fb_sign_in
(
    _fb_user_id                             text,
    _email                                  text,
    _office_id                              integer,
    _name                                   text,
    _token                                  text,
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
    DECLARE _user_id                        integer;
    DECLARE _login_id                       bigint;
    DECLARE _auto_register                  boolean = false;
BEGIN
    IF(COALESCE(_office_id, 0) = 0) THEN
        IF(SELECT COUNT(*) = 1 FROM core.offices) THEN
            SELECT office_id INTO _office_id
            FROM core.offices;
        END IF;
    END IF;

    IF account.is_restricted_user(_email) THEN
        --LOGIN IS RESTRICTED TO THIS USER
        RETURN QUERY
        SELECT NULL::bigint, false, 'Access is denied'::text;

        RETURN;
    END IF;

    SELECT user_id INTO _user_id
    FROM account.users
    WHERE LOWER(account.users.email) = LOWER(_email);

    IF NOT account.user_exists(_email) AND account.can_register_with_facebook() THEN
        INSERT INTO account.users(role_id, office_id, email, name)
        SELECT account.get_registration_role_id(_email), account.get_registration_office_id(), _email, _name
        RETURNING user_id INTO _user_id;
    END IF;

    IF NOT account.fb_user_exists(_user_id) THEN
        INSERT INTO account.fb_access_tokens(user_id, fb_user_id, token)
        SELECT COALESCE(_user_id, account.get_user_id_by_email(_email)), _fb_user_id, _token;
    ELSE
        UPDATE account.fb_access_tokens
        SET token = _token
        WHERE user_id = _user_id;    
    END IF;

    IF(_user_id IS NULL) THEN
        SELECT user_id INTO _user_id
        FROM account.users
        WHERE LOWER(account.users.email) = LOWER(_email);
    END IF;
    
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


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.fb_user_exists.sql --<--<--
DROP FUNCTION IF EXISTS account.fb_user_exists(_user_id integer);

CREATE FUNCTION account.fb_user_exists(_user_id integer)
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.fb_access_tokens
        WHERE account.fb_access_tokens.user_id = _user_id
		AND NOT account.fb_access_tokens.deleted
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.get_email_by_user_id.sql --<--<--
DROP FUNCTION IF EXISTS account.get_email_by_user_id(_user_id integer);

CREATE FUNCTION account.get_email_by_user_id(_user_id integer)
RETURNS text
STABLE
AS
$$
BEGIN
    RETURN
        account.users.email
    FROM account.users
    WHERE account.users.user_id = _user_id
	AND NOT account.users.deleted;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.get_name_by_user_id.sql --<--<--
DROP FUNCTION IF EXISTS account.get_name_by_user_id(_user_id integer);

CREATE FUNCTION account.get_name_by_user_id(_user_id integer)
RETURNS national character varying(100)
STABLE
AS
$$
BEGIN
    RETURN
        account.users.name
    FROM account.users
    WHERE account.users.user_id = _user_id
	AND NOT account.users.deleted;
END
$$
LANGUAGE plpgsql;




-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.get_office_id_by_login_id.sql --<--<--
DROP FUNCTION IF EXISTS account.get_office_id_by_login_id(_login_id bigint);

CREATE FUNCTION account.get_office_id_by_login_id(_login_id bigint)
RETURNS integer
AS
$$
BEGIN
	RETURN
	(
		SELECT account.logins.office_id 
		FROM account.logins
		WHERE account.logins.login_id = _login_id
	);
END;
$$
LANGUAGE plpgsql;

--SELECT account.get_office_id_by_login_id(1);


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.get_registration_office_id.sql --<--<--
DROP FUNCTION IF EXISTS account.get_registration_office_id();

CREATE FUNCTION account.get_registration_office_id()
RETURNS integer
AS
$$
BEGIN
    RETURN account.configuration_profiles.registration_office_id
    FROM account.configuration_profiles
    WHERE account.configuration_profiles.is_active
	AND NOT account.configuration_profiles.deleted;
END
$$
LANGUAGE plpgsql;

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.get_registration_role_id.sql --<--<--
DROP FUNCTION IF EXISTS account.get_registration_role_id(_email text);

CREATE FUNCTION account.get_registration_role_id(_email text)
RETURNS integer
STABLE
AS
$$
    DECLARE _is_admin               boolean = false;
    DECLARE _role_id                integer;
BEGIN
    IF EXISTS
    (
        SELECT * 
		FROM account.installed_domains
        WHERE account.installed_domains.admin_email = _email
		AND NOT account.installed_domains.deleted
    ) THEN
        _is_admin = true;
    END IF;
   
    IF(_is_admin) THEN
        SELECT
            account.roles.role_id
        INTO
            _role_id
        FROM account.roles
        WHERE account.roles.is_administrator
		AND NOT account.roles.deleted
        LIMIT 1;
    ELSE
        SELECT 
            account.configuration_profiles.registration_role_id
        INTO
            _role_id
        FROM account.configuration_profiles
        WHERE account.configuration_profiles.is_active
		AND NOT account.configuration_profiles.deleted;
    END IF;

    RETURN _role_id;
END
$$
LANGUAGE plpgsql;

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.get_role_name_by_role_id.sql --<--<--
DROP FUNCTION IF EXISTS account.get_role_name_by_role_id(_role_id integer);

CREATE FUNCTION account.get_role_name_by_role_id(_role_id integer)
RETURNS national character varying(100)
AS
$$
BEGIN
    RETURN
    (
        SELECT account.roles.role_name
        FROM account.roles
        WHERE account.roles.role_id = _role_id
    );
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.get_user_id_by_email.sql --<--<--
DROP FUNCTION IF EXISTS account.get_user_id_by_email(_email national character varying(100));

CREATE FUNCTION account.get_user_id_by_email(_email national character varying(100))
RETURNS integer
AS
$$
BEGIN
    RETURN user_id
    FROM account.users
    WHERE LOWER(account.users.email) = LOWER(_email)
	AND NOT account.users.deleted;	
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.get_user_id_by_login_id.sql --<--<--
DROP FUNCTION IF EXISTS account.get_user_id_by_login_id(_login_id bigint);

CREATE FUNCTION account.get_user_id_by_login_id(_login_id bigint)
RETURNS integer
AS
$$
BEGIN
    RETURN user_id
    FROM account.logins
    WHERE account.logins.login_id = _login_id
    AND NOT account.logins.deleted;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.google_sign_in.sql --<--<--
DROP FUNCTION IF EXISTS account.google_sign_in
(
    _email                                  text,
    _office_id                              integer,
    _name                                   text,
    _token                                  text,
    _browser                                text,
    _ip_address                             text,
    _culture                                text
);

CREATE FUNCTION account.google_sign_in
(
    _email                                  text,
    _office_id                              integer,
    _name                                   text,
    _token                                  text,
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
    DECLARE _user_id                        integer;
    DECLARE _login_id                       bigint;
BEGIN    
    IF(COALESCE(_office_id, 0) = 0) THEN
        IF(SELECT COUNT(*) = 1 FROM core.offices) THEN
            SELECT office_id INTO _office_id
            FROM core.offices;
        END IF;
    END IF;

    IF account.is_restricted_user(_email) THEN
        --LOGIN IS RESTRICTED TO THIS USER
        RETURN QUERY
        SELECT NULL::bigint, false, 'Access is denied';

        RETURN;
    END IF;

    IF NOT account.user_exists(_email) AND account.can_register_with_google() THEN
        INSERT INTO account.users(role_id, office_id, email, name)
        SELECT account.get_registration_role_id(_email), account.get_registration_office_id(), _email, _name
        RETURNING user_id INTO _user_id;
    END IF;

    SELECT user_id INTO _user_id
    FROM account.users
    WHERE LOWER(account.users.email) = LOWER(_email);

    IF NOT account.google_user_exists(_user_id) THEN
        INSERT INTO account.google_access_tokens(user_id, token)
        SELECT COALESCE(_user_id, account.get_user_id_by_email(_email)), _token;
    ELSE
        UPDATE account.google_access_tokens
        SET token = _token
        WHERE user_id = _user_id;    
    END IF;

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


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.google_user_exists.sql --<--<--
DROP FUNCTION IF EXISTS account.google_user_exists(_user_id integer);

CREATE FUNCTION account.google_user_exists(_user_id integer)
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.google_access_tokens
        WHERE account.google_access_tokens.user_id = _user_id
		AND NOT account.google_access_tokens.deleted		
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.has_account.sql --<--<--
DROP FUNCTION IF EXISTS account.has_account(_email national character varying(100));

CREATE FUNCTION account.has_account(_email national character varying(100))
RETURNS bool
AS
$$
    DECLARE _count                          integer;
BEGIN
    SELECT COUNT(*) INTO _count 
	FROM account.users 
	WHERE lower(email) = LOWER(_email)
	AND NOT account.users.deleted;
	
    RETURN COALESCE(_count, 0) = 1;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.has_active_reset_request.sql --<--<--
DROP FUNCTION IF EXISTS account.has_active_reset_request(_email text);

CREATE FUNCTION account.has_active_reset_request(_email text)
RETURNS boolean
AS
$$
    DECLARE _expires_on                     TIMESTAMP WITH TIME ZONE = NOW() + INTERVAL '24 Hours';
BEGIN
    IF EXISTS
    (
        SELECT * FROM account.reset_requests
        WHERE LOWER(email) = LOWER(_email)
        AND expires_on <= _expires_on
		AND NOT account.reset_requests.deleted
    ) THEN        
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.is_admin.sql --<--<--
DROP FUNCTION IF EXISTS account.is_admin(_user_id integer);

CREATE FUNCTION account.is_admin(_user_id integer)
RETURNS boolean
AS
$$
BEGIN
    RETURN
    (
        SELECT account.roles.is_administrator FROM account.users
        INNER JOIN account.roles
        ON account.users.role_id = account.roles.role_id
        WHERE account.users.user_id=$1
    );
END
$$
LANGUAGE PLPGSQL;

--SELECT * FROM account.is_admin(1);



-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.is_restricted_user.sql --<--<--
DROP FUNCTION IF EXISTS account.is_restricted_user(_email national character varying(100));

CREATE FUNCTION account.is_restricted_user(_email national character varying(100))
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.users
        WHERE LOWER(account.users.email) = LOWER(_email)
        AND NOT account.users.status
		AND NOT account.users.deleted
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.is_valid_client_token.sql --<--<--
DROP FUNCTION IF EXISTS account.is_valid_client_token(_client_token text, _ip_address text, _user_agent text);

CREATE FUNCTION account.is_valid_client_token(_client_token text, _ip_address text, _user_agent text)
RETURNS bool
STABLE
AS
$$
    DECLARE _created_on TIMESTAMP WITH TIME ZONE;
    DECLARE _expires_on TIMESTAMP WITH TIME ZONE;
    DECLARE _revoked boolean;
BEGIN
    IF(COALESCE(_client_token, '') = '') THEN
        RETURN false;
    END IF;

    SELECT
        created_on,
        expires_on,
        revoked
    INTO
        _created_on,
        _expires_on,
        _revoked    
    FROM account.access_tokens
    WHERE client_token = _client_token
    AND ip_address = _ip_address
    AND user_agent = _user_agent;
    
    IF(COALESCE(_revoked, true)) THEN
        RETURN false;
    END IF;

    IF(_created_on > NOW()) THEN
        RETURN false;
    END IF;

    IF(COALESCE(_expires_on, NOW()) <= NOW()) THEN
        RETURN false;
    END IF;
    
    RETURN true;
END
$$
LANGUAGE plpgsql;

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.is_valid_login_id.sql --<--<--
DROP FUNCTION IF EXISTS account.is_valid_login_id(bigint);

CREATE FUNCTION account.is_valid_login_id(bigint)
RETURNS boolean
STABLE
AS
$$
BEGIN
    IF EXISTS(SELECT 1 FROM account.logins WHERE login_id=$1) THEN
            RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;

--SELECT account.is_valid_login_id(1);

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.reset_account.sql --<--<--
DROP FUNCTION IF EXISTS account.reset_account
(
    _email                                  text,
    _browser                                text,
    _ip_address                             text
);

CREATE FUNCTION account.reset_account
(
    _email                                  text,
    _browser                                text,
    _ip_address                             text
)
RETURNS SETOF account.reset_requests
AS
$$
    DECLARE _request_id                     uuid;
    DECLARE _user_id                        integer;
    DECLARE _name                           text;
    DECLARE _expires_on                     TIMESTAMP WITH TIME ZONE = NOW() + INTERVAL '24 Hours';
BEGIN
    IF(NOT account.user_exists(_email) OR account.is_restricted_user(_email)) THEN
        RETURN;
    END IF;

    SELECT
        user_id,
        name
    INTO
        _user_id,
        _name
    FROM account.users
    WHERE LOWER(email) = LOWER(_email);

    IF account.has_active_reset_request(_email) THEN
        RETURN QUERY
        SELECT * FROM account.reset_requests
        WHERE LOWER(email) = LOWER(_email)
        AND expires_on <= _expires_on
        LIMIT 1;
        
        RETURN;
    END IF;

    INSERT INTO account.reset_requests(user_id, email, name, browser, ip_address, expires_on)
    SELECT _user_id, _email, _name, _browser, _ip_address, _expires_on
    RETURNING request_id INTO _request_id;

    RETURN QUERY
    SELECT *
    FROM account.reset_requests
    WHERE request_id = _request_id;

    RETURN;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.sign_in.sql --<--<--
DROP FUNCTION IF EXISTS account.sign_in
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

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/account.user_exists.sql --<--<--
DROP FUNCTION IF EXISTS account.user_exists(_email national character varying(100));

CREATE FUNCTION account.user_exists(_email national character varying(100))
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.users
        WHERE LOWER(account.users.email) = LOWER(_email)
		AND NOT account.users.deleted
    ) THEN
        RETURN true;
    END IF;
    
    RETURN false;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.relationships/auth.sql --<--<--


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.relationships/core.sql --<--<--
ALTER TABLE core.offices
ADD FOREIGN KEY(audit_user_id) REFERENCES account.users;

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/02.triggers/account.token_auto_expiry_trigger.sql --<--<--
DROP FUNCTION IF EXISTS account.token_auto_expiry_trigger() CASCADE;

CREATE FUNCTION account.token_auto_expiry_trigger()
RETURNS trigger
AS
$$
BEGIN
    UPDATE account.access_tokens
    SET 
        revoked = true,
        revoked_on = NOW()
    WHERE ip_address = NEW.ip_address
    AND user_agent = NEW.user_agent;

    RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER account_token_auto_expiry_trigger
BEFORE INSERT
ON account.access_tokens
FOR EACH ROW
EXECUTE PROCEDURE account.token_auto_expiry_trigger();


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/03.menus/menus.sql --<--<--
SELECT * FROM core.create_app('Frapid.Account', 'Account', 'Account', '1.0', 'MixERP Inc.', 'December 1, 2015', 'grey lock', '/dashboard/account/user/list', NULL::text[]);

SELECT * FROM core.create_menu('Frapid.Account', 'Roles', 'Roles', '/dashboard/account/roles', 'users', '');
SELECT * FROM core.create_menu('Frapid.Account', 'Users', 'Users', '', 'user', '');
SELECT * FROM core.create_menu('Frapid.Account', 'AddNewUser', 'Add a New User', '/dashboard/account/user/add', 'user', 'Users');
SELECT * FROM core.create_menu('Frapid.Account', 'ChangePassword', 'Change Password', '/dashboard/account/user/change-password', 'user', 'Users');
SELECT * FROM core.create_menu('Frapid.Account', 'ListUsers', 'List Users', '/dashboard/account/user/list', 'user', 'Users');

SELECT * FROM core.create_menu('Frapid.Account', 'ConfigurationProfile', 'Configuration Profile', '/dashboard/account/configuration-profile', 'configure', '');
SELECT * FROM core.create_menu('Frapid.Account', 'EmailTemplates', 'Email Templates', '', 'mail', '');
SELECT * FROM core.create_menu('Frapid.Account', 'AccountVerification', 'Account Verification', '/dashboard/account/email-templates/account-verification', 'checkmark box', 'Email Templates');
SELECT * FROM core.create_menu('Frapid.Account', 'PasswordReset', 'Password Reset', '/dashboard/account/email-templates/password-reset', 'key', 'Email Templates');
SELECT * FROM core.create_menu('Frapid.Account', 'WelcomeEmail', 'Welcome Email', '/dashboard/account/email-templates/welcome-email', 'star', 'Email Templates');
SELECT * FROM core.create_menu('Frapid.Account', 'WelcomeEmail3rdParty', 'Welcome Email (3rd Party)', '/dashboard/account/email-templates/welcome-email-other', 'star outline', 'Email Templates');


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/04.default-values/01.default-values.sql --<--<--
INSERT INTO account.roles
SELECT 1000,   'Guest',                 false UNION ALL
SELECT 2000,   'Website User',          false UNION ALL
SELECT 3000,   'Partner',               false UNION ALL
SELECT 4000,   'Content Editor',        false UNION ALL
SELECT 5000,   'Backoffice User',       false UNION ALL
SELECT 9999,   'Admin',                 true;


INSERT INTO account.configuration_profiles(profile_name, is_active, allow_registration, registration_role_id, registration_office_id)
SELECT 'Default', true, true, 2000, 1;

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/05.scrud-views/account.configuration_profile_scrud_view.sql --<--<--
DROP VIEW IF EXISTS account.configuration_profile_scrud_view;

CREATE VIEW account.configuration_profile_scrud_view
AS
SELECT
	account.configuration_profiles.configuration_profile_id,
	account.configuration_profiles.profile_name,
	account.configuration_profiles.is_active,
	account.configuration_profiles.allow_registration,
	account.roles.role_name AS defult_role,
	core.offices.office_code || ' (' || core.offices.office_name || ')' AS default_office
FROM account.configuration_profiles
LEFT JOIN account.roles
ON account.roles.role_id = account.configuration_profiles.registration_role_id
LEFT JOIN core.offices
ON core.offices.office_id = account.configuration_profiles.registration_office_id
WHERE NOT account.configuration_profiles.deleted;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/05.scrud-views/account.user_scrud_view.sql --<--<--
DROP VIEW IF EXISTS account.user_scrud_view;

CREATE VIEW account.user_scrud_view
AS
SELECT
	account.users.user_id,
	account.users.email,
	account.users.name,
	account.users.phone,
	core.offices.office_code || ' (' || core.offices.office_name || ')' AS office,
	account.roles.role_name
FROM account.users
INNER JOIN account.roles
ON account.roles.role_id = account.users.role_id
INNER JOIN core.offices
ON core.offices.office_id = account.users.office_id
WHERE NOT account.users.deleted;


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/05.scrud-views/account.user_selector_view.sql --<--<--
DROP VIEW IF EXISTS account.user_selector_view;

CREATE VIEW account.user_selector_view
AS
SELECT
    account.users.user_id,
    account.users.name AS user_name
FROM account.users
WHERE NOT account.users.deleted;

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/05.views/account.sign_in_view.sql --<--<--
DROP VIEW IF EXISTS account.sign_in_view;

CREATE VIEW account.sign_in_view
AS
SELECT
    account.logins.login_id,
    account.users.name,
    account.users.email,
    account.logins.user_id,
    account.roles.role_id,
    account.roles.role_name,
    account.roles.is_administrator,
    account.logins.browser,
    account.logins.ip_address,
    account.logins.login_timestamp,
    account.logins.culture,
    account.logins.is_active,
    account.logins.office_id,
    core.offices.office_code,
    core.offices.office_name,
    core.offices.office_code || ' (' || core.offices.office_name || ')' AS office,
    core.offices.logo,
    core.offices.registration_date,
    core.offices.po_box,
    core.offices.address_line_1,
    core.offices.address_line_2,
    core.offices.street,
    core.offices.city,
    core.offices.state,
    core.offices.zip_code,
    core.offices.country,
    core.offices.email AS office_email,
    core.offices.phone,
    core.offices.fax,
    core.offices.url,
    core.offices.currency_code,
    core.currencies.currency_name,
    core.currencies.currency_symbol,
    core.currencies.hundredth_name,
    core.offices.pan_number,
    account.users.last_seen_on
FROM account.logins
INNER JOIN account.users
ON account.users.user_id = account.logins.user_id
INNER JOIN account.roles
ON account.roles.role_id = account.users.role_id
INNER JOIN core.offices
ON core.offices.office_id = account.logins.office_id
LEFT JOIN core.currencies
ON core.currencies.currency_code = core.offices.currency_code
WHERE NOT account.logins.deleted;



-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/PostgreSQL/1.x/1.0/src/99.ownership.sql --<--<--
DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT * FROM pg_tables 
    WHERE NOT schemaname = ANY(ARRAY['pg_catalog', 'information_schema'])
    AND tableowner <> 'frapid_db_user'
    LOOP
        EXECUTE 'ALTER TABLE '|| this.schemaname || '.' || this.tablename ||' OWNER TO frapid_db_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;

DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT oid::regclass::text as mat_view
    FROM   pg_class
    WHERE  relkind = 'm'
    LOOP
        EXECUTE 'ALTER TABLE '|| this.mat_view ||' OWNER TO frapid_db_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;

DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT 'ALTER '
        || CASE WHEN p.proisagg THEN 'AGGREGATE ' ELSE 'FUNCTION ' END
        || quote_ident(n.nspname) || '.' || quote_ident(p.proname) || '(' 
        || pg_catalog.pg_get_function_identity_arguments(p.oid) || ') OWNER TO frapid_db_user;' AS sql
    FROM   pg_catalog.pg_proc p
    JOIN   pg_catalog.pg_namespace n ON n.oid = p.pronamespace
    WHERE  NOT n.nspname = ANY(ARRAY['pg_catalog', 'information_schema'])
    LOOP        
        EXECUTE this.sql;
    END LOOP;
END
$$
LANGUAGE plpgsql;


DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT * FROM pg_views
    WHERE NOT schemaname = ANY(ARRAY['pg_catalog', 'information_schema'])
    AND viewowner <> 'frapid_db_user'
    LOOP
        EXECUTE 'ALTER VIEW '|| this.schemaname || '.' || this.viewname ||' OWNER TO frapid_db_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;


DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT 'ALTER SCHEMA ' || nspname || ' OWNER TO frapid_db_user;' AS sql FROM pg_namespace
    WHERE nspname NOT LIKE 'pg_%'
    AND nspname <> 'information_schema'
    LOOP
        EXECUTE this.sql;
    END LOOP;
END
$$
LANGUAGE plpgsql;



DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT      'ALTER TYPE ' || n.nspname || '.' || t.typname || ' OWNER TO frapid_db_user;' AS sql
    FROM        pg_type t 
    LEFT JOIN   pg_catalog.pg_namespace n ON n.oid = t.typnamespace 
    WHERE       (t.typrelid = 0 OR (SELECT c.relkind = 'c' FROM pg_catalog.pg_class c WHERE c.oid = t.typrelid)) 
    AND         NOT EXISTS(SELECT 1 FROM pg_catalog.pg_type el WHERE el.oid = t.typelem AND el.typarray = t.oid)
    AND         typtype NOT IN ('b')
    AND         n.nspname NOT IN ('pg_catalog', 'information_schema')
    LOOP
        EXECUTE this.sql;
    END LOOP;
END
$$
LANGUAGE plpgsql;


DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'report_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT * FROM pg_tables 
    WHERE NOT schemaname = ANY(ARRAY['pg_catalog', 'information_schema'])
    AND tableowner <> 'report_user'
    LOOP
        EXECUTE 'GRANT SELECT ON TABLE '|| this.schemaname || '.' || this.tablename ||' TO report_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;

DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'report_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT oid::regclass::text as mat_view
    FROM   pg_class
    WHERE  relkind = 'm'
    LOOP
        EXECUTE 'GRANT SELECT ON TABLE '|| this.mat_view  ||' TO report_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;

DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'report_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT 'GRANT EXECUTE ON '
        || CASE WHEN p.proisagg THEN 'AGGREGATE ' ELSE 'FUNCTION ' END
        || quote_ident(n.nspname) || '.' || quote_ident(p.proname) || '(' 
        || pg_catalog.pg_get_function_identity_arguments(p.oid) || ') TO report_user;' AS sql
    FROM   pg_catalog.pg_proc p
    JOIN   pg_catalog.pg_namespace n ON n.oid = p.pronamespace
    WHERE  NOT n.nspname = ANY(ARRAY['pg_catalog', 'information_schema'])
    LOOP        
        EXECUTE this.sql;
    END LOOP;
END
$$
LANGUAGE plpgsql;


DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'report_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT * FROM pg_views
    WHERE NOT schemaname = ANY(ARRAY['pg_catalog', 'information_schema'])
    AND viewowner <> 'report_user'
    LOOP
        EXECUTE 'GRANT SELECT ON '|| this.schemaname || '.' || this.viewname ||' TO report_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;


DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'report_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT 'GRANT USAGE ON SCHEMA ' || nspname || ' TO report_user;' AS sql FROM pg_namespace
    WHERE nspname NOT LIKE 'pg_%'
    AND nspname <> 'information_schema'
    LOOP
        EXECUTE this.sql;
    END LOOP;
END
$$
LANGUAGE plpgsql;


