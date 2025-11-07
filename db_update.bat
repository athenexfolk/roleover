@echo off
REM Usage: db_update.bat [MigrationName]

REM Update database using dotnet ef database update

cd Roleover.API
dotnet ef database update
cd ..
