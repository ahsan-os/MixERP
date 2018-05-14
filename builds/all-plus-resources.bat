@echo off
SET builddir=%~dp0

call build-resource.bat
cd %builddir%
call all.bat

