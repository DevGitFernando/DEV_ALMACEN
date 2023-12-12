
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPreAltaProductos' and xType = 'U' ) 
	Drop Table CatPreAltaProductos 
Go--#SQL 

Create Table CatPreAltaProductos 
( 
	CodigoEAN varchar(30) Not Null,
	IdProveedor varchar(4) Not Null,
	ClaveSSA_Sal varchar(15) Not Null, 	 
	DescripcionSal varchar(200) Not Null Default '', 
	IdTipoProducto varchar(2) Not Null Default '1',
	Descripcion varchar(200) Not Null Default '', 	
	EsMedicamentoControlado bit default 'False', 
    EsSectorSalud bit Not Null Default 'false',	
	IdClasificacion varchar(4) Not Null Default '', 
	Segmento bit Not Null default 'true',  
	Laboratorio varchar(200) Not Null Default '',
	IdPresentacion varchar(3) Not Null, 
	PrecioMaxPublico numeric(14, 4) Not Null Default 0,
	ManejaCodigoEAN bit Not Null default 'true',    
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatPreAltaProductos Add Constraint PK_CatPreAltaProductos Primary Key ( CodigoEAN, IdProveedor ) 
Go--#SQL


Alter Table CatPreAltaProductos Add Constraint FK_CatPreAltaProductos_CatTiposDeProducto 
	Foreign Key ( IdTipoProducto ) References CatTiposDeProducto ( IdTipoProducto ) 
Go--#SQL 

Alter Table CatPreAltaProductos Add Constraint FK_CatPreAltaProductos_CatClasificacionesSSA 
	Foreign Key ( IdClasificacion ) References CatClasificacionesSSA ( IdClasificacion ) 
Go--#SQL 

Alter Table CatPreAltaProductos Add Constraint FK_CatPreAltaProductos_CatPresentaciones 
	Foreign Key ( IdPresentacion ) References CatPresentaciones ( IdPresentacion ) 
Go--#SQL 

--------------------

