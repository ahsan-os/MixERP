@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/SQL Server/2.x/2.0" false
copy elastic-email.sql elastic-email-blank.sql
del elastic-email.sql
copy elastic-email-blank.sql ..\..\elastic-email-blank.sql