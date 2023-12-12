------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFG_ClavesSSA_Precios__CargaMasiva' and xType = 'U' ) 
   Drop Table CFG_ClavesSSA_Precios__CargaMasiva
Go--#SQL 

Create Table CFG_ClavesSSA_Precios__CargaMasiva  
( 
	IdEstado varchar(2) Not Null Default '', 
	IdCliente varchar(4) Not Null Default '',
	IdSubCliente varchar(4) Not Null Default '',		
	IdClaveSSA_Sal varchar(4) Not Null Default '',

	ClaveSSA varchar(50) Not Null Default '', 
	ClaveSSA_Proceso varchar(20) Not Null Default '', 
	ClaveValida bit Not Null Default 'False', 	
	DescripcionClaveSSA varchar(max) Not Null Default '', 		

	ContenidoPaquete_Licitado int Not Null Default 0, 
	IdPresentacion_Licitado varchar(50) Not Null Default '', 
	Dispensacion_CajasCompletas int Not Null Default 0, 
	SAT_ClaveDeProducto_Servicio varchar(50) Not Null Default '', 
	SAT_UnidadDeMedida varchar(50) Not Null Default '', 


	Precio Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0, 
	Factor int Not Null Default 1 
) 
Go--#SQL 


