@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/PostgreSQL/2.x/2.0" true
copy sendgrid.sql sendgrid-sample.sql
del sendgrid.sql
copy sendgrid-sample.sql ..\..\sendgrid-sample.sql