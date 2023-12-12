------------------------------------------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_ND_PedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table INT_ND_PedidosDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_ND_PedidosDet_Lotes' and xType = 'U' ) 
   Drop Table INT_ND_PedidosDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_ND_PedidosEnc' and xType = 'U' ) 
   Drop Table INT_ND_PedidosDet 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_ND_PedidosEnc' and xType = 'U' ) 
   Drop Table INT_ND_PedidosEnc  
Go--#SQL 

Create Table INT_ND_PedidosEnc 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioPedido varchar(30) Not Null, 

	FolioPedidoReferencia varchar(30) Not Null, 
	ReferenciaFolioPedido varchar(30) Not Null, 
	
	FolioMovtoInv varchar(30) Not Null, 
	IdPersonal varchar(4) Not Null, 
	FechaRegistro datetime Not Null Default getdate(),  
	IdProveedor varchar(4) Not Null Default '', 
	Observaciones varchar(100) Not Null Default '', 
	
	EsNoSolicitado bit Not Null Default 'false', 
		
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0,	
	FechaPromesaEntrega datetime Not Null Default getdate(), 	
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_INT_ND_PedidosEnc' and xType = 'PK') 
Begin 
	Alter Table INT_ND_PedidosEnc Add Constraint PK_INT_ND_PedidosEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_INT_ND_PedidosEnc_CatEmpresas' and xType = 'F') 
Begin 
	Alter Table INT_ND_PedidosEnc Add Constraint FK_INT_ND_PedidosEnc___CatEmpresas 
		Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_INT_ND_PedidosEnc_CatFarmacias' and xType = 'F') 
Begin 
	Alter Table INT_ND_PedidosEnc Add Constraint FK_INT_ND_PedidosEnc___CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_INT_ND_PedidosEnc_CatPersonal' and xType = 'F') 
Begin 
	Alter Table INT_ND_PedidosEnc Add Constraint FK_INT_ND_PedidosEnc___CatPersonal  
		Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
End
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_INT_ND_PedidosEnc_CatProveedores' and xType = 'F') 
Begin 
	Alter Table INT_ND_PedidosEnc Add Constraint FK_INT_ND_PedidosEnc___CatProveedores 
		Foreign Key ( IdProveedor ) References CatProveedores ( IdProveedor ) 
End 
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_ND_PedidosDet' and xType = 'U' ) 
	Drop Table INT_ND_PedidosDet 
Go--#SQL 

Create Table INT_ND_PedidosDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioPedido varchar(30) Not Null, 
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
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_INT_ND_PedidosDet' and xType = 'PK' ) 
Begin 
	Alter Table INT_ND_PedidosDet Add Constraint PK_INT_ND_PedidosDet 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN ) 
End 
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_INT_ND_PedidosDet_INT_ND_PedidosEnc' and xType = 'F' ) 
Begin 
	Alter Table INT_ND_PedidosDet Add Constraint FK_INT_ND_PedidosDet___INT_ND_PedidosEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
		References INT_ND_PedidosEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_INT_ND_PedidosDet_CatProductos_CodigosRelacionados' and xType = 'F' ) 
Begin 
	Alter Table INT_ND_PedidosDet Add Constraint FK_INT_ND_PedidosDet___CatProductos_CodigosRelacionados 
		Foreign Key ( IdProducto, CodigoEAN ) 
		References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
End 
Go--#SQL 

If Not Exists ( Select * From sysobjects so (nolock) Where name = 'CK_INT_ND_PedidosDet_Cantidad' and xType = 'C' ) 
Begin 
	Alter Table INT_ND_PedidosDet With NoCheck 
		Add Constraint CK_INT_ND_PedidosDet_Cantidad Check Not For Replication (CantidadRecibida >= 0)
End 	
Go--#SQL 


------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_ND_PedidosDet_Lotes' and xType = 'U' ) 
	Drop Table INT_ND_PedidosDet_Lotes 
Go--#SQL 

Create Table INT_ND_PedidosDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null, 
	FolioPedido varchar(30) Not Null, 
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
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_INT_ND_PedidosDet_Lotes' and xType = 'PK' ) 
Begin 
	Alter Table INT_ND_PedidosDet_Lotes Add Constraint PK_INT_ND_PedidosDet_Lotes 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote  ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_INT_ND_PedidosDet_Lotes_INT_ND_PedidosDet' and xType = 'F' ) 
Begin 
	Alter Table INT_ND_PedidosDet_Lotes Add Constraint FK_INT_ND_PedidosDet_Lotes___INT_ND_PedidosDet
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN ) 
		References INT_ND_PedidosDet ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_INT_ND_PedidosDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'F' ) 
Begin 
	Alter Table INT_ND_PedidosDet_Lotes Add Constraint FK_INT_ND_PedidosDet_Lotes___FarmaciaProductos_CodigoEAN_Lotes 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
		References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
End 
Go--#SQL 

If Not Exists ( Select * From sysobjects so (nolock) Where name = 'CK_INT_ND_PedidosDet_Lotes_Cantidad' and xType = 'C' ) 
Begin 
	Alter Table INT_ND_PedidosDet_Lotes With NoCheck 
		Add Constraint CK_INT_ND_PedidosDet_Lotes_Cantidad Check Not For Replication (CantidadRecibida >= 0)
End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_ND_PedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
	Drop Table INT_ND_PedidosDet_Lotes_Ubicaciones 
Go--#SQL 

Create Table INT_ND_PedidosDet_Lotes_Ubicaciones 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null, 
	FolioPedido varchar(30) Not Null, 
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
Go--#SQL

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_INT_ND_PedidosDet_Lotes_Ubicaciones' and xType = 'PK' ) 
Begin 
	Alter Table INT_ND_PedidosDet_Lotes_Ubicaciones Add Constraint PK_INT_ND_PedidosDet_Lotes_Ubicaciones 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño  ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_INT_ND_PedidosDet_Lotes_Ubicaciones_INT_ND_PedidosDet_Lotes' and xType = 'F' ) 
Begin 
	Alter Table INT_ND_PedidosDet_Lotes_Ubicaciones Add Constraint FK_INT_ND_PedidosDet_Lotes_Ubicaciones___INT_ND_PedidosDet_Lotes
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote ) 
		References INT_ND_PedidosDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote ) 
End 
Go--#SQL 

If Not Exists ( Select * From sysobjects so (nolock) Where name = 'CK_INT_ND_PedidosDet_Lotes_Ubicaciones_Cantidad' and xType = 'C' ) 
Begin 
	Alter Table INT_ND_PedidosDet_Lotes_Ubicaciones With NoCheck 
		Add Constraint CK_INT_ND_PedidosDet_Lotes_Ubicaciones_Cantidad Check Not For Replication (CantidadRecibida >= 0)
End 
Go--#SQL 

