If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_Nota_OrdenDeCompra' and xType = 'U' ) 
   Drop Table COM_Nota_OrdenDeCompra 
Go--#SQL    

Create Table COM_Nota_OrdenDeCompra 
( 
	ArbolModulo varchar(4) Not Null, 
	NombreParametro varchar(50) Not Null, 
	Valor varchar(7000) Not Null Default '', 
	Descripcion varchar(500) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0  
)
Go--#SQL  

Alter Table COM_Nota_OrdenDeCompra Add Constraint PK_COM_Nota_OrdenDeCompra Primary Key ( ArbolModulo, NombreParametro ) 
Go--#SQL  



--------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_COM_Nota_OrdenDeCompra' and xType = 'P' )
   Drop Proc spp_Mtto_COM_Nota_OrdenDeCompra 
Go--#SQL  

Create Proc spp_Mtto_COM_Nota_OrdenDeCompra ( @ArbolModulo varchar(4), @NombreParametro varchar(50), @Valor varchar(7000), 
	@Descripcion varchar(500), @Actualizar tinyint = 0 ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From COM_Nota_OrdenDeCompra (NoLock) Where ArbolModulo = @ArbolModulo and NombreParametro = @NombreParametro ) 
	   Insert Into COM_Nota_OrdenDeCompra ( ArbolModulo, NombreParametro, Valor, Descripcion, Status ) 
	   Select @ArbolModulo, @NombreParametro, @Valor, @Descripcion, 'A'
	Else 
	   Begin 
	       If @Actualizar = 1 
	          Begin  
			     Update COM_Nota_OrdenDeCompra Set Status = 'A', Valor = @Valor, Actualizado = 0 
			     Where ArbolModulo = @ArbolModulo and NombreParametro = @NombreParametro 
			  End 
	   End 	
End 
Go--#SQL     