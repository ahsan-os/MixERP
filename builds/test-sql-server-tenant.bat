@echo off
SET builddir=%~dp0

cd %builddir%..\src\Frapid.Web\bin\

@echo Creating SQL Server Tenant
call frapid.exe create site sqlserver.test provider System.Data.SqlClient cleanup when done
