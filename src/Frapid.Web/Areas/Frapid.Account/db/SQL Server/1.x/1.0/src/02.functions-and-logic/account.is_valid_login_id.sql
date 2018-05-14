IF OBJECT_ID('account.is_valid_login_id') IS NOT NULL
DROP FUNCTION account.is_valid_login_id;

GO

CREATE FUNCTION account.is_valid_login_id(@login_id bigint)
RETURNS bit
BEGIN
    IF EXISTS(SELECT 1 FROM account.logins WHERE login_id=@login_id)
	BEGIN
        RETURN 1;
    END;

    RETURN 0;
END;

GO

--SELECT account.is_valid_login_id(1);