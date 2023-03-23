@echo off

SET visualstudio=%1
SET debugpath=FlappyBird2D\\bin\\Win64\\Debug
SET releasepath=FlappyBird2D\\bin\\Win64\\Release
SET shippingpath=FlappyBird2D\\bin\\Win64\\Shipping
SET dllpath=ThirdParty\\Lib

if "%visualstudio%" == "" (
    echo make a choice visual studio version...
    GOTO:EOF
)

Bin\\premake5.exe %visualstudio%

if not exist debugpath (
    mkdir %debugpath%
    xcopy /Y /S /Q %dllpath%\*.dll %debugpath%\
)

if not exist releasepath (
    mkdir %releasepath%
    xcopy /Y /S /Q %dllpath%\*.dll %releasepath%\
)

if not exist shippingpath (
    mkdir %shippingpath%
    xcopy /Y /S /Q %dllpath%\*.dll %shippingpath%\
)