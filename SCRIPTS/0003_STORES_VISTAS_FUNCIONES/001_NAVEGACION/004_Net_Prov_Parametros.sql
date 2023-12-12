If Exists ( Select Name From Sysobjects Where Name = 'Net_Proveedores' and xType = 'U' )
      Drop Table Net_Proveedores
Go--#SQL 

Create Table Net_Proveedores
(
     IdProveedor varchar(4) Not Null,
     LoginProv varchar(20) Not Null,
     Password varchar(500) Not Null Default '',
     FechaRegistro datetime Not Null Default getdate(),
     FechaActualizacion datetime Not Null Default getdate(),
     Keyx int Not Null,
     Status varchar(1) Not Null Default 'A',
     Actualizado tinyint Not Null Default 0 
)
Go--#SQL

Alter Table Net_Proveedores Add Constraint Pk_Net_Proveedores Primary Key ( IdProveedor )
Go--#SQL


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

