If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet_LotesRegistrar' and xType = 'U' ) 
	Drop Table TransferenciasEnvioDet_LotesRegistrar 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet_Lotes' and xType = 'U' ) 
	Drop Table TransferenciasEnvioDet_Lotes  
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet' and xType = 'U' ) 
	Drop Table TransferenciasEnvioDet 
Go--#SQL  


If Exists ( Select * From Sysobjects (NoLock) Where Name = 'TransferenciasDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table TransferenciasDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet_Lotes' and xType = 'U' ) 
	Drop Table TransferenciasDet_Lotes  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet' and xType = 'U' ) 
	Drop Table TransferenciasDet 
Go--#SQL  



---------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnc' and xType = 'U' ) 
	Drop Table TransferenciasEnc 
Go--#SQL  

Create Table TransferenciasEnc
(
	IdEmpresa varchar(3) Not Null,  
	IdEstado varchar(2) Not Null,             --- Quien envia la Transferencia 
	IdFarmacia varchar(4) Not Null,	          --- Quien envia la Transferencia 
	IdAlmacen varchar(2) Not Null Default '', --- Quien envia la Transferencia 
	EsTransferenciaAlmacen bit Not Null Default 'false', 
	FolioTransferencia varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	FolioMovtoInv varchar(30) Not Null, 
	FolioMovtoInvRef varchar(30) Not Null Default '', -- Solo para Entradas por Transferencia, 
	FolioTransferenciaRef varchar(30) Not Null Default '', 
	TipoTransferencia varchar(2) Not Null Default 'TS', 
	DestinoEsAlmacen bit Not Null Default 'false', 
	FechaTransferencia datetime Not Null Default getdate(), 
	FechaRegistro datetime Not Null Default getdate(),  
	IdPersonal varchar(4) Not Null, 
	Observaciones varchar(500) Not Null Default '', 

	IdPersonalCancela varchar(4) Not Null Default '',     
	FechaCancelacion datetime Not Null Default getdate(), 

	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0, 
	IdEstadoRecibe varchar(2) Not Null,             --- Quien recibe la Transferencia 
	IdFarmaciaRecibe varchar(4) Not Null,           --- Quien recibe la Transferencia 
	IdAlmacenRecibe varchar(2) Not Null Default '', --- Quien recibe la Transferencia 
	TransferenciaAplicada bit Not Null Default 0, 
	IdPersonalRegistra varchar(4) Not Null Default '', 
	FechaAplicada datetime Not Null Default GetDate(),  	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table TransferenciasEnc Add Constraint PK_TransferenciasEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia ) 
Go--#SQL  

Alter Table TransferenciasEnc Add Constraint FK_TransferenciasEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table TransferenciasEnc Add Constraint FK_TransferenciasEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table TransferenciasEnc Add Constraint FK_TransferenciasEncRecibe_CatFarmacias 
	Foreign Key ( IdEstadoRecibe, IdFarmaciaRecibe ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table TransferenciasEnc Add Constraint FK_TransferenciasEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  
---

---------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet' and xType = 'U' ) 
	Drop Table TransferenciasDet 
Go--#SQL  

Create Table TransferenciasDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioTransferencia varchar(30) Not Null, 
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

Alter Table TransferenciasDet Add Constraint PK_TransferenciasDet 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL 

Alter Table TransferenciasDet Add Constraint FK_TransferenciasDet_TransferenciasEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia ) 
	References TransferenciasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia ) 
Go--#SQL  

Alter Table TransferenciasDet Add Constraint FK_TransferenciasDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL 

---------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasDet_Lotes' and xType = 'U' ) 
	Drop Table TransferenciasDet_Lotes  
Go--#SQL  

Create Table TransferenciasDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmaciaEnvia varchar(2) Not Null, 	
	IdSubFarmaciaRecibe varchar(2) Not Null, 		
	FolioTransferencia varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	Renglon int not null,  
	EsConsignacion bit Not Null Default 'False',   
	CostoUnitario numeric(14,4) Not Null Default 0, 
	Cant_Enviada numeric(14,4) Not Null Default 0, 
	Cant_Devuelta numeric(14,4) Not Null Default 0, 
	CantidadEnviada Numeric(14,4) Not Null Default 0, --- == (Cant_Entregada - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table TransferenciasDet_Lotes Add Constraint PK_TransferenciasDet_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table TransferenciasDet_Lotes Add Constraint FK_TransferenciasDet_Lotes_TransferenciasDet 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdProducto, CodigoEAN, Renglon ) 
	References TransferenciasDet ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL  

