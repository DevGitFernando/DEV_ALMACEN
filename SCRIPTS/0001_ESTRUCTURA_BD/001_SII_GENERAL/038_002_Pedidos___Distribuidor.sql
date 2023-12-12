If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table PedidosDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet_Lotes' and xType = 'U' ) 
	Drop Table PedidosDet_Lotes  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet' and xType = 'U' ) 
	Drop Table PedidosDet 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnc' and xType = 'U' ) 
	Drop Table PedidosEnc 
Go--#SQL 


--------------- Envio 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet_Lotes_Registrar' and xType = 'U' ) 
	Drop Table PedidosEnvioDet_Lotes_Registrar  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet_Lotes' and xType = 'U' ) 
	Drop Table PedidosEnvioDet_Lotes  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet' and xType = 'U' ) 
	Drop Table PedidosEnvioDet 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioEnc' and xType = 'U' ) 
	Drop Table PedidosEnvioEnc 
Go--#SQL 


/* 
---------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatDistribuidores' and xType = 'U' ) 
	Drop Table CatDistribuidores  
Go--#SxQL  

Create Table CatDistribuidores
( 
	IdDistribuidor varchar(4) Not Null, 
	NombreDistribuidor varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#xSQL  

Alter Table CatDistribuidores Add Constraint PK_CatDistribuidores Primary Key ( IdDistribuidor ) 
Go--#SxQL  
*/ 

------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnc' and xType = 'U' ) 
	Drop Table PedidosEnc 
Go--#SQL  

Create Table PedidosEnc
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioPedido varchar(30) Not Null, 
	TipoDeEntrada varchar(6) Not Null Default '',  
	EsConsignacion bit Not Null Default 'false',  	
	FolioMovtoInv varchar(30) Not Null, 
	IdPersonal varchar(4) Not Null, 

	-- FechaSistema datetime Default GetDate(),     --- Fecha de Sistema en que se realizo el movimiento 
	FechaRegistro datetime Not Null Default getdate(),  --- Fecha de Sistema en que se realizo el movimiento 	
	IdDistribuidor varchar(4) Not Null Default '', 
	ReferenciaPedido varchar(20) Not Null Default '', 
	Observaciones varchar(100) Not Null Default '', 
	
	-- Descuento Numeric(14,4) Not Null Default 0, 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0,
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  


Alter Table PedidosEnc Add Constraint PK_PedidosEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL  

Alter Table PedidosEnc Add Constraint FK_PedidosEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table PedidosEnc Add Constraint FK_PedidosEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

--Alter Table PedidosEnc Add Constraint FK_PedidosEnc_CatCajeros  
--	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatCajeros ( IdEstado, IdFarmacia, IdPersonal ) 
--Go 
Alter Table PedidosEnc Add Constraint FK_PedidosEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 


Alter Table PedidosEnc Add Constraint FK_PedidosEnc_CatDistribuidores 
	Foreign Key ( IdDistribuidor ) References CatDistribuidores ( IdDistribuidor ) 
Go--#SQL  

------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet' and xType = 'U' ) 
	Drop Table PedidosDet 
Go--#SQL  

Create Table PedidosDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioPedido varchar(30) Not Null, 
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

Alter Table PedidosDet Add Constraint PK_PedidosDet Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL 

Alter Table PedidosDet Add Constraint FK_PedidosDet_PedidosEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) References PedidosEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL  

Alter Table PedidosDet Add Constraint FK_PedidosDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL  

------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet_Lotes' and xType = 'U' ) 
	Drop Table PedidosDet_Lotes  
Go--#SQL  

Create Table PedidosDet_Lotes 
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
	EsConsignacion Bit Not Null Default 'false',  	
	Cant_Recibida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table PedidosDet_Lotes Add Constraint PK_PedidosDet_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table PedidosDet_Lotes Add Constraint FK_PedidosDet_Lotes_PedidosDet
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN, Renglon ) 
	References PedidosDet ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL  

Alter Table PedidosDet_Lotes Add Constraint FK_PedidosDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  

------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table PedidosDet_Lotes_Ubicaciones 
Go--#SQL 

