@echo off

cd /d "%~dp0"

echo Current directory: %CD%

move  "bin\Release\net35\Disparity.dll" "..\1985_Unity5\Assets\Libraries\Plugins"

pause
