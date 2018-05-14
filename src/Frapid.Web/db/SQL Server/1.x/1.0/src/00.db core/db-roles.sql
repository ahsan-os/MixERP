IF NOT EXISTS 
(
	SELECT name  
     FROM master.sys.server_principals
     WHERE name = 'frapid_db_user'
 )
BEGIN
    CREATE LOGIN [frapid_db_user] WITH PASSWORD = N'change-on-deployment@123'
END;
GO

IF NOT EXISTS 
(
	SELECT name  
    FROM master.sys.server_principals
    WHERE name = 'report_user'
)
BEGIN
    CREATE LOGIN [report_user] WITH PASSWORD = N'change-on-deployment@123'
END;
GO



IF NOT EXISTS 
(
	SELECT * FROM sys.database_principals 
	WHERE name = 'frapid_db_user'
)
BEGIN
    CREATE USER frapid_db_user FOR LOGIN frapid_db_user;
    EXEC sp_addrolemember 'db_owner', 'frapid_db_user';
END;
GO


IF NOT EXISTS 
(
	SELECT * FROM sys.database_principals 
	WHERE name = 'report_user'
)
BEGIN
    CREATE USER report_user FOR LOGIN report_user;
    EXEC sp_addrolemember 'db_datareader', 'report_user';
END;
GO