Alter Table TransferenciasDet_Lotes Add Constraint FK_TransferenciasDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  


---------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'TransferenciasDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table TransferenciasDet_Lotes_Ubicaciones 
Go--#SQL 

Create Table TransferenciasDet_Lotes_Ubicaciones  
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmaciaEnvia varchar(2) Not Null, 	
	IdSubFarmaciaRecibe varchar(2) Not Null, 		
	FolioTransferencia varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null,  
	Renglon int not null,  
	EsConsignacion bit Not Null Default 'False',  	

	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null,     

	Cant_Enviada numeric(14,4) Not Null Default 0, 
	Cant_Devuelta numeric(14,4) Not Null Default 0, 	
	CantidadEnviada Numeric(14,4) Not Null Default 0, --- == (Cant_Entregada - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table TransferenciasDet_Lotes_Ubicaciones Add Constraint PK_TransferenciasDet_Lotes_Ubicaciones 
	Primary Key (   
	    IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, FolioTransferencia, 
	    IdProducto, CodigoEAN, ClaveLote, Renglon, IdPasillo, IdEstante, IdEntrepaño  
                ) 
Go--#SQL  

Alter Table TransferenciasDet_Lotes_Ubicaciones Add Constraint PK_TransferenciasDet_Lotes_Ubicaciones_TransferenciasDet_Lotes  
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
    References TransferenciasDet_Lotes  
                ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon )   
Go--#SQL  






---------------------------------------------------
/*
	Guardar las Tranferencias de Salida, estas serán enviadas a la Farmacia ó Almacen destino, 
	se envia la información completa de la Transferencia para cargarla directamente en el Destino 
	con la finalidad de evitar la recaptura de información.

	La relacion entre Transferencias y TransferenciasEnvio solamente será logica, por efectos de no
	tener Transferencias que no correspondan a la Farmacia en Base de Datos.
*/
--------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioEnc' and xType = 'U' ) 
	Drop Table TransferenciasEnvioEnc 
Go--#SQL  

Create Table TransferenciasEnvioEnc
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstadoEnvia varchar(2) Not Null,             --- Quien envia la Transferencia 
	IdFarmaciaEnvia varchar(4) Not Null,	       --- Quien envia la Transferencia 
	IdAlmacenEnvia varchar(2) Not Null Default '', --- Quien envia la Transferencia 
	EsTransferenciaAlmacen bit Not Null Default 'false', 
	FolioTransferencia varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	FolioMovtoInv varchar(30) Not Null, 
	-- FolioMovtoInvRef varchar(30) Not Null Default '', -- Solo para Entradas por Transferencia, 
	TipoTransferencia varchar(2) Not Null Default 'E', 
	DestinoEsAlmacen bit Not Null Default 'false', 
	FechaTransferencia datetime Not Null Default getdate(), 
	FechaRegistro datetime Not Null Default getdate(),  
	IdPersonal varchar(4) Not Null, 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0, 
	IdEstadoRecibe varchar(2) Not Null,             --- Quien recibe la Transferencia 
	IdFarmaciaRecibe varchar(4) Not Null,           --- Quien recibe la Transferencia 
	IdAlmacenRecibe varchar(2) Not Null Default '', --- Quien recibe la Transferencia 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table TransferenciasEnvioEnc Add Constraint PK_TransferenciasEnvioEnc 
	Primary Key ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, FolioTransferencia ) 
Go--#SQL  

