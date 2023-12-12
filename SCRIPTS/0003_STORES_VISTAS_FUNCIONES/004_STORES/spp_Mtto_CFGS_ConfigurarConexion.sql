------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGS_ConfigurarConexiones' and xType = 'P' )
   Drop Proc spp_CFGS_ConfigurarConexiones 
Go--#SQL 

Create Proc spp_CFGS_ConfigurarConexiones 
(
	@IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@Servidor varchar(100) = '', @WebService varchar(100) = '', @PaginaWeb varchar(100) = '', 
	@SSL bit = 0, 
	@Status varchar(1) = '' 
) 
With Encryption 	
As 
Begin 

	If Not Exists ( Select * From CFGS_ConfigurarConexiones (NoLock) 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia ) -- Servidor = @Servidor and WebService = @WebService and PaginaWeb = @PaginaWeb ) 
	   Insert Into CFGS_ConfigurarConexiones ( IdEstado, IdFarmacia, Servidor, WebService, PaginaWeb, SSL, Status ) 
	   Select @IdEstado, @IdFarmacia, @Servidor, @WebService, @PaginaWeb, @SSL, @Status  	
    Else 
       Update CFGS_ConfigurarConexiones 
       Set 
		Servidor = @Servidor, WebService = @WebService, 
		PaginaWeb = @PaginaWeb, SSL =  @SSL, Status = @Status, Actualizado = 0  
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- Servidor = @Servidor and WebService = @WebService and PaginaWeb = @PaginaWeb 

End 
Go--#SQL

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGS_ConfigurarObtencion' and xType = 'P' )
   Drop Proc spp_CFGS_ConfigurarObtencion  
Go--#SQL

Create Proc spp_CFGS_ConfigurarObtencion 
(
	@Periodicidad smallint = 0, @Semanas smallint = 0, @bLunes bit = 0, @bMartes bit = 0, @bMiercoles bit = 0,
	@bJueves bit = 0, @bViernes bit = 0, @bSabado bit = 0, @bDomingo bit = 0, 
	@HoraEjecucion Datetime = null, @bRepetirProceso bit = 0, @Tiempo int = 0, @TipoTiempo int = 0, 
	@bFechaTerminacion bit = 0, @FechaTerminacion datetime = null, 
	@RutaUbicacionArchivos varchar(500) = '', @RutaUbicacionArchivosEnviados varchar(500) = '', 
	@ServicioHabilitado bit = 1, @TipoReplicacion smallint = 1, @DiasRevision int = 15, 
	@TipoDePaquete smallint = 3, @TamañoDePaquete bigint = 0    	   
) 
With Encryption 	
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From CFGS_ConfigurarObtencion 
	
	Insert Into CFGS_ConfigurarObtencion ( Periodicidad, Semanas, bLunes, bMartes, bMiercoles, bJueves, bViernes, bSabado, bDomingo, 
		   HoraEjecucion, bRepetirProceso, Tiempo, TipoTiempo, bFechaTerminacion, FechaTerminacion, 
		   RutaUbicacionArchivos, RutaUbicacionArchivosEnviados, ServicioHabilitado, TipoReplicacion, DiasRevision, 
		   TipoDePaquete, TamañoDePaquete  ) 
	Select @Periodicidad, @Semanas, @bLunes, @bMartes, @bMiercoles, @bJueves, @bViernes, @bSabado, @bDomingo, 
		   @HoraEjecucion, @bRepetirProceso, @Tiempo, @TipoTiempo, @bFechaTerminacion, @FechaTerminacion, 
		   @RutaUbicacionArchivos, @RutaUbicacionArchivosEnviados, @ServicioHabilitado, @TipoReplicacion, @DiasRevision, 
		   @TipoDePaquete, @TamañoDePaquete  

End 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGS_ConfigurarIntegracion' and xType = 'P' )
   Drop Proc spp_CFGS_ConfigurarIntegracion  
Go--#SQL

Create Proc spp_CFGS_ConfigurarIntegracion 
( 
	@RutaArchivosRecibidos varchar(500) = '', @RutaArchivosIntegrados varchar(500) = '', 
	@bFechaTerminacion bit = 0, @FechaTerminacion datetime = null, @Tiempo smallint = 0, @TipoTiempo smallint = 0, 
	@ServicioHabilitado bit = 1  
) 
With Encryption 	
As 
Begin 
Set NoCount On 

	--- Solo debe existir una configuracion de integracion 
	Delete From CFGS_ConfigurarIntegracion 
		
	Insert Into CFGS_ConfigurarIntegracion ( RutaArchivosRecibidos, RutaArchivosIntegrados, bFechaTerminacion, FechaTerminacion, 
		Tiempo, TipoTiempo, ServicioHabilitado ) 
	Select @RutaArchivosRecibidos, @RutaArchivosIntegrados, @bFechaTerminacion, @FechaTerminacion, @Tiempo, @TipoTiempo, @ServicioHabilitado  

End 
Go--#SQL 


------------------------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGSC_ConfigurarConexion' and xType = 'P' )
   Drop Proc spp_CFGSC_ConfigurarConexion 
Go--#SQL 

Create Proc spp_CFGSC_ConfigurarConexion 
( 
	@Servidor varchar(100) = '', @WebService varchar(100) = '', @PaginaWeb varchar(100) = '', 
	@SSL bit = 0, 	
	@IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @Status varchar(1) = '', 
	@ServidorFTP varchar(100) = '', @UserFTP varchar(50) = '', @PasswordFTP varchar(500) = '', @ModoActivoDeTransferenciaFTP bit = 0 
)
With Encryption 	
As 
Begin 

	--- Solo debe existir una configuracion de conexion 
	Delete From  CFGSC_ConfigurarConexion
	
	Insert Into CFGSC_ConfigurarConexion ( Servidor, WebService, PaginaWeb, SSL, IdEstado, IdFarmacia, Status, ServidorFTP, UserFTP, PasswordFTP, ModoActivoDeTransferenciaFTP ) 
	Select @Servidor, @WebService, @PaginaWeb, @SSL, @IdEstado, @IdFarmacia, @Status, @ServidorFTP, @UserFTP, @PasswordFTP, @ModoActivoDeTransferenciaFTP 	
	
	
End 
Go--#SQL

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGSC_ConfigurarObtencion' and xType = 'P' )
   Drop Proc spp_CFGSC_ConfigurarObtencion  
Go--#SQL

Create Proc spp_CFGSC_ConfigurarObtencion 
( 
	@Periodicidad smallint = 0, @Semanas smallint = 0, @bLunes bit = 0, @bMartes bit = 0, @bMiercoles bit = 0,
	@bJueves bit = 0, @bViernes bit = 0, @bSabado bit = 0, @bDomingo bit = 0, 
	@HoraEjecucion Datetime = null, @bRepetirProceso bit = 0, @Tiempo int = 0, @TipoTiempo int = 0, 
	@bFechaTerminacion bit = 0, @FechaTerminacion datetime = null, 
	@RutaUbicacionArchivos varchar(500) = '', @RutaUbicacionArchivosEnviados varchar(500) = '', 
	@ServicioHabilitado bit = 1, @TipoReplicacion smallint = 1, @DiasRevision int = 15, 
	@TipoDePaquete smallint = 3, @TamañoDePaquete bigint = 0 
) 
With Encryption 	
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From CFGSC_ConfigurarObtencion 
	
	Insert Into CFGSC_ConfigurarObtencion ( Periodicidad, Semanas, bLunes, bMartes, bMiercoles, bJueves, bViernes, bSabado, bDomingo, 
		   HoraEjecucion, bRepetirProceso, Tiempo, TipoTiempo, bFechaTerminacion, FechaTerminacion, 
		   RutaUbicacionArchivos, RutaUbicacionArchivosEnviados, ServicioHabilitado, TipoReplicacion, DiasRevision, 
		   TipoDePaquete, TamañoDePaquete  ) 
	Select @Periodicidad, @Semanas, @bLunes, @bMartes, @bMiercoles, @bJueves, @bViernes, @bSabado, @bDomingo, 
		   @HoraEjecucion, @bRepetirProceso, @Tiempo, @TipoTiempo, @bFechaTerminacion, @FechaTerminacion, 
		   @RutaUbicacionArchivos, @RutaUbicacionArchivosEnviados, @ServicioHabilitado, @TipoReplicacion, @DiasRevision, 
		   @TipoDePaquete, @TamañoDePaquete    

End 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGSC_ConfigurarIntegracion' and xType = 'P' )
   Drop Proc spp_CFGSC_ConfigurarIntegracion  
Go--#SQL

Create Proc spp_CFGSC_ConfigurarIntegracion 
( 
	@RutaArchivosRecibidos varchar(500) = '', @RutaArchivosIntegrados varchar(500) = '', 
	@bFechaTerminacion bit = 0, @FechaTerminacion datetime = null, @Tiempo smallint = 0, 
	@TipoTiempo smallint = 0, @ServicioHabilitado bit = 1   
) 
With Encryption 	
As 
Begin 
Set NoCount On 

	--- Solo debe existir una configuracion de integracion 
	Delete From CFGSC_ConfigurarIntegracion 
		
	Insert Into CFGSC_ConfigurarIntegracion ( RutaArchivosRecibidos, RutaArchivosIntegrados, bFechaTerminacion, FechaTerminacion, 
		Tiempo, TipoTiempo, ServicioHabilitado ) 
	Select @RutaArchivosRecibidos, @RutaArchivosIntegrados, @bFechaTerminacion, @FechaTerminacion, @Tiempo, @TipoTiempo, @ServicioHabilitado  

