DROP SCHEMA IF EXISTS core CASCADE;
CREATE SCHEMA core;

CREATE TABLE core.countries
(
    country_code                            	national character varying(12) PRIMARY KEY,
    country_name                            	national character varying(100) NOT NULL,
    audit_user_id                           	integer,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

CREATE TABLE core.apps
(
	app_id										SERIAL,
    app_name                                    national character varying(100) PRIMARY KEY,
	i18n_key									national character varying(200) NOT NULL,
    name                                        national character varying(100),
    version_number                              national character varying(100),
    publisher                                   national character varying(100),
    published_on                                date,
    icon                                        text,
    landing_url                                 text,
    audit_user_id                           	integer,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

CREATE UNIQUE INDEX apps_app_name_uix
ON core.apps(UPPER(app_name))
WHERE NOT deleted;

CREATE TABLE core.app_dependencies
(
    app_dependency_id                           SERIAL PRIMARY KEY,
    app_name                                    national character varying(100) REFERENCES core.apps,
    depends_on                                  national character varying(100) REFERENCES core.apps,
    audit_user_id                           	integer,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);


CREATE TABLE core.menus
(
    menu_id                                     SERIAL PRIMARY KEY,
    sort                                        integer,
	i18n_key									national character varying(200) NOT NULL,
    app_name                                    national character varying(100) NOT NULL REFERENCES core.apps,
    menu_name                                   national character varying(100) NOT NULL,
    url                                         text,
    icon                                        national character varying(100),
    parent_menu_id                              integer REFERENCES core.menus,
    audit_user_id                           	integer,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

CREATE UNIQUE INDEX menus_app_name_menu_name_uix
ON core.menus(UPPER(app_name), UPPER(menu_name))
WHERE NOT deleted;

CREATE TABLE core.currencies
(
	currency_id									SERIAL,
    currency_code                           	national character varying(12) PRIMARY KEY,
    currency_symbol                         	national character varying(12) NOT NULL,
    currency_name                           	national character varying(48) NOT NULL UNIQUE,
    hundredth_name                          	national character varying(48) NOT NULL,
    audit_user_id                           	integer,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

CREATE TABLE core.offices
(
    office_id                                   SERIAL PRIMARY KEY,
    office_code                                 national character varying(12) NOT NULL,
    office_name                                 national character varying(150) NOT NULL,
    nick_name                                   national character varying(50),
    registration_date                           date,
	currency_code								national character varying(12),
    po_box                                      national character varying(128),
    address_line_1                              national character varying(128),   
    address_line_2                              national character varying(128),
    street                                      national character varying(50),
    city                                        national character varying(50),
    state                                       national character varying(50),
    zip_code                                    national character varying(24),
    country                                     national character varying(50),
    phone                                       national character varying(24),
    fax                                         national character varying(24),
    email                                       national character varying(128),
    url                                         national character varying(50),
    logo                                        public.photo,
    parent_office_id                            integer NULL REFERENCES core.offices,
	registration_number							national character varying(100),
	pan_number									national character varying(50),
	allow_transaction_posting					boolean NOT NULL DEFAULT(false),
    audit_user_id                               integer,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);


CREATE TABLE core.verification_statuses
(
    verification_status_id                  smallint PRIMARY KEY,
    verification_status_name                national character varying(128) NOT NULL,
    audit_user_id                           integer,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

COMMENT ON TABLE core.verification_statuses IS 
'Verification statuses are integer values used to represent the state of a transaction.
For example, a verification status of value "0" would mean that the transaction has not yet been verified.
A negative value indicates that the transaction was rejected, whereas a positive value means approved.

Remember:
1. Only approved transactions appear on ledgers and final reports.
2. Cash repository balance is maintained on the basis of LIFO principle. 

   This means that cash balance is affected (reduced) on your repository as soon as a credit transaction is posted,
   without the transaction being approved on the first place. If you reject the transaction, the cash balance then increases.
   This also means that the cash balance is not affected (increased) on your repository as soon as a debit transaction is posted.
   You will need to approve the transaction.

   It should however be noted that the cash repository balance might be less than the total cash shown on your balance sheet,
   if you have pending transactions to verify. You cannot perform EOD operation if you have pending verifications.
';

CREATE TABLE core.week_days
(
	week_day_id                 			integer NOT NULL CHECK(week_day_id >=1 AND week_day_id <=7) PRIMARY KEY,
	week_day_code               			national character varying(12) NOT NULL UNIQUE,
	week_day_name               			national character varying(50) NOT NULL UNIQUE,
    audit_user_id                           integer,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

CREATE TABLE core.genders
(
	gender_code                             national character varying(4) NOT NULL PRIMARY KEY,
	gender_name                             national character varying(50) NOT NULL UNIQUE,
	audit_user_id                           integer,
	audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)    
);

CREATE TABLE core.marital_statuses
(
	marital_status_id                       SERIAL NOT NULL PRIMARY KEY,
	marital_status_code                     national character varying(12) NOT NULL UNIQUE,
	marital_status_name                     national character varying(128) NOT NULL,
	is_legally_recognized_marriage          boolean NOT NULL DEFAULT(false),
	audit_user_id                           integer,    
	audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

CREATE TABLE core.notifications
(
    notification_id                             uuid PRIMARY KEY DEFAULT(gen_random_uuid()),
    event_timestamp                             TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
	tenant										national character varying(1000),
	office_id									integer REFERENCES core.offices,
	associated_app								national character varying(100) NOT NULL REFERENCES core.apps,
    associated_menu_id                          integer REFERENCES core.menus,
    to_user_id                                  integer,
    to_role_id                                  integer,
	to_login_id									bigint,
    url                                         national character varying(2000),
    formatted_text                              national character varying(4000),
    icon                                        national character varying(100)    
);

CREATE TABLE core.notification_statuses
(
    notification_status_id                      uuid PRIMARY KEY DEFAULT(gen_random_uuid()),
    notification_id                             uuid NOT NULL REFERENCES core.notifications,
    last_seen_on                                TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
    seen_by                                     integer NOT NULL
);
