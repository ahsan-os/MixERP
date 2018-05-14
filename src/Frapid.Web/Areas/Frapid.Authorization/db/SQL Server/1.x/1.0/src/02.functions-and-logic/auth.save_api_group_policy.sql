IF OBJECT_ID('auth.save_api_group_policy') IS NOT NULL
DROP PROCEDURE auth.save_api_group_policy;

GO

CREATE PROCEDURE auth.save_api_group_policy
(
    @role_id            integer,
    @entity_name        national character varying(500),
    @office_id          integer,
    @access_type_ids    national character varying(MAX),
    @allow_access       bit
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

        IF(@role_id IS NULL OR @office_id IS NULL)
        BEGIN
            RETURN;
        END;
        
        DELETE FROM auth.group_entity_access_policy
        WHERE auth.group_entity_access_policy.access_type_id 
        NOT IN
        (
            SELECT 
            CONVERT(integer, LTRIM(RTRIM(member)))
            FROM core.split(@access_type_ids)
        )
        AND role_id = @role_id
        AND office_id = @office_id
        AND entity_name = @entity_name
        AND access_type_id IN
        (
            SELECT access_type_id
            FROM auth.access_types
        );

        WITH access_types
        AS
        (
            SELECT 
            CONVERT(integer, LTRIM(RTRIM(member))) AS access_type_id
            FROM core.split(@access_type_ids)
        )
        
        INSERT INTO auth.group_entity_access_policy(role_id, office_id, entity_name, access_type_id, allow_access)
        SELECT @role_id, @office_id, @entity_name, access_type_id, @allow_access
        FROM access_types
        WHERE access_type_id NOT IN
        (
            SELECT access_type_id
            FROM auth.group_entity_access_policy
            WHERE auth.group_entity_access_policy.role_id = @role_id
            AND auth.group_entity_access_policy.office_id = @office_id
        );

        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

GO

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');
EXECUTE auth.create_api_access_policy '{Admin}', @office_id, '', '{*}', 1;

GO
