IF OBJECT_ID('account.complete_reset') IS NOT NULL
DROP PROCEDURE account.complete_reset;

GO

CREATE PROCEDURE account.complete_reset
(
    @request_id                     uniqueidentifier,
    @password                       national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @user_id                integer;
    DECLARE @email                  national character varying(500);

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

        SELECT
            @user_id = account.users.user_id,
            @email = account.users.email
        FROM account.reset_requests
        INNER JOIN account.users
        ON account.users.user_id = account.reset_requests.user_id
        WHERE account.reset_requests.request_id = @request_id
        AND expires_on >= getutcdate()
    	AND account.reset_requests.deleted = 0;

        
        UPDATE account.users
        SET
            password = @password
        WHERE user_id = @user_id;

        UPDATE account.reset_requests
        SET confirmed = 1, confirmed_on = getutcdate()
        WHERE account.reset_requests.request_id = @request_id;

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