@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/Sql Server/2.x/2.0" true
copy calendar.sql calendar-sample.sql
del calendar.sql
copy calendar-sample.sql ..\..\calendar-sample.sql