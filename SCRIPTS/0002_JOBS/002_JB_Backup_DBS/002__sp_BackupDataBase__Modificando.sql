If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_BackupDB' and xType = 'P' ) 
   Drop Proc sp_BackupDB
Go--#SQL 

Create Proc sp_BackupDB 
( 
	@Ejecutar smallint = 0, @BaseDeDatos varchar(100) = '', @NombreBK varchar(100) = '', 
	@RutaRespaldo varchar(500) = '', -- @RutaRespaldo_Aux varchar(500) = '',
	@RutaRAR varchar(200) = 'C:\Program Files\WinRAR\' 
)
With Encryption
As 
Begin 

Declare @sSql varchar(8000), 
		@sBd varchar(1000), 
		@sBd_Name varchar(1000), 	
		@sRuta varchar(500), 
		@sRuta_Aux varchar(500), 		
		@sHora varchar(100) 

Declare @sRAR  varchar(1000), 
		@Comprimir smallint -- = 0  

    If @BaseDeDatos = '' 
       Set @sBd = db_name()
    Else 
       Set @sBd = @BaseDeDatos 

	-- Set @sBd = 'FarmaciaScSoft' 
	Set @sRuta = 'D:\BaseDeDatos\Respaldos\' 
	
	if @RutaRespaldo <> '' 
	   Set @sRuta = @RutaRespaldo 
----	
----	if @RutaRespaldo_Aux <> '' 
----	   Set @sRuta_Aux = @RutaRespaldo_Aux  
	
	
	Set @sBd_Name = @sBd
	Set @sRAR = '' 
	
	if @RutaRAR <>  '' 
	   Set @Comprimir  = 1 

	If @NombreBK <> '' 
	   Set @sBd_Name = @NombreBK 


	-- Formatear la hora de creacion 
	Set @sHora = right('00' + cast(datepart(mm, getdate()) as varchar), 2) 
	Set @sHora = @sHora + right('00' + cast(datepart(dd, getdate()) as varchar), 2)	
	Set @sHora = @sHora + '_' + right('00' + cast(datepart(hh, getdate()) as varchar), 2)	
	Set @sHora = @sHora + right('00' + cast(datepart(minute, getdate()) as varchar), 2)	
	Set @sHora = @sHora + right('00' + cast(datepart(second, getdate()) as varchar), 2)			

	Set @sRAR = @sBd_Name + '_2K' + right(cast( datepart(yy, getdate()) as varchar),2) +  @sHora + '.rar' 	
	Set @sHora = @sBd_Name + '_2K' + right(cast( datepart(yy, getdate()) as varchar),2) +  @sHora + '.bak' 	


	Set @sSql = ' Backup Database [' + @sBd + '] ' + char(13) + 
				'	To Disk = N' + char(39) + @sRuta + @sHora + char(39) + char(13) + 
				'	WITH NOFORMAT, INIT,  NAME = N' + char(39) + @sBd + '-Completa Base de datos Copia de seguridad' + char(39) + ',' + char(13) + 
				'	SKIP, NOREWIND, NOUNLOAD,  STATS = 10 ' + char(13) + 
				'  ' 
				
    Set @sSql = char(34) + @RutaRAR + '\rar.exe ' + char(34) + ' a -m5 -df -p33e790cd8a7fad434930d99b67ba8efd -ep '    
    Set @sSql = @sSql + ' ' + char(34) + @sRuta + @sRAR + char(34) + ' ' + char(34) + @sRuta + @sHora + char(34) 
			    
		    				
	If @Ejecutar = 0  
	Begin 
	   Print @sSql -- as Salida 
	End    
	Else 
	Begin 
	   Exec(@sSql) 	
	   
	   If @Comprimir = 1 
	   Begin 	  
	   
			EXEC sp_configure 'show advanced options', 1
			RECONFIGURE
			EXEC sp_configure 'xp_cmdshell', 1 
			RECONFIGURE	   
	     		    
	     		    
			--- Armar la cadena para Winrar 
			---- Parametros 
			--   -df	Eliminar fichero origen 
			--   -m5	Nivel de Compresion  [ 0-nada... 3-normal 5-máxima ] 
			--   -p		Contraseña 
			--   -ep	No incluir la ruta del Directorio 
			
		    Set @sSql = char(34) + @RutaRAR + '\rar.exe ' + char(34) + ' a -m5 -df -p33e790cd8a7fad434930d99b67ba8efd -ep '    
		    Set @sSql = @sSql + ' ' + char(34) + @sRuta + @sRAR + char(34) + ' ' + char(34) + @sRuta + @sHora + char(34) 
		    
			-- Print @sSql -- @sRuta + @sRAR + '       ' + @sRuta + @sHora  			
			Set @sSql = char(34) + @sSql + char(34) 			
			EXEC master.dbo.xp_cmdshell @sSql, no_output  
			-- print @sSql 
			
			
			
			EXEC sp_configure 'xp_cmdshell', 0 
			RECONFIGURE 
			EXEC sp_configure 'show advanced options', 0	
			RECONFIGURE	   
						
	   End 	   
	End    

End
Go--#SQL 
 	