IF OBJECT_ID('auth.has_access') IS NOT NULL
DROP PROCEDURE auth.has_access;

GO

CREATE PROCEDURE auth.has_access(@login_id integer, @entity national character varying(500), @access_type_id integer)
AS
BEGIN    
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	DECLARE @user_id									integer = account.get_user_id_by_login_id(@login_id);
    DECLARE @role_id                                    integer;
    DECLARE @group_all_policy                           bit;
    DECLARE @group_all@entity_specific_access_type      bit;
    DECLARE @group_specific_entity_all_access_type      bit;
    DECLARE @group_explicit_policy                      bit;
    DECLARE @effective_group_policy                     bit;
    DECLARE @user_all_policy                            bit;
    DECLARE @user_all_entity_specific_access_type       bit;
    DECLARE @user_specific_entity_all_access_type       bit;
    DECLARE @user_explicit_policy                       bit;
    DECLARE @effective_user_policy                      bit;

    --USER AUTHORIZATION BASED ON ALL ENTITIES AND ALL ACCESS TYPES
    SELECT 
        @user_all_policy = allow_access 
    FROM auth.entity_access_policy
    WHERE user_id = @user_id
    AND access_type_id IS NULL
    AND COALESCE(entity_name, '') = '';

    --USER AUTHORIZATION BASED ON ALL ENTITIES AND SPECIFIED ACCESS TYPE
    SELECT 
        @user_all_entity_specific_access_type = allow_access
    FROM auth.entity_access_policy
    WHERE user_id = @user_id
    AND access_type_id = @access_type_id
    AND COALESCE(entity_name, '') = '';

    --USER AUTHORIZATION BASED ON SPECIFIED ENTITY AND ALL ACCESS TYPES
    SELECT
        @user_specific_entity_all_access_type = allow_access        
    FROM auth.entity_access_policy
    WHERE user_id = @user_id
    AND access_type_id IS NULL
    AND entity_name = @entity;

    --USER AUTHORIZATION BASED ON SPECIFIED ENTITY AND SPECIFIED ACCESS TYPE
    SELECT 
        @user_explicit_policy = allow_access
    FROM auth.entity_access_policy
    WHERE user_id = @user_id
    AND access_type_id = @access_type_id
    AND entity_name = @entity;

    --EFFECTIVE USER POLICY BASED ON PRECEDENCE.
    SET @effective_user_policy = COALESCE(@user_explicit_policy, @user_specific_entity_all_access_type, @user_all_entity_specific_access_type, @user_all_policy);

    IF(@effective_user_policy IS NOT NULL)
	BEGIN
        SELECT @effective_user_policy;
        RETURN;
    END;


    SELECT @role_id = role_id FROM account.users WHERE user_id = @user_id;


    --GROUP AUTHORIZATION BASED ON ALL ENTITIES AND ALL ACCESS TYPES
    SELECT 
        @group_all_policy = allow_access        
    FROM auth.group_entity_access_policy
    WHERE role_id = @role_id
    AND access_type_id IS NULL
    AND COALESCE(entity_name, '') = '';

    --GROUP AUTHORIZATION BASED ON ALL ENTITIES AND SPECIFIED ACCESS TYPE
    SELECT 
        @group_all@entity_specific_access_type = allow_access
    FROM auth.group_entity_access_policy
    WHERE role_id = @role_id
    AND access_type_id = @access_type_id
    AND COALESCE(entity_name, '') = '';

    --GROUP AUTHORIZATION BASED ON SPECIFIED ENTITY AND ALL ACCESS TYPES
    SELECT
        @group_specific_entity_all_access_type = allow_access
    FROM auth.group_entity_access_policy
    WHERE role_id = @role_id
    AND access_type_id IS NULL
    AND entity_name = @entity;

    --GROUP AUTHORIZATION BASED ON SPECIFIED ENTITY AND SPECIFIED ACCESS TYPE
    SELECT 
        @group_explicit_policy = allow_access        
    FROM auth.group_entity_access_policy
    WHERE role_id = @role_id
    AND access_type_id = @access_type_id
    AND entity_name = @entity;

    --EFFECTIVE GROUP POLICY BASED ON PRECEDENCE.
    SET @effective_group_policy = COALESCE(@group_explicit_policy, @group_specific_entity_all_access_type, @group_all@entity_specific_access_type, @group_all_policy);

    SELECT COALESCE(@effective_group_policy, 0);
    RETURN;
END;


GO
