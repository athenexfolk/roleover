@echo off
REM Usage: migration.bat [MigrationName]

SETLOCAL

SET MIGRATION_NAME=%1
IF "%MIGRATION_NAME%"=="" SET MIGRATION_NAME=InitialCreate

REM Run the EF Core migration command

dotnet ef migrations add %MIGRATION_NAME% --project Roleover.Infrastructure --startup-project Roleover.API --output-dir Persistence/Migrations

ENDLOCAL
