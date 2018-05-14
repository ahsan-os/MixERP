@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/SQL Server/2.x/2.0" true
copy google.sql google-sample.sql
del google.sql
copy google-sample.sql ..\..\google-sample.sql