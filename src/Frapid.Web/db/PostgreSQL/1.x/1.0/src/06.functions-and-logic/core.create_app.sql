DROP FUNCTION IF EXISTS core.create_app
(
    _app_name                                   text,
	_i18n_key									national character varying(200),
    _name                                       text,
    _version_number                             text,
    _publisher                                  text,
    _published_on                               date,
    _icon                                       text,
    _landing_url                                text,
    _dependencies                               text[]
);

CREATE FUNCTION core.create_app
(
    _app_name                                   text,
	_i18n_key									national character varying(200),
    _name                                       text,
    _version_number                             text,
    _publisher                                  text,
    _published_on                               date,
    _icon                                       text,
    _landing_url                                text,
    _dependencies                               text[]
)
RETURNS void
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 1
        FROM core.apps
        WHERE LOWER(core.apps.app_name) = LOWER(_app_name)
    ) THEN
        UPDATE core.apps
        SET
			i18n_key = _i18n_key,
            name = _name,
            version_number = _version_number,
            publisher = _publisher,
            published_on = _published_on,
            icon = _icon,
            landing_url = _landing_url
        WHERE
            app_name = _app_name;
    ELSE
        INSERT INTO core.apps(app_name, i18n_key, name, version_number, publisher, published_on, icon, landing_url)
        SELECT _app_name, _i18n_key, _name, _version_number, _publisher, _published_on, _icon, _landing_url;
    END IF;

    DELETE FROM core.app_dependencies
    WHERE app_name = _app_name;

    INSERT INTO core.app_dependencies(app_name, depends_on)
    SELECT _app_name, UNNEST(_dependencies);
END
$$
LANGUAGE plpgsql;
