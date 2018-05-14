@echo off
bundler\SqlBundler.exe ..\..\..\ "db/SQL Server/1.1.update" false
copy account-blank-1.1.update.sql ..\account-blank-1.1.update.sql