-------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasEnc' and xType = 'U' ) 
Begin 
Create Table OrdenesDeComprasEnc
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioOrdenCompra varchar(30) Not Null, 
	FolioOrdenCompraReferencia varchar(30) Not Null, --- Orden de Compra Generada en Servidor Central 	
	
	FolioMovtoInv varchar(30) Not Null, 
	IdPersonal varchar(4) Not Null, 
	
	FechaRegistro datetime Not Null Default getdate(),  
	FechaSistema datetime Default GetDate(),     --- Fecha de Sistema en que se realizo el movimiento 
	IdProveedor varchar(4) Not Null Default '', 

	ReferenciaDocto varchar(20) Not Null Default '', 
	FechaDocto datetime Not Null Default getdate(), 
	FechaVenceDocto datetime Not Null Default getdate() + 30, 
	Observaciones varchar(100) Not Null Default '', 

	--EsPromocionRegalo bit Not Null Default 'false', 	
	
	-- Descuento Numeric(14,4) Not Null Default 0, 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0,
	
-- Criterios para colocacion de pedidos 	
	FechaPromesaEntrega datetime Not Null Default getdate(), 	
	--PorcSurtido numeric(14,4) Not Null Default 0, 
	--PorcPuntualidad numeric(14,4) Not Null Default 0, 
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_OrdenesDeComprasEnc' and xType = 'PK') 
Begin 
	Alter Table OrdenesDeComprasEnc Add Constraint PK_OrdenesDeComprasEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_OrdenesDeComprasEnc_CatEmpresas' and xType = 'F') 
Begin 
	Alter Table OrdenesDeComprasEnc Add Constraint FK_OrdenesDeComprasEnc_CatEmpresas 
		Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_OrdenesDeComprasEnc_CatFarmacias' and xType = 'F') 
Begin 
	Alter Table OrdenesDeComprasEnc Add Constraint FK_OrdenesDeComprasEnc_CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_OrdenesDeComprasEnc_CatPersonal' and xType = 'F') 
Begin 
	Alter Table OrdenesDeComprasEnc Add Constraint FK_OrdenesDeComprasEnc_CatPersonal  
		Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
End
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_OrdenesDeComprasEnc_CatProveedores' and xType = 'F') 
Begin 
	Alter Table OrdenesDeComprasEnc Add Constraint FK_OrdenesDeComprasEnc_CatProveedores 
		Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
End 
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet' and xType = 'U' ) 
Begin 
Create Table OrdenesDeComprasDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioOrdenCompra varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 	
	CodigoEAN varchar(30) Not Null, 
	Renglon int Not Null,  
	CantidadPrometida Numeric(14,4) Not Null Default 0, 
	Cant_Recibida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	--PorcSurtimiento Numeric(14,4) Not Null Default 0, 
	CostoUnitario Numeric(14,4) Not Null Default 0, 	
	TasaIva Numeric(14,4) Not Null Default 0, 
	SubTotal Numeric(14,4) Not Null Default 0, 
	ImpteIva Numeric(14,4) Not Null Default 0, 
	Importe Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_OrdenesDeComprasDet' and xType = 'PK' ) 
Begin 
	Alter Table OrdenesDeComprasDet Add Constraint PK_OrdenesDeComprasDet 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN ) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_OrdenesDeComprasDet_OrdenesDeComprasEnc' and xType = 'F' ) 
Begin 
	Alter Table OrdenesDeComprasDet Add Constraint FK_OrdenesDeComprasDet_OrdenesDeComprasEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra ) 
		References OrdenesDeComprasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_OrdenesDeComprasDet_CatProductos_CodigosRelacionados' and xType = 'F' ) 
Begin 
	Alter Table OrdenesDeComprasDet Add Constraint FK_OrdenesDeComprasDet_CatProductos_CodigosRelacionados 
		Foreign Key ( IdProducto, CodigoEAN ) 
		References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
End 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------  
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet_Lotes' and xType = 'U' ) 
Begin 
Create Table OrdenesDeComprasDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null, 
	FolioOrdenCompra varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	Renglon int not null,  
	--CantidadPrometida Numeric(14,4) Not Null Default 0, 	
	Cant_Recibida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion	
	--PorcSurtimiento Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_OrdenesDeComprasDet_Lotes' and xType = 'PK' ) 
Begin 
	Alter Table OrdenesDeComprasDet_Lotes Add Constraint PK_OrdenesDeComprasDet_Lotes 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN, ClaveLote  ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_OrdenesDeComprasDet_Lotes_OrdenesDeComprasDet' and xType = 'F' ) 
Begin 
	Alter Table OrdenesDeComprasDet_Lotes Add Constraint FK_OrdenesDeComprasDet_Lotes_OrdenesDeComprasDet
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN ) 
		References OrdenesDeComprasDet ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_OrdenesDeComprasDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'F' ) 
Begin 
	Alter Table OrdenesDeComprasDet_Lotes Add Constraint FK_OrdenesDeComprasDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
		References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'OrdenesDeComprasDet_Lotes_Ubicaciones' and xType = 'U' ) 
Begin 
Create Table OrdenesDeComprasDet_Lotes_Ubicaciones 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null, 
	FolioOrdenCompra varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	--Renglon int not null,
	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null,
  
	--CantidadPrometida Numeric(14,4) Not Null Default 0, 	
	Cant_Recibida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion	
	--PorcSurtimiento Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_OrdenesDeComprasDet_Lotes_Ubicaciones' and xType = 'PK' ) 
Begin 
	Alter Table OrdenesDeComprasDet_Lotes_Ubicaciones Add Constraint PK_OrdenesDeComprasDet_Lotes_Ubicaciones 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño  ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_OrdenesDeComprasDet_Lotes_Ubicaciones_OrdenesDeComprasDet_Lotes' and xType = 'F' ) 
Begin 
	Alter Table OrdenesDeComprasDet_Lotes_Ubicaciones Add Constraint FK_OrdenesDeComprasDet_Lotes_Ubicaciones_OrdenesDeComprasDet_Lotes
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN, ClaveLote ) 
		References OrdenesDeComprasDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioOrdenCompra, IdProducto, CodigoEAN, ClaveLote ) 
End 
Go--#SQL 
