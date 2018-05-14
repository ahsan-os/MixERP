@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/Sql Server/2.x/2.0" true
copy social.sql social-sample.sql
del social.sql
copy social-sample.sql ..\..\social-sample.sql