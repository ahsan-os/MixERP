-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
EXECUTE dbo.drop_schema 'auth';
GO
CREATE SCHEMA auth;

GO

CREATE TABLE auth.access_types
(
    access_type_id                          integer PRIMARY KEY,
    access_type_name                        national character varying(48) NOT NULL,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted									bit DEFAULT(0)
);

CREATE UNIQUE INDEX access_types_uix
ON auth.access_types(access_type_name)
WHERE deleted = 0;


CREATE TABLE auth.group_entity_access_policy
(
    group_entity_access_policy_id           integer IDENTITY NOT NULL PRIMARY KEY,
    entity_name                             national character varying(500) NULL,
    office_id                               integer NOT NULL REFERENCES core.offices,
    role_id                                 integer NOT NULL REFERENCES account.roles,
    access_type_id                          integer NULL REFERENCES auth.access_types,
    allow_access                            bit NOT NULL,
    audit_user_id                           integer NULL REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted									bit DEFAULT(0)
);

CREATE TABLE auth.entity_access_policy
(
    entity_access_policy_id                 integer IDENTITY NOT NULL PRIMARY KEY,
    entity_name                             national character varying(500) NULL,
    office_id                               integer NOT NULL REFERENCES core.offices,
    user_id                                 integer NOT NULL REFERENCES account.users,
    access_type_id                          integer NULL REFERENCES auth.access_types,
    allow_access                            bit NOT NULL,
    audit_user_id                           integer NULL REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted									bit DEFAULT(0)
);

CREATE TABLE auth.group_menu_access_policy
(
    group_menu_access_policy_id             bigint IDENTITY PRIMARY KEY,
    office_id                               integer NOT NULL REFERENCES core.offices,
    menu_id                                 integer NOT NULL REFERENCES core.menus,
    role_id                                 integer REFERENCES account.roles,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted									bit DEFAULT(0)
);

CREATE UNIQUE INDEX menu_access_uix
ON auth.group_menu_access_policy(office_id, menu_id, role_id)
WHERE deleted = 0;

CREATE TABLE auth.menu_access_policy
(
    menu_access_policy_id                   bigint IDENTITY PRIMARY KEY,
    office_id                               integer NOT NULL REFERENCES core.offices,
    menu_id                                 integer NOT NULL REFERENCES core.menus,
    user_id                                 integer NULL REFERENCES account.users,
    allow_access                            bit,
    disallow_access                         bit,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted									bit DEFAULT(0),
											CONSTRAINT menu_access_policy_access_chk CHECK(NOT(allow_access= 1 AND disallow_access = 1))
);


CREATE UNIQUE INDEX menu_access_policy_uix
ON auth.menu_access_policy(office_id, menu_id, user_id)
WHERE deleted = 0;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/02.functions-and-logic/auth.create_api_access_policy.sql --<--<--
IF OBJECT_ID('auth.create_api_access_policy') IS NOT NULL
DROP PROCEDURE auth.create_api_access_policy;

GO

