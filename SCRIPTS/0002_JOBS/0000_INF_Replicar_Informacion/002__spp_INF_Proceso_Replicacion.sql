If Exists ( Select Name From Sysobjects Where Name = 'spp_INF_Proceso_Replicacion' and xType = 'P' )
	Drop Proc spp_INF_Proceso_Replicacion
Go--#SQL 

---  Exec spp_INF_Proceso_Replicacion 'SII_PtoVta_EnBlanco', '[CENTRAL].[SII_OficinaCentral]', 'SII_PtoVta_EnBlanco'

-- Exec spp_INF_Proceso_Replicacion 'SII_OficinaCentral', '[SVRPUEBLA].[SII_RegionalPuebla]', 'SII_OficinaCentral', 'CFGSC_EnvioCatalogos', 1, 1 

-- Exec spp_INF_Proceso_Replicacion 'SII_OficinaCentral', '[SVROAXACA].[SII_Oaxaca]', 'SII_OficinaCentral', 'CFGSC_EnvioCatalogos', 1, 1 


--		Exec spp_INF_Proceso_Replicacion  'CFGC_EnvioDetalles', 1, 1 


Create Proc spp_INF_Proceso_Replicacion 
(
	@TablaDeControl varchar(100) = 'CFGC_EnvioDetalles',  
	@TipoProceso int = 1, 
	@Ejecutar int = 0  		 	
)
With Encryption 
As
Begin 
Set NoCount On 
Declare @sTabla varchar(500), 
        @sSql varchar(max), 
		@Esquema varchar(100), 
		@sMsj varchar(1000), 
		@sMsj_Error varchar(1000), 
				 
		@iActualizado int, 
		@sFiltro varchar(500), 
		@iValor int   		         

Declare 
	@Proceso_Exito int,  
	@iError int	

Declare 
	@BaseDeDatosOrigen varchar(100), 
	@BaseDeDatosDestino varchar(100), 
	@BaseDeDatosEstructura varchar(100) 
	


----Begin tran 
----print 'Begin tran' 

	Set @Proceso_Exito = 0 
	Set @Esquema = '.dbo.'           
    Set @sSql = '' 
    Set @iError = 0 
	
	Set @iActualizado = 0  
	Set @sFiltro = 0 
	Set @iValor = 0 
	
	Set @iError = 0 
	Set @sMsj = 'El tipo de proceso solicitado no es válido, verifique.' 
	Set @sMsj_Error = '' 
	
	if @TablaDeControl = '' 
	   Set @TablaDeControl = 'CFGC_EnvioDetalles' 
	
------------------------------------------- Proceso bajo transaccion 
	Set @BaseDeDatosOrigen = '[localhost].[SII_21_1101_LIBRES]' 
	Set @BaseDeDatosDestino = 'SII_21_1182___CEDIS_Puebla'  
	Set @BaseDeDatosEstructura = 'SII_21_1182___CEDIS_Puebla'  


	Set @BaseDeDatosOrigen = '[REGIONAL].[SII_RegionalPuebla]' 
	Set @BaseDeDatosDestino = '[REGIONAL].[SII_RegionalPuebla]'  
	Set @BaseDeDatosEstructura = '[REGIONAL].[SII_RegionalPuebla]'   
	
	Set @BaseDeDatosOrigen = db_name()  	
	Set @BaseDeDatosEstructura = db_name() 


--Begin tran 

	Begin Try 
	
		Exec spp_INF_Procesar_Informacion @TablaDeControl, 1, 1 
		Set @Proceso_Exito = @@error 
		
		If @Proceso_Exito <> 0 
			Begin 
				Set @iError = 1 
				Set @sMsj_Error = ERROR_MESSAGE() 
			End 
		Else 
			Begin 
				Exec spp_INF_SEND @BaseDeDatosOrigen, @BaseDeDatosDestino, @BaseDeDatosEstructura, @TablaDeControl, 1, 1 	
				Set @Proceso_Exito = @@error 
			End 
				
		
		If @Proceso_Exito <> 0 
			Begin 
				Set @iError = 2 
				Set @sMsj_Error = ERROR_MESSAGE() 
			End 
		Else 
			Begin 
				Exec spp_INF_Procesar_Informacion @TablaDeControl, 2, 1 	
				Set @Proceso_Exito = @@error 
			End 		
		
		If @Proceso_Exito <> 0 
			Begin 
				Set @iError = 3 
				Set @sMsj_Error = ERROR_MESSAGE() 
			End 
					
		
				
		If @Proceso_Exito = 0 
		Begin 
			Set @Proceso_Exito = 1 
		    --Commit tran 
		End 
		
	End Try 
	Begin Catch 
		Set @iError = 4  
		Set @sMsj_Error = ERROR_MESSAGE() 	
		--Rollback Tran 
	End Catch 


--		Exec spp_INF_Proceso_Replicacion  'CFGC_EnvioDetalles', 1, 1   


	If @Proceso_Exito = 1 
	Begin 
		Set @sMsj = 'Proceso concluido satisfactoriamente.' 
	End 
	Else  
	Begin    
		Set @sMsj = 'Proceso concluido con errores.' 	
		If @@trancount > 0 
		Begin 
		   Rollback tran 
		End 
		
		raiserror ( @sMsj_Error, 10, 16 ) 
	End 

	Select @sMsj as Resultado, @iError as Error, @sMsj_Error as Mensaje_Error   
	

----If @Proceso_Exito = 1 
----   commit tran 
   
------------------------------------------- Proceso bajo transaccion 
	
	
	 
    
End 
Go--#SQL 


---------------------------------------------- 
