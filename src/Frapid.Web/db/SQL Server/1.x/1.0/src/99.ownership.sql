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
