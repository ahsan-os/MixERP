-->-->-- src/Frapid.Web/Areas/Frapid.SchemaUpdater/db/SQL Server/1.x/1.0/src/09.menus/0.menu.sql --<--<--
EXECUTE core.create_app 'Frapid.SchemaUpdater', 'Updates', 'Updates', '1.0', 'MixERP Inc.', 'December 1, 2015', 'teal refresh', '/dashboard/updater/home', null;
EXECUTE core.create_menu 'Frapid.SchemaUpdater', 'Offices', 'Offices', '/dashboard/updater/home', 'building outline', '';

GO

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.SchemaUpdater',
'{*}';

EXECUTE auth.create_app_menu_policy
'User', 
@office_id, 
'Frapid.SchemaUpdater',
'{Offices}';

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.SchemaUpdater',
'{*}';


GO

-->-->-- src/Frapid.Web/Areas/Frapid.SchemaUpdater/db/SQL Server/1.x/1.0/src/99.ownership.sql --<--<--
EXEC sp_addrolemember  @rolename = 'db_owner', @membername  = 'frapid_db_user'
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

