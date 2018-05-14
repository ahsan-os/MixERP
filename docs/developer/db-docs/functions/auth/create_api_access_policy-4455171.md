# auth.create_api_access_policy function:

```plpgsql
CREATE OR REPLACE FUNCTION auth.create_api_access_policy(_role_names text[], _office_id integer, _entity_name text, _access_types text[], _allow_access boolean)
RETURNS void
```
* Schema : [auth](../../schemas/auth.md)
* Function Name : create_api_access_policy
* Arguments : _role_names text[], _office_id integer, _entity_name text, _access_types text[], _allow_access boolean
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION auth.create_api_access_policy(_role_names text[], _office_id integer, _entity_name text, _access_types text[], _allow_access boolean)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
    DECLARE _role_id                integer;
    DECLARE _role_ids               integer[];
    DECLARE _access_type_ids        int[];
BEGIN
    IF(_role_names = '{*}'::text[]) THEN
        SELECT
            array_agg(role_id)
        INTO
            _role_ids
        FROM account.roles
		WHERE NOT account.roles.deleted;
    ELSE
        SELECT
            array_agg(role_id)
        INTO
            _role_ids
        FROM account.roles
        WHERE role_name = ANY(_role_names)
		AND NOT account.roles.deleted;
    END IF;

    IF(_access_types = '{*}'::text[]) THEN
        SELECT
            array_agg(access_type_id)
        INTO
            _access_type_ids
        FROM auth.access_types
		WHERE NOT auth.access_types.deleted;
    ELSE
        SELECT
            array_agg(access_type_id)
        INTO
            _access_type_ids
        FROM auth.access_types
        WHERE access_type_name = ANY(_access_types)
		AND NOT auth.access_types.deleted;
    END IF;

    IF(_role_ids IS NOT NULL) THEN
        FOREACH _role_id IN ARRAY _role_ids
        LOOP
            PERFORM auth.save_api_group_policy(_role_id, _entity_name, _office_id, _access_type_ids, _allow_access);
        END LOOP;
    END IF;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

