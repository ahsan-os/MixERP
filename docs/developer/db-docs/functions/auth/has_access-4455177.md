# auth.has_access function:

```plpgsql
CREATE OR REPLACE FUNCTION auth.has_access(_login_id bigint, _entity text, _access_type_id integer)
RETURNS boolean
```
* Schema : [auth](../../schemas/auth.md)
* Function Name : has_access
* Arguments : _login_id bigint, _entity text, _access_type_id integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION auth.has_access(_login_id bigint, _entity text, _access_type_id integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    DECLARE _user_id                                    integer = account.get_user_id_by_login_id(_login_id);
    DECLARE _role_id                                    integer;
    DECLARE _group_all_policy                           boolean = false;
    DECLARE _group_all_entity_specific_access_type      boolean = false;
    DECLARE _group_specific_entity_all_access_type      boolean = false;
    DECLARE _group_explicit_policy                      boolean = false;
    DECLARE _effective_group_policy                     boolean = false;
    DECLARE _user_all_policy                            boolean = false;
    DECLARE _user_all_entity_specific_access_type       boolean = false;
    DECLARE _user_specific_entity_all_access_type       boolean = false;
    DECLARE _user_explicit_policy                       boolean = false;
    DECLARE _effective_user_policy                      boolean = false;
BEGIN    
    --USER AUTHORIZATION BASED ON ALL ENTITIES AND ALL ACCESS TYPES
    SELECT 
        allow_access 
    INTO 
        _user_all_policy
    FROM auth.entity_access_policy
    WHERE auth.entity_access_policy.user_id = _user_id
    AND auth.entity_access_policy.access_type_id IS NULL
    AND COALESCE(entity_name, '') = ''
	AND NOT auth.entity_access_policy.deleted;

    --USER AUTHORIZATION BASED ON ALL ENTITIES AND SPECIFIED ACCESS TYPE
    SELECT 
        allow_access
    INTO
        _user_all_entity_specific_access_type
    FROM auth.entity_access_policy
    WHERE auth.entity_access_policy.user_id = _user_id
    AND auth.entity_access_policy.access_type_id = _access_type_id
    AND COALESCE(entity_name, '') = ''
	AND NOT auth.entity_access_policy.deleted;

    --USER AUTHORIZATION BASED ON SPECIFIED ENTITY AND ALL ACCESS TYPES
    SELECT
        allow_access
    INTO
        _user_specific_entity_all_access_type
    FROM auth.entity_access_policy
    WHERE auth.entity_access_policy.user_id = _user_id
    AND auth.entity_access_policy.access_type_id IS NULL
    AND auth.entity_access_policy.entity_name = _entity
	AND NOT auth.entity_access_policy.deleted;

    --USER AUTHORIZATION BASED ON SPECIFIED ENTITY AND SPECIFIED ACCESS TYPE
    SELECT 
        allow_access
    INTO
        _user_explicit_policy
    FROM auth.entity_access_policy
    WHERE auth.entity_access_policy.user_id = _user_id
    AND auth.entity_access_policy.access_type_id = _access_type_id
    AND auth.entity_access_policy.entity_name = _entity
	AND NOT auth.entity_access_policy.deleted;

    --EFFECTIVE USER POLICY BASED ON PRECEDENCE.
    _effective_user_policy := COALESCE(_user_explicit_policy, _user_specific_entity_all_access_type, _user_all_entity_specific_access_type, _user_all_policy);

    IF(_effective_user_policy IS NOT NULL) THEN
        RETURN _effective_user_policy;
    END IF;

    SELECT role_id INTO _role_id FROM account.users WHERE user_id = _user_id;

    --GROUP AUTHORIZATION BASED ON ALL ENTITIES AND ALL ACCESS TYPES
    SELECT 
        allow_access 
    INTO 
        _group_all_policy
    FROM auth.group_entity_access_policy
    WHERE auth.group_entity_access_policy.role_id = _role_id
    AND auth.group_entity_access_policy.access_type_id IS NULL
    AND COALESCE(entity_name, '') = ''
	AND NOT auth.group_entity_access_policy.deleted;

    --GROUP AUTHORIZATION BASED ON ALL ENTITIES AND SPECIFIED ACCESS TYPE
    SELECT 
        allow_access
    INTO
        _group_all_entity_specific_access_type
    FROM auth.group_entity_access_policy
    WHERE auth.group_entity_access_policy.role_id = _role_id
    AND auth.group_entity_access_policy.access_type_id = _access_type_id
    AND COALESCE(entity_name, '') = ''
	AND NOT auth.group_entity_access_policy.deleted;

    --GROUP AUTHORIZATION BASED ON SPECIFIED ENTITY AND ALL ACCESS TYPES
    SELECT
        allow_access
    INTO
        _group_specific_entity_all_access_type
    FROM auth.group_entity_access_policy
    WHERE auth.group_entity_access_policy.role_id = _role_id
    AND auth.group_entity_access_policy.access_type_id IS NULL
    AND entity_name = _entity
	AND NOT auth.group_entity_access_policy.deleted;

    --GROUP AUTHORIZATION BASED ON SPECIFIED ENTITY AND SPECIFIED ACCESS TYPE
    SELECT 
        allow_access
    INTO
        _group_explicit_policy
    FROM auth.group_entity_access_policy
    WHERE auth.group_entity_access_policy.role_id = _role_id
    AND auth.group_entity_access_policy.access_type_id = _access_type_id
    AND auth.group_entity_access_policy.entity_name = _entity
	AND NOT auth.group_entity_access_policy.deleted;

    --EFFECTIVE GROUP POLICY BASED ON PRECEDENCE.
    _effective_group_policy := COALESCE(_group_explicit_policy, _group_specific_entity_all_access_type, _group_all_entity_specific_access_type, _group_all_policy);

    RETURN COALESCE(_effective_group_policy, false);    
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

