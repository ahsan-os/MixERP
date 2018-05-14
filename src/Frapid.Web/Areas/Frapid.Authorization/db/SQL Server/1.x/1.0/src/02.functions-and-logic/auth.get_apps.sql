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
