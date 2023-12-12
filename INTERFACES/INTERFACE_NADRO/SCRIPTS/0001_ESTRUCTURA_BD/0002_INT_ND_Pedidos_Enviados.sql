------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'INT_ND_Pedidos_Enviados_Det' and xType = 'U' ) 
   Drop Table INT_ND_Pedidos_Enviados_Det
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'INT_ND_Pedidos_Enviados_Doctos' and xType = 'U' ) 
   Drop Table INT_ND_Pedidos_Enviados_Doctos 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'INT_ND_Pedidos_Enviados' and xType = 'U' ) 
   Drop Table INT_ND_Pedidos_Enviados   
Go--#SQL 

Create Table INT_ND_Pedidos_Enviados 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,					
	FolioPedido varchar(8) Not Null, -- Unique, 	-------------- Consecutivo general de Pedidos		

	TipoDeFarmacias smallint Not Null Default 0,	-- 1 ==> Administradas	2 ==> No Administradas	

	IdProveedor varchar(4) Not Null,
	IdPersonal varchar(4) Not Null, 
	FechaRegistro datetime Not Null Default getdate(), 
	FechaRequeridaEntrega datetime Not Null Default getdate(),
	
	TipoRemision Int Not Null Default 0, 
	
	Status varchar(2) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table INT_ND_Pedidos_Enviados Add Constraint PK_INT_ND_Pedidos_Enviados 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL 

Alter Table INT_ND_Pedidos_Enviados Add Constraint FK_INT_ND_Pedidos_Enviados___CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL

Alter Table INT_ND_Pedidos_Enviados Add Constraint FK_INT_ND_Pedidos_Enviados___CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table INT_ND_Pedidos_Enviados Add Constraint FK_INT_ND_Pedidos_Enviados___CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

Alter Table INT_ND_Pedidos_Enviados Add Constraint FK_INT_ND_Pedidos_Enviados_CatProveedores 
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
Go--#SQL  


------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'INT_ND_Pedidos_Enviados_Doctos' and xType = 'U' ) 
   Drop Table INT_ND_Pedidos_Enviados_Doctos 
Go--#SQL 

Create Table INT_ND_Pedidos_Enviados_Doctos
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 	
	FolioPedido varchar(8) Not Null, 	

	IdPersonal varchar(4) Not Null, 
	Observaciones varchar(200) Not Null Default '', 
		
	NombreDocumento varchar(200) Not Null Default '',		---	Nombre documento integrado 
	ContenidoDocumento varchar(max) Not Null Default '',	---	Contenido documento integrado 
	SegmentoDocumento varchar(100) Not Null Default ''		---	Hoja integrada			
) 
Go--#SQL 

Alter Table INT_ND_Pedidos_Enviados_Doctos Add Constraint PK_INT_ND_Pedidos_Enviados_Doctos  
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL 

Alter Table INT_ND_Pedidos_Enviados_Doctos Add Constraint FK_INT_ND_Pedidos_Enviados_Doctos___INT_ND_Pedidos_Enviados 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
	References INT_ND_Pedidos_Enviados ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido )
Go--#SQL


------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'INT_ND_Pedidos_Enviados_Det' and xType = 'U' ) 
   Drop Table INT_ND_Pedidos_Enviados_Det
Go--#SQL 

Create Table INT_ND_Pedidos_Enviados_Det
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 	
	FolioPedido varchar(8) Not Null, 	

	CodigoCliente varchar(20) Not Null, 
	ReferenciaPedido varchar(20) Not Null, 
	IdFarmaciaPedido varchar(4) Not Null,			----- Poner de forma automatica en base a la informacion relacionada  

	ClaveSSA varchar(20) Not Null, 
	CodigoProducto varchar(30) Not Null, 
	CodigoEAN varchar(30) Not Null,
	CodigoEAN_Existe int Not Null Default 0,    	
	
	Cantidad int Not Null Default 0, 
	CantidadRecibida int Not Null Default 0, 
	CantidadExcedente int Not Null Default 0, 
	 	
	CantidadDañadoMalEstado int Not Null Default 0, 
	CantidadCaducado int Not Null Default 0, 

	Precio Numeric(14,4) Not Null Default 0, 
	PrecioUnitario Numeric(14,4) Not Null Default 0,	
	TasaIva Numeric(14,4) Not Null Default 0, 		
	Iva Numeric(14,4) Not Null Default 0,
	ImpteIva Numeric(14,4) Not Null Default 0, 	
	Importe Numeric(14,4) Not Null Default 0,

	EsValidado Bit Not Null Default 0,
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 			
) 
Go--#SQL 

Alter Table INT_ND_Pedidos_Enviados_Det Add Constraint PK_INT_ND_Pedidos_Enviados_Det
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, ReferenciaPedido, ClaveSSA, CodigoProducto, CodigoEAN  ) 
Go--#SQL 

Alter Table INT_ND_Pedidos_Enviados_Det Add Constraint FK_INT_ND_Pedidos_Enviados_Det___INT_ND_Pedidos_Enviados 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
	References INT_ND_Pedidos_Enviados ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido )
Go--#SQL 

Alter Table INT_ND_Pedidos_Enviados_Det Add Constraint FK_INT_ND_Pedidos_Enviados_Det___CatFarmaciasPedido
	Foreign Key ( IdEstado, IdFarmaciaPedido ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

