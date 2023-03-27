@echo off

echo run generate project files...

SET visualstudio=%1
SET premake5=Bin\\premake5.exe
SET project=FlappyBird2D

if "%visualstudio%" == "" (
    echo make a choice visual studio version...
    GOTO:EOF
)

if not exist Crash (
    echo can't find Crash directory...
    echo generate Crash directory...
    mkdir Crash
)

echo run premake5.exe...
%premake5% %visualstudio%

echo copy DLL files...
python Script\\CopyDLL.py

echo start visual studio solution...
start %project%\\%project%.sln
PAUSE