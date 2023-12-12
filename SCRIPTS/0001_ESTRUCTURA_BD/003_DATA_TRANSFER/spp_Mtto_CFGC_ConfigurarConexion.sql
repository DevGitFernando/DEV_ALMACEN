------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGC_ConfigurarConexion' and xType = 'P' )
   Drop Proc spp_CFGC_ConfigurarConexion 
Go--#SQL  

Create Proc spp_CFGC_ConfigurarConexion ( @Servidor varchar(100), @WebService varchar(100), @PaginaWeb varchar(100), 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @Status varchar(1), 
	@ServidorFTP varchar(100), @UserFTP varchar(50), @PasswordFTP varchar(500)  ) 
With Encryption 	
As 
Begin 

	--- Solo debe existir una configuracion de conexion 
	Delete From  CFGC_ConfigurarConexion
	
	Insert Into CFGC_ConfigurarConexion ( Servidor, WebService, PaginaWeb, IdEstado, IdFarmacia, Status, ServidorFTP, UserFTP, PasswordFTP ) 
	Select @Servidor, @WebService, @PaginaWeb, @IdEstado, @IdFarmacia, @Status, @ServidorFTP, @UserFTP, @PasswordFTP 	

End 
Go--#SQL   

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGC_ConfigurarObtencion' and xType = 'P' )
   Drop Proc spp_CFGC_ConfigurarObtencion  
Go--#SQL

Create Proc spp_CFGC_ConfigurarObtencion ( @Periodicidad smallint, @Semanas smallint, @bLunes bit, @bMartes bit, @bMiercoles bit,
	@bJueves bit, @bViernes bit, @bSabado bit, @bDomingo bit, @HoraEjecucion Datetime, @bRepetirProceso bit, @Tiempo int, @TipoTiempo int, 
	@bFechaTerminacion bit, @FechaTerminacion datetime, @RutaUbicacionArchivos varchar(500), @RutaUbicacionArchivosEnviados varchar(500) ) 
With Encryption 	
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From CFGC_ConfigurarObtencion 
	
	Insert Into CFGC_ConfigurarObtencion ( Periodicidad, Semanas, bLunes, bMartes, bMiercoles, bJueves, bViernes, bSabado, bDomingo, 
		   HoraEjecucion, bRepetirProceso, Tiempo, TipoTiempo, bFechaTerminacion, FechaTerminacion, RutaUbicacionArchivos, RutaUbicacionArchivosEnviados  ) 
	Select @Periodicidad, @Semanas, @bLunes, @bMartes, @bMiercoles, @bJueves, @bViernes, @bSabado, @bDomingo, 
		   @HoraEjecucion, @bRepetirProceso, @Tiempo, @TipoTiempo, @bFechaTerminacion, @FechaTerminacion, @RutaUbicacionArchivos, @RutaUbicacionArchivosEnviados 

End 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGC_ConfigurarIntegracion' and xType = 'P' )
   Drop Proc spp_CFGC_ConfigurarIntegracion  
Go--#SQL

Create Proc spp_CFGC_ConfigurarIntegracion ( @RutaArchivosRecibidos varchar(500), @RutaArchivosIntegrados varchar(500), 
	@bFechaTerminacion bit, @FechaTerminacion datetime, @Tiempo smallint, @TipoTiempo smallint ) 
With Encryption 	
As 
Begin 
Set NoCount On 

	--- Solo debe existir una configuracion de integracion 
	Delete From CFGC_ConfigurarIntegracion 
		
	Insert Into CFGC_ConfigurarIntegracion ( RutaArchivosRecibidos, RutaArchivosIntegrados, bFechaTerminacion, FechaTerminacion, Tiempo, TipoTiempo ) 
	Select @RutaArchivosRecibidos, @RutaArchivosIntegrados, @bFechaTerminacion, @FechaTerminacion, @Tiempo, @TipoTiempo 

End 
Go--#SQL 