CREATE PROCEDURE auth.create_api_access_policy
(
    @role_names                     national character varying(MAX),
    @office_id                      integer,
    @entity_name                    national character varying(500),
    @access_type_names              national character varying(MAX),
    @allow_access                   bit
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @role_id                integer;    
    DECLARE @access_types			TABLE(access_type_name national character varying(100));
    DECLARE @roles					TABLE(role_name national character varying(100));
    DECLARE @access_type_ids        national character varying(MAX);
    DECLARE @role_ids               TABLE(role_id integer);
	
    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

    	INSERT INTO @access_types
    	SELECT 
            CONVERT(integer, LTRIM(RTRIM(member)))	
    	FROM core.array_split(REPLACE(@access_type_names, '*', ''));
    	
    	INSERT INTO @roles
    	SELECT
    	*
    	FROM core.array_split(REPLACE(@role_names, '*', ''));
    	
        IF(@role_names = '{*}')
        BEGIN
    		INSERT INTO @role_ids
            SELECT role_id
            FROM account.roles;
        END
        ELSE
        BEGIN
    		INSERT INTO @role_ids
            SELECT role_id
            FROM account.roles
            WHERE role_name IN (SELECT * from @roles);
        END;

        IF(@access_type_names = '{*}')
        BEGIN
            SELECT @access_type_ids = COALESCE(@access_type_ids + ',', '') + CONVERT(varchar, access_type_id)
            FROM auth.access_types;
        END
        ELSE
        BEGIN
            SELECT @access_type_ids = COALESCE(@access_type_ids + ',', '') + CONVERT(varchar, access_type_id)
            FROM auth.access_types
            WHERE access_type_name IN (SELECT * FROM @access_types);
        END;


    	DECLARE curse CURSOR FOR SELECT role_id FROM @role_ids
    	OPEN curse
    	FETCH NEXT FROM curse INTO @role_id
    	WHILE @@Fetch_Status=0
    	BEGIN
            EXECUTE auth.save_api_group_policy @role_id, @entity_name, @office_id, @access_type_ids, @allow_access;
    		FETCH NEXT FROM curse INTO @role_id
    	END;
                
        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/02.functions-and-logic/auth.create_app_menu_policy.sql --<--<--
IF OBJECT_ID('auth.create_app_menu_policy') IS NOT NULL
DROP PROCEDURE auth.create_app_menu_policy;

GO


CREATE PROCEDURE auth.create_app_menu_policy
(
    @role_name                      national character varying(500),
    @office_id                      integer,
    @app_name                       national character varying(500),
    @menu_names                     national character varying(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @role_id                integer;
    DECLARE @menus					TABLE(menu_name national character varying(100));
    DECLARE @menu_ids               national character varying(MAX);

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

    	INSERT INTO @menus
        SELECT member
        FROM core.split(@menu_names);

        SELECT
            @role_id = role_id        
        FROM account.roles
        WHERE role_name = @role_name;

        IF(@menu_names = '{*}')
        BEGIN
            SELECT @menu_ids = COALESCE(@menu_ids + ',', '') + CONVERT(national character varying(500), menu_id)
            FROM core.menus
            WHERE app_name = @app_name;
        END
        ELSE
        BEGIN
            SELECT @menu_ids = COALESCE(@menu_ids + ',', '') + CONVERT(national character varying(500), menu_id)
            FROM core.menus
            WHERE app_name = @app_name
            AND menu_name IN (SELECT * FROM @menus);
        END;
        
        EXECUTE auth.save_group_menu_policy @role_id, @office_id, @menu_ids, @app_name;    

                
        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/02.functions-and-logic/auth.get_apps.sql --<--<--
IF OBJECT_ID('auth.get_apps') IS NOT NULL
DROP FUNCTION auth.get_apps;

GO

CREATE FUNCTION auth.get_apps(@user_id integer, @office_id integer)
RETURNS @result TABLE
(
	app_id								integer,
    app_name                            national character varying(500),
	i18n_key							national character varying(500),
    name                                national character varying(500),
    version_number                      national character varying(500),
    publisher                           national character varying(500),
    published_on                        date,
    icon                                national character varying(MAX),
    landing_url                         national character varying(500)
)
AS
BEGIN
    INSERT INTO @result
    SELECT
		core.apps.app_id,
        core.apps.app_name,
		core.apps.i18n_key,
        core.apps.name,
        core.apps.version_number,
        core.apps.publisher,
        core.apps.published_on,
        core.apps.icon,
        core.apps.landing_url
    FROM core.apps
    WHERE core.apps.app_name IN
    (
        SELECT DISTINCT menus.app_name
        FROM auth.get_menu(@user_id, @office_id)
        AS menus
    );
    
    RETURN;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/02.functions-and-logic/auth.get_group_menu_policy.sql --<--<--
IF OBJECT_ID('auth.get_group_menu_policy') IS NOT NULL
DROP PROCEDURE auth.get_group_menu_policy;

GO


CREATE PROCEDURE auth.get_group_menu_policy
(
    @role_id        integer,
    @office_id      integer
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	DECLARE @result TABLE
	(
		row_number                      integer,
		menu_id                         integer,
		app_name                        national character varying(500),
		app_i18n_key					national character varying(500),
		menu_name                       national character varying(500),
		i18n_key						national character varying(500),
		allowed                         bit,
		url                             national character varying(500),
		sort                            integer,
		icon                            national character varying(100),
		parent_menu_id                  integer
	);

    INSERT INTO @result(menu_id)
    SELECT core.menus.menu_id
    FROM core.menus
    ORDER BY core.menus.app_name, core.menus.sort, core.menus.menu_id;

    --GROUP POLICY
    UPDATE @result
    SET allowed = 1
    FROM @result AS result
    INNER JOIN auth.group_menu_access_policy
    ON auth.group_menu_access_policy.menu_id = result.menu_id
    WHERE office_id = @office_id
    AND role_id = @role_id;
   
    
    UPDATE @result
    SET
        app_name        = core.menus.app_name,
        menu_name       = core.menus.menu_name,
		i18n_key		= core.menus.i18n_key,
        url             = core.menus.url,
        sort            = core.menus.sort,
        icon            = core.menus.icon,
        parent_menu_id  = core.menus.parent_menu_id
    FROM @result AS result
    INNER JOIN core.menus
    ON core.menus.menu_id = result.menu_id;

    UPDATE @result
    SET
        app_i18n_key    = core.apps.i18n_key
    FROM @result AS result
    INNER JOIN core.apps
    ON core.apps.app_name = result.app_name;

    SELECT * FROM @result
    ORDER BY app_name, sort, menu_id;
END;

GO

--EXECUTE auth.get_group_menu_policy 1, 1, '';


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/02.functions-and-logic/auth.get_menu.sql --<--<--
IF OBJECT_ID('auth.get_menu') IS NOT NULL
DROP FUNCTION auth.get_menu;

GO


CREATE FUNCTION auth.get_menu
(
    @user_id                            integer, 
    @office_id                          integer
)
RETURNS @result TABLE
(
	menu_id                             integer,
	app_name                            national character varying(500),
	app_i18n_key						national character varying(500),
	menu_name                           national character varying(500),
	i18n_key							national character varying(500),
	url                                 national character varying(500),
	sort                                integer,
	icon                                national character varying(500),
	parent_menu_id                      integer
)
AS
BEGIN
    DECLARE @role_id                    integer;

    SELECT
        @role_id = role_id        
    FROM account.users
    WHERE user_id = @user_id;

    --GROUP POLICY
    INSERT INTO @result(menu_id)
    SELECT auth.group_menu_access_policy.menu_id
    FROM auth.group_menu_access_policy
    WHERE office_id = @office_id
    AND role_id = @role_id;

    --USER POLICY : ALLOWED MENUS
    INSERT INTO @result(menu_id)
    SELECT auth.menu_access_policy.menu_id
    FROM auth.menu_access_policy
    WHERE office_id = @office_id
    AND user_id = @user_id
    AND allow_access = 1
    AND auth.menu_access_policy.menu_id NOT IN
    (
        SELECT menu_id
        FROM @result
    );

    --USER POLICY : DISALLOWED MENUS
    DELETE FROM @result
    WHERE menu_id
    IN
    (
        SELECT auth.menu_access_policy.menu_id
        FROM auth.menu_access_policy
        WHERE office_id = @office_id
        AND user_id = @user_id
        AND disallow_access = 1
    );

    
    UPDATE @result
    SET
        app_name        = core.menus.app_name,
		i18n_key		= core.menus.i18n_key,
        menu_name       = core.menus.menu_name,
        url             = core.menus.url,
        sort            = core.menus.sort,
        icon            = core.menus.icon,
        parent_menu_id  = core.menus.parent_menu_id
    FROM @result AS result
    INNER JOIN core.menus
    ON core.menus.menu_id = result.menu_id;

    UPDATE @result
    SET
        app_i18n_key   = core.apps.i18n_key
    FROM @result AS result
    INNER JOIN core.apps    
    ON core.apps.app_name = result.app_name;    

    RETURN;
END;

--EXECUTE auth.get_menu 1, 1, '';


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/02.functions-and-logic/auth.get_user_menu_policy.sql --<--<--
IF OBJECT_ID('auth.get_user_menu_policy') IS NOT NULL
DROP PROCEDURE auth.get_user_menu_policy;

GO

CREATE PROCEDURE auth.get_user_menu_policy
(
    @user_id        integer,
    @office_id      integer
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	DECLARE @result TABLE
	(
		row_number                      integer,
		menu_id                         integer,
		app_name                        national character varying(500),
		app_i18n_key					national character varying(500),
		menu_name                       national character varying(500),
		i18n_key						national character varying(500),
		allowed                         bit,
		disallowed                      bit,
		url                             national character varying(500),
		sort                            integer,
		icon                            national character varying(100),
		parent_menu_id                  integer
	);
	
    DECLARE @role_id                    integer;

    SELECT
        @role_id = role_id
    FROM account.users
    WHERE user_id = @user_id;

    INSERT INTO @result(menu_id)
    SELECT core.menus.menu_id
    FROM core.menus
    ORDER BY core.menus.app_name, core.menus.sort, core.menus.menu_id;

    --GROUP POLICY
    UPDATE @result
    SET allowed = 1
    FROM  @result AS result
    INNER JOIN auth.group_menu_access_policy
    ON auth.group_menu_access_policy.menu_id = result.menu_id
    WHERE office_id = @office_id
    AND role_id = @role_id;
    
    --USER POLICY : ALLOWED MENUS
    UPDATE @result
    SET allowed = 1
    FROM @result AS result 
    INNER JOIN auth.menu_access_policy
    ON auth.menu_access_policy.menu_id = result.menu_id
    WHERE office_id = @office_id
    AND user_id = @user_id
    AND allow_access = 1;


    --USER POLICY : DISALLOWED MENUS
    UPDATE @result
    SET disallowed = 1
    FROM @result AS result
    INNER JOIN auth.menu_access_policy
    ON result.menu_id = auth.menu_access_policy.menu_id 
    WHERE office_id = @office_id
    AND user_id = @user_id
    AND disallow_access = 1;
   
    
    UPDATE @result
    SET
        app_name        = core.menus.app_name,
		i18n_key		= core.menus.i18n_key,
        menu_name       = core.menus.menu_name,
        url             = core.menus.url,
        sort            = core.menus.sort,
        icon            = core.menus.icon,
        parent_menu_id  = core.menus.parent_menu_id
    FROM @result AS result 
    INNER JOIN core.menus
    ON core.menus.menu_id = result.menu_id;

    UPDATE @result
    SET
        app_i18n_key       = core.apps.i18n_key
    FROM @result AS result
    INNER JOIN core.apps
    ON core.apps.app_name = result.app_name;    

    SELECT * FROM @result
    ORDER BY app_name, sort, menu_id;
END;

GO

--EXECUTE auth.get_user_menu_policy 1, 1, '';


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/02.functions-and-logic/auth.has_access.sql --<--<--
IF OBJECT_ID('auth.has_access') IS NOT NULL
DROP PROCEDURE auth.has_access;

GO

CREATE PROCEDURE auth.has_access(@login_id integer, @entity national character varying(500), @access_type_id integer)
AS
BEGIN    
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	DECLARE @user_id									integer = account.get_user_id_by_login_id(@login_id);
    DECLARE @role_id                                    integer;
    DECLARE @group_all_policy                           bit;
    DECLARE @group_all@entity_specific_access_type      bit;
    DECLARE @group_specific_entity_all_access_type      bit;
    DECLARE @group_explicit_policy                      bit;
    DECLARE @effective_group_policy                     bit;
    DECLARE @user_all_policy                            bit;
    DECLARE @user_all_entity_specific_access_type       bit;
    DECLARE @user_specific_entity_all_access_type       bit;
    DECLARE @user_explicit_policy                       bit;
    DECLARE @effective_user_policy                      bit;

    --USER AUTHORIZATION BASED ON ALL ENTITIES AND ALL ACCESS TYPES
    SELECT 
        @user_all_policy = allow_access 
    FROM auth.entity_access_policy
    WHERE user_id = @user_id
    AND access_type_id IS NULL
    AND COALESCE(entity_name, '') = '';

    --USER AUTHORIZATION BASED ON ALL ENTITIES AND SPECIFIED ACCESS TYPE
    SELECT 
        @user_all_entity_specific_access_type = allow_access
    FROM auth.entity_access_policy
    WHERE user_id = @user_id
    AND access_type_id = @access_type_id
    AND COALESCE(entity_name, '') = '';

    --USER AUTHORIZATION BASED ON SPECIFIED ENTITY AND ALL ACCESS TYPES
    SELECT
        @user_specific_entity_all_access_type = allow_access        
    FROM auth.entity_access_policy
    WHERE user_id = @user_id
    AND access_type_id IS NULL
    AND entity_name = @entity;

    --USER AUTHORIZATION BASED ON SPECIFIED ENTITY AND SPECIFIED ACCESS TYPE
    SELECT 
        @user_explicit_policy = allow_access
    FROM auth.entity_access_policy
    WHERE user_id = @user_id
    AND access_type_id = @access_type_id
    AND entity_name = @entity;

    --EFFECTIVE USER POLICY BASED ON PRECEDENCE.
    SET @effective_user_policy = COALESCE(@user_explicit_policy, @user_specific_entity_all_access_type, @user_all_entity_specific_access_type, @user_all_policy);

    IF(@effective_user_policy IS NOT NULL)
	BEGIN
        SELECT @effective_user_policy;
        RETURN;
    END;


    SELECT @role_id = role_id FROM account.users WHERE user_id = @user_id;


    --GROUP AUTHORIZATION BASED ON ALL ENTITIES AND ALL ACCESS TYPES
    SELECT 
        @group_all_policy = allow_access        
    FROM auth.group_entity_access_policy
    WHERE role_id = @role_id
    AND access_type_id IS NULL
    AND COALESCE(entity_name, '') = '';

    --GROUP AUTHORIZATION BASED ON ALL ENTITIES AND SPECIFIED ACCESS TYPE
    SELECT 
        @group_all@entity_specific_access_type = allow_access
    FROM auth.group_entity_access_policy
    WHERE role_id = @role_id
    AND access_type_id = @access_type_id
    AND COALESCE(entity_name, '') = '';

    --GROUP AUTHORIZATION BASED ON SPECIFIED ENTITY AND ALL ACCESS TYPES
    SELECT
        @group_specific_entity_all_access_type = allow_access
    FROM auth.group_entity_access_policy
    WHERE role_id = @role_id
    AND access_type_id IS NULL
    AND entity_name = @entity;

    --GROUP AUTHORIZATION BASED ON SPECIFIED ENTITY AND SPECIFIED ACCESS TYPE
    SELECT 
        @group_explicit_policy = allow_access        
    FROM auth.group_entity_access_policy
    WHERE role_id = @role_id
    AND access_type_id = @access_type_id
    AND entity_name = @entity;

    --EFFECTIVE GROUP POLICY BASED ON PRECEDENCE.
    SET @effective_group_policy = COALESCE(@group_explicit_policy, @group_specific_entity_all_access_type, @group_all@entity_specific_access_type, @group_all_policy);

    SELECT COALESCE(@effective_group_policy, 0);
    RETURN;
END;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/02.functions-and-logic/auth.save_api_group_policy.sql --<--<--
IF OBJECT_ID('auth.save_api_group_policy') IS NOT NULL
DROP PROCEDURE auth.save_api_group_policy;

GO

CREATE PROCEDURE auth.save_api_group_policy
(
    @role_id            integer,
    @entity_name        national character varying(500),
    @office_id          integer,
    @access_type_ids    national character varying(MAX),
    @allow_access       bit
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

        IF(@role_id IS NULL OR @office_id IS NULL)
        BEGIN
            RETURN;
        END;
        
        DELETE FROM auth.group_entity_access_policy
        WHERE auth.group_entity_access_policy.access_type_id 
        NOT IN
        (
            SELECT 
            CONVERT(integer, LTRIM(RTRIM(member)))
            FROM core.split(@access_type_ids)
        )
        AND role_id = @role_id
        AND office_id = @office_id
        AND entity_name = @entity_name
        AND access_type_id IN
        (
            SELECT access_type_id
            FROM auth.access_types
        );

        WITH access_types
        AS
        (
            SELECT 
            CONVERT(integer, LTRIM(RTRIM(member))) AS access_type_id
            FROM core.split(@access_type_ids)
        )
        
        INSERT INTO auth.group_entity_access_policy(role_id, office_id, entity_name, access_type_id, allow_access)
        SELECT @role_id, @office_id, @entity_name, access_type_id, @allow_access
        FROM access_types
        WHERE access_type_id NOT IN
        (
            SELECT access_type_id
            FROM auth.group_entity_access_policy
            WHERE auth.group_entity_access_policy.role_id = @role_id
            AND auth.group_entity_access_policy.office_id = @office_id
        );

        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

GO

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');
EXECUTE auth.create_api_access_policy '{Admin}', @office_id, '', '{*}', 1;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/02.functions-and-logic/auth.save_group_menu_policy.sql --<--<--
IF OBJECT_ID('auth.save_group_menu_policy') IS NOT NULL
DROP PROCEDURE auth.save_group_menu_policy;

GO


CREATE PROCEDURE auth.save_group_menu_policy
(
    @role_id        integer,
    @office_id      integer,
    @menu_ids       national character varying(MAX),
    @app_name       national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	DECLARE @menus	TABLE(menu_id integer);

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

    	INSERT INTO @menus
        SELECT CONVERT(integer, LTRIM(RTRIM(member)))
        FROM core.split(@menu_ids);    	
    	
        IF(@role_id IS NULL OR @office_id IS NULL)
        BEGIN
            RETURN;
        END;
        
        DELETE FROM auth.group_menu_access_policy
        WHERE auth.group_menu_access_policy.menu_id NOT IN(SELECT * from @menus)
        AND role_id = @role_id
        AND office_id = @office_id
        AND menu_id IN
        (
            SELECT menu_id
            FROM core.menus
            WHERE @app_name = ''
            OR app_name = @app_name
        );
        
        INSERT INTO auth.group_menu_access_policy(role_id, office_id, menu_id)
        SELECT @role_id, @office_id, menu_id
        FROM @menus
        WHERE menu_id NOT IN
        (
            SELECT menu_id
            FROM auth.group_menu_access_policy
            WHERE auth.group_menu_access_policy.role_id = @role_id
            AND auth.group_menu_access_policy.office_id = @office_id
        );

        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/02.functions-and-logic/auth.save_user_menu_policy.sql --<--<--
IF OBJECT_ID('auth.save_user_menu_policy') IS NOT NULL
DROP PROCEDURE auth.save_user_menu_policy;

GO


CREATE PROCEDURE auth.save_user_menu_policy
(
    @user_id                        integer,
    @office_id                      integer,
    @allowed_menu_ids               national character varying(MAX),
    @disallowed_menu_ids            national character varying(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	DECLARE @allowed_menus			TABLE(menu_id integer);
	DECLARE @disallowed_menus		TABLE(menu_id integer);

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;
        
    	INSERT INTO @allowed_menus
        SELECT  CONVERT(integer, LTRIM(RTRIM(member))) FROM core.split(@allowed_menu_ids);

    	INSERT INTO @disallowed_menus
        SELECT  CONVERT(integer, LTRIM(RTRIM(member))) FROM core.split(@disallowed_menu_ids);

        INSERT INTO auth.menu_access_policy(office_id, user_id, menu_id)
        SELECT @office_id, @user_id, core.menus.menu_id
        FROM core.menus
        WHERE core.menus.menu_id NOT IN
        (
            SELECT auth.menu_access_policy.menu_id
            FROM auth.menu_access_policy
            WHERE user_id = @user_id
            AND office_id = @office_id
        );

        UPDATE auth.menu_access_policy
        SET allow_access = NULL, disallow_access = NULL
        WHERE user_id = @user_id
        AND office_id = @office_id;

        UPDATE auth.menu_access_policy
        SET allow_access = 1
        WHERE user_id = @user_id
        AND office_id = @office_id
        AND menu_id IN(SELECT * from @allowed_menus);

        UPDATE auth.menu_access_policy
        SET disallow_access = 1
        WHERE user_id = @user_id
        AND office_id = @office_id
        AND menu_id IN(SELECT * from @disallowed_menus);

        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/03.menus/0.menus.sql --<--<--
EXECUTE core.create_app 'Frapid.Authorization', 'Authorization', 'Authorization', '1.0', 'MixERP Inc.', 'December 1, 2015', 'purple privacy', '/dashboard/authorization/menu-access/group-policy', '{Frapid.Account}';



EXECUTE core.create_menu 'Frapid.Authorization', 'EntityAccessPolicy', 'Entity Access Policy', '', 'lock', '';
EXECUTE core.create_menu 'Frapid.Authorization', 'GroupEntityAccessPolicy', 'Group Entity Access Policy', '/dashboard/authorization/entity-access/group-policy', 'users', 'Entity Access Policy';
EXECUTE core.create_menu 'Frapid.Authorization', 'UserEntityAccessPolicy', 'User Entity Access Policy', '/dashboard/authorization/entity-access/user-policy', 'user', 'Entity Access Policy';
EXECUTE core.create_menu 'Frapid.Authorization', 'MenuAccessPolicy', 'Menu Access Policy', '', 'toggle on', '';
EXECUTE core.create_menu 'Frapid.Authorization', 'GroupPolicy', 'Group Policy', '/dashboard/authorization/menu-access/group-policy', 'users', 'Menu Access Policy';
EXECUTE core.create_menu 'Frapid.Authorization', 'UserPolicy', 'User Policy', '/dashboard/authorization/menu-access/user-policy', 'user', 'Menu Access Policy';

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/03.menus/1.menu-policy.sql --<--<--
GO

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');
EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Authorization',
'{*}';

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Account',
'{*}';



EXECUTE auth.create_app_menu_policy
'User', 
@office_id, 
'Frapid.Account',
'{Configuration Profile, Email Templates, Account Verification, Password Reset, Welcome Email, Welcome Email (3rd Party)}';

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Account',
'{*}';

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Authorization',
'{*}';

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/03.menus/2.menu-policy-account.sql --<--<--


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/04.default-values/01.default-values.sql --<--<--
INSERT INTO auth.access_types(access_type_id, access_type_name)
SELECT 1, 'Read'            UNION ALL
SELECT 2, 'Create'          UNION ALL
SELECT 3, 'Edit'            UNION ALL
SELECT 4, 'Delete'          UNION ALL
SELECT 5, 'CreateFilter'    UNION ALL
SELECT 6, 'DeleteFilter'    UNION ALL
SELECT 7, 'Export'          UNION ALL
SELECT 8, 'ExportData'      UNION ALL
SELECT 9, 'ImportData'      UNION ALL
SELECT 10, 'Execute'        UNION ALL
SELECT 11, 'Verify';

-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/05.views/auth.entity_view.sql --<--<--
IF OBJECT_ID('auth.entity_view') IS NOT NULL
DROP VIEW auth.entity_view;
GO

CREATE VIEW auth.entity_view
AS
SELECT 
    information_schema.tables.table_schema, 
    information_schema.tables.table_name, 
    information_schema.tables.table_schema + '.' +
    information_schema.tables.table_name AS object_name, 
    information_schema.tables.table_type
FROM information_schema.tables 
WHERE (information_schema.tables.table_type='BASE TABLE' OR information_schema.tables.table_type='VIEW')
AND table_schema NOT IN ('pg_catalog', 'information_schema');


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/10.policy/access_policy.sql --<--<--
GO

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');
EXECUTE auth.create_api_access_policy '{Admin}', @office_id, '', '{*}', 1;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Authorization/db/SQL Server/1.x/1.0/src/99.ownership.sql --<--<--
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

