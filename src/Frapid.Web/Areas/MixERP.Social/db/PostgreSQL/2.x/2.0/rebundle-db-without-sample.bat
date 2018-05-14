@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/PostgreSQL/2.x/2.0" false
copy social.sql social-blank.sql
del social.sql
copy social-blank.sql ..\..\social-blank.sql