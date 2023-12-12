If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFG_ConfigurarIntegracionBDS' and xType = 'P' )
   Drop Proc spp_CFG_ConfigurarIntegracionBDS  
Go--#SQL  

Create Proc spp_CFG_ConfigurarIntegracionBDS ( @RutaArchivosRecibidos varchar(500), @RutaArchivosIntegrados varchar(500), 
	@Tiempo smallint, @TipoTiempo smallint ) 
With Encryption 	
As 
Begin 
Set NoCount On 

	--- Solo debe existir una configuracion de integracion 
	Delete From CFG_ConfigurarIntegracionBDS 
		
	Insert Into CFG_ConfigurarIntegracionBDS ( RutaBDS_Integrar, RutaBDS_Integradas, Tiempo, TipoTiempo ) 
	Select @RutaArchivosRecibidos, @RutaArchivosIntegrados, @Tiempo, @TipoTiempo 

End 
Go--#SQL  
