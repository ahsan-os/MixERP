-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
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



-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/04.default-values/01.default-values.sql --<--<--
DO
$$
BEGIN
	DELETE FROM config.custom_field_data_types;
	
	IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='text') THEN
		INSERT INTO config.custom_field_data_types(data_type, underlying_type)
		SELECT 'Text', 'national character varying(500)';
	END IF;

	IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Number') THEN
		INSERT INTO config.custom_field_data_types(data_type, underlying_type)
		SELECT 'Number', 'integer';
	END IF;

	IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Positive Number') THEN
		INSERT INTO config.custom_field_data_types(data_type, underlying_type)
		SELECT 'Positive Number', 'integer';
	END IF;

	IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Money') THEN
		INSERT INTO config.custom_field_data_types(data_type, underlying_type)
		SELECT 'Money', 'numeric(30, 6)';
	END IF;

	IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Money (Positive Value Only)') THEN
		INSERT INTO config.custom_field_data_types(data_type, underlying_type)
		SELECT 'Money (Positive Value Only)', 'numeric(30, 6)';
	END IF;

	IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Date') THEN
		INSERT INTO config.custom_field_data_types(data_type, underlying_type)
		SELECT 'Date', 'date';
	END IF;

	IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Date & Time') THEN
		INSERT INTO config.custom_field_data_types(data_type, underlying_type)
		SELECT 'Date & Time', 'datetimeoffset';
	END IF;

	IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Time') THEN
		INSERT INTO config.custom_field_data_types(data_type, underlying_type)
		SELECT 'Time', 'time';
	END IF;

	IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='True/False') THEN
		INSERT INTO config.custom_field_data_types(data_type, underlying_type)
		SELECT 'True/False', 'bit';
	END IF;

	IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Long Text') THEN
		INSERT INTO config.custom_field_data_types(data_type, underlying_type)
		SELECT 'Long Text', 'national character varying(MAX)';
	END IF;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/05.scrud-views/config.smtp_config_scrud_view.sql --<--<--
DROP VIEW IF EXISTS config.smtp_config_scrud_view;

CREATE VIEW config.smtp_config_scrud_view
AS
SELECT
	config.smtp_configs.smtp_config_id,
	config.smtp_configs.configuration_name,
	config.smtp_configs.enabled,
	config.smtp_configs.is_default,
	config.smtp_configs.from_display_name,
	config.smtp_configs.from_email_address
FROM config.smtp_configs
WHERE NOT config.smtp_configs.deleted;

-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/05.views/config.custom_field_definition_view.sql --<--<--
DROP VIEW IF EXISTS config.custom_field_definition_view;

CREATE VIEW config.custom_field_definition_view
AS
SELECT
    config.custom_field_forms.table_name,
    config.custom_field_forms.key_name,
    config.custom_field_setup.custom_field_setup_id,
    config.custom_field_setup.form_name,
    config.custom_field_setup.field_order,
    config.custom_field_setup.field_name,
    config.custom_field_setup.field_label,
    config.custom_field_setup.description,
    config.custom_field_data_types.data_type,
    config.custom_field_data_types.underlying_type,
    ''::text AS resource_id,
    ''::text AS value
FROM config.custom_field_setup
INNER JOIN config.custom_field_data_types
ON config.custom_field_data_types.data_type = config.custom_field_setup.data_type
INNER JOIN config.custom_field_forms
ON config.custom_field_forms.form_name = config.custom_field_setup.form_name
WHERE NOT config.custom_field_setup.deleted
ORDER BY field_order;

-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/05.views/config.custom_field_view.sql --<--<--
DROP VIEW IF EXISTS config.custom_field_view;

CREATE VIEW config.custom_field_view 
AS 
SELECT 
	custom_field_forms.table_name,
	custom_field_forms.key_name,
	custom_field_setup.custom_field_setup_id,
	custom_field_setup.form_name,
	custom_field_setup.field_order,
	custom_field_setup.field_name,
	custom_field_setup.field_label,
	custom_field_setup.description,
	custom_field_data_types.underlying_type,
	custom_fields.resource_id,
	custom_fields.value
