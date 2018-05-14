-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.1.update/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.1.update/src/04.default-values/01.default-values.sql --<--<--


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.1.update/src/05.scrud-views/empty.sql --<--<--


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.1.update/src/06.functions-and-logic/account.complete_reset.sql --<--<--
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

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.1.update/src/06.functions-and-logic/empty.sql --<--<--


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.1.update/src/10.policy/empty.sql --<--<--


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.1.update/src/99.ownership.sql --<--<--
IF NOT EXISTS
(
	SELECT * FROM sys.database_principals
	WHERE name = 'frapid_db_user'
)
BEGIN
CREATE USER frapid_db_user FROM LOGIN frapid_db_user;
END
GO

EXEC sp_addrolemember  @rolename = 'db_owner', @membername  = 'frapid_db_user'
GO

IF NOT EXISTS
(
	SELECT * FROM sys.database_principals
	WHERE name = 'report_user'
)
BEGIN
CREATE USER report_user FROM LOGIN report_user;
END
GO

EXEC sp_addrolemember  @rolename = 'db_datareader', @membername  = 'report_user'
GO


DECLARE @proc sysname
DECLARE @cmd varchar(8000)

DECLARE cur CURSOR FOR 
SELECT '[' + schema_name(schema_id) + '].[' + name + ']' FROM sys.objects
WHERE type IN('FN')
AND is_ms_shipped = 0
ORDER BY 1
OPEN cur
FETCH next from cur into @proc
WHILE @@FETCH_STATUS = 0
BEGIN
     SET @cmd = 'GRANT EXEC ON ' + @proc + ' TO report_user';
     EXEC (@cmd)

     FETCH next from cur into @proc
END
CLOSE cur
DEALLOCATE cur

GO

