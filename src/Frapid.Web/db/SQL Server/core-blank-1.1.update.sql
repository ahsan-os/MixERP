-->-->-- src/Frapid.Web/db/SQL Server/1.1.update/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--


-->-->-- src/Frapid.Web/db/SQL Server/1.1.update/src/04.default-values/01.default-values.sql --<--<--


-->-->-- src/Frapid.Web/db/SQL Server/1.1.update/src/05.scrud-views/empty.sql --<--<--


-->-->-- src/Frapid.Web/db/SQL Server/1.1.update/src/06.functions-and-logic/empty.sql --<--<--


-->-->-- src/Frapid.Web/db/SQL Server/1.1.update/src/06.triggers/core.check_parent_office_trigger.sql --<--<--
IF OBJECT_ID('core.check_parent_office_trigger') IS NOT NULL
DROP TRIGGER core.check_parent_office_trigger

GO

CREATE TRIGGER core.check_parent_office_trigger
ON core.offices 
FOR INSERT, UPDATE
AS 
BEGIN
	IF((SELECT office_id FROM INSERTED) = (SELECT parent_office_id FROM INSERTED))
		RAISERROR('Same office cannot be a parent office', 16, 1);
	RETURN;
END;

GO

-->-->-- src/Frapid.Web/db/SQL Server/1.1.update/src/10.policy/empty.sql --<--<--


-->-->-- src/Frapid.Web/db/SQL Server/1.1.update/src/99.ownership.sql --<--<--
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