End 
Go--#SQL

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGSC_ConfigurarConexiones' and xType = 'P' )
   Drop Proc spp_CFGSC_ConfigurarConexiones 
Go--#SQL 

Create Proc spp_CFGSC_ConfigurarConexiones 
( 
	@IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@Servidor varchar(100) = '', @WebService varchar(100) = '', @PaginaWeb varchar(100) = '', 
	@SSL bit = 0, 
	@Status varchar(1) = ''
)
With Encryption 	
As 
Begin 

	If Not Exists ( Select * From CFGSC_ConfigurarConexiones (NoLock) 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia ) -- Servidor = @Servidor and WebService = @WebService and PaginaWeb = @PaginaWeb ) 
	   Insert Into CFGSC_ConfigurarConexiones ( IdEstado, IdFarmacia, Servidor, WebService, PaginaWeb, SSL, Status ) 
	   Select @IdEstado, @IdFarmacia, @Servidor, @WebService, @PaginaWeb, @SSL, @Status  	
    Else 
       Update CFGSC_ConfigurarConexiones Set Servidor = @Servidor, WebService = @WebService, PaginaWeb = @PaginaWeb, SSL = @SSL, 
       Status = @Status, Actualizado = 0  
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia -- Servidor = @Servidor and WebService = @WebService and PaginaWeb = @PaginaWeb 

End 
Go--#SQL


------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGCR_ConfigurarObtencion' and xType = 'P' )
   Drop Proc spp_CFGCR_ConfigurarObtencion  
Go--#SQL

Create Proc spp_CFGCR_ConfigurarObtencion 
( 
	@Periodicidad smallint = 0, @Semanas smallint = 0, @bLunes bit = 0, @bMartes bit = 0, @bMiercoles bit = 0,
	@bJueves bit = 0, @bViernes bit = 0, @bSabado bit = 0, @bDomingo bit = 0, 
	@HoraEjecucion Datetime = null, @bRepetirProceso bit = 0, @Tiempo int = 0, @TipoTiempo int = 0, 
	@bFechaTerminacion bit = 0, @FechaTerminacion datetime = null, 
	@RutaUbicacionArchivos varchar(500) = '', @RutaUbicacionArchivosEnviados varchar(500) = '', 
	@ServicioHabilitado bit = 1, @TipoReplicacion smallint = 1, @DiasRevision int = 15, 
	@TipoDePaquete smallint = 3, @TamañoDePaquete bigint = 0    	   	   
) 
With Encryption 	
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From CFGCR_ConfigurarObtencion 
	
	Insert Into CFGCR_ConfigurarObtencion ( Periodicidad, Semanas, bLunes, bMartes, bMiercoles, bJueves, bViernes, bSabado, bDomingo, 
		   HoraEjecucion, bRepetirProceso, Tiempo, TipoTiempo, bFechaTerminacion, FechaTerminacion, 
		   RutaUbicacionArchivos, RutaUbicacionArchivosEnviados, ServicioHabilitado, TipoReplicacion, DiasRevision, 
		   TipoDePaquete, TamañoDePaquete  ) 
	Select @Periodicidad, @Semanas, @bLunes, @bMartes, @bMiercoles, @bJueves, @bViernes, @bSabado, @bDomingo, 
		   @HoraEjecucion, @bRepetirProceso, @Tiempo, @TipoTiempo, @bFechaTerminacion, @FechaTerminacion, 
		   @RutaUbicacionArchivos, @RutaUbicacionArchivosEnviados, @ServicioHabilitado, @TipoReplicacion, @DiasRevision, 
		   @TipoDePaquete, @TamañoDePaquete   

End 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGCR_ConfigurarIntegracion' and xType = 'P' )
   Drop Proc spp_CFGCR_ConfigurarIntegracion  
Go--#SQL

Create Proc spp_CFGCR_ConfigurarIntegracion 
( 
	@RutaArchivosRecibidos varchar(500) = '', @RutaArchivosIntegrados varchar(500) = '', 
	@bFechaTerminacion bit = 0, @FechaTerminacion datetime = null, @Tiempo smallint = 0, @TipoTiempo smallint = 0, 
	@ServicioHabilitado bit = 1
) 
With Encryption 	
As 
Begin 
Set NoCount On 

	--- Solo debe existir una configuracion de integracion 
	Delete From CFGCR_ConfigurarIntegracion 
		
	Insert Into CFGCR_ConfigurarIntegracion ( RutaArchivosRecibidos, RutaArchivosIntegrados, bFechaTerminacion, FechaTerminacion, Tiempo, TipoTiempo, ServicioHabilitado ) 
	Select @RutaArchivosRecibidos, @RutaArchivosIntegrados, @bFechaTerminacion, @FechaTerminacion, @Tiempo, @TipoTiempo, @ServicioHabilitado

End 
Go--#SQL 

