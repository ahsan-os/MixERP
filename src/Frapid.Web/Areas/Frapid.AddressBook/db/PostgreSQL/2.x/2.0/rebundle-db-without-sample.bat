@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/PostgreSQL/2.x/2.0" false
copy addressbook.sql addressbook-blank.sql
del addressbook.sql
copy addressbook-blank.sql ..\..\addressbook-blank.sql