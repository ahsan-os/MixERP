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
