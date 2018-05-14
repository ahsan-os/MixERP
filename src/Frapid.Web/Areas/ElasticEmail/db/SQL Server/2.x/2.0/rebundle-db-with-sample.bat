@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/SQL Server/2.x/2.0" true
copy elastic-email.sql elastic-email-sample.sql
del elastic-email.sql
copy elastic-email-sample.sql ..\..\elastic-email-sample.sql