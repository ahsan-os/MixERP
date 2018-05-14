# core.create_menu function:

```plpgsql
CREATE OR REPLACE FUNCTION core.create_menu(_sort integer, _app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_id integer)
RETURNS integer
```
* Schema : [core](../../schemas/core.md)
* Function Name : create_menu
* Arguments : _sort integer, _app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_id integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.create_menu(_sort integer, _app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_id integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
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
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

