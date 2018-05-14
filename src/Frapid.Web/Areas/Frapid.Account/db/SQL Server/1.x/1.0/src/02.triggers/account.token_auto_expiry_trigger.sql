IF OBJECT_ID('account.token_auto_expiry_trigger') IS NOT NULL
DROP TRIGGER account.token_auto_expiry_trigger;

GO

CREATE TRIGGER account.token_auto_expiry_trigger
ON account.access_tokens
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON;
    SET XACT_ABORT ON;
	
	DECLARE @ip_address national character varying(100);
	DECLARE @user_agent national character varying(500);
	
	SELECT
		@ip_address = ip_address,
		@user_agent = user_agent
	FROM inserted;

	UPDATE account.access_tokens
	SET
		revoked = 1,
		revoked_on = getutcdate()
    WHERE ip_address = @ip_address
    AND user_agent = @user_agent;
    		

	INSERT INTO account.access_tokens(access_token_id, issued_by, audience, ip_address, user_agent, header, subject, token_id, application_id, login_id, client_token, claims, created_on, expires_on, revoked, revoked_by, revoked_on)
	SELECT access_token_id, issued_by, audience, ip_address, user_agent, header, subject, token_id, application_id, login_id, client_token, claims, created_on, expires_on, revoked, revoked_by, revoked_on
	FROM inserted;	
END;

GO