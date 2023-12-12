If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CierreDeInventario_005' and xType = 'P' ) 
   Drop Proc spp_Mtto_CierreDeInventario_005 
Go--#SQL 

Create Proc spp_Mtto_CierreDeInventario_005  
(	
	@BaseDeDatos varchar(100) = '', @RutaRespaldo varchar(500) = '', @Prefijo varchar(100) = ''  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare @sSql varchar(8000), 
		@sBd varchar(1000), 
		@sBd_Name varchar(1000), 	
		@sRuta varchar(100), 
		@sHora varchar(100) 

 
	Set @sBd = @BaseDeDatos 
	Set @sBd_Name = @BaseDeDatos

	-- Set @sBd = 'FarmaciaScSoft' 
	Set @sRuta = 'D:\BaseDeDatos\Respaldos\' 
	Set @sRuta = @RutaRespaldo  

	-- Formatear la hora de creacion 
	Set @sHora = right('00' + cast(datepart(mm, getdate()) as varchar), 2) 
	Set @sHora = @sHora + right('00' + cast(datepart(dd, getdate()) as varchar), 2)	
	Set @sHora = @sHora + '_' + right('00' + cast(datepart(hh, getdate()) as varchar), 2)	
	Set @sHora = @sHora + right('00' + cast(datepart(minute, getdate()) as varchar), 2)	
	Set @sHora = @sHora + right('00' + cast(datepart(second, getdate()) as varchar), 2)			


	If @Prefijo <> '' 
	   Set @sHora = @sBd_Name + '_2K' + right(cast( datepart(yy, getdate()) as varchar),2) +  @sHora + '__' + @Prefijo + '.bak' 
	Else 
	   Set @sHora = @sBd_Name + '_2K' + right(cast( datepart(yy, getdate()) as varchar),2) +  @sHora + '.bak' 	


	Set @sSql = ' Backup Database [' + @sBd + '] ' + char(13) + 
				'	To Disk = N' + char(39) + @sRuta + @sHora + char(39) + char(13) + 
				'	WITH NOFORMAT, INIT,  NAME = N' + char(39) + @sBd + '-Completa Base de datos Copia de seguridad' + char(39) + ',' + char(13) + 
				'	SKIP, NOREWIND, NOUNLOAD,  STATS = 10 ' + char(13) + 
				'  ' 
	Exec(@sSql) 	
	--print @sSql 
			
End 
Go--#SQL 

--		select * from Net_CFGC_Respaldos  

