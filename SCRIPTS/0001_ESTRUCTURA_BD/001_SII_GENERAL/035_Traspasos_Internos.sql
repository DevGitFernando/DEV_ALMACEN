If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table TraspasosDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet_Lotes' and xType = 'U' ) 
	Drop Table TraspasosDet_Lotes  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet' and xType = 'U' ) 
	Drop Table TraspasosDet 
Go--#SQL 

-------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosEnc' and xType = 'U' ) 
	Drop Table TraspasosEnc 
Go--#SQL 

Create Table TraspasosEnc
(
	IdEmpresa varchar(3) Not Null,  
	IdEstado varchar(2) Not Null,             --- Quien envia la Transferencia 
	IdFarmacia varchar(4) Not Null,	          --- Quien envia la Transferencia 

	FolioTraspaso varchar(30) Not Null,
	IdSubFarmaciaOrigen varchar(2) Not Null,
	IdSubFarmaciaDestino varchar(2) Not Null, 
	FolioMovtoInv varchar(30) Not Null, 
	FolioMovtoInvRef varchar(30) Not Null Default '', -- Solo para Entradas por Transferencia, 
	FolioTraspasoRef varchar(30) Not Null Default '', 
	TipoTraspaso varchar(3) Not Null Default 'TIS', 
	FechaTraspaso datetime Not Null Default getdate(), 
	FechaRegistro datetime Not Null Default getdate(),  
	IdPersonal varchar(4) Not Null, 
	Observaciones varchar(500) Not Null Default '', 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table TraspasosEnc Add Constraint PK_TraspasosEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTraspaso ) 
Go--#SQL 

Alter Table TraspasosEnc Add Constraint FK_TraspasosEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table TraspasosEnc Add Constraint FK_TraspasosEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table TraspasosEnc Add Constraint FK_TraspasosEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 

---------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet' and xType = 'U' ) 
	Drop Table TraspasosDet 
Go--#SQL 

Create Table TraspasosDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioTraspaso varchar(30) Not Null,	 
	IdProducto varchar(8) Not Null, 	
	CodigoEAN varchar(30) Not Null, 
	Renglon int Not Null,  
 
	Cantidad Numeric(14,4) Not Null Default 0, 

	CostoUnitario Numeric(14,4) Not Null Default 0, 	
	TasaIva Numeric(14,4) Not Null Default 0, 
	SubTotal Numeric(14,4) Not Null Default 0, 
	ImpteIva Numeric(14,4) Not Null Default 0, 
	Importe Numeric(14,4) Not Null Default 0,	 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table TraspasosDet Add Constraint PK_TraspasosDet 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTraspaso, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL 

Alter Table TraspasosDet Add Constraint FK_TraspasosDet_TraspasosEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTraspaso ) 
	References TraspasosEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioTraspaso ) 
Go--#SQL 

Alter Table TraspasosDet Add Constraint FK_TraspasosDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet_Lotes' and xType = 'U' ) 
	Drop Table TraspasosDet_Lotes  
Go--#SQL 

Create Table TraspasosDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null,	 
	FolioTraspaso varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	Renglon int not null,  
	EsConsignacion tinyint Not Null Default 0,  	
	Cantidad Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table TraspasosDet_Lotes Add Constraint PK_TraspasosDet_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioTraspaso, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL 

Alter Table TraspasosDet_Lotes Add Constraint FK_TraspasosDet_Lotes_TraspasosDet 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTraspaso, IdProducto, CodigoEAN, Renglon ) 
	References TraspasosDet ( IdEmpresa, IdEstado, IdFarmacia, FolioTraspaso, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL 

Alter Table TraspasosDet_Lotes Add Constraint FK_TraspasosDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL 

---------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TraspasosDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table TraspasosDet_Lotes_Ubicaciones 
Go--#SQL 

Create Table TraspasosDet_Lotes_Ubicaciones 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null,	 
	FolioTraspaso varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null,
	Renglon int Not Null,
	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null, 
	  
	EsConsignacion tinyint Not Null Default 0,  	
	Cantidad Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table TraspasosDet_Lotes_Ubicaciones Add Constraint PK_TraspasosDet_Lotes_Ubicaciones 
    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioTraspaso, IdProducto, CodigoEAN, ClaveLote, Renglon, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL 

Alter Table TraspasosDet_Lotes_Ubicaciones Add Constraint FK_TraspasosDet_Lotes_Ubicaciones_TraspasosDet_Lotes
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioTraspaso, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
    References TraspasosDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioTraspaso, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL 

