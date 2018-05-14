@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/PostgreSQL/1.x/1.0" false
copy account.sql account-blank.sql
del account.sql
copy account-blank.sql ..\..\account-blank.sql