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
