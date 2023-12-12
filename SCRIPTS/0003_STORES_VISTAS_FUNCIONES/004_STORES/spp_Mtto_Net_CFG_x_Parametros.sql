--------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Net_CFGS_Parametros' and xType = 'P' )
   Drop Proc spp_Mtto_Net_CFGS_Parametros 
Go--#SQL  

Create Proc spp_Mtto_Net_CFGS_Parametros 
( 
	@ArbolModulo varchar(4), @NombreParametro varchar(100), @Valor varchar(200), 
	@Descripcion varchar(500), @Actualizar tinyint = 0  
) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From Net_CFGS_Parametros (NoLock) 
				    Where ArbolModulo = @ArbolModulo and NombreParametro = @NombreParametro ) 
	   Insert Into Net_CFGS_Parametros ( ArbolModulo, NombreParametro, Valor, Descripcion, Status ) 
	   Select @ArbolModulo, @NombreParametro, upper(@Valor), @Descripcion, 'A'  
	Else 
	   Begin 
	       If @Actualizar = 1 
	          Begin  
			     Update Net_CFGS_Parametros Set Status = 'A', Valor = upper(@Valor), Actualizado = 0 
			     Where ArbolModulo = @ArbolModulo and NombreParametro = @NombreParametro 
			  End 
	   End 	
End 
Go--#SQL     

-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Net_CFGC_Parametros' and xType = 'P' )
   Drop Proc spp_Mtto_Net_CFGC_Parametros 
Go--#SQL   

Create Proc spp_Mtto_Net_CFGC_Parametros 
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), 
	@ArbolModulo varchar(4), @NombreParametro varchar(100), @Valor varchar(200), @Descripcion varchar(500), 
	@EsDeSistema bit = 0, @EsEditable bit = 0, @Actualizar tinyint = 0 
) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From Net_CFGC_Parametros (NoLock) 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ArbolModulo = @ArbolModulo and NombreParametro = @NombreParametro ) 
	   Insert Into Net_CFGC_Parametros ( IdEstado, IdFarmacia, ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema, Status, EsEditable ) 
	   Select @IdEstado, @IdFarmacia, @ArbolModulo, @NombreParametro, upper(@Valor), @Descripcion, @EsDeSistema, 'A', @EsEditable 
	Else 
	   Begin 
	       If @Actualizar = 1 
	          Begin  
			     Update Net_CFGC_Parametros Set Status = 'A', Valor = upper(@Valor), Actualizado = 0 
			     Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ArbolModulo = @ArbolModulo and NombreParametro = @NombreParametro
			  End 
	   End 
End 
Go--#SQL      
	