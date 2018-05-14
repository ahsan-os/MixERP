IF OBJECT_ID('account.confirm_registration') IS NOT NULL
DROP PROCEDURE account.confirm_registration;

GO

CREATE PROCEDURE account.confirm_registration(@token uniqueidentifier)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @can_confirm        bit;
    DECLARE @office_id          integer;
    DECLARE @role_id            integer;

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;
        
        SET @can_confirm = account.can_confirm_registration(@token);

        IF(@can_confirm = 0)
        BEGIN
            SELECT 0;
            RETURN;
        END;

        SELECT
        TOP 1
            @office_id = registration_office_id        
        FROM account.configuration_profiles
        WHERE is_active = 1;

        INSERT INTO account.users(email, password, office_id, role_id, name, phone)
        SELECT email, password, @office_id, account.get_registration_role_id(email), name, phone
        FROM account.registrations
        WHERE registration_id = @token
    	AND confirmed = 0;

        UPDATE account.registrations
        SET 
            confirmed = 1, 
            confirmed_on = getutcdate()
        WHERE registration_id = @token;
    

        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;

        SELECT 1;
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
