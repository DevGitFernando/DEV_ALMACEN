------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGS_ConfigurarFTP_BDS' and xType = 'P' )
   Drop Proc spp_CFGS_ConfigurarFTP_BDS  
Go--#SQL

Create Proc spp_CFGS_ConfigurarFTP_BDS 
( 
	@RutaFTP_BDS_Integrar varchar(500), @Habilitado int = 0, 
	@Tiempo smallint, @TipoTiempo smallint ) 
With Encryption 	
As 
Begin 
Set NoCount On 

	--- Solo debe existir una configuracion de integracion 
	Delete From CFGS_ConfigurarFTP_BDS 
		
	Insert Into CFGS_ConfigurarFTP_BDS ( RutaFTP_BDS_Integrar, Habilitado, Tiempo, TipoTiempo ) 
	Select @RutaFTP_BDS_Integrar, @Habilitado, @Tiempo, @TipoTiempo 

End 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFGR_ConfigurarFTP_BDS' and xType = 'P' )
   Drop Proc spp_CFGR_ConfigurarFTP_BDS  
Go--#SQL

Create Proc spp_CFGR_ConfigurarFTP_BDS 
( 
	@RutaFTP_BDS_Integrar varchar(500), @Habilitado int = 0, 
	@Tiempo smallint, @TipoTiempo smallint ) 
With Encryption 	
As 
Begin 
Set NoCount On 

	--- Solo debe existir una configuracion de integracion 
	Delete From CFGR_ConfigurarFTP_BDS 
		
	Insert Into CFGR_ConfigurarFTP_BDS ( RutaFTP_BDS_Integrar, Habilitado, Tiempo, TipoTiempo ) 
	Select @RutaFTP_BDS_Integrar, @Habilitado, @Tiempo, @TipoTiempo 

End 
Go--#SQL 