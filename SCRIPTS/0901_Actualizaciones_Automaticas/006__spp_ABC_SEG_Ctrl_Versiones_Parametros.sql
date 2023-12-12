-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_ABC_SEG_Ctrl_Versiones_Parametros' and xType = 'P' )
   Drop Proc spp_ABC_SEG_Ctrl_Versiones_Parametros 
Go--#SQL  

Create Proc spp_ABC_SEG_Ctrl_Versiones_Parametros 
( 
	@NombreParametro varchar(100) = '', @Valor varchar(200) = '', 
	@Descripcion varchar(500) = '' , @Actualizar tinyint = 0  
) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From SEG_Ctrl_Versiones_Parametros (NoLock) 
				    Where NombreParametro = @NombreParametro ) 
	   Insert Into SEG_Ctrl_Versiones_Parametros ( NombreParametro, Valor, Descripcion, Status ) 
	   Select @NombreParametro, upper(@Valor), @Descripcion, 'A'  
	Else 
	   Begin 
	       If @Actualizar = 1 
	          Begin  
			     Update SEG_Ctrl_Versiones_Parametros Set Status = 'A', Valor = upper(@Valor), Actualizado = 0 
			     Where NombreParametro = @NombreParametro 
			  End 
	   End 	
End 
Go--#SQL     

