@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/SQL Server/2.x/2.0" false
copy google.sql google-blank.sql
del google.sql
copy google-blank.sql ..\..\google-blank.sql