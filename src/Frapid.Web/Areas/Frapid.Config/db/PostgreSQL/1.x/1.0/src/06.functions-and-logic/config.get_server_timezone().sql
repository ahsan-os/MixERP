DROP FUNCTION IF EXISTS config.get_server_timezone();

CREATE FUNCTION config.get_server_timezone()
RETURNS national character varying(200)
AS
$$
BEGIN
    RETURN
        pg_catalog.pg_settings.setting
    FROM pg_catalog.pg_settings
    WHERE name = 'log_timezone';
END
$$
LANGUAGE plpgsql;

--SELECT * FROM config.get_server_timezone();

