IF OBJECT_ID('account.has_account') IS NOT NULL
DROP FUNCTION account.has_account;

GO

CREATE FUNCTION account.has_account(@email national character varying(100))
RETURNS bit
AS
BEGIN
    DECLARE @count                          integer;

    SELECT @count = count(*) 
	FROM account.users 
	WHERE email = @email
	AND account.users.deleted = 0;
    IF COALESCE(@count, 0) = 1
    BEGIN
		RETURN 1;
    END;
    
    RETURN 0;
END;


GO