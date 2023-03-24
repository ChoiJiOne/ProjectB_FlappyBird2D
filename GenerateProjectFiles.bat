@echo off

SET visualstudio=%1
SET premake5=Bin\\premake5.exe
SET project=FlappyBird2D

if "%visualstudio%" == "" (
    echo make a choice visual studio version...
    GOTO:EOF
)

%premake5% %visualstudio%
python Script\\CopyDLL.py
start %project%\\%project%.sln
PAUSE