------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'SEG_Ctrl_Versiones_Parametros' and xType = 'U' ) 
   Drop Table SEG_Ctrl_Versiones_Parametros 
Go--#SQL    

Create Table SEG_Ctrl_Versiones_Parametros 
( 
	NombreParametro varchar(100) Not Null, 
	Valor varchar(200) Not Null Default '', 
	Descripcion varchar(500) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0  
)
Go--#SQL  

Alter Table SEG_Ctrl_Versiones_Parametros Add Constraint PK_SEG_Ctrl_Versiones_Parametros Primary Key ( NombreParametro ) 
Go--#SQL  


