@echo off
bundler\SqlBundler.exe ..\..\..\ "db/PostgreSQL/1.1.update" false
copy accounr-blank-1.1.update.sql ..\account-blank-1.1.update.sql