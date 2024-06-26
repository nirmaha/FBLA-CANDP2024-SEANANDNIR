@echo off
setlocal

echo Cleaning the project...
dotnet clean
if %ERRORLEVEL% neq 0 (
    echo Error during cleaning.
    exit /b %ERRORLEVEL%
)

echo Publishing the project...
dotnet publish -r win-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true --self-contained true -o publish
if %ERRORLEVEL% neq 0 (
    echo Error during publishing.
    exit /b %ERRORLEVEL%
)

echo Publish completed successfully.
echo Published files are located in: %CD%\publish

endlocal
