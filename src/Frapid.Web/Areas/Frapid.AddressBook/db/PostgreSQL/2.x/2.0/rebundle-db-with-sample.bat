@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/PostgreSQL/2.x/2.0" true
copy addressbook.sql addressbook-sample.sql
del addressbook.sql
copy addressbook-sample.sql ..\..\addressbook-sample.sql