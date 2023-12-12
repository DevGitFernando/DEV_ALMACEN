If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistDet_Lotes' and xType = 'U' ) 
	Drop Table PedidosDistDet_Lotes  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistDet' and xType = 'U' ) 
	Drop Table PedidosDistDet 
Go--#SQL  

-------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistEnc' and xType = 'U' ) 
	Drop Table PedidosDistEnc 
Go--#SQL  

Create Table PedidosDistEnc
(
	IdEmpresa varchar(3) Not Null,  
	IdEstado varchar(2) Not Null,             --- Quien envia la Salida 
	IdFarmacia varchar(4) Not Null,	          --- Quien envia la Salida 
	IdAlmacen varchar(2) Not Null Default '', --- Quien envia la Salida 
	EsSalidaAlmacen bit Not Null Default 'false', 
	FolioDistribucion varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	FolioMovtoInv varchar(30) Not Null, 
	FolioMovtoInvRef varchar(30) Not Null Default '', -- Solo para Entradas por Transferencia, 
	FolioPedidoRef varchar(30) Not Null Default '', 
	TipoSalida varchar(3) Not Null Default 'SPD', 
	DestinoEsAlmacen bit Not Null Default 'false', 
	FechaSalida datetime Not Null Default getdate(), 
	FechaRegistro datetime Not Null Default getdate(),  
	IdPersonal varchar(4) Not Null, 
	Observaciones varchar(500) Not Null Default '', 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0, 
	IdEstadoRecibe varchar(2) Not Null,             --- Quien recibe la Salida 
	IdFarmaciaRecibe varchar(4) Not Null,           --- Quien recibe la Salida 
	IdAlmacenRecibe varchar(2) Not Null Default '', --- Quien recibe la Salida 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table PedidosDistEnc Add Constraint PK_PedidosDistEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDistribucion ) 
Go--#SQL  

Alter Table PedidosDistEnc Add Constraint FK_PedidosDistEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table PedidosDistEnc Add Constraint FK_PedidosDistEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table PedidosDistEnc Add Constraint FK_PedidosDistEncRecibe_CatFarmacias 
	Foreign Key ( IdEstadoRecibe, IdFarmaciaRecibe ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table PedidosDistEnc Add Constraint FK_PedidosDistEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  
---


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistDet' and xType = 'U' ) 
	Drop Table PedidosDistDet 
Go--#SQL  

Create Table PedidosDistDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioDistribucion varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 	
	CodigoEAN varchar(30) Not Null, 
	Renglon int Not Null,  
	-- EsConsignacion tinyint Not Null Default 0,  	
	UnidadDeEntrada smallint Not Null Default 1, 
	Cant_Enviada Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadEnviada Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	CostoUnitario Numeric(14,4) Not Null Default 0, 	
	TasaIva Numeric(14,4) Not Null Default 0, 
	SubTotal Numeric(14,4) Not Null Default 0, 
	ImpteIva Numeric(14,4) Not Null Default 0, 
	Importe Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table PedidosDistDet Add Constraint PK_PedidosDistDet 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDistribucion, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL 

Alter Table PedidosDistDet Add Constraint FK_PedidosDistDet_PedidosDistEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDistribucion ) 
	References PedidosDistEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioDistribucion ) 
Go--#SQL  

Alter Table PedidosDistDet Add Constraint FK_PedidosDistDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistDet_Lotes' and xType = 'U' ) 
	Drop Table PedidosDistDet_Lotes  
Go--#SQL  

Create Table PedidosDistDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmaciaEnvia varchar(2) Not Null, 	
	IdSubFarmaciaRecibe varchar(2) Not Null, 		
	FolioDistribucion varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	Renglon int not null,  
	EsConsignacion bit Not Null Default 'False',  	
	CantidadEnviada Numeric(14,4) Not Null Default 0, --- == (Cant_Entregada - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table PedidosDistDet_Lotes Add Constraint PK_PedidosDistDet_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, FolioDistribucion, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table PedidosDistDet_Lotes Add Constraint FK_PedidosDistDet_Lotes_PedidosDistDet 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDistribucion, IdProducto, CodigoEAN, Renglon ) 
	References PedidosDistDet ( IdEmpresa, IdEstado, IdFarmacia, FolioDistribucion, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL  

Alter Table PedidosDistDet_Lotes Add Constraint FK_PedidosDistDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  


---------- Tabla para registrar los Lotes que se envian de una farmacia a otra 
------If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PedidosDistEnvioDet_LotesRegistrar' and xType = 'U' ) 
------	Drop Table PedidosDistEnvioDet_LotesRegistrar 
------Go--#SQL 
------
------Create Table PedidosDistEnvioDet_LotesRegistrar
------(
------	IdEmpresa varchar(3) Not Null, 
------	IdEstadoEnvia varchar(2) Not Null, 
------	IdFarmaciaEnvia varchar(4) Not Null, 
------    IdSubFarmaciaEnvia varchar(2) Not Null, 
------    	
------	IdEstadoRecibe varchar(2) Not Null,             --- Quien recibe la Transferencia 
------	IdFarmaciaRecibe varchar(4) Not Null,           --- Quien recibe la Transferencia 	
------    IdSubFarmaciaRecibe varchar(2) Not Null, 	
------	
------	FolioDistribucion varchar(30) Not Null, 	
------	IdProducto varchar(8) Not Null, 
------	CodigoEAN varchar(30) Not Null, 
------	ClaveLote varchar(30) Not Null, 
------	Renglon int not null,  	
------	EsConsignacion Bit Not Null Default 'false',  	
------	Existencia Numeric(14, 4) Not Null Default 0, 
------	FechaCaducidad datetime Not Null Default getdate(), 
------	FechaRegistro datetime Not Null Default getdate(),  
------	Status varchar(1) Not Null Default 'A', 
------	Actualizado tinyint Not Null Default 0 
------)
------Go--#SQL  
------
------Alter Table PedidosDistEnvioDet_LotesRegistrar Add Constraint PK_PedidosDistEnvioDet_LotesRegistrar 
------	Primary Key ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, FolioDistribucion, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
------Go--#SQL  
------
------Alter Table PedidosDistEnvioDet_LotesRegistrar Add Constraint FK_PedidosDistEnvioDet_LotesRegistrar_PedidosDistDet 
------	Foreign Key ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, FolioDistribucion, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
------	References PedidosDistEnvioDet_Lotes ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, FolioDistribucion, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
------Go--#SQL  
