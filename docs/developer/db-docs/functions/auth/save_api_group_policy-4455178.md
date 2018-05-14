# auth.save_api_group_policy function:

```plpgsql
CREATE OR REPLACE FUNCTION auth.save_api_group_policy(_role_id integer, _entity_name character varying, _office_id integer, _access_type_ids integer[], _allow_access boolean)
RETURNS void
```
* Schema : [auth](../../schemas/auth.md)
* Function Name : save_api_group_policy
* Arguments : _role_id integer, _entity_name character varying, _office_id integer, _access_type_ids integer[], _allow_access boolean
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION auth.save_api_group_policy(_role_id integer, _entity_name character varying, _office_id integer, _access_type_ids integer[], _allow_access boolean)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF(_role_id IS NULL OR _office_id IS NULL) THEN
        RETURN;
    END IF;
    
    DELETE FROM auth.group_entity_access_policy
    WHERE auth.group_entity_access_policy.access_type_id 
    NOT IN
    (
        SELECT * from unnest(_access_type_ids)
    )
    AND role_id = _role_id
    AND office_id = _office_id
    AND entity_name = _entity_name
    AND access_type_id IN
    (
        SELECT access_type_id
        FROM auth.access_types
    );

    WITH access_types
    AS
    (
        SELECT unnest(_access_type_ids) AS _access_type_id
    )
    
    INSERT INTO auth.group_entity_access_policy(role_id, office_id, entity_name, access_type_id, allow_access)
    SELECT _role_id, _office_id, _entity_name, _access_type_id, _allow_access
    FROM access_types
    WHERE _access_type_id NOT IN
    (
        SELECT access_type_id
        FROM auth.group_entity_access_policy
        WHERE auth.group_entity_access_policy.role_id = _role_id
        AND auth.group_entity_access_policy.office_id = _office_id
    );

    RETURN;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

