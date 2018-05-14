@echo off
SET builddir=%~dp0

cd %builddir%..\src\Frapid.Web\bin\

@echo Creating a Test App
call frapid.exe create app TestApp

rmdir %builddir%..\src\Frapid.Web\Areas\TestApp /Q /S