FROM config.custom_field_setup
INNER JOIN config.custom_field_data_types ON custom_field_data_types.data_type = custom_field_setup.data_type
INNER JOIN config.custom_field_forms ON custom_field_forms.form_name = custom_field_setup.form_name
INNER JOIN config.custom_fields ON custom_fields.custom_field_setup_id = custom_field_setup.custom_field_setup_id
WHERE NOT config.custom_field_setup.deleted;


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/05.views/config.filter_name_view.sql --<--<--
DROP VIEW IF EXISTS config.filter_name_view;

CREATE VIEW config.filter_name_view
AS
SELECT
    DISTINCT
    object_name,
    filter_name,
    is_default
FROM config.filters
WHERE NOT config.filters.deleted;

-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/06.functions-and-logic/config.create_custom_field.sql --<--<--
DROP FUNCTION IF EXISTS config.create_custom_field
(
    _form_name                  national character varying(100),
    _before_field               national character varying(500),
    _field_order                integer,
    _after_field                national character varying(500),
    _field_name                 national character varying(100),
    _field_label                national character varying(200),
    _data_type                  national character varying(50),
    _description                national character varying(500)
);

CREATE FUNCTION config.create_custom_field
(
    _form_name                  national character varying(100),
    _before_field               national character varying(500),
    _field_order                integer,
    _after_field                national character varying(500),
    _field_name                 national character varying(100),
    _field_label                national character varying(200),
    _data_type                  national character varying(50),
    _description                national character varying(500)
)
RETURNS void
AS
$$
    DECLARE _table_name         national character varying(500);
    DECLARE _key_name           national character varying(500);
    DECLARE _sql                text;
    DECLARE _key_data_type      national character varying(500);
    DECLARE _cf_data_type       national character varying(500);
BEGIN
    SELECT
        config.custom_field_forms.table_name,
        config.custom_field_forms.key_name
    INTO
        _table_name,
        _key_name
    FROM config.custom_field_forms
    WHERE config.custom_field_forms.form_name = _form_name
	AND NOT config.custom_field_forms.deleted;

    SELECT 
        format_type(a.atttypid, a.atttypmod)
    INTO
        _key_data_type
    FROM   pg_index i
    JOIN   pg_attribute a ON a.attrelid = i.indrelid
                         AND a.attnum = ANY(i.indkey)
    WHERE  i.indrelid = _table_name::regclass
    AND    i.indisprimary;

    SELECT
        underlying_type
    INTO
        _cf_data_type
    FROM config.custom_field_data_types
    WHERE config.custom_field_data_types.data_type = _data_type
	AND NOT config.custom_field_data_types.deleted;

    
    _sql := 'CREATE TABLE IF NOT EXISTS %s_cf
            (
                %s %s PRIMARY KEY REFERENCES %1$s
            );';

    EXECUTE format(_sql, _table_name, _key_name, _key_data_type);

    _sql := 'DO
            $ALTER$
            BEGIN
                IF NOT EXISTS
                (
                    SELECT 1
                    FROM   pg_attribute 
                    WHERE  attrelid = ''%s_cf''::regclass
                    AND    attname = ''%s''
                    AND    NOT attisdropped
                ) THEN
                    ALTER TABLE %1$s_cf
                    ADD COLUMN %2$s	%s;
                END IF;
            END
            $ALTER$
            LANGUAGE plpgsql;';
                
   EXECUTE format(_sql, _table_name, lower(_field_name), _cf_data_type);

   IF NOT EXISTS
   (
        SELECT 1
        FROM config.custom_field_setup
        WHERE config.custom_field_setup.form_name = _form_name
        AND config.custom_field_setup.field_name = _field_name
		AND NOT config.custom_field_setup.deleted
   ) THEN
       INSERT INTO config.custom_field_setup(form_name, before_field, field_order, after_field, field_name, field_label, data_type, description)
       SELECT _form_name, _before_field, _field_order, _after_field, _field_name, _field_label, _data_type, _description;
   END IF;
END
$$
LANGUAGE plpgsql;

-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/06.functions-and-logic/config.get_custom_field_form_name.sql --<--<--
DROP FUNCTION IF EXISTS config.get_custom_field_form_name
(
    _table_name character varying
);

CREATE FUNCTION config.get_custom_field_form_name
(
    _table_name character varying
)
RETURNS national character varying(500)
STABLE
AS
$$
BEGIN
    RETURN form_name 
    FROM config.custom_field_forms
    WHERE config.custom_field_forms.table_name = _table_name
	AND NOT config.custom_field_forms.deleted;
END
$$
LANGUAGE plpgsql;

-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/06.functions-and-logic/config.get_custom_field_setup_id_by_table_name.sql --<--<--
DROP FUNCTION IF EXISTS config.get_custom_field_setup_id_by_table_name
(
    _schema_name national character varying(100), 
    _table_name national character varying(100),
    _field_name national character varying(100)
);

CREATE FUNCTION config.get_custom_field_setup_id_by_table_name
(
    _schema_name national character varying(100), 
    _table_name national character varying(100),
    _field_name national character varying(100)
)
RETURNS integer
AS
$$
BEGIN
    RETURN custom_field_setup_id
    FROM config.custom_field_setup
    WHERE config.custom_field_setup.form_name = config.get_custom_field_form_name(_schema_name, _table_name)
    AND config.custom_field_setup.field_name = _field_name
	AND NOT config.custom_field_setup.deleted;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/06.functions-and-logic/config.get_server_timezone().sql --<--<--
DROP FUNCTION IF EXISTS config.get_server_timezone();

CREATE FUNCTION config.get_server_timezone()
RETURNS national character varying(200)
AS
$$
BEGIN
    RETURN
        pg_catalog.pg_settings.setting
    FROM pg_catalog.pg_settings
    WHERE name = 'log_timezone';
END
$$
LANGUAGE plpgsql;

--SELECT * FROM config.get_server_timezone();



-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/06.functions-and-logic/config.get_user_id_by_login_id.sql --<--<--
DROP FUNCTION IF EXISTS config.get_user_id_by_login_id(_login_id bigint);

CREATE FUNCTION config.get_user_id_by_login_id(_login_id bigint)
RETURNS integer
AS
$$
BEGIN
    RETURN 
    user_id
    FROM account.logins
    WHERE account.logins.login_id = _login_id
	AND NOT account.logins.deleted;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/09.menus/0.menu.sql --<--<--
SELECT * FROM core.create_app('Frapid.Config', 'Config', 'Config', '1.0', 'MixERP Inc.', 'December 1, 2015', 'orange configure', '/dashboard/config/offices', null);
SELECT * FROM core.create_menu('Frapid.Config', 'Offices', 'Offices', '/dashboard/config/offices', 'building outline', '');
SELECT * FROM core.create_menu('Frapid.Config', 'SMTP', 'SMTP', '/dashboard/config/smtp', 'at', '');
SELECT * FROM core.create_menu('Frapid.Config', 'FileManager', 'File Manager', '/dashboard/config/file-manager', 'file text outline', '');

SELECT * FROM auth.create_app_menu_policy
(
    'Admin', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.Config',
    '{*}'::text[]
);

-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/09.menus/1.menu-policy.sql --<--<--
SELECT * FROM auth.create_app_menu_policy
(
    'User', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.Config',
    '{Offices}'::text[]
);

SELECT * FROM auth.create_app_menu_policy
(
    'Admin', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.Config',
    '{*}'::text[]
);


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/10.policy/access_policy.sql --<--<--
SELECT * FROM auth.create_api_access_policy('{*}', 1, 'config.kanban_details', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{*}', 1, 'config.kanbans', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{*}', 1, 'config.filter_name_view', '{*}', true);

SELECT * FROM auth.create_api_access_policy('{User}', core.get_office_id_by_office_name('Default'), 'core.offices', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{Admin}', core.get_office_id_by_office_name('Default'), '', '{*}', true);


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/PostgreSQL/1.x/1.0/src/99.ownership.sql --<--<--
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


