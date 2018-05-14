DROP FUNCTION IF EXISTS auth.create_app_menu_policy
(
    _role_name                      text,
    _office_id                      integer,
    _app_name                       text,
    _menu_names                     text[]
);

CREATE FUNCTION auth.create_app_menu_policy
(
    _role_name                      text,
    _office_id                      integer,
    _app_name                       text,
    _menu_names                     text[]
)
RETURNS void
AS
$$
    DECLARE _role_id                integer;
    DECLARE _menu_ids               int[];
BEGIN
    SELECT
        account.roles.role_id
    INTO
        _role_id
    FROM account.roles
    WHERE account.roles.role_name = _role_name
	AND NOT account.roles.deleted;

    IF(_menu_names = '{*}'::text[]) THEN
        SELECT
            array_agg(menu_id)
        INTO
            _menu_ids
        FROM core.menus
        WHERE core.menus.app_name = _app_name
		AND NOT core.menus.deleted;
    ELSE
        SELECT
            array_agg(menu_id)
        INTO
            _menu_ids
        FROM core.menus
        WHERE core.menus.app_name = _app_name
        AND core.menus.menu_name = ANY(_menu_names)
		AND NOT core.menus.deleted;
    END IF;
    
    PERFORM auth.save_group_menu_policy(_role_id, _office_id, array_to_string(_menu_ids, ','), _app_name);    
END
$$
LANGUAGE plpgsql;
