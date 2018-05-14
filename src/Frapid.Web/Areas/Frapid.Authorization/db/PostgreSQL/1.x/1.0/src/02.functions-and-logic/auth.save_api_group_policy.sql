DROP FUNCTION IF EXISTS auth.save_api_group_policy
(
    _role_id            integer,
    _entity_name        national character varying(500),
    _office_id          integer,
    _access_type_ids    int[],
    _allow_access       boolean
);

CREATE FUNCTION auth.save_api_group_policy
(
    _role_id            integer,
    _entity_name        national character varying(500),
    _office_id          integer,
    _access_type_ids    int[],
    _allow_access       boolean
)
RETURNS void
AS
$$
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
$$
LANGUAGE plpgsql;

