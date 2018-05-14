IF OBJECT_ID('auth.create_app_menu_policy') IS NOT NULL
DROP PROCEDURE auth.create_app_menu_policy;

GO


CREATE PROCEDURE auth.create_app_menu_policy
(
    @role_name                      national character varying(500),
    @office_id                      integer,
    @app_name                       national character varying(500),
    @menu_names                     national character varying(MAX)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @role_id                integer;
    DECLARE @menus					TABLE(menu_name national character varying(100));
    DECLARE @menu_ids               national character varying(MAX);

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

    	INSERT INTO @menus
        SELECT member
        FROM core.split(@menu_names);

        SELECT
            @role_id = role_id        
        FROM account.roles
        WHERE role_name = @role_name;

        IF(@menu_names = '{*}')
        BEGIN
            SELECT @menu_ids = COALESCE(@menu_ids + ',', '') + CONVERT(national character varying(500), menu_id)
            FROM core.menus
            WHERE app_name = @app_name;
        END
        ELSE
        BEGIN
            SELECT @menu_ids = COALESCE(@menu_ids + ',', '') + CONVERT(national character varying(500), menu_id)
            FROM core.menus
            WHERE app_name = @app_name
            AND menu_name IN (SELECT * FROM @menus);
        END;
        
        EXECUTE auth.save_group_menu_policy @role_id, @office_id, @menu_ids, @app_name;    

                
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
