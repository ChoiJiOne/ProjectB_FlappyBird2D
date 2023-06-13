@echo off

SET mode=%1
SET crash=Crash\\
SET content=Content\\
SET IP=127.0.0.1
SET PORT=7777
SET execute=FlappyBird2D\\bin\\Win64\\%mode%\\Game.exe

start %execute% Crash=%crash% Content=%content% IP=%IP% PORT=%PORT%