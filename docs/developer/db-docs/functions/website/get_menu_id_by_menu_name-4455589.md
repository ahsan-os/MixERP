# website.get_menu_id_by_menu_name function:

```plpgsql
CREATE OR REPLACE FUNCTION website.get_menu_id_by_menu_name(_menu_name character varying)
RETURNS integer
```
* Schema : [website](../../schemas/website.md)
* Function Name : get_menu_id_by_menu_name
* Arguments : _menu_name character varying
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION website.get_menu_id_by_menu_name(_menu_name character varying)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN
    (
		SELECT menu_id
		FROM website.menus
		WHERE menu_name = _menu_name
		AND NOT website.menus.deleted
	);
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

