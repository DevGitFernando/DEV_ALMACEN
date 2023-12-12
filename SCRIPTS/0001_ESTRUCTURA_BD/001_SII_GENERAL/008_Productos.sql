If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos_Estado' and xType = 'U' ) 
	Drop Table CatProductos_Estado --Farmacia 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos_CodigosRelacionados' and xType = 'U' ) 
	Drop Table CatProductos_CodigosRelacionados 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos' and xType = 'U' ) 
	Drop Table CatProductos 
Go--#SQL  


---------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatLaboratorios' and xType = 'U' ) 
	Drop Table CatLaboratorios 
Go--#SQL  

Create Table CatLaboratorios 
( 
	IdLaboratorio varchar(4) Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatLaboratorios Add Constraint PK_CatLaboratorios Primary Key ( IdLaboratorio )
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPresentaciones' and xType = 'U' ) 
	Drop Table CatPresentaciones 
Go--#SQL  

Create Table CatPresentaciones 
( 
	IdPresentacion varchar(3) Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatPresentaciones Add Constraint PK_CatPresentaciones Primary Key ( IdPresentacion )
Go--#SQL  

-------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos' and xType = 'U' ) 
	Drop Table CatProductos 
Go--#SQL  

Create Table CatProductos 
(
	IdProducto varchar(8) Not Null, 
	IdClaveSSA_Sal varchar(4) Not Null, 
	Descripcion varchar(200) Not Null Default '', 
	DescripcionCorta varchar(100) Not Null Default '', 	
	IdClasificacion varchar(4) Not Null Default '', 
	IdTipoProducto varchar(2) Not Null Default '1', 
	EsMedicamentoControlado bit default 'False', 
	IdFamilia varchar(2) Not Null, 
	IdSubFamilia varchar(2) Not Null, 
	IdSegmento varchar(2) Not Null, 	
	IdLaboratorio varchar(4) Default '', 
	IdPresentacion varchar(3) Not Null, 
	Descomponer bit Not Null default 'false', 
	ContenidoPaquete int Not Null Default 0, 
	ManejaCodigoEAN bit Not Null default 'true', 
    UtilidadProducto numeric(14, 4) Not Null Default 0,
    PrecioMaxPublico numeric(14, 4) Not Null Default 0,
    DescuentoGral numeric(14, 4) Not Null Default 0, 
    EsSectorSalud bit Not Null Default 'false',  
    FechaUpdate datetime Not Null Default getdate(),    
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CatProductos Add Constraint PK_CatProductos Primary Key ( IdProducto ) 
Go--#SQL 

Alter Table CatProductos Add Constraint FK_CatProductos_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA_Sal ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
Go--#SQL 

Alter Table CatProductos Add Constraint FK_CatProductos_CatLaboratorios 
	Foreign Key ( IdLaboratorio ) References CatLaboratorios ( IdLaboratorio )
Go--#SQL  

Alter Table CatProductos Add Constraint FK_CatProductos_CatSubFamilias 
	Foreign Key ( IdFamilia, IdSubFamilia ) References CatSubFamilias ( IdFamilia, IdSubFamilia ) 
Go--#SQL  

Alter Table CatProductos Add Constraint FK_CatProductos_CatSegmentosSubFamilias 
	Foreign Key ( IdFamilia, IdSubFamilia, IdSegmento ) References CatSegmentosSubFamilias ( IdFamilia, IdSubFamilia, IdSegmento ) 
Go--#SQL  

Alter Table CatProductos Add Constraint FK_CatProductos_CatClasificacionesSSA 
	Foreign Key ( IdClasificacion ) References CatClasificacionesSSA ( IdClasificacion ) 
Go--#SQL  

Alter Table CatProductos Add Constraint FK_CatProductos_CatPresentaciones 
	Foreign Key ( IdPresentacion ) References CatPresentaciones ( IdPresentacion ) 
Go--#SQL  

Alter Table CatProductos Add Constraint FK_CatProductos_CatTiposDeProducto 
	Foreign Key ( IdTipoProducto ) References CatTiposDeProducto ( IdTipoProducto ) 
Go--#SQL  
--------------------

-------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos_Historico' and xType = 'U' ) 
	Drop Table CatProductos_Historico 
Go--#SQL  

Create Table CatProductos_Historico 
(
	IdProducto varchar(8) Not Null, 
	IdClaveSSA_Sal varchar(4) Not Null, 
	Descripcion varchar(200) Not Null Default '', 
	DescripcionCorta varchar(100) Not Null Default '', 	
	IdClasificacion varchar(4) Not Null Default '', 
	IdTipoProducto varchar(2) Not Null Default '1', 
	EsMedicamentoControlado bit default 'False', 
	IdFamilia varchar(2) Not Null, 
	IdSubFamilia varchar(2) Not Null, 
	IdSegmento varchar(2) Not Null, 	
	IdLaboratorio varchar(4) Default '', 
	IdPresentacion varchar(3) Not Null, 
	Descomponer bit Not Null default 'false', 
	ContenidoPaquete int Not Null Default 0, 
	ManejaCodigoEAN bit Not Null default 'true', 
    UtilidadProducto numeric(14, 4) Not Null Default 0,
    PrecioMaxPublico numeric(14, 4) Not Null Default 0,
    DescuentoGral numeric(14, 4) Not Null Default 0, 
    EsSectorSalud bit Not Null Default 'false',  
    FechaRegistro datetime Not Null Default getdate(),    
    IdPersonal varchar(4) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	Keyx int identity(1,1)  
)
Go--#SQL 

Alter Table CatProductos_Historico Add Constraint PK_CatProductos_Historico Primary Key ( IdProducto, FechaRegistro ) 
Go--#SQL  

Alter Table CatProductos_Historico Add Constraint PK_CatProductos_HistoricoCatProductos 
	Foreign Key ( IdProducto ) References CatProductos ( IdProducto ) 
Go--#SQL  


---------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos_CodigosRelacionados' and xType = 'U' ) 
	Drop Table CatProductos_CodigosRelacionados 
Go--#SQL  

Create Table CatProductos_CodigosRelacionados 
(
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null Unique NonClustered, 
	CodigoEAN_Interno varchar(30) Not Null, 

	ContenidoPiezasUnitario Int Not Null Default 0, 
	ContenidoCorrugado Int Not Null Default 0, 
	Cajas_Cama Int Not Null Default 0, 
	Cajas_Tarima Int Not Null Default 0, 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatProductos_CodigosRelacionados Add Constraint PK_CatProductos_CodigosRelacionados Primary Key ( IdProducto, CodigoEAN ) 
Go--#SQL  

Alter Table CatProductos_CodigosRelacionados Add Constraint FK_CatProductos_CodigosRelacionados_CatProductos 
	Foreign Key ( IdProducto )  References CatProductos ( IdProducto )
Go--#SQL  
----------------------- 



---------------- Asignar Productos por Estado 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos_Estado' and xType = 'U' ) 
	Drop Table CatProductos_Estado --Farmacia 
Go--#SQL 

Create Table CatProductos_Estado 
(
	IdEstado varchar(2) Not Null, 
	IdProducto varchar(8) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatProductos_Estado Add Constraint PK_CatProductos_Estado Primary Key ( IdEstado, IdProducto ) 
Go--#SQL  

Alter Table CatProductos_Estado Add Constraint FK_CatProductos_Estado_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL  

Alter Table CatProductos_Estado Add Constraint FK_CatProductos_Estado_CatProductos  
	Foreign Key ( IdProducto ) References CatProductos ( IdProducto ) 
Go--#SQL  
----------------------- 


-------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos_RegistrosSanitarios' and xType = 'U' ) 
	Drop Table CatProductos_RegistrosSanitarios 
Go--#SQL 

Create Table CatProductos_RegistrosSanitarios 
( 
	IdProducto varchar(8) Not Null,
	CodigoEAN varchar(30) Not Null, 
	Consecutivo varchar(4) Not Null Default '', 
	Tipo varchar(3) Not Null Default '', 
	Año varchar(4) Not Null Default '', 	
	FechaVigencia DateTime Not Null,
	Documento text default '', -------
	NombreDocto varchar(200) Default '', 
	Status varchar(4) Not Null Default '', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatProductos_RegistrosSanitarios Add Constraint PK_CatProductos_RegistrosSanitarios 
	Primary Key ( IdProducto, CodigoEAN )
Go--#SQL 

Alter Table CatProductos_RegistrosSanitarios Add Constraint FK_CatProductos_RegistrosSanitarios_CodigosRelacionados
	Foreign Key (IdProducto, CodigoEAN) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN )
Go--#SQL 

-------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProductos_RegSSA_Imagen' and xType = 'U' ) 
	Drop Table CatProductos_RegSSA_Imagen 
Go--#SQL 

Create Table CatProductos_RegSSA_Imagen 
( 
	IdProducto varchar(8) Not Null,
	CodigoEAN varchar(30) Not Null, 
	Consecutivo varchar(4) Not Null, 
	
	FechaRegistro datetime Not Null Default getdate(), 
	NombreArchivo varchar(200) Not Null Default '', 
	Extencion varchar(10) Not Null Default '', 
	TamArchivo numeric(14, 4) Not Null Default 0, 
    RegSSA_Archivo Text Null Default '',     

	Status varchar(4) Not Null Default '', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatProductos_RegSSA_Imagen Add Constraint PK_CatProductos_RegSSA_Imagen 
	Primary Key ( IdProducto, CodigoEAN )
Go--#SQL 


Alter Table CatProductos_RegSSA_Imagen Add Constraint FK_CatProductos_RegSSA_Imagen_CatProductos_RegistrosSanitarios 
	Foreign Key (IdProducto, CodigoEAN) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN )
Go--#SQL 
 