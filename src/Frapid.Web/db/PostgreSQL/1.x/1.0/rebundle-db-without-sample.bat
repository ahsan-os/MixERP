@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/PostgreSQL/1.x/1.0" false
copy core.sql core-blank.sql
del core.sql
copy core-blank.sql ..\..\core-blank.sql