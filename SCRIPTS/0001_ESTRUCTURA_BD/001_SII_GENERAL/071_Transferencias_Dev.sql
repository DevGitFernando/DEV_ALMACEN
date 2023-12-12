If Exists ( Select * From Sysobjects (NoLock) Where Name = 'DevolucionTransferenciasDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table DevolucionTransferenciasDet_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionTransferenciasDet_Lotes' and xType = 'U' ) 
	Drop Table DevolucionTransferenciasDet_Lotes  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionTransferenciasDet' and xType = 'U' ) 
	Drop Table DevolucionTransferenciasDet 
Go--#SQL  



---------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionTransferenciasEnc' and xType = 'U' ) 
	Drop Table DevolucionTransferenciasEnc 
Go--#SQL  

Create Table DevolucionTransferenciasEnc
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,             --- Quien envia la Transferencia 
	IdFarmacia varchar(4) Not Null,	          --- Quien envia la Transferencia 
	IdAlmacen varchar(2) Not Null Default '', --- Quien envia la Transferencia 
	EsTransferenciaAlmacen bit Not Null Default 'false',
	FolioDevolucion varchar(30) Not Null,
	FolioTransferencia varchar(30) Not Null,
	FolioMovtoInv varchar(30) Not Null,
	FolioMovtoInvRef varchar(30) Not Null Default '', -- Solo para Entradas por Transferencia, 
	FolioTransferenciaRef varchar(30) Not Null Default '',
	TipoTransferencia varchar(6) Not Null Default 'DT', 
	DestinoEsAlmacen bit Not Null Default 'false',
	FechaTransferencia datetime Not Null Default getdate(),
	FechaRegistro datetime Not Null Default getdate(),
	IdPersonal varchar(4) Not Null,
	Observaciones varchar(500) Not Null Default '', 
	IdMotivo varchar(2) Not Null Default '', 
	SubTotal Numeric(14,4) Not Null Default 0,
	Iva Numeric(14,4) Not Null Default 0,
	Total Numeric(14,4) Not Null Default 0,
	--IdEstadoRecibe varchar(2) Not Null Default '',             --- Quien recibe la Transferencia 
	--IdFarmaciaRecibe varchar(4) Not Null Default '',           --- Quien recibe la Transferencia 
	--IdAlmacenRecibe varchar(2) Not Null Default '', --- Quien recibe la Transferencia 
	--TransferenciaAplicada bit Not Null Default 0,
	--IdPersonalRegistra varchar(4) Not Null Default '',
	----FechaAplicada datetime Not Null Default GetDate(),
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table DevolucionTransferenciasEnc Add Constraint PK_DevolucionTransferenciasEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion ) 
Go--#SQL  

Alter Table DevolucionTransferenciasEnc Add Constraint FK_DevolucionTransferenciasEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table DevolucionTransferenciasEnc Add Constraint FK_DevolucionTransferenciasEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

--Alter Table DevolucionTransferenciasEnc Add Constraint FK_DevolucionTransferenciasEncRecibe_CatFarmacias 
--	Foreign Key ( IdEstadoRecibe, IdFarmaciaRecibe ) References CatFarmacias ( IdEstado, IdFarmacia ) 
--Go--#-SQL 

Alter Table DevolucionTransferenciasEnc Add Constraint FK_DevolucionTransferenciasEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL

Alter Table DevolucionTransferenciasEnc Add Constraint FK_DevolucionTransferenciasEnc_TransferenciasEnc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia ) References TransferenciasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia ) 
Go--#SQL 

Alter Table DevolucionTransferenciasEnc Add Constraint FK_DevolucionTransferenciasEnc___CatMotivos_Dev_Transferencia  
	Foreign Key ( IdMotivo ) References CatMotivos_Dev_Transferencia ( IdMotivo ) 
Go--#SQL 


---------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionTransferenciasDet' and xType = 'U' ) 
	Drop Table DevolucionTransferenciasDet 
Go--#SQL  

Create Table DevolucionTransferenciasDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioDevolucion varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 	
	CodigoEAN varchar(30) Not Null, 
	Renglon int Not Null,  
	UnidadDeEntrada smallint Not Null Default 1, 
	Cant_Enviada Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0,
	CantidadEnviada Numeric(14,4) Not Null Default 0,
	CostoUnitario Numeric(14,4) Not Null Default 0, 	
	TasaIva Numeric(14,4) Not Null Default 0, 
	SubTotal Numeric(14,4) Not Null Default 0, 
	ImpteIva Numeric(14,4) Not Null Default 0, 
	Importe Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table DevolucionTransferenciasDet Add Constraint PK_DevolucionTransferenciasDet
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL 

Alter Table DevolucionTransferenciasDet Add Constraint FK_DevolucionTransferenciasDet_DevolucionTransferenciasEnc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion ) 
	References DevolucionTransferenciasEnc 
Go--#SQL  

Alter Table DevolucionTransferenciasDet Add Constraint FK_DevolucionTransferenciasDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL 

------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionTransferenciasDet_Lotes' and xType = 'U' ) 
	Drop Table DevolucionTransferenciasDet_Lotes  
Go--#SQL  

Create Table DevolucionTransferenciasDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	FolioDevolucion varchar(30) Not Null,
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null,
	IdSubFarmacia varchar(2) Not Null,
	ClaveLote varchar(30) Not Null, 
	Renglon int not null,  
	EsConsignacion bit Not Null Default 'False',
	Cant_Enviada Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0,
	CantidadEnviada Numeric(14,4) Not Null Default 0, --- == (Cant_Entregada - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table DevolucionTransferenciasDet_Lotes Add Constraint PK_DevolucionTransferenciasDet_Lotes
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table DevolucionTransferenciasDet_Lotes Add Constraint FK_DevolucionTransferenciasDet_Lotes_DevolucionTransferenciasDet 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, IdProducto, CodigoEAN, Renglon ) 
	References DevolucionTransferenciasDet
Go--#SQL  

Alter Table DevolucionTransferenciasDet_Lotes Add Constraint FK_DevolucionTransferenciasDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  


---------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'DevolucionTransferenciasDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table DevolucionTransferenciasDet_Lotes_Ubicaciones 
Go--#SQL 

Create Table DevolucionTransferenciasDet_Lotes_Ubicaciones  
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null,
	FolioDevolucion varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null,  
	Renglon int not null,  
	EsConsignacion bit Not Null Default 'False',  	

	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null,     
	
	Cant_Enviada Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0,
	CantidadEnviada Numeric(14,4) Not Null Default 0, --- == (Cant_Entregada - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table DevolucionTransferenciasDet_Lotes_Ubicaciones Add Constraint PK_DevolucionTransferenciasDet_Lotes_Ubicaciones
	Primary Key (   
	    IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, 
	    IdProducto, CodigoEAN, ClaveLote, Renglon, IdPasillo, IdEstante, IdEntrepaño  
                ) 
Go--#SQL  

Alter Table DevolucionTransferenciasDet_Lotes_Ubicaciones Add Constraint PK_DevolucionTransferenciasDet_Lotes_Ubicaciones_DevolucionTransferenciasDet_Lotes
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
    References DevolucionTransferenciasDet_Lotes  
                ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, IdProducto, CodigoEAN, ClaveLote, Renglon )   
Go--#SQL 

