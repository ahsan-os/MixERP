IF OBJECT_ID('account.is_valid_client_token') IS NOT NULL
DROP FUNCTION account.is_valid_client_token;

GO

CREATE FUNCTION account.is_valid_client_token(@client_token national character varying(MAX), @ip_address national character varying(500), @user_agent national character varying(500))
RETURNS bit
AS
BEGIN
    DECLARE @created_on datetimeoffset;
    DECLARE @expires_on datetimeoffset;
    DECLARE @revoked bit;

    IF(COALESCE(@client_token, '') = '')
    BEGIN
        RETURN 0;
    END;

    SELECT
        @created_on = created_on,
        @expires_on = expires_on,
        @revoked = revoked
    FROM account.access_tokens
    WHERE client_token = @client_token
    AND ip_address = @ip_address
    AND user_agent = @user_agent
	AND account.access_tokens.deleted = 0;
    
    IF(COALESCE(@revoked, 1)) = 1
    BEGIN
        RETURN 0;
    END;

    IF(@created_on > getutcdate())
    BEGIN
        RETURN 0;
    END;

    IF(COALESCE(@expires_on, getutcdate()) <= getutcdate())
    BEGIN
        RETURN 0;
    END;
    
    RETURN 1;
END;

GO
