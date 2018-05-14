@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/PostgreSQL/1.x/1.0" true
copy website.sql website-sample.sql
del website.sql
copy website-sample.sql ..\..\website-sample.sql