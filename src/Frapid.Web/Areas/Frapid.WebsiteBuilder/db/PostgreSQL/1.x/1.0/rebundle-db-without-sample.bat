@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/PostgreSQL/1.x/1.0" false
copy website.sql website-blank.sql
del website.sql
copy website-blank.sql ..\..\website-blank.sql