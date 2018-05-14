IF OBJECT_ID('core.create_menu2') IS NOT NULL
DROP PROCEDURE core.create_menu2;

GO

CREATE PROCEDURE core.create_menu2
(
    @sort                                       integer,
    @app_name                                   national character varying(100),
	@i18n_key									national character varying(200),
    @menu_name                                  national character varying(100),
    @url                                        national character varying(100),
    @icon                                       national character varying(100),
    @parent_menu_id                             integer
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @menu_id                            integer;
    
    IF EXISTS
    (
       SELECT 1
       FROM core.menus
       WHERE app_name = @app_name
       AND menu_name = @menu_name
    )
    BEGIN
        UPDATE core.menus
        SET
			i18n_key = @i18n_key,
            sort = @sort,
            url = @url,
            icon = @icon,
            parent_menu_id = @parent_menu_id
       WHERE app_name = @app_name
       AND menu_name = @menu_name;
       
		SELECT
			@menu_id = menu_id
		FROM core.menus
		WHERE app_name = @app_name
		AND menu_name = @menu_name;
    END
    ELSE
    BEGIN
        INSERT INTO core.menus(sort, app_name, i18n_key, menu_name, url, icon, parent_menu_id)
        SELECT @sort, @app_name, @i18n_key, @menu_name, @url, @icon, @parent_menu_id;
        
		SET @menu_id = SCOPE_IDENTITY();
    END;

    SELECT @menu_id;
END;

GO

IF OBJECT_ID('core.create_menu') IS NOT NULL
DROP PROCEDURE core.create_menu;

GO

CREATE PROCEDURE core.create_menu
(
    @app_name                                   national character varying(100),
	@i18n_key									national character varying(200),
    @menu_name                                  national character varying(100),
    @url                                        national character varying(100),
    @icon                                       national character varying(100),
    @parent_menu_name                           national character varying(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @parent_menu_id                     integer;

    SELECT @parent_menu_id = menu_id
    FROM core.menus
    WHERE menu_name = @parent_menu_name
    AND app_name = @app_name;
	
	PRINT @parent_menu_id;
	
    EXECUTE core.create_menu2 0, @app_name, @i18n_key, @menu_name, @url, @icon, @parent_menu_id;
END;


GO