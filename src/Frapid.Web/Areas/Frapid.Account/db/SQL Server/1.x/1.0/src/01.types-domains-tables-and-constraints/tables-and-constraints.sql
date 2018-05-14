EXECUTE dbo.drop_schema 'account';
GO
CREATE SCHEMA account;

GO

CREATE TABLE account.roles
(
    role_id										integer PRIMARY KEY,
    role_name									national character varying(100) NOT NULL UNIQUE,
    is_administrator							bit NOT NULL DEFAULT(0),
    audit_user_id								integer,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE TABLE account.installed_domains
(
    domain_id									integer IDENTITY NOT NULL PRIMARY KEY,
    domain_name									national character varying(500),
    admin_email									national character varying(500),
    audit_user_id                           	integer,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE UNIQUE INDEX installed_domains_domain_name_uix
ON account.installed_domains(domain_name)
WHERE deleted = 0;


CREATE TABLE account.configuration_profiles
(
    configuration_profile_id					integer IDENTITY PRIMARY KEY,
    profile_name								national character varying(100) NOT NULL UNIQUE,
    is_active									bit NOT NULL DEFAULT(1),    
    allow_registration							bit NOT NULL DEFAULT(1),
    registration_office_id						integer NOT NULL REFERENCES core.offices,
    registration_role_id						integer NOT NULL REFERENCES account.roles,
    allow_facebook_registration					bit NOT NULL DEFAULT(1),
    allow_google_registration					bit NOT NULL DEFAULT(1),
    google_signin_client_id						national character varying(500),
    google_signin_scope							national character varying(500),
    facebook_app_id								national character varying(500),
    facebook_scope								national character varying(500),
    audit_user_id								integer,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE UNIQUE INDEX configuration_profile_uix
ON account.configuration_profiles(is_active)
WHERE is_active = 1
AND deleted = 0;

CREATE TABLE account.registrations
(
    registration_id								uniqueidentifier PRIMARY KEY DEFAULT(NEWID()),
    name										national character varying(100),
    email										national character varying(100) NOT NULL,
    phone										national character varying(100),
    password									national character varying(500),
    browser										national character varying(500),
    ip_address									national character varying(50),
    registered_on								datetimeoffset NOT NULL DEFAULT(getutcdate()),
    confirmed									bit DEFAULT(0),
    confirmed_on								datetimeoffset,
    audit_user_id                           	integer,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE UNIQUE INDEX registrations_email_uix
ON account.registrations(email)
WHERE deleted = 0;

CREATE TABLE account.users
(
    user_id										integer IDENTITY PRIMARY KEY,
    email										national character varying(100) NOT NULL,
    password									national character varying(500),
    office_id									integer NOT NULL REFERENCES core.offices,
    role_id										integer NOT NULL REFERENCES account.roles,
    name										national character varying(100),
    phone										national character varying(100),
    status										bit DEFAULT(1),
    created_on									datetimeoffset NOT NULL DEFAULT(getutcdate()),
	last_seen_on								datetimeoffset,
	last_ip										national character varying(500),
	last_browser								national character varying(500),
    audit_user_id								integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE UNIQUE INDEX users_email_uix
ON account.users(email)
WHERE deleted = 0;

ALTER TABLE account.configuration_profiles
ADD FOREIGN KEY(audit_user_id) REFERENCES account.users;

ALTER TABLE account.roles
ADD FOREIGN KEY(audit_user_id) REFERENCES account.users;

CREATE TABLE account.reset_requests
(
    request_id									uniqueidentifier PRIMARY KEY DEFAULT(NEWID()),
    user_id										integer NOT NULL REFERENCES account.users,
    email										national character varying(500),
    name										national character varying(500),
    requested_on								datetimeoffset NOT NULL DEFAULT(getutcdate()),
    expires_on									datetimeoffset NOT NULL DEFAULT(dateadd(d, 1, getutcdate())),
    browser										national character varying(500),
    ip_address									national character varying(50),
    confirmed									bit DEFAULT(0),
    confirmed_on								datetimeoffset,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);




CREATE TABLE account.fb_access_tokens
(
    user_id										integer PRIMARY KEY REFERENCES account.users,
    fb_user_id									national character varying(500),
    token										national character varying(MAX),
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE TABLE account.google_access_tokens
(
    user_id										integer PRIMARY KEY REFERENCES account.users,
    token										national character varying(MAX),
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE TABLE account.logins
(
    login_id									bigint IDENTITY PRIMARY KEY,
    user_id										integer REFERENCES account.users,
    office_id									integer REFERENCES core.offices,
    browser										national character varying(500),
    ip_address									national character varying(50),
    is_active									bit NOT NULL DEFAULT(1),
    login_timestamp								datetimeoffset NOT NULL DEFAULT(getutcdate()),
    culture										national character varying(12) NOT NULL,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE TABLE account.applications
(
    application_id                              uniqueidentifier DEFAULT(NEWID()) PRIMARY KEY,
    application_name                            national character varying(100) NOT NULL,
    display_name                                national character varying(100),
    version_number                              national character varying(100),
    publisher                                   national character varying(100) NOT NULL,
    published_on                                date,
    application_url                             national character varying(500),
    description                                 national character varying(500),
    browser_based_app                           bit NOT NULL,
    privacy_policy_url                          national character varying(500),
    terms_of_service_url                        national character varying(500),
    support_email                               national character varying(100),
    culture                                     national character varying(12),
    redirect_url                                national character varying(500),
    app_secret                                  national character varying(500) UNIQUE,
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE UNIQUE INDEX applications_app_name_uix
ON account.applications(application_name)
WHERE deleted = 0;

CREATE TABLE account.access_tokens
(
    access_token_id                             uniqueidentifier DEFAULT(NEWID()) PRIMARY KEY,
    issued_by                                   national character varying(500) NOT NULL,
    audience                                    national character varying(500) NOT NULL,
    ip_address                                  national character varying(100),
    user_agent                                  national character varying(500),
    header                                      national character varying(500),
    subject                                     national character varying(500),
    token_id                                    national character varying(500),
    application_id                              uniqueidentifier NULL REFERENCES account.applications,
    login_id                                    bigint NOT NULL REFERENCES account.logins,
    client_token                                national character varying(MAX),
    claims                                      national character varying(MAX),
    created_on                                  datetimeoffset NOT NULL,
    expires_on                                  datetimeoffset NOT NULL,
    revoked                                     bit NOT NULL DEFAULT(0),
    revoked_by                                  integer REFERENCES account.users,
    revoked_on                                  datetimeoffset,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

