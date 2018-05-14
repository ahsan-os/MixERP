@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/SQL Server/2.x/2.0" false
copy sparkpostmail.sql sparkpostmail-blank.sql
del sparkpostmail.sql
copy sparkpostmail-blank.sql ..\..\sparkpostmail-blank.sql