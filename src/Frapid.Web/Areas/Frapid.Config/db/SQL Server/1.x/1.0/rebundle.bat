@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/SQL Server/1.x/1.0" false
copy config.sql ..\..\config.sql