Alter Table TransferenciasEnvioEnc Add Constraint FK_TransferenciasEnvioEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table TransferenciasEnvioEnc Add Constraint FK_TransferenciasEnvioEnc_CatFarmacias 
	Foreign Key ( IdEstadoEnvia, IdFarmaciaEnvia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet' and xType = 'U' ) 
	Drop Table TransferenciasEnvioDet 
Go--#SQL  

Create Table TransferenciasEnvioDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstadoEnvia varchar(2) Not Null, 
	IdFarmaciaEnvia varchar(4) Not Null,  

	IdEstadoRecibe varchar(2) Not Null,             --- Quien recibe la Transferencia 
	IdFarmaciaRecibe varchar(4) Not Null,           --- Quien recibe la Transferencia 
	
	FolioTransferencia varchar(30) Not Null, 
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

Alter Table TransferenciasEnvioDet Add Constraint PK_TransferenciasEnvioDet 
	Primary Key ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL 

Alter Table TransferenciasEnvioDet Add Constraint FK_TransferenciasEnvioDet_TransferenciasEnc 
	Foreign Key ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, FolioTransferencia ) 
	References TransferenciasEnvioEnc ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, FolioTransferencia ) 
Go--#SQL  

Alter Table TransferenciasEnvioDet Add Constraint FK_TransferenciasEnvioDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet_Lotes' and xType = 'U' ) 
	Drop Table TransferenciasEnvioDet_Lotes  
Go--#SQL  

Create Table TransferenciasEnvioDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstadoEnvia varchar(2) Not Null, 
	IdFarmaciaEnvia varchar(4) Not Null, 
    IdSubFarmaciaEnvia varchar(2) Not Null, 

	IdEstadoRecibe varchar(2) Not Null,             --- Quien recibe la Transferencia 
	IdFarmaciaRecibe varchar(4) Not Null,           --- Quien recibe la Transferencia 
    IdSubFarmaciaRecibe varchar(2) Not Null, 

	FolioTransferencia varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	Renglon int not null,  
	EsConsignacion Bit Not Null Default 'false',  	
    CostoUnitario numeric(14,4) Not Null Default 0,  	
	Cant_Enviada numeric(14,4) Not Null Default 0, 
	Cant_Devuelta numeric(14,4) Not Null Default 0, 		
	CantidadEnviada Numeric(14,4) Not Null Default 0, --- == (Cant_Entregada - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table TransferenciasEnvioDet_Lotes Add Constraint PK_TransferenciasEnvioDet_Lotes 
	Primary Key ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table TransferenciasEnvioDet_Lotes Add Constraint FK_TransferenciasEnvioDet_Lotes_TransferenciasDet 
	Foreign Key ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, Renglon ) 
	References TransferenciasEnvioDet ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL  

/*
Alter Table TransferenciasEnvioDet_Lotes Add Constraint FK_TransferenciasEnvioDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEstadoEnvia, IdFarmaciaEnvia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEstado, IdFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go
*/
 
 
---- Tabla para registrar los Lotes que se envian de una farmacia a otra 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEnvioDet_LotesRegistrar' and xType = 'U' ) 
	Drop Table TransferenciasEnvioDet_LotesRegistrar 
Go--#SQL 

Create Table TransferenciasEnvioDet_LotesRegistrar
(
	IdEmpresa varchar(3) Not Null, 
	IdEstadoEnvia varchar(2) Not Null, 
	IdFarmaciaEnvia varchar(4) Not Null, 
    IdSubFarmaciaEnvia varchar(2) Not Null, 
    	
	IdEstadoRecibe varchar(2) Not Null,             --- Quien recibe la Transferencia 
	IdFarmaciaRecibe varchar(4) Not Null,           --- Quien recibe la Transferencia 	
    IdSubFarmaciaRecibe varchar(2) Not Null, 	
	
	FolioTransferencia varchar(30) Not Null, 	
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	Renglon int not null,  	
	EsConsignacion Bit Not Null Default 'false',  	
	Existencia Numeric(14, 4) Not Null Default 0, 
	CostoUnitario numeric(14,4) Not Null Default 0, 
	FechaCaducidad datetime Not Null Default getdate(), 
	FechaRegistro datetime Not Null Default getdate(),  
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table TransferenciasEnvioDet_LotesRegistrar Add Constraint PK_TransferenciasEnvioDet_LotesRegistrar 
	Primary Key ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table TransferenciasEnvioDet_LotesRegistrar Add Constraint FK_TransferenciasEnvioDet_LotesRegistrar_TransferenciasDet 
	Foreign Key ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
	References TransferenciasEnvioDet_Lotes ( IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

