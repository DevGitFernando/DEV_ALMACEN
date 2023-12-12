/* 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGC_Respaldos' and xType = 'U' ) 
   Drop Table Net_CFGC_Respaldos 
Go 

Create Table Net_CFGC_Respaldos 
( 
	PublicNameServer varchar(100) Not Null Default '', 
	RutaDeRespaldos varchar(500) Not Null Default '', 
	RutaDeArchivosDeSistema varchar(500) Not Null Default '', 	
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0, 
    RutaDeArchivosDeSistema varchar(500) Not Null Default '' 
)
Go 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGS_Respaldos' and xType = 'U' ) 
   Drop Table Net_CFGS_Respaldos 
Go 

Create Table Net_CFGS_Respaldos 
( 
	PublicNameServer varchar(100) Not Null Default '', 
	RutaDeRespaldos varchar(500) Not Null Default '', 
	RutaDeArchivosDeSistema varchar(500) Not Null Default '', 	
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0,  
    RutaDeArchivosDeSistema varchar(500) Not Null Default '' 	 	
)
Go 

-- Alter Table Net_CFGC_Respaldos Add RutaDeArchivosDeSistema varchar(500) Not Null Default '' 
-- Alter Table Net_CFGS_Respaldos Add RutaDeArchivosDeSistema varchar(500) Not Null Default '' 



If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFGSC_Respaldos' and xType = 'U' ) 
   Drop Table Net_CFGSC_Respaldos 
Go 

Create Table Net_CFGSC_Respaldos 
( 
	PublicNameServer varchar(100) Not Null Default '', 
	RutaDeRespaldos varchar(500) Not Null Default '', 
	RutaDeArchivosDeSistema varchar(500) Not Null Default '', 	
	Tiempo smallint Not Null Default 0, 
	TipoTiempo smallint Not Null Default 0 
)
Go 

*/
------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_CFGC_Respaldos' and xType = 'P' )
   Drop Proc spp_Net_CFGC_Respaldos  
Go

Create Proc spp_Net_CFGC_Respaldos ( @NombreServidor varchar(100), 
	@RutaRespaldos varchar(500), @RutaArchivosSistema varchar(500), @Tiempo int, @TipoTiempo int  ) 
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From Net_CFGC_Respaldos 
	
	Insert Into Net_CFGC_Respaldos ( PublicNameServer, RutaDeRespaldos, RutaDeArchivosDeSistema, Tiempo, TipoTiempo  ) 
	Select @NombreServidor, @RutaRespaldos, @RutaArchivosSistema, @Tiempo, @TipoTiempo

End 
Go 

------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_CFGS_Respaldos' and xType = 'P' )
   Drop Proc spp_Net_CFGS_Respaldos  
Go

Create Proc spp_Net_CFGS_Respaldos ( @NombreServidor varchar(100), 
	@RutaRespaldos varchar(500), @RutaArchivosSistema varchar(500),@Tiempo int, @TipoTiempo int  ) 
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From Net_CFGS_Respaldos 
	
	Insert Into Net_CFGS_Respaldos ( PublicNameServer, RutaDeRespaldos, RutaDeArchivosDeSistema, Tiempo, TipoTiempo  ) 
	Select @NombreServidor, @RutaRespaldos, @RutaArchivosSistema, @Tiempo, @TipoTiempo

End 
Go 


------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_CFGSC_Respaldos' and xType = 'P' )
   Drop Proc spp_Net_CFGSC_Respaldos  
Go

Create Proc spp_Net_CFGSC_Respaldos ( @NombreServidor varchar(100), 
	@RutaRespaldos varchar(500), @RutaArchivosSistema varchar(500),@Tiempo int, @TipoTiempo int  ) 
As 
Begin 
Set NoCount On 
	
	--- Solo debe existir una configuracion de obtencion 
	Delete From Net_CFGSC_Respaldos 
	
	Insert Into Net_CFGSC_Respaldos ( PublicNameServer, RutaDeRespaldos, RutaDeArchivosDeSistema, Tiempo, TipoTiempo  ) 
	Select @NombreServidor, @RutaRespaldos, @RutaArchivosSistema, @Tiempo, @TipoTiempo

End 
Go 
