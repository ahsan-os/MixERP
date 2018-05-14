@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/SQL Server/1.x/1.0" false
copy account.sql account-blank.sql
del account.sql
copy account-blank.sql ..\..\account-blank.sql