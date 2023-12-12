----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_Productos' and xType = 'U' ) 
   Drop Table INT_ND_Productos 
Go--#SQL 

Create Table INT_ND_Productos 
( 
	Keyx int Identity(1, 1), 
	Keyx_Auxiliar int Not Null, 		
	IdEstado varchar(2) Not Null Default '', 
	ClaveSSA_ND varchar(20) Not Null Default '', 
	Codigo varchar(20) Not Null Default '', 	
	Descripcion varchar(200) Not Null Default '', 
	Proveedor varchar(50) Not Null Default '', 
	CodigoEAN_ND varchar(20) Not Null Default '', 
	ContenidoPaquete int Null Default 0, 
	ClaveSSA varchar(30) Not Null Default '', 
	IdProducto varchar(8) Not Null Default '', 
	CodigoEAN varchar(20) Not Null Default '' 
) 	
Go--#SQL 

Alter Table INT_ND_Productos Add Constraint PK_INT_ND_Productos Primary Key ( Keyx_Auxiliar ) 
Go--#SQL 

