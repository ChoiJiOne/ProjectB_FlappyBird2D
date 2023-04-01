@echo off

echo start build script...

SET build=msbuild.exe
SET project=FlappyBird2D
SET visualstudio=%1
SET option=%2
SET premake5=Bin\\premake5.exe

if not exist %project% (
    echo can't find %project% visual studio solution
)

echo run premake5.exe...
%premake5% %visualstudio%

%build% %project%\\%project%.sln -property:Configuration=%option% -target:Rebuild