IF OBJECT_ID('account.has_active_reset_request') IS NOT NULL
DROP FUNCTION account.has_active_reset_request;

GO

CREATE FUNCTION account.has_active_reset_request(@email national character varying(500))
RETURNS bit
AS
BEGIN
    DECLARE @expires_on                     datetimeoffset = dateadd(d, 1, getutcdate());

    IF EXISTS
    (
        SELECT * FROM account.reset_requests
        WHERE email = @email
        AND expires_on <= @expires_on
		AND account.reset_requests.deleted = 0
    )
    BEGIN        
        RETURN 1;
    END;

    RETURN 0;
END;

GO