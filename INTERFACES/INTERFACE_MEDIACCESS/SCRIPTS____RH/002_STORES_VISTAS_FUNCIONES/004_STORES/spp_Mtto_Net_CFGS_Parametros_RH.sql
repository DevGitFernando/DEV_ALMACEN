


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Net_CFGS_Parametros_RH' and xType = 'P' )
   Drop Proc spp_Mtto_Net_CFGS_Parametros_RH
Go--#SQL   

Create Proc spp_Mtto_Net_CFGS_Parametros_RH 
( 
	@ArbolModulo varchar(4), @NombreParametro varchar(50), @Valor varchar(200), @Descripcion varchar(500), 
	@Actualizar tinyint = 0 
) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From Net_CFGS_Parametros (NoLock) 
			Where ArbolModulo = @ArbolModulo and NombreParametro = @NombreParametro ) 
	   Insert Into Net_CFGS_Parametros ( ArbolModulo, NombreParametro, Valor, Descripcion, Status, Actualizado ) 
	   Select @ArbolModulo, @NombreParametro, upper(@Valor), @Descripcion, 'A', 0 
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
	