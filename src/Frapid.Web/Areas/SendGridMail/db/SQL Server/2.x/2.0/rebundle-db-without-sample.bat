@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/SQL Server/2.x/2.0" false
copy sendgrid.sql sendgrid-blank.sql
del sendgrid.sql
copy sendgrid-blank.sql ..\..\sendgrid-blank.sql