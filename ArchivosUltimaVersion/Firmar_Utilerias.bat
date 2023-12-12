@echo off
cd D:\PROYECTOS\SII_OFUSCATE\Release\
prompt Signature$g
cls
@echo. 
sn -Ra "GenerarPasswords.exe" "GenerarPasswords.snk" 
@echo. 
@echo. 
@echo. 
sn -Ra "SC_SqlManager.exe" "SC_SqlManager.snk" 
@echo. 
@echo. 
@echo. 
sn -Ra "ConfiguracionCs.exe" "ConfiguracionCs.snk" 
@echo. 
@echo. 
@echo.  
pause
