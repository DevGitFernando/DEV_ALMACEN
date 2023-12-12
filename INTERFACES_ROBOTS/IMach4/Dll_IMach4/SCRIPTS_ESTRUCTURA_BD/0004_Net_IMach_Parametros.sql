If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_Net_Parametros' and xType = 'U' ) 
   Drop Table IMach_Net_Parametros 
Go--#SQL      

Create Table IMach_Net_Parametros  
( 
	NombreParametro varchar(50) Not Null, 
	Valor varchar(200) Not Null Default '', 
	Descripcion varchar(500) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0  
)
Go--#SQL   

Alter Table IMach_Net_Parametros Add Constraint PK_IMach_Net_Parametros Primary Key ( NombreParametro ) 
Go--#SQL   

--------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_IMach_Net_Parametros' and xType = 'P' )
   Drop Proc spp_Mtto_IMach_Net_Parametros 
Go--#SQL   

Create Proc spp_Mtto_IMach_Net_Parametros ( 
	@NombreParametro varchar(50), @Valor varchar(200), @Descripcion varchar(500), @Actualizar tinyint = 0 ) 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From IMach_Net_Parametros (NoLock) 
			Where NombreParametro = @NombreParametro ) 
	   Insert Into IMach_Net_Parametros ( NombreParametro, Valor, Descripcion, Status ) 
	   Select @NombreParametro, @Valor, @Descripcion, 'A'
	Else 
	   Begin 
	       If @Actualizar = 1 
	          Begin  
			     Update IMach_Net_Parametros Set Status = 'A', Valor = @Valor, Actualizado = 0 
			     Where NombreParametro = @NombreParametro
			  End 
	   End 
End 
Go--#SQL     
--------------------- 
