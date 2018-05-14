DROP FUNCTION IF EXISTS auth.get_menu
(
    _user_id                            integer, 
    _office_id                          integer
);

CREATE FUNCTION auth.get_menu
(
    _user_id                            integer, 
    _office_id                          integer
)
RETURNS TABLE
(
    menu_id                             integer,
    app_name                            national character varying(100),
	app_i18n_key						national character varying(200),
    menu_name                           national character varying(100),
	i18n_key							national character varying(200),
    url                                 text,
    sort                                integer,
    icon                                national character varying(100),
    parent_menu_id                      integer
)
AS
$$
    DECLARE _role_id                    integer;
BEGIN
    SELECT
        role_id
    INTO
        _role_id
    FROM account.users
    WHERE account.users.user_id = _user_id
	AND NOT account.users.deleted;

    DROP TABLE IF EXISTS _temp_menu;
    CREATE TEMPORARY TABLE _temp_menu
    (
        menu_id                         integer,
        app_name                        national character varying(100),
		app_i18n_key					national character varying(200),
        menu_name                       national character varying(100),
		i18n_key						national character varying(200),
        url                             text,
        sort                            integer,
        icon                            national character varying(100),
        parent_menu_id                  integer
    ) ON COMMIT DROP;


    --GROUP POLICY
    INSERT INTO _temp_menu(menu_id)
    SELECT auth.group_menu_access_policy.menu_id
    FROM auth.group_menu_access_policy
    WHERE auth.group_menu_access_policy.office_id = _office_id
    AND auth.group_menu_access_policy.role_id = _role_id
	AND NOT auth.group_menu_access_policy.deleted;

    --USER POLICY : ALLOWED MENUS
    INSERT INTO _temp_menu(menu_id)
    SELECT auth.menu_access_policy.menu_id
    FROM auth.menu_access_policy
    WHERE auth.menu_access_policy.office_id = _office_id
    AND auth.menu_access_policy.user_id = _user_id
    AND auth.menu_access_policy.allow_access
    AND auth.menu_access_policy.menu_id NOT IN
    (
        SELECT _temp_menu.menu_id
        FROM _temp_menu
    )
	AND NOT auth.menu_access_policy.deleted;

    --USER POLICY : DISALLOWED MENUS
    DELETE FROM _temp_menu
    WHERE _temp_menu.menu_id
    IN
    (
        SELECT auth.menu_access_policy.menu_id
        FROM auth.menu_access_policy
        WHERE auth.menu_access_policy.office_id = _office_id
        AND auth.menu_access_policy.user_id = _user_id
        AND auth.menu_access_policy.disallow_access
		AND NOT auth.menu_access_policy.deleted
    );

    
    UPDATE _temp_menu
    SET
        app_name        = core.menus.app_name,
        menu_name       = core.menus.menu_name,
		i18n_key	    = core.menus.i18n_key,
        url             = core.menus.url,
        sort            = core.menus.sort,
        icon            = core.menus.icon,
        parent_menu_id  = core.menus.parent_menu_id
    FROM core.menus
    WHERE core.menus.menu_id = _temp_menu.menu_id;

    UPDATE _temp_menu
    SET
        app_i18n_key       = core.apps.i18n_key
    FROM core.apps
    WHERE core.apps.app_name = _temp_menu.app_name;
    
    RETURN QUERY
    SELECT * FROM _temp_menu;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM auth.get_menu(1, 1, '');