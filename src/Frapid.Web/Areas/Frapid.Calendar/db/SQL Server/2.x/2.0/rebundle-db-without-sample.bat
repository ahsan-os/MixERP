@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/Sql Server/2.x/2.0" false
copy calendar.sql calendar-blank.sql
del calendar.sql
copy calendar-blank.sql ..\..\calendar-blank.sql