
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_OrdenesCompra_CodigosEAN_Det' and xType = 'U' ) 
   Drop Table COM_OCEN_OrdenesCompra_CodigosEAN_Det
Go--#SQL 


If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_OrdenesCompra_Claves_Det' and xType = 'U' ) 
   Drop Table COM_OCEN_OrdenesCompra_Claves_Det 
Go--#SQL


------------------------------------------------ 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_OrdenesCompra_Claves_Enc' and xType = 'U' ) 
   Drop Table COM_OCEN_OrdenesCompra_Claves_Enc  
Go--#SQL 

Create Table COM_OCEN_OrdenesCompra_Claves_Enc
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	FolioOrden varchar(8) Not Null, -- Unique, 	-------------- Consecutivo general de Ordenes de Compra
	FacturarA varchar(3) Not Null,	
	IdProveedor varchar(4) Not Null, 	 		
	IdPersonal varchar(4) Not Null, 
	FechaRegistro datetime Not Null Default getdate(),
	EstadoEntrega varchar(2) Not Null, 
	EntregarEn varchar(4) Not Null Default '',    ---- Farmacia / Almacen centralizador del Estado 
	FechaRequeridaEntrega datetime Not Null Default getdate(),
	FechaColocacion datetime Not Null Default getdate(),
	TipoOrden smallint Not Null Default 1, 
	--FechaPromesaEntrega datetime Not Null Default cast('2010-01-01' as datetime), 
	Observaciones varchar(200) Not Null Default '',
	EsAutomatica bit Not Null Default 0,
	FolioPedido varchar(6) Not Null Default '', 
	EsCentral bit Not Null Default 0,	
    EsContado bit Not Null Default 0, 
	
	IdCliente varchar(4) Not Null Default '', 
	IdSubCliente varchar(4) Not Null Default '', 
	FolioOrdenVinculada varchar(8) Not Null Default '', 
	HabilitadoParaPago bit Not Null Default 1, 
	DiasDePLazo int Not Null Default 0, 
	
	Status varchar(2) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table COM_OCEN_OrdenesCompra_Claves_Enc Add Constraint PK_COM_OCEN_OrdenesCompra_Claves_Enc 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden ) 
Go--#SQL 

Alter Table COM_OCEN_OrdenesCompra_Claves_Enc Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Enc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL
  
Alter Table COM_OCEN_OrdenesCompra_Claves_Enc Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Enc_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL  

Alter Table COM_OCEN_OrdenesCompra_Claves_Enc Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Enc_CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table COM_OCEN_OrdenesCompra_Claves_Enc Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Enc_CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

Alter Table COM_OCEN_OrdenesCompra_Claves_Enc Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Enc_CatProveedores 
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
Go--#SQL  

  
---------------------------------------------------------------------------  

If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_OrdenesCompra_Claves_Det' and xType = 'U' ) 
   Drop Table COM_OCEN_OrdenesCompra_Claves_Det
Go--#SQL 

Create Table COM_OCEN_OrdenesCompra_Claves_Det
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	FolioOrden varchar(8) Not Null, 			
	IdClaveSSA varchar(4) Not Null, 
	--CodigoEAN varchar(30) Not Null,
	Cantidad int Not Null Default 0, 	
	Precio Numeric(14,4) Not Null Default 0, 
	Descuento Numeric(14,4) Not Null Default 0, 
	TasaIva Numeric(14,4) Not Null Default 0, 		
	Iva Numeric(14,4) Not Null Default 0,
	PrecioUnitario Numeric(14,4) Not Null Default 0, 
	PrecioReferencia numeric(14,4) Not Null Default 0, 
	ImpteIva Numeric(14,4) Not Null Default 0, 	
	Importe Numeric(14,4) Not Null Default 0, 	

	CodigoEAN_Bloqueado bit Not Null Default 0, 
	IdPersonal_Bloquea Varchar(4) Not Null Default '', 
	Fecha_Bloqueado datetime Not Null Default getdate(), 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 			
) 
Go--#SQL 

Alter Table COM_OCEN_OrdenesCompra_Claves_Det Add Constraint PK_COM_OCEN_OrdenesCompra_Claves_Det
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdClaveSSA  ) 
Go--#SQL 

Alter Table COM_OCEN_OrdenesCompra_Claves_Det Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Det_COM_OCEN_OrdenesCompra_Claves_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden ) 
	References COM_OCEN_OrdenesCompra_Claves_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden )
Go--#SQL

Alter Table COM_OCEN_OrdenesCompra_Claves_Det Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Det_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
Go--#SQL 



---------------------------------------------------------------------------  

If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_OrdenesCompra_CodigosEAN_Det' and xType = 'U' ) 
   Drop Table COM_OCEN_OrdenesCompra_CodigosEAN_Det
Go--#SQL 

Create Table COM_OCEN_OrdenesCompra_CodigosEAN_Det
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	FolioOrden varchar(8) Not Null,

	Partida int Not Null Default 1,  	 			
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null,
	Cantidad int Not Null Default 0,
		
	CantidadSobreCompra int Not Null Default 0,
	ComisionNegociadora Numeric(14, 4) Not Null Default 0.0000, 
	 	
	AplicaCosto bit Default 'false',   
	Precio Numeric(14,4) Not Null Default 0, 
	Descuento Numeric(14,4) Not Null Default 0, 
	TasaIva Numeric(14,4) Not Null Default 0, 		
	Iva Numeric(14,4) Not Null Default 0,
	PrecioUnitario Numeric(14,4) Not Null Default 0,
	ImpteIva Numeric(14,4) Not Null Default 0, 	
	Importe Numeric(14,4) Not Null Default 0,
	ObservacionesSobreCompra varchar(100) Not Null Default '',
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 			
) 
Go--#SQL 

Alter Table COM_OCEN_OrdenesCompra_CodigosEAN_Det Add Constraint PK_COM_OCEN_OrdenesCompra_CodigosEAN_Det
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden, Partida, IdProducto, CodigoEAN  ) 
Go--#SQL 

Alter Table COM_OCEN_OrdenesCompra_CodigosEAN_Det Add Constraint FK_COM_OCEN_OrdenesCompra_CodigosEAN_Det_COM_OCEN_OrdenesCompra_Claves_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden ) 
	References COM_OCEN_OrdenesCompra_Claves_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden )
Go--#SQL

Alter Table COM_OCEN_OrdenesCompra_CodigosEAN_Det Add Constraint FK_COM_OCEN_OrdenesCompra_CodigosEAN_Det_CatProductos_CodigosRelacionados
	Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN )
Go--#SQL 