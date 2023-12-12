

net stop MSSQLSERVER

CD 

sqlservr.exe -m -T4022 -T3659 -q"Latin1_General_CI_AS" -s"MSSQLSERVER" 

net start MSSQLSERVER 
