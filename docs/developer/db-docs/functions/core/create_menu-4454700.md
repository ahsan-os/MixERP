# core.create_menu function:

```plpgsql
CREATE OR REPLACE FUNCTION core.create_menu(_sort integer, _app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_name text)
RETURNS integer
```
* Schema : [core](../../schemas/core.md)
* Function Name : create_menu
* Arguments : _sort integer, _app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_name text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.create_menu(_sort integer, _app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_name text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
    DECLARE _parent_menu_id                     integer;
BEGIN
    SELECT menu_id INTO _parent_menu_id
    FROM core.menus
    WHERE LOWER(menu_name) = LOWER(_parent_menu_name)
    AND LOWER(app_name) = LOWER(_app_name)
	AND NOT core.menus.deleted;

    RETURN core.create_menu(_sort, _app_name, _i18n_key, _menu_name, _url, _icon, _parent_menu_id);
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

