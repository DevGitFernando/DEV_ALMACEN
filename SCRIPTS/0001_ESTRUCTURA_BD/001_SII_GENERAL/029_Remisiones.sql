
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet_Lotes' and xType = 'U' ) 
	Drop Table RemisionesDet_Lotes  
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet' and xType = 'U' ) 
	Drop Table RemisionesDet 
Go--#SQL 

--------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesEnc' and xType = 'U' ) 
	Drop Table RemisionesEnc 
Go--#SQL  

Create Table RemisionesEnc 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioRemision varchar(30) Not Null, 
	FolioVenta varchar(30) Not Null, 
	--EsConsignacion tinyint Not Null Default 0,  	
	FolioMovtoInv varchar(30) Not Null, 
	FechaSistema datetime Default GetDate(),
	FechaRegistro datetime Default getdate(), 
	IdCaja varchar(2) Not Null Default '', 
	IdPersonal varchar(4) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdPrograma varchar(4) Not Null, 
	IdSubPrograma varchar(4) Not Null, 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Descuento Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0, 
	TipoDeVenta smallint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table RemisionesEnc Add Constraint PK_RemisionesEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision ) 
Go--#SQL  

Alter Table RemisionesEnc Add Constraint FK_RemisionesEnc_VentasEnc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL 

Alter Table RemisionesEnc Add Constraint FK_RemisionesEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table RemisionesEnc Add Constraint FK_RemisionesEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table RemisionesEnc Add Constraint FK_RemisionesEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  
---------------------


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet' and xType = 'U' ) 
	Drop Table RemisionesDet 
Go--#SQL  

Create Table RemisionesDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioRemision varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	Renglon int Not Null,   	
	UnidadDeSalida smallint Not Null Default 1, 
	CantidadVendida Numeric(14,4) Not Null Default 0,
	CostoUnitario Numeric(14,4) Not Null Default 0,  	
	PrecioUnitario Numeric(14,4) Not Null Default 0, 
	TasaIva Numeric(14,4) Not Null Default 0, 
	ImpteIva Numeric(14,4) Not Null Default 0, 
	PorcDescto Numeric(14,4) Not Null Default 0, 
	ImpteDescto Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table RemisionesDet Add Constraint PK_RemisionesDet Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL  

Alter Table RemisionesDet Add Constraint FK_RemisionesDet_RemisionesEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision ) References RemisionesEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision ) 
Go--#SQL  


Alter Table RemisionesDet Add Constraint FK_RemisionesDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDet_Lotes' and xType = 'U' ) 
	Drop Table RemisionesDet_Lotes  
Go--#SQL  

Create Table RemisionesDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	FolioRemision varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	EsConsignacion Bit Not Null Default 'false', 
	Renglon int not null,  
	CantidadVendida Numeric(14,4) Not Null Default 0,
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table RemisionesDet_Lotes Add Constraint PK_RemisionesDet_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioRemision, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table RemisionesDet_Lotes Add Constraint FK_RemisionesDet_Lotes_RemisionesDet 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision, IdProducto, CodigoEAN, Renglon ) 
	References RemisionesDet ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL  

Alter Table RemisionesDet_Lotes Add Constraint FK_RemisionesDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  


