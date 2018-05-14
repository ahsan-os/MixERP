# core.create_menu function:

```plpgsql
CREATE OR REPLACE FUNCTION core.create_menu(_app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_name text)
RETURNS integer
```
* Schema : [core](../../schemas/core.md)
* Function Name : create_menu
* Arguments : _app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_name text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.create_menu(_app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_name text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN core.create_menu(0, _app_name, _i18n_key, _menu_name, _url, _icon, _parent_menu_name);
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

