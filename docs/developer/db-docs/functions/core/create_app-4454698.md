# core.create_app function:

```plpgsql
CREATE OR REPLACE FUNCTION core.create_app(_app_name text, _i18n_key character varying, _name text, _version_number text, _publisher text, _published_on date, _icon text, _landing_url text, _dependencies text[])
RETURNS void
```
* Schema : [core](../../schemas/core.md)
* Function Name : create_app
* Arguments : _app_name text, _i18n_key character varying, _name text, _version_number text, _publisher text, _published_on date, _icon text, _landing_url text, _dependencies text[]
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.create_app(_app_name text, _i18n_key character varying, _name text, _version_number text, _publisher text, _published_on date, _icon text, _landing_url text, _dependencies text[])
 RETURNS void
 LANGUAGE plpgsql
AS $function$
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
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

