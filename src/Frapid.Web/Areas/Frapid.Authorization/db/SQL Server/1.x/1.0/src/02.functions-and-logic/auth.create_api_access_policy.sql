IF OBJECT_ID('auth.create_api_access_policy') IS NOT NULL
DROP PROCEDURE auth.create_api_access_policy;

GO

CREATE PROCEDURE auth.create_api_access_policy
(
    @role_names                     national character varying(MAX),
    @office_id                      integer,
    @entity_name                    national character varying(500),
    @access_type_names              national character varying(MAX),
    @allow_access                   bit
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @role_id                integer;    
    DECLARE @access_types			TABLE(access_type_name national character varying(100));
    DECLARE @roles					TABLE(role_name national character varying(100));
    DECLARE @access_type_ids        national character varying(MAX);
    DECLARE @role_ids               TABLE(role_id integer);
	
    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

    	INSERT INTO @access_types
    	SELECT 
            CONVERT(integer, LTRIM(RTRIM(member)))	
    	FROM core.array_split(REPLACE(@access_type_names, '*', ''));
    	
    	INSERT INTO @roles
    	SELECT
    	*
    	FROM core.array_split(REPLACE(@role_names, '*', ''));
    	
        IF(@role_names = '{*}')
        BEGIN
    		INSERT INTO @role_ids
            SELECT role_id
            FROM account.roles;
        END
        ELSE
        BEGIN
    		INSERT INTO @role_ids
            SELECT role_id
            FROM account.roles
            WHERE role_name IN (SELECT * from @roles);
        END;

        IF(@access_type_names = '{*}')
        BEGIN
            SELECT @access_type_ids = COALESCE(@access_type_ids + ',', '') + CONVERT(varchar, access_type_id)
            FROM auth.access_types;
        END
        ELSE
        BEGIN
            SELECT @access_type_ids = COALESCE(@access_type_ids + ',', '') + CONVERT(varchar, access_type_id)
            FROM auth.access_types
            WHERE access_type_name IN (SELECT * FROM @access_types);
        END;


    	DECLARE curse CURSOR FOR SELECT role_id FROM @role_ids
    	OPEN curse
    	FETCH NEXT FROM curse INTO @role_id
    	WHILE @@Fetch_Status=0
    	BEGIN
            EXECUTE auth.save_api_group_policy @role_id, @entity_name, @office_id, @access_type_ids, @allow_access;
    		FETCH NEXT FROM curse INTO @role_id
    	END;
                
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
