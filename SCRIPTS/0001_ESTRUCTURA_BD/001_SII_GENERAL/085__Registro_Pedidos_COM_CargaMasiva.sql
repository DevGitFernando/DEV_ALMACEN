If Exists ( Select * From sysobjects (NoLock) Where Name = 'Registro_Pedidos_CargaMasiva' and xType = 'U' ) 
   Drop Table Registro_Pedidos_CargaMasiva
Go--#SQL 

Create Table Registro_Pedidos_CargaMasiva  
( 
	IdClaveSSA varchar(4) Not Null Default '',
	ClaveSSA varchar(30) default '',
	DescripcionClaveSSA varchar(max) default '', 
	Presentacion varchar(100) default '', 
	ContenidoPaquete int Not Null Default 0,
	CantidadPzas Int Not Null Default 0, 
	CantidadCajas Int Not Null Default 0,
	Existencia Int Not Null Default 0 	
	
) 
Go--#SQL 
