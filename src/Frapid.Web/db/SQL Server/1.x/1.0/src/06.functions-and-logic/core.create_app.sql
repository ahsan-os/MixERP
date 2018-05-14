IF OBJECT_ID('core.create_app') IS NOT NULL
DROP PROCEDURE core.create_app;

GO

CREATE PROCEDURE core.create_app
(
    @app_name                                   national character varying(100),
	@i18n_key									national character varying(200),
    @name                                       national character varying(100),
    @version_number                             national character varying(100),
    @publisher                                  national character varying(100),
    @published_on                               date,
    @icon                                       national character varying(MAX),
    @landing_url                                national character varying(100),
    @dependencies                               national character varying(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF EXISTS
    (
        SELECT 1
        FROM core.apps
        WHERE LOWER(core.apps.app_name) = LOWER(@app_name)
    )
    BEGIN
        UPDATE core.apps
        SET
			i18n_key = @i18n_key,
            name = @name,
            version_number = @version_number,
            publisher = @publisher,
            published_on = @published_on,
            icon = @icon,
            landing_url = @landing_url
        WHERE
            app_name = @app_name;
    END
    ELSE
    BEGIN
        INSERT INTO core.apps(app_name, i18n_key, name, version_number, publisher, published_on, icon, landing_url)
        SELECT @app_name, @i18n_key, @name, @version_number, @publisher, @published_on, @icon, @landing_url;
    END;

    DELETE FROM core.app_dependencies
    WHERE app_name = @app_name;

	IF(ltrim(rtrim(COALESCE(@dependencies, ''))) != '')
	BEGIN
		INSERT INTO core.app_dependencies(app_name, depends_on)
		SELECT @app_name, member
		FROM core.array_split(@dependencies);
	END;
END;

GO