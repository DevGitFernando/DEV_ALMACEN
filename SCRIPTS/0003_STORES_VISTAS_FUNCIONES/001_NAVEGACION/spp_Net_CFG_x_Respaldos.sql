-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_CFGC_Respaldos' and xType = 'P' )
   Drop Proc spp_Net_CFGC_Respaldos  
Go--#SQL 

Create Proc spp_Net_CFGC_Respaldos 
( 
	@NombreServidor varchar(100), 
	@RutaRespaldos varchar(500), @RutaArchivosSistema varchar(500), @Tiempo int, @TipoTiempo int, 
	@ServicioHabilitado smallint = 0, 
	@EnvioFTP_Habilitado smallint = 0, 
	@bLunes smallint = 0, @bMartes smallint = 0, @bMiercoles smallint = 0, @bJueves smallint = 0, 
	@bViernes smallint = 0, @bSabado smallint = 0, @bDomingo smallint = 0, 
	@HoraInicia varchar(20) = '', @HoraFinaliza varchar(20) = ''   
) 
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From Net_CFGC_Respaldos 
	
	Insert Into Net_CFGC_Respaldos 
	( 
		PublicNameServer, RutaDeRespaldos, RutaDeArchivosDeSistema, Tiempo, TipoTiempo, ServicioHabilitado, 
		EnvioFTP_Habilitado, bLunes, bMartes, bMiercoles, bJueves, bViernes, bSabado, bDomingo, HoraInicia, HoraFinaliza   
	) 
	Select @NombreServidor, @RutaRespaldos, @RutaArchivosSistema, @Tiempo, @TipoTiempo, @ServicioHabilitado, 
		@EnvioFTP_Habilitado, @bLunes, @bMartes, @bMiercoles, @bJueves, @bViernes, @bSabado, @bDomingo, @HoraInicia, @HoraFinaliza


End 
Go--#SQL  


-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_CFGS_Respaldos' and xType = 'P' )
   Drop Proc spp_Net_CFGS_Respaldos  
Go--#SQL 

Create Proc spp_Net_CFGS_Respaldos 
( 
	@NombreServidor varchar(100), 
	@RutaRespaldos varchar(500), @RutaArchivosSistema varchar(500), @Tiempo int, @TipoTiempo int, 
	@ServicioHabilitado smallint = 0, 
	@EnvioFTP_Habilitado smallint = 0, 
	@bLunes smallint = 0, @bMartes smallint = 0, @bMiercoles smallint = 0, @bJueves smallint = 0, 
	@bViernes smallint = 0, @bSabado smallint = 0, @bDomingo smallint = 0, 
	@HoraInicia varchar(20) = '', @HoraFinaliza varchar(20) = ''   
) 
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From Net_CFGS_Respaldos 
	
	Insert Into Net_CFGS_Respaldos 
	( 
		PublicNameServer, RutaDeRespaldos, RutaDeArchivosDeSistema, Tiempo, TipoTiempo, ServicioHabilitado, 
		EnvioFTP_Habilitado, bLunes, bMartes, bMiercoles, bJueves, bViernes, bSabado, bDomingo, HoraInicia, HoraFinaliza   
	) 
	Select @NombreServidor, @RutaRespaldos, @RutaArchivosSistema, @Tiempo, @TipoTiempo, @ServicioHabilitado, 
		@EnvioFTP_Habilitado, @bLunes, @bMartes, @bMiercoles, @bJueves, @bViernes, @bSabado, @bDomingo, @HoraInicia, @HoraFinaliza


End 
Go--#SQL  

-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_CFGSC_Respaldos' and xType = 'P' )
   Drop Proc spp_Net_CFGSC_Respaldos  
Go--#SQL 

Create Proc spp_Net_CFGSC_Respaldos 
( 
	@NombreServidor varchar(100), 
	@RutaRespaldos varchar(500), @RutaArchivosSistema varchar(500), @Tiempo int, @TipoTiempo int, 
	@ServicioHabilitado smallint = 0, 
	@EnvioFTP_Habilitado smallint = 0, 
	@bLunes smallint = 0, @bMartes smallint = 0, @bMiercoles smallint = 0, @bJueves smallint = 0, 
	@bViernes smallint = 0, @bSabado smallint = 0, @bDomingo smallint = 0, 
	@HoraInicia varchar(20) = '', @HoraFinaliza varchar(20) = ''  
) 
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From Net_CFGSC_Respaldos 
	
	Insert Into Net_CFGSC_Respaldos 
	( 
		PublicNameServer, RutaDeRespaldos, RutaDeArchivosDeSistema, Tiempo, TipoTiempo, ServicioHabilitado, 
		EnvioFTP_Habilitado, bLunes, bMartes, bMiercoles, bJueves, bViernes, bSabado, bDomingo, HoraInicia, HoraFinaliza   
	) 
	Select @NombreServidor, @RutaRespaldos, @RutaArchivosSistema, @Tiempo, @TipoTiempo, @ServicioHabilitado, 
		@EnvioFTP_Habilitado, @bLunes, @bMartes, @bMiercoles, @bJueves, @bViernes, @bSabado, @bDomingo, @HoraInicia, @HoraFinaliza


End 
Go--#SQL  
