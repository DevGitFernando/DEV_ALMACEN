If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Prov_Parametros' and xType = 'U' ) 
   Drop Table Net_Prov_Parametros 
Go--#SQL 

Create Table Net_Prov_Parametros 
( 
--	IdEstado varchar(2) Not Null, 
	IdProveedor varchar(4) Not Null, 
--	ArbolModulo varchar(4) Not Null, 	
	NombreParametro varchar(50) Not Null, 
	Valor varchar(200) Not Null Default '', 
	Descripcion varchar(500) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0  
)
Go--#SQL 

Alter Table Net_Prov_Parametros Add Constraint PK_Net_Prov_Parametros Primary Key ( IdProveedor, NombreParametro ) 
Go--#SQL  

