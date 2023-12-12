If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet_Lotes' and xType = 'U' ) 
   Drop Table DevolucionesDet_Lotes 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet' and xType = 'U' ) 
   Drop Table DevolucionesDet 
Go--#SQL 


------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesEnc' and xType = 'U' ) 
   Drop Table DevolucionesEnc 
Go--#SQL  

Create Table DevolucionesEnc 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioDevolucion varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	TipoDeDevolucion tinyint Not Null Default 0, 
	Referencia varchar(50) Not Null Default '', 
	FolioMovtoInv varchar(30) Not Null, 
	FechaSistema datetime Default getdate(),     --- Fecha de Sistema en que se realizo el movimiento 
	FechaSistemaDevol datetime Default getdate(),     --- Fecha de Sistema en que se realizo el movimiento 	
	FechaRegistro datetime Default getdate(), 	
	IdPersonal varchar(4) Not Null, 
	Observaciones varchar(200) Not Null Default '', 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0, 
	Corte tinyint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 	
)
Go--#SQL  

Alter Table DevolucionesEnc Add Constraint PK_DevolucionesEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion ) 
Go--#SQL  

Alter Table DevolucionesEnc Add Constraint FK_DevolucionesEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 	

Alter Table DevolucionesEnc Add Constraint FK_DevolucionesEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 	

Alter Table DevolucionesEnc Add Constraint FK_DevolucionesEnc_CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 	


-------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet' and xType = 'U' ) 
   Drop Table DevolucionesDet 
Go--#SQL 

Create Table DevolucionesDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 	
	FolioDevolucion varchar(30) Not Null, 	
	-- EsConsignacion tinyint Not Null Default 0,  	
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	UnidadDeEntrada smallint Not Null Default 1, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	PrecioCosto_Unitario Numeric(14,4) Not Null Default 0, 
	TasaIva Numeric(14,4) Not Null Default 0, 
	ImpteIva Numeric(14,4) Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table DevolucionesDet Add Constraint PK_DevolucionesDet Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, IdProducto, CodigoEAN ) 
Go--#SQL  

Alter Table DevolucionesDet Add Constraint FK_DevolucionesDet_DevolucionesEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion ) References DevolucionesEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion ) 
Go--#SQL  

Alter Table DevolucionesDet Add Constraint FK_DevolucionesDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL 


-------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'DevolucionesDet_Lotes' and xType = 'U' ) 
   Drop Table DevolucionesDet_Lotes 
Go--#SQL 

Create Table DevolucionesDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	FolioDevolucion varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	EsConsignacion bit Not Null Default 'false',  	
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL  

Alter Table DevolucionesDet_Lotes Add Constraint PK_DevolucionesDet_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  

Alter Table DevolucionesDet_Lotes Add Constraint FK_DevolucionesDet_Lotes_DevolucionesDet 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, IdProducto, CodigoEAN ) 
	References DevolucionesDet ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, IdProducto, CodigoEAN ) 
Go--#SQL  

Alter Table DevolucionesDet_Lotes Add Constraint FK_DevolucionesDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  


------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'DevolucionesDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table DevolucionesDet_Lotes_Ubicaciones 
Go--#SQL 

Create Table DevolucionesDet_Lotes_Ubicaciones 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	FolioDevolucion varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null,
	
	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null,
	 
	EsConsignacion bit Not Null Default 'false',  	
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL  

Alter Table DevolucionesDet_Lotes_Ubicaciones Add Constraint PK_DevolucionesDet_Lotes_Ubicaciones 
    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL  

Alter Table DevolucionesDet_Lotes_Ubicaciones Add Constraint FK_DevolucionesDet_Lotes_Ubicaciones_DevolucionesDet_Lotes
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, IdProducto, CodigoEAN, ClaveLote ) 
    References DevolucionesDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioDevolucion, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL 