Create Table PedidosDet_Lotes_Ubicaciones 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	FolioPedido varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null,
	
	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null,
 
	Renglon int not null,  
	EsConsignacion Bit Not Null Default 'false',  	
	Cant_Recibida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table PedidosDet_Lotes_Ubicaciones Add Constraint PK_PedidosDet_Lotes_Ubicaciones
    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote, Renglon, 
                  IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL  

Alter Table PedidosDet_Lotes_Ubicaciones Add Constraint FK_PedidosDet_Lotes_Ubicaciones_PedidosDet_Lotes
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote, Renglon 
                ) 
    References PedidosDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote, Renglon 
                ) 
Go--#SQL 



--------------------------------------------------- PEDIDO_ENVIO 
--------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioEnc' and xType = 'U' ) 
	Drop Table PedidosEnvioEnc 
Go--#SQL  

Create Table PedidosEnvioEnc
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioPedido varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	FolioMovtoInv varchar(30) Not Null, 
	IdPersonal varchar(4) Not Null, 

	-- FechaSistema datetime Default GetDate(),     --- Fecha de Sistema en que se realizo el movimiento 
	FechaRegistro datetime Not Null Default getdate(),  --- Fecha de Sistema en que se realizo el movimiento 	
	IdDistribuidor varchar(4) Not Null Default '', 
	ReferenciaPedido varchar(20) Not Null Default '', 
	Observaciones varchar(100) Not Null Default '', 
	
	-- Descuento Numeric(14,4) Not Null Default 0, 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0,
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  


Alter Table PedidosEnvioEnc Add Constraint PK_PedidosEnvioEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL  

Alter Table PedidosEnvioEnc Add Constraint FK_PedidosEnvioEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table PedidosEnvioEnc Add Constraint FK_PedidosEnvioEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

--Alter Table PedidosEnc Add Constraint FK_PedidosEnc_CatCajeros  
--	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatCajeros ( IdEstado, IdFarmacia, IdPersonal ) 
--Go 
Alter Table PedidosEnvioEnc Add Constraint FK_PedidosEnvioEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 


Alter Table PedidosEnvioEnc Add Constraint FK_PedidosEnvioEnc_CatDistribuidores 
	Foreign Key ( IdDistribuidor ) References CatDistribuidores ( IdDistribuidor ) 
Go--#SQL  


------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet' and xType = 'U' ) 
	Drop Table PedidosEnvioDet 
Go--#SQL  

Create Table PedidosEnvioDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioPedido varchar(30) Not Null, 
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

Alter Table PedidosEnvioDet Add Constraint PK_PedidosEnvioDet Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL 

Alter Table PedidosEnvioDet Add Constraint FK_PedidosEnvioDet_PedidosEnvioEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) References PedidosEnvioEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL  

Alter Table PedidosEnvioDet Add Constraint FK_PedidosEnvioDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL  


------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet_Lotes' and xType = 'U' ) 
	Drop Table PedidosEnvioDet_Lotes  
Go--#SQL  

Create Table PedidosEnvioDet_Lotes 
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
	EsConsignacion Bit Not Null Default 'false',  	
	Cant_Recibida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	CantidadRegistrada Numeric(14,4) Not Null Default 0,	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table PedidosEnvioDet_Lotes Add Constraint PK_PedidosEnvioDet_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table PedidosEnvioDet_Lotes Add Constraint FK_PedidosEnvioDet_Lotes_PedidosEnvioDet 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN, Renglon ) 
	References PedidosEnvioDet ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL  


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosEnvioDet_Lotes_Registrar' and xType = 'U' ) 
	Drop Table PedidosEnvioDet_Lotes_Registrar  
Go--#SQL  

Create Table PedidosEnvioDet_Lotes_Registrar 
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
	EsConsignacion Bit Not Null Default 'false',  	
	Existencia Numeric(14, 4) Not Null Default 0,
	CantidadRegistrada Numeric(14,4) Not Null Default 0, 
	FechaCaducidad datetime Not Null Default getdate(), 
	FechaRegistro datetime Not Null Default getdate(),	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table PedidosEnvioDet_Lotes_Registrar Add Constraint PK_PedidosEnvioDet_Lotes_Registrar 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table PedidosEnvioDet_Lotes_Registrar Add Constraint FK_PedidosEnvioDet_Lotes_Registrar_PedidosEnvioDet_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
	References PedidosEnvioDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioPedido, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL