@echo off
bundler\SqlBundler.exe ..\..\..\ "db/PostgreSQL/1.1.update" false
copy core-blank-1.1.update.sql ..\core-blank-1.1.update.sql