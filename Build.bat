@echo off

echo start build script...

SET build=msbuild.exe
SET project=FlappyBird2D
SET visualstudio=%1
SET option=%2

if not exist %project% (
    echo can't find %project% visual studio solution
    echo generate visual studio solution...
    Bin\\premake5.exe %visualstudio%
)

%build% %project%\\%project%.sln -property:Configuration=%option% -target:Rebuild