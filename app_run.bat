
@echo off
REM Usage: app_run.bat [watch]

REM Check for 'watch' argument
IF /I "%1"=="watch" (
	dotnet watch run --project Roleover.API
) ELSE (
	dotnet run --project Roleover.API
)
