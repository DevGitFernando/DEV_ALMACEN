If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_JOB_Replica_Direcciones' and xType = 'P' ) 
    Drop Proc spp_JOB_Replica_Direcciones 
Go--#SQL 

--		Exec spp_JOB_Replica_Direcciones 'SII_Regional_DF', '[CentralDatos].[SII_OficinaCentral]', '09'  

Create Proc spp_JOB_Replica_Direcciones 
(   
    @BaseDeDatosOrigen varchar(100) = '', 
    @BaseDeDatosDestino varchar(100) = '', 
	@IdEstado varchar(2) = '21' 
) 
With Encryption 
As 
Begin 
Declare 
	@sSql varchar(8000), 
	@sSql_Insert  varchar(8000), 		
	@sSql_Update varchar(8000), 	
	@Tabla varchar(200),   
	@Alias_Origen_Base varchar(10),
	@Alias_Destino_Base varchar(10)
		
	
    Set @Alias_Origen_Base = 'Bd_O.'
    Set @Alias_Destino_Base = 'Bd_D.' 	

	Set @Tabla = 'CFGS_ConfigurarConexiones' 
	
	

	Exec spp_INF_SEND_Detalles  
		@BaseDeDatosOrigen, @BaseDeDatosDestino, @BaseDeDatosOrigen, 
		@Tabla, @sSql output, 1, 1, @sSql_Insert output, @sSql_Update  output  

	Set @sSql_Update = @sSql_Update + ' Where ' + @Alias_Origen_Base + 'IdEstado = ' + char(39) + @IdEstado + char(39) 
	Exec(@sSql_Update) 
	Exec(@sSql_Insert) 	
	
	print @sSql_Update 			
	print '' 	
	print @sSql_Insert 	


End 
Go--#SQL 
