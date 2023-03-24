@echo off

echo run hot reload...

SET project=FlappyBird2D
SET visualstudio=%1
SET premake5=Bin\\premake5.exe

if not exist %project% (
	echo don't exist %project%...
    echo please run "GenerateProjectFiles.bat" script...
) else (
    echo run premake5.exe...
    %premake5% %visualstudio%

    echo done...
    PAUSE
)