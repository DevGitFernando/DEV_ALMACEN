------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_CFGC_Respaldos' and xType = 'P' )
   Drop Proc spp_Net_CFGC_Respaldos  
Go--#SQL  

Create Proc spp_Net_CFGC_Respaldos ( @NombreServidor varchar(100), 
	@RutaRespaldos varchar(500), @RutaArchivosSistema varchar(500), @Tiempo int, @TipoTiempo int  ) 
With Encryption 
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From Net_CFGC_Respaldos 
	
	Insert Into Net_CFGC_Respaldos ( PublicNameServer, RutaDeRespaldos, RutaDeArchivosDeSistema, Tiempo, TipoTiempo  ) 
	Select @NombreServidor, @RutaRespaldos, @RutaArchivosSistema, @Tiempo, @TipoTiempo

End 
Go--#SQL  

------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_CFGS_Respaldos' and xType = 'P' )
   Drop Proc spp_Net_CFGS_Respaldos  
Go--#SQL  

Create Proc spp_Net_CFGS_Respaldos ( @NombreServidor varchar(100), 
	@RutaRespaldos varchar(500), @RutaArchivosSistema varchar(500),@Tiempo int, @TipoTiempo int  ) 
With Encryption 
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From Net_CFGS_Respaldos 
	
	Insert Into Net_CFGS_Respaldos ( PublicNameServer, RutaDeRespaldos, RutaDeArchivosDeSistema, Tiempo, TipoTiempo  ) 
	Select @NombreServidor, @RutaRespaldos, @RutaArchivosSistema, @Tiempo, @TipoTiempo

End 
Go--#SQL  


------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_CFGSC_Respaldos' and xType = 'P' )
   Drop Proc spp_Net_CFGSC_Respaldos  
Go--#SQL 

Create Proc spp_Net_CFGSC_Respaldos ( @NombreServidor varchar(100), 
	@RutaRespaldos varchar(500), @RutaArchivosSistema varchar(500),@Tiempo int, @TipoTiempo int  ) 
With Encryption 
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From Net_CFGSC_Respaldos 
	
	Insert Into Net_CFGSC_Respaldos ( PublicNameServer, RutaDeRespaldos, RutaDeArchivosDeSistema, Tiempo, TipoTiempo  ) 
	Select @NombreServidor, @RutaRespaldos, @RutaArchivosSistema, @Tiempo, @TipoTiempo

End 
Go--#SQL 
