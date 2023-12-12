------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGC_ConfigurarConexion' and xType = 'P' )
   Drop Proc spp_CFGC_ConfigurarConexion 
Go--#SQL  

Create Proc spp_CFGC_ConfigurarConexion 
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
	Delete From  CFGC_ConfigurarConexion
	
	Insert Into CFGC_ConfigurarConexion ( Servidor, WebService, PaginaWeb, SSL, IdEstado, IdFarmacia, Status, ServidorFTP, UserFTP, PasswordFTP, ModoActivoDeTransferenciaFTP ) 
	Select @Servidor, @WebService, @PaginaWeb, @SSL, @IdEstado, @IdFarmacia, @Status, @ServidorFTP, @UserFTP, @PasswordFTP, @ModoActivoDeTransferenciaFTP  	

End 
Go--#SQL   

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGC_ConfigurarObtencion' and xType = 'P' )
   Drop Proc spp_CFGC_ConfigurarObtencion  
Go--#SQL

Create Proc spp_CFGC_ConfigurarObtencion 
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
	Delete From CFGC_ConfigurarObtencion 
	
	Insert Into CFGC_ConfigurarObtencion ( Periodicidad, Semanas, bLunes, bMartes, bMiercoles, bJueves, bViernes, bSabado, bDomingo, 
		   HoraEjecucion, bRepetirProceso, Tiempo, TipoTiempo, bFechaTerminacion, FechaTerminacion, 
		   RutaUbicacionArchivos, RutaUbicacionArchivosEnviados, TipoReplicacion, DiasRevision, TipoDePaquete, TamañoDePaquete ) 
	Select @Periodicidad, @Semanas, @bLunes, @bMartes, @bMiercoles, @bJueves, @bViernes, @bSabado, @bDomingo, 
		   @HoraEjecucion, @bRepetirProceso, @Tiempo, @TipoTiempo, @bFechaTerminacion, @FechaTerminacion, 
		   @RutaUbicacionArchivos, @RutaUbicacionArchivosEnviados, @TipoReplicacion, @DiasRevision, @TipoDePaquete, @TamañoDePaquete   

End 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGC_ConfigurarIntegracion' and xType = 'P' )
   Drop Proc spp_CFGC_ConfigurarIntegracion  
Go--#SQL

Create Proc spp_CFGC_ConfigurarIntegracion 
( 
	@RutaArchivosRecibidos varchar(500) = '', @RutaArchivosIntegrados varchar(500) = '', 
	@bFechaTerminacion bit = 0, @FechaTerminacion datetime = null, @Tiempo smallint = 0, @TipoTiempo smallint = 0, @ServicioHabilitado bit = 1   
) 
With Encryption 	
As 
Begin 
Set NoCount On 

	--- Solo debe existir una configuracion de integracion 
	Delete From CFGC_ConfigurarIntegracion 
		
	Insert Into CFGC_ConfigurarIntegracion ( RutaArchivosRecibidos, RutaArchivosIntegrados, 
		bFechaTerminacion, FechaTerminacion, Tiempo, TipoTiempo, ServicioHabilitado ) 
	Select @RutaArchivosRecibidos, @RutaArchivosIntegrados, @bFechaTerminacion, @FechaTerminacion, @Tiempo, @TipoTiempo, @ServicioHabilitado  

End 
Go--#SQL 

