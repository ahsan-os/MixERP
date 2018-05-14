IF OBJECT_ID('auth.save_group_menu_policy') IS NOT NULL
DROP PROCEDURE auth.save_group_menu_policy;

GO


CREATE PROCEDURE auth.save_group_menu_policy
(
    @role_id        integer,
    @office_id      integer,
    @menu_ids       national character varying(MAX),
    @app_name       national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	DECLARE @menus	TABLE(menu_id integer);

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

    	INSERT INTO @menus
        SELECT CONVERT(integer, LTRIM(RTRIM(member)))
        FROM core.split(@menu_ids);    	
    	
        IF(@role_id IS NULL OR @office_id IS NULL)
        BEGIN
            RETURN;
        END;
        
        DELETE FROM auth.group_menu_access_policy
        WHERE auth.group_menu_access_policy.menu_id NOT IN(SELECT * from @menus)
        AND role_id = @role_id
        AND office_id = @office_id
        AND menu_id IN
        (
            SELECT menu_id
            FROM core.menus
            WHERE @app_name = ''
            OR app_name = @app_name
        );
        
        INSERT INTO auth.group_menu_access_policy(role_id, office_id, menu_id)
        SELECT @role_id, @office_id, menu_id
        FROM @menus
        WHERE menu_id NOT IN
        (
            SELECT menu_id
            FROM auth.group_menu_access_policy
            WHERE auth.group_menu_access_policy.role_id = @role_id
            AND auth.group_menu_access_policy.office_id = @office_id
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
