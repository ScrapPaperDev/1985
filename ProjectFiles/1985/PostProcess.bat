@echo off

cd /d "%~dp0"

echo Current directory: %CD%

move  "Disparity\bin\Release\net35\Disparity.dll" "..\1985_Unity5\Assets\Libraries\Plugins"
move  "UnityDisparity\bin\Release\net35\Disparity-Unity.dll" "..\1985_Unity5\Assets\Libraries\Plugins"
move  "Humble1985\bin\Release\net35\Humble1985.dll" "..\1985_Unity5\Assets\Libraries\Plugins"

pause
