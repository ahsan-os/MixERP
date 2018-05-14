DROP SCHEMA IF EXISTS config CASCADE;
CREATE SCHEMA config;

CREATE TABLE config.kanbans
(
    kanban_id                                   BIGSERIAL NOT NULL PRIMARY KEY,
    object_name                                 national character varying(128) NOT NULL,
    user_id                                     integer REFERENCES account.users,
    kanban_name                                 national character varying(128) NOT NULL,
    description                                 text,
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)    
);
CREATE TABLE config.kanban_details
(
    kanban_detail_id                            BIGSERIAL NOT NULL PRIMARY KEY,
    kanban_id                                   bigint NOT NULL REFERENCES config.kanbans(kanban_id),
    rating                                      smallint CHECK(rating>=0 AND rating<=5),
    resource_id                                 national character varying(128) NOT NULL,
    audit_user_id                               integer NULL REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)    
);

CREATE UNIQUE INDEX kanban_details_kanban_id_resource_id_uix
ON config.kanban_details(kanban_id, resource_id)
WHERE NOT deleted;


CREATE TABLE config.smtp_configs
(
    smtp_config_id                              SERIAL PRIMARY KEY,
    configuration_name                          national character varying(256) NOT NULL UNIQUE,
    enabled                                     boolean NOT NULL DEFAULT false,
    is_default                                  boolean NOT NULL DEFAULT false,
    from_display_name                           national character varying(256) NOT NULL,
    from_email_address                          national character varying(256) NOT NULL,
    smtp_host                                   national character varying(256) NOT NULL,
    smtp_enable_ssl                             boolean NOT NULL DEFAULT true,
    smtp_username                               national character varying(256) NOT NULL,
    smtp_password                               national character varying(256) NOT NULL,
    smtp_port                                   integer NOT NULL DEFAULT(587),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);


CREATE TABLE config.email_queue
(
    queue_id                                    BIGSERIAL NOT NULL PRIMARY KEY,
    application_name                            national character varying(256),
    from_name                                   national character varying(256) NOT NULL,
    from_email                                  national character varying(256) NOT NULL,
    reply_to                                    national character varying(256) NOT NULL,
    reply_to_name                               national character varying(256) NOT NULL,
    subject                                     national character varying(256) NOT NULL,
    send_to                                     national character varying(256) NOT NULL,
    attachments                                 text,
    message                                     text NOT NULL,
    added_on                                    TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
	send_on										TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
    delivered                                   boolean NOT NULL DEFAULT(false),
    delivered_on                                TIMESTAMP WITH TIME ZONE,
    canceled                                    boolean NOT NULL DEFAULT(false),
    canceled_on                                 TIMESTAMP WITH TIME ZONE,
	is_test										boolean NOT NULL DEFAULT(false),
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);


CREATE TABLE config.sms_queue
(
    queue_id                                    BIGSERIAL NOT NULL PRIMARY KEY,
    application_name                            national character varying(256),
    from_name                                   national character varying(256) NOT NULL,
    from_number                                 national character varying(256) NOT NULL,
    subject                                     national character varying(256) NOT NULL,
    send_to                                     national character varying(256) NOT NULL,
    message                                     text NOT NULL,
    added_on                                    TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
	send_on										TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
    delivered                                   boolean NOT NULL DEFAULT(false),
    delivered_on                                TIMESTAMP WITH TIME ZONE,
    canceled                                    boolean NOT NULL DEFAULT(false),
    canceled_on                                 TIMESTAMP WITH TIME ZONE,
	is_test										boolean NOT NULL DEFAULT(false),
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);


CREATE TABLE config.filters
(
    filter_id                                   BIGSERIAL NOT NULL PRIMARY KEY,
    object_name                                 text NOT NULL,
    filter_name                                 text NOT NULL,
    is_default                                  boolean NOT NULL DEFAULT(false),
    is_default_admin                            boolean NOT NULL DEFAULT(false),
    filter_statement                            national character varying(12) NOT NULL DEFAULT('WHERE'),
    column_name                                 text NOT NULL,
	data_type									text NOT NULL DEFAULT(''),
    filter_condition                            integer NOT NULL,
    filter_value                                text,
    filter_and_value                            text,
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

CREATE INDEX filters_object_name_inx
ON config.filters(object_name)
WHERE NOT deleted;

CREATE TABLE config.custom_field_data_types
(
    data_type                                   national character varying(50) NOT NULL PRIMARY KEY,
	underlying_type								national character varying(500) NOT NULL,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

CREATE TABLE config.custom_field_forms
(
    form_name                                   national character varying(100) NOT NULL PRIMARY KEY,
    table_name                                  national character varying(500) NOT NULL UNIQUE,
    key_name                                    national character varying(500) NOT NULL,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);


CREATE TABLE config.custom_field_setup
(
    custom_field_setup_id                       SERIAL NOT NULL PRIMARY KEY,
    form_name                                   national character varying(100) NOT NULL
                                                REFERENCES config.custom_field_forms,
	before_field								national character varying(500),
    field_order                                 integer NOT NULL DEFAULT(0),
	after_field									national character varying(500),
    field_name                                  national character varying(100) NOT NULL,
    field_label                                 national character varying(200) NOT NULL,                   
    data_type                                   national character varying(50)
                                                REFERENCES config.custom_field_data_types,
    description                                 text NOT NULL,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

CREATE TABLE config.custom_fields
(
    custom_field_id                             SERIAL PRIMARY KEY,
    custom_field_setup_id                       integer NOT NULL REFERENCES config.custom_field_setup,
    resource_id                                 national character varying(500) NOT NULL,
    value                                       text,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

