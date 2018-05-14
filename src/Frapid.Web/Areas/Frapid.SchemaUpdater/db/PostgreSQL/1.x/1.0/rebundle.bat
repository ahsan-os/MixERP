@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/PostgreSQL/1.x/1.0" false
copy updater.sql ..\..\updater.sql