# auth.save_user_menu_policy function:

```plpgsql
CREATE OR REPLACE FUNCTION auth.save_user_menu_policy(_user_id integer, _office_id integer, _allowed text, _disallowed text)
RETURNS void
```
* Schema : [auth](../../schemas/auth.md)
* Function Name : save_user_menu_policy
* Arguments : _user_id integer, _office_id integer, _allowed text, _disallowed text
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION auth.save_user_menu_policy(_user_id integer, _office_id integer, _allowed text, _disallowed text)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
    DECLARE _allowed_menu_ids       integer[] = public.text_to_int_array(_allowed);
    DECLARE _disallowed_menu_ids    integer[] = public.text_to_int_array(_disallowed);
BEGIN
    INSERT INTO auth.menu_access_policy(office_id, user_id, menu_id)
    SELECT _office_id, _user_id, core.menus.menu_id
    FROM core.menus
    WHERE core.menus.menu_id NOT IN
    (
        SELECT auth.menu_access_policy.menu_id
        FROM auth.menu_access_policy
        WHERE user_id = _user_id
        AND office_id = _office_id
    );

    UPDATE auth.menu_access_policy
    SET allow_access = NULL, disallow_access = NULL
    WHERE user_id = _user_id
    AND office_id = _office_id;

    UPDATE auth.menu_access_policy
    SET allow_access = true
    WHERE user_id = _user_id
    AND office_id = _office_id
    AND menu_id IN(SELECT * from unnest(_allowed_menu_ids));

    UPDATE auth.menu_access_policy
    SET disallow_access = true
    WHERE user_id = _user_id
    AND office_id = _office_id
    AND menu_id IN(SELECT * from unnest(_disallowed_menu_ids));

    
    RETURN;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

