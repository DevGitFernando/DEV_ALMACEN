

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesProveedorDet_Lotes_Ubicaciones' and xType = 'U' ) 
Drop Table DevolucionesProveedorDet_Lotes_Ubicaciones  
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesProveedorDet_Lotes' and xType = 'U' ) 
Drop Table DevolucionesProveedorDet_Lotes  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesProveedorDet' and xType = 'U' ) 
	Drop Table DevolucionesProveedorDet 
Go--#SQL  

-------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesProveedorEnc' and xType = 'U' ) 
	Drop Table DevolucionesProveedorEnc 
Go--#SQL  

Create Table DevolucionesProveedorEnc
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	--IdAlmacen varchar(2) Not Null Default '', 
	--EsCompraAlmacen bit Not Null Default 'false', 
	FolioDevProv varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	FolioMovtoInv varchar(30) Not Null, 
	IdPersonal varchar(4) Not Null, 

	FechaSistema datetime Default GetDate(),     --- Fecha de Sistema en que se realizo el movimiento 
	IdProveedor varchar(4) Not Null Default '', 
	ReferenciaDocto varchar(20) Not Null Default '', 
	FechaDocto datetime Not Null Default getdate(), 
	FechaVenceDocto datetime Not Null Default getdate() + 30, 
	Observaciones varchar(100) Not Null Default '', 
	--EsCompraConPromocion tinyint Not Null Default 0, 

	--EsPromocionRegalo bit Not Null Default 'false', 	
	
	-- Descuento Numeric(14,4) Not Null Default 0, 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0,
	
	----DoctoPagado bit Not Null Default 'false',  --- Revisar con Gilberto 
	----AbonoDocto Numeric(14,4) Not Null Default 0, 
	----SaldoDocto Numeric(14,4) Not Null Default 0, 	
	----FechaPagoDocto datetime Not Null default cast('1900-01-01' as datetime), --- Revisar con Gilberto  

	FechaRegistro datetime Not Null Default getdate(),  
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  


Alter Table DevolucionesProveedorEnc Add Constraint PK_DevolucionesProveedorEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevProv ) 
Go--#SQL  

Alter Table DevolucionesProveedorEnc Add Constraint FK_DevolucionesProveedorEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table DevolucionesProveedorEnc Add Constraint FK_DevolucionesProveedorEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

 
Alter Table DevolucionesProveedorEnc Add Constraint FK_DevolucionesProveedorEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 


Alter Table DevolucionesProveedorEnc Add Constraint FK_DevolucionesProveedorEnc_CatProveedores 
	Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
Go--#SQL  


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesProveedorDet' and xType = 'U' ) 
	Drop Table DevolucionesProveedorDet 
Go--#SQL  

Create Table DevolucionesProveedorDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioDevProv varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 	
	CodigoEAN varchar(30) Not Null, 
	Renglon int Not Null,  
	-- EsConsignacion tinyint Not Null Default 0,  	
	UnidadDeEntrada smallint Not Null Default 1, 
	Cant_Recibida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	CostoUnitario Numeric(14,4) Not Null Default 0, 	
	TasaIva Numeric(14,4) Not Null Default 0, 
	SubTotal Numeric(14,4) Not Null Default 0, 
	ImpteIva Numeric(14,4) Not Null Default 0, 
	Importe Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table DevolucionesProveedorDet Add Constraint PK_DevolucionesProveedorDet 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevProv, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL 

Alter Table DevolucionesProveedorDet Add Constraint FK_DevolucionesProveedorDet_DevolucionesProveedorEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevProv ) References DevolucionesProveedorEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioDevProv ) 
Go--#SQL  

Alter Table DevolucionesProveedorDet Add Constraint FK_DevolucionesProveedorDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL  


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesProveedorDet_Lotes' and xType = 'U' ) 
	Drop Table DevolucionesProveedorDet_Lotes  
Go--#SQL  

Create Table DevolucionesProveedorDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	FolioDevProv varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	Renglon int not null,  
	EsConsignacion Bit Not Null Default 'false',  	
	Cant_Recibida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table DevolucionesProveedorDet_Lotes Add Constraint PK_DevolucionesProveedorDet_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevProv, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table DevolucionesProveedorDet_Lotes Add Constraint FK_DevolucionesProveedorDet_Lotes_DevolucionesProveedorDet 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevProv, IdProducto, CodigoEAN, Renglon ) 
	References DevolucionesProveedorDet ( IdEmpresa, IdEstado, IdFarmacia, FolioDevProv, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL  

Alter Table DevolucionesProveedorDet_Lotes Add Constraint FK_DevolucionesProveedorDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  


------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesProveedorDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table DevolucionesProveedorDet_Lotes_Ubicaciones 
Go--#SQL 
 
Create Table DevolucionesProveedorDet_Lotes_Ubicaciones 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	FolioDevProv varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	Renglon int not null, 
	
	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null,
	  
	EsConsignacion Bit Not Null Default 'false',  	
	Cant_Recibida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table DevolucionesProveedorDet_Lotes_Ubicaciones Add Constraint PK_DevolucionesProveedorDet_Lotes_Ubicaciones 
    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevProv, IdProducto, CodigoEAN, ClaveLote, Renglon, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL
  
Alter Table DevolucionesProveedorDet_Lotes_Ubicaciones Add Constraint FK_DevolucionesProveedorDet_Lotes_Ubicaciones_DevolucionesProveedorDet_Lotes 
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevProv, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
    References DevolucionesProveedorDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevProv, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL 