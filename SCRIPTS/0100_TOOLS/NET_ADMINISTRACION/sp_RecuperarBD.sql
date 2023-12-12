------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'sp_RecuperarBD' and xType = 'P' ) 
   Drop Proc sp_RecuperarBD 
Go 

Create Proc sp_RecuperarBD ( @BaseDeDatos varchar(100) = '', @Ejecutar int = 1 ) 
As 
Begin 
Set NoCount On 
Declare @sSql varchar(1000) 

/* 
	Set @sSql = 'ALTER DATABASE ' + char(39) + @BaseDeDatos + char(39) + ' SET EMERGENCY ' + char(13) + 
		'ALTER DATABASE ' + char(39) + @BaseDeDatos + char(39) + ' SET SINGLE_USER ' + char(13) + 
		'DBCC CHECKDB ( ' + char(39) + @BaseDeDatos + char(39) + ', REPAIR_ALLOW_DATA_LOSS )WITH NO_INFOMSGS ' + char(13) + 
		'ALTER DATABASE ' + char(39) + @BaseDeDatos + char(39) + ' SET MULTI_USER ' 
*/ 
 
	Set @sSql = 
		'ALTER DATABASE ' + @BaseDeDatos + ' SET SINGLE_USER WITH ROLLBACK IMMEDIATE' + char(13) + 	
		'ALTER DATABASE ' + @BaseDeDatos + ' SET EMERGENCY ' + char(13) + 
		-- 'DBCC CHECKDB (' + @BaseDeDatos + ', REPAIR_ALLOW_DATA_LOSS ) WITH NO_INFOMSGS ' + char(13) + 
		'DBCC CHECKDB (' + @BaseDeDatos + ', REPAIR_ALLOW_DATA_LOSS ) WITH ALL_ERRORMSGS  ' + char(13) + 
		'ALTER DATABASE ' + @BaseDeDatos + ' SET MULTI_USER ' 

	If @Ejecutar = 1
	   Exec(@sSql) 
	Else    
	   Print @sSql 

----	ALTER DATABASE @BaseDeDatos SET EMERGENCY --;– lo primero que haremos es pasar la bbdd del modo “RECOVERY_PENDING” al modo “EMERGENCY”  
----	ALTER DATABASE @BaseDeDatos SET SINGLE_USER 
----	DBCC CHECKDB (@BaseDeDatos, REPAIR_ALLOW_DATA_LOSS )WITH NO_INFOMSGS 
----	ALTER DATABASE @BaseDeDatos SET MULTI_USER 

End
Go 
