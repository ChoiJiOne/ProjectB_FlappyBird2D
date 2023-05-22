@echo off

SET mode=%1
SET crash=Crash\\
SET content=Content\\
SET execute=FlappyBird2D\\bin\\Win64\\%mode%\\Game.exe

start %execute% Crash=%crash% Content=%content%