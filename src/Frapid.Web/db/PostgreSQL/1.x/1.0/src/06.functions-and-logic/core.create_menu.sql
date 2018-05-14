DROP FUNCTION IF EXISTS core.create_menu
(
    _sort                                       integer,
    _app_name                                   text,
	_i18n_key									national character varying(200),
    _menu_name                                  text,
    _url                                        text,
    _icon                                       text,
    _parent_menu_id                             integer    
);

DROP FUNCTION IF EXISTS core.create_menu
(
    _sort                                       integer,
    _app_name                                   text,
	_i18n_key									national character varying(200),
    _menu_name                                  text,
    _url                                        text,
    _icon                                       text,
    _parent_menu_name                           text
);

CREATE FUNCTION core.create_menu
(
    _sort                                       integer,
    _app_name                                   text,
	_i18n_key									national character varying(200),
    _menu_name                                  text,
    _url                                        text,
    _icon                                       text,
    _parent_menu_id                             integer
    
)
RETURNS integer
AS
$$
    DECLARE _menu_id                            integer;
BEGIN
    IF EXISTS
    (
       SELECT 1
       FROM core.menus
       WHERE LOWER(app_name) = LOWER(_app_name)
       AND LOWER(menu_name) = LOWER(_menu_name)
    ) THEN
        UPDATE core.menus
        SET
			i18n_key = _i18n_key,
            sort = _sort,
            url = _url,
            icon = _icon,
            parent_menu_id = _parent_menu_id
       WHERE LOWER(app_name) = LOWER(_app_name)
       AND LOWER(menu_name) = LOWER(_menu_name)
       RETURNING menu_id INTO _menu_id;        
    ELSE
        INSERT INTO core.menus(sort, app_name, i18n_key, menu_name, url, icon, parent_menu_id)
        SELECT _sort, _app_name, _i18n_key, _menu_name, _url, _icon, _parent_menu_id
        RETURNING menu_id INTO _menu_id;        
    END IF;

    RETURN _menu_id;
END
$$
LANGUAGE plpgsql;


CREATE FUNCTION core.create_menu
(
    _sort                                       integer,
    _app_name                                   text,
	_i18n_key									national character varying(200),
    _menu_name                                  text,
    _url                                        text,
    _icon                                       text,
    _parent_menu_name                           text    
)
RETURNS integer
AS
$$
    DECLARE _parent_menu_id                     integer;
BEGIN
    SELECT menu_id INTO _parent_menu_id
    FROM core.menus
    WHERE LOWER(menu_name) = LOWER(_parent_menu_name)
    AND LOWER(app_name) = LOWER(_app_name)
	AND NOT core.menus.deleted;

    RETURN core.create_menu(_sort, _app_name, _i18n_key, _menu_name, _url, _icon, _parent_menu_id);
END
$$
LANGUAGE plpgsql;


DROP FUNCTION IF EXISTS core.create_menu
(
    _app_name                                   text,
	_i18n_key									national character varying(200),
    _menu_name                                  text,
    _url                                        text,
    _icon                                       text,
    _parent_menu_name                           text    
);

CREATE FUNCTION core.create_menu
(
    _app_name                                   text,
	_i18n_key									national character varying(200),
    _menu_name                                  text,
    _url                                        text,
    _icon                                       text,
    _parent_menu_name                           text    
)
RETURNS integer
AS
$$
BEGIN
    RETURN core.create_menu(0, _app_name, _i18n_key, _menu_name, _url, _icon, _parent_menu_name);
END
$$
LANGUAGE plpgsql;