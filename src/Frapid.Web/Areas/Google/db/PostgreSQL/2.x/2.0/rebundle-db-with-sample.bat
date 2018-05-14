@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/PostgreSQL/2.x/2.0" true
copy google.sql google-sample.sql
del google.sql
copy google-sample.sql ..\..\google-sample.sql