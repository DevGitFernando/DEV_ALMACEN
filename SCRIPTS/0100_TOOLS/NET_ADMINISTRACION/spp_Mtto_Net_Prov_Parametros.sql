--------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Net_Prov_Parametros' and xType = 'P' )
   Drop Proc spp_Mtto_Net_Prov_Parametros 
Go--#SQL   

Create Proc spp_Mtto_Net_Prov_Parametros ( @IdProveedor varchar(4), @NombreParametro varchar(50), @Valor varchar(200), 
	@Descripcion varchar(500), @Actualizar tinyint = 0 ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From Net_Prov_Parametros (NoLock) Where IdProveedor = @IdProveedor and NombreParametro = @NombreParametro ) 
	   Insert Into Net_Prov_Parametros ( IdProveedor, NombreParametro, Valor, Descripcion, Status ) 
	   Select @IdProveedor, @NombreParametro, @Valor, @Descripcion, 'A'
	Else 
	   Begin 
	       If @Actualizar = 1 
	          Begin  
			     Update Net_Prov_Parametros Set Status = 'A', Valor = @Valor, Actualizado = 0 
			     Where IdProveedor = @IdProveedor and NombreParametro = @NombreParametro 
			  End 
	   End 	
End 
Go--#SQL  
