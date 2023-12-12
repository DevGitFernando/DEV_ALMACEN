
/*
	select top 1 'EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = ' + char(39) + name + char(39) + ' ' + char(13) + char(10) + 
		'Go ' + char(13) + char(10) +		
		'drop database [' +  name + ']' 
	from sysdatabases 
	where name like  'int_%' 
*/	  


Declare 
	@sDbName varchar(500), 
	@sSql varchar(max) 

/* 
EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'INT_localhost_SII_11_001_0015__SAN DIEGO DE LA UNIÓN_CAISES_20130531_161804'
GO
USE [master]
GO
DROP DATABASE [INT_localhost_SII_11_001_0015__SAN DIEGO DE LA UNIÓN_CAISES_20130531_161804]
GO
*/ 		
	
	-- Barrer todos los campos de la tabla
	Declare #BDS 
	Cursor For 
	Select Name 
	From sysdatabases 
	Where name like  'int_%' 
	Open #BDS Fetch From #BDS into @sDbName 
	While @@Fetch_status = 0 
		Begin      
			Set @sSql = 'Exec msdb.dbo.sp_delete_database_backuphistory @database_name = ' + char(39) + @sDbName + char(39) + ' ' + char(10) --+ char(13) 
			Exec(@sSql) 
			--print @sSql 
			
			Set @sSql = @sSql + 'Drop database [' + @sDbName + '] '  + char(10)  
			Exec(@sSql) 
			--print @sSql 
			
			
			Fetch next From #BDS into @sDbName
		End 
	Close #BDS              
	Deallocate #BDS	
	
	
	