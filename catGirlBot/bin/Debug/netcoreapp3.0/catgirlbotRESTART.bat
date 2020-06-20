@echo off
:loop
start catGirlBot.exe ...
timeout /t 600 >null
taskkill /f /im catGirlBot.exe >nul
goto loop