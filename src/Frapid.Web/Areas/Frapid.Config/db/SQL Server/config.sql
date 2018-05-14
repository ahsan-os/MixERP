-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
EXECUTE dbo.drop_schema 'config';
GO
CREATE SCHEMA config;

GO

CREATE TABLE config.kanbans
(
    kanban_id                                   bigint IDENTITY NOT NULL PRIMARY KEY,
    object_name                                 national character varying(128) NOT NULL,
    user_id                                     integer REFERENCES account.users,
    kanban_name                                 national character varying(128) NOT NULL,
    description                                 national character varying(500),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);
CREATE TABLE config.kanban_details
(
    kanban_detail_id                            bigint IDENTITY NOT NULL PRIMARY KEY,
    kanban_id                                   bigint NOT NULL REFERENCES config.kanbans(kanban_id),
    rating                                      smallint CHECK(rating>=0 AND rating<=5),
    resource_id                                 national character varying(128) NOT NULL,
    audit_user_id                               integer NULL REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)    
);

CREATE UNIQUE INDEX kanban_details_kanban_id_resource_id_uix
ON config.kanban_details(kanban_id, resource_id)
WHERE deleted = 0;


CREATE TABLE config.smtp_configs
(
    smtp_config_id                              integer IDENTITY PRIMARY KEY,
    configuration_name                          national character varying(256) NOT NULL UNIQUE,
    enabled                                     bit NOT NULL DEFAULT 0,
    is_default                                  bit NOT NULL DEFAULT 0,
    from_display_name                           national character varying(256) NOT NULL,
    from_email_address                          national character varying(256) NOT NULL,
    smtp_host                                   national character varying(256) NOT NULL,
    smtp_enable_ssl                             bit NOT NULL DEFAULT 1,
    smtp_username                               national character varying(256) NOT NULL,
    smtp_password                               national character varying(256) NOT NULL,
    smtp_port                                   integer NOT NULL DEFAULT(587),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE TABLE config.email_queue
(
    queue_id                                    bigint IDENTITY NOT NULL PRIMARY KEY,
    application_name                            national character varying(256),
    from_name                                   national character varying(256) NOT NULL,
    from_email                                  national character varying(256) NOT NULL,
    reply_to                                    national character varying(256) NOT NULL,
    reply_to_name                               national character varying(256) NOT NULL,
    subject                                     national character varying(256) NOT NULL,
    send_to                                     national character varying(256) NOT NULL,
    attachments                                 national character varying(2000),
    message                                     national character varying(MAX) NOT NULL,
    added_on                                    datetimeoffset NOT NULL DEFAULT(getutcdate()),
	send_on										datetimeoffset NOT NULL DEFAULT(getutcdate()),
    delivered                                   bit NOT NULL DEFAULT(0),
    delivered_on                                datetimeoffset,
    canceled                                    bit NOT NULL DEFAULT(0),
    canceled_on                                 datetimeoffset,
	is_test										bit NOT NULL DEFAULT(0),
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE TABLE config.sms_queue
(
    queue_id                                    bigint IDENTITY NOT NULL PRIMARY KEY,
    application_name                            national character varying(256),
    from_name                                   national character varying(256) NOT NULL,
    from_number                                 national character varying(256) NOT NULL,
    subject                                     national character varying(256) NOT NULL,
    send_to                                     national character varying(256) NOT NULL,
    message                                     national character varying(MAX) NOT NULL,
    added_on                                    datetimeoffset NOT NULL DEFAULT(getutcdate()),
	send_on										datetimeoffset NOT NULL DEFAULT(getutcdate()),
    delivered                                   bit NOT NULL DEFAULT(0),
    delivered_on                                datetimeoffset,
    canceled                                    bit NOT NULL DEFAULT(0),
    canceled_on                                 datetimeoffset,
	is_test										bit NOT NULL DEFAULT(0),
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE TABLE config.filters
(
    filter_id                                   bigint IDENTITY NOT NULL PRIMARY KEY,
    object_name                                 national character varying(500) NOT NULL,
    filter_name                                 national character varying(500) NOT NULL,
    is_default                                  bit NOT NULL DEFAULT(0),
    is_default_admin                            bit NOT NULL DEFAULT(0),
    filter_statement                            national character varying(12) NOT NULL DEFAULT('WHERE'),
    column_name                                 national character varying(500) NOT NULL,
	data_type									national character varying(500) NOT NULL DEFAULT(''),
    filter_condition                            integer NOT NULL,
    filter_value                                national character varying(500),
    filter_and_value                            national character varying(500),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE INDEX filters_object_name_inx
ON config.filters(object_name)
WHERE deleted = 0;

CREATE TABLE config.custom_field_data_types
(
    data_type                                   national character varying(50) NOT NULL PRIMARY KEY,
	underlying_type								national character varying(500) NOT NULL,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE TABLE config.custom_field_forms
(
    form_name                                   national character varying(100) NOT NULL PRIMARY KEY,
    table_name                                  national character varying(500) NOT NULL UNIQUE,
    key_name                                    national character varying(500) NOT NULL,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE TABLE config.custom_field_setup
(
    custom_field_setup_id                       integer IDENTITY NOT NULL PRIMARY KEY,
    form_name                                   national character varying(100) NOT NULL
                                                REFERENCES config.custom_field_forms,
	before_field								national character varying(500),
    field_order                                 integer NOT NULL DEFAULT(0),
	after_field									national character varying(500),
    field_name                                  national character varying(100) NOT NULL,
    field_label                                 national character varying(200) NOT NULL,                   
    data_type                                   national character varying(50)
                                                REFERENCES config.custom_field_data_types,
    description                                 national character varying(500) NOT NULL,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE TABLE config.custom_fields
(
    custom_field_id                             bigint IDENTITY NOT NULL PRIMARY KEY,
    custom_field_setup_id                       integer NOT NULL REFERENCES config.custom_field_setup,
    resource_id                                 national character varying(500) NOT NULL,
    value                                       national character varying(MAX),
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/04.default-values/01.default-values.sql --<--<--
GO

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='text')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Text', 'national character varying(500)';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Number')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Number', 'integer';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Number')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Positive Number', 'integer';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Number')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Money', 'numeric(30, 6)';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Number')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Money (Positive Value Only)', 'numeric(30, 6)';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Date')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Date', 'date';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Date')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Date & Time', 'datetimeoffset';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='True/False')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'True/False', 'bit';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Long Text')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Long Text', 'national character varying(MAX)';
END;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/05.scrud-views/config.smtp_config_scrud_view.sql --<--<--
IF OBJECT_ID('config.smtp_config_scrud_view') IS NOT NULL
DROP VIEW config.smtp_config_scrud_view;

GO

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
WHERE config.smtp_configs.deleted = 0;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/05.views/config.custom_field_definition_view.sql --<--<--
IF OBJECT_ID('config.custom_field_definition_view') IS NOT NULL
DROP VIEW config.custom_field_definition_view;

GO

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
    '' AS resource_id,
    '' AS value
FROM config.custom_field_setup
INNER JOIN config.custom_field_data_types
ON config.custom_field_data_types.data_type = config.custom_field_setup.data_type
INNER JOIN config.custom_field_forms
ON config.custom_field_forms.form_name = config.custom_field_setup.form_name
WHERE config.custom_field_setup.deleted = 0;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/05.views/config.custom_field_view.sql --<--<--
IF OBJECT_ID('config.custom_field_view ') IS NOT NULL
DROP VIEW config.custom_field_view;

GO


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
WHERE config.custom_field_setup.deleted = 0;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/05.views/config.filter_name_view.sql --<--<--
IF OBJECT_ID('config.filter_name_view') IS NOT NULL
DROP VIEW config.filter_name_view;
GO
CREATE VIEW config.filter_name_view
AS
SELECT
    DISTINCT
    object_name,
    filter_name,
    is_default
FROM config.filters
WHERE config.filters.deleted = 0;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/06.functions-and-logic/config.get_custom_field_definition.sql --<--<--

IF OBJECT_ID('config.get_custom_field_definition') IS NOT NULL
DROP PROCEDURE config.get_custom_field_definition;

GO


CREATE PROCEDURE config.get_custom_field_definition
(
    @table_name             national character varying(500),
    @resource_id            national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	DECLARE @result TABLE
	(
		table_name              national character varying(100),
		key_name                national character varying(100),
		custom_field_setup_id   integer,
		form_name               national character varying(100),
		field_order             integer,
		field_name              national character varying(100),
		field_label             national character varying(100),
		description             national character varying(500),
		data_type               national character varying(50),
		underlying_type			national character varying(100),
		resource_id             national character varying(500),
		value                   national character varying(500)
	);


    
    INSERT INTO @result
    SELECT * FROM config.custom_field_definition_view
    WHERE config.custom_field_definition_view.table_name = @table_name
    ORDER BY field_order;

    UPDATE @result
    SET resource_id = @resource_id;

    UPDATE @result
    SET value = config.custom_fields.value
    FROM @result result
    INNER JOIN config.custom_fields
    ON result.custom_field_setup_id = config.custom_fields.custom_field_setup_id
    WHERE config.custom_fields.resource_id = @resource_id;
    
    SELECT * FROM @result;
    RETURN;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/06.functions-and-logic/config.get_custom_field_form_name.sql --<--<--
IF OBJECT_ID('config.get_custom_field_form_name') IS NOT NULL
DROP FUNCTION config.get_custom_field_form_name;

GO

CREATE FUNCTION config.get_custom_field_form_name
(
    @table_name character varying
)
RETURNS national character varying
BEGIN
    RETURN 
    (
		SELECT form_name 
		FROM config.custom_field_forms
		WHERE table_name = @table_name
		AND config.custom_field_forms.deleted = 0
	);
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/06.functions-and-logic/config.get_custom_field_setup_id_by_table_name.sql --<--<--
IF OBJECT_ID('config.get_custom_field_setup_id_by_table_name') IS NOT NULL
DROP FUNCTION config.get_custom_field_setup_id_by_table_name;

GO


CREATE FUNCTION config.get_custom_field_setup_id_by_table_name
(
    @schema_name						national character varying(100), 
    @table_name							national character varying(100),
    @field_name							national character varying(100)
)
RETURNS integer
AS
BEGIN
    RETURN 
    (
		SELECT custom_field_setup_id
		FROM config.custom_field_setup
		WHERE form_name = config.get_custom_field_form_name(@schema_name + '.' + @table_name)
		AND field_name = @field_name
		AND config.custom_field_setup.deleted = 0
	);
END;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/06.functions-and-logic/config.get_user_id_by_login_id.sql --<--<--
IF OBJECT_ID('config.get_user_id_by_login_id') IS NOT NULL
DROP FUNCTION config.get_user_id_by_login_id;

GO

CREATE FUNCTION config.get_user_id_by_login_id(@login_id bigint)
RETURNS integer
AS
BEGIN
    RETURN
    ( 
		SELECT
		user_id
		FROM account.logins
		WHERE login_id = @login_id
		AND account.logins.deleted = 0
	);
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/09.menus/0.menu.sql --<--<--
EXECUTE core.create_app 'Frapid.Config', 'Config', 'Config', '1.0', 'MixERP Inc.', 'December 1, 2015', 'orange configure', '/dashboard/config/offices', null;
EXECUTE core.create_menu 'Frapid.Config', 'Offices', 'Offices', '/dashboard/config/offices', 'building outline', '';
EXECUTE core.create_menu 'Frapid.Config', 'SMTP', 'SMTP', '/dashboard/config/smtp', 'at', '';
EXECUTE core.create_menu 'Frapid.Config', 'FileManager', 'File Manager', '/dashboard/config/file-manager', 'file national character varying(500) outline', '';

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/09.menus/1.menu-policy.sql --<--<--
DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Config',
'{*}';

EXECUTE auth.create_app_menu_policy
'User', 
@office_id, 
'Frapid.Config',
'{Offices}';

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Config',
'{*}';


GO

-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/10.policy/access_policy.sql --<--<--
DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_api_access_policy '{*}', @office_id, 'config.kanban_details', '{*}', 1;
EXECUTE auth.create_api_access_policy '{*}', @office_id, 'config.flag_types', '{*}', 1;
EXECUTE auth.create_api_access_policy '{*}', @office_id, 'config.flag_view', '{*}', 1;
EXECUTE auth.create_api_access_policy '{*}', @office_id, 'config.kanbans', '{*}', 1;
EXECUTE auth.create_api_access_policy '{*}', @office_id, 'config.filter_name_view', '{*}', 1;

EXECUTE auth.create_api_access_policy '{User}', @office_id, 'core.offices', '{*}', 1;
EXECUTE auth.create_api_access_policy '{Admin}', @office_id, '', '{*}', 1;



GO


-->-->-- src/Frapid.Web/Areas/Frapid.Config/db/SQL Server/1.x/1.0/src/99.ownership.sql --<--<--
EXEC sp_addrolemember  @rolename = 'db_owner', @membername  = 'frapid_db_user'
GO

EXEC sp_addrolemember  @rolename = 'db_datareader', @membername  = 'report_user'
GO

DECLARE @proc sysname
DECLARE @cmd varchar(8000)

DECLARE cur CURSOR FOR 
SELECT '[' + schema_name(schema_id) + '].[' + name + ']' FROM sys.objects
WHERE type IN('FN')
AND is_ms_shipped = 0
ORDER BY 1
OPEN cur
FETCH next from cur into @proc
WHILE @@FETCH_STATUS = 0
BEGIN
     SET @cmd = 'GRANT EXEC ON ' + @proc + ' TO report_user';
     EXEC (@cmd)

     FETCH next from cur into @proc
END
CLOSE cur
DEALLOCATE cur

GO

