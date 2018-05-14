@echo off
SET builddir=%~dp0

xcopy "%~dp0..\src\Frapid.Web\Resources\_Configs\Assets" "%~dp0..\src\Frapid.Web\Resources\Configs\Assets\" /s/y

@echo Creating PostgreSQL Tenant
cd %builddir%..\src\Frapid.Web\bin\
call frapid.exe create site postgresql.localhost provider Npgsql
@echo Creating SQL Server Tenant
call frapid.exe create site sqlserver.localhost provider System.Data.SqlClient

