If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table AjustesInv_Det_Lotes_Ubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes' and xType = 'U' ) 
	Drop Table AjustesInv_Det_Lotes 
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det' and xType = 'U' ) 
	Drop Table AjustesInv_Det 
Go--#SQL

--------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Enc' and xType = 'U' ) 
	Drop Table AjustesInv_Enc 
Go--#SQL

Create Table AjustesInv_Enc 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	Poliza varchar(8) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	FechaSistema datetime Default GetDate(),     --- Fecha de Sistema en que se realizo el movimiento  	
	FechaRegistro datetime Default GetDate(),  --- Fecha en que se registro movimiento 
	IdPersonalRegistra varchar(6) Not Null Default '', 
	Observaciones varchar(500) Not Null Default '', 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0, 
	PolizaAplicada varchar(1) Not Null Default 'N', 
	MovtoAplicado varchar(1) Not Null Default 'N',  
	Status varchar(1) Not Null Default 'A', 
	
	FolioVentaEntrada varchar(20) Not Null Default '',  
	FolioVentaSalida varchar(20) Not Null Default '',  	
	FolioConsignacionEntrada varchar(20) Not Null Default '',  
	FolioConsignacionSalida varchar(20) Not Null Default '',  		
	
	Keyx int identity(1,1), 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL

Alter Table AjustesInv_Enc Add Constraint PK_AjustesInv_Enc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Poliza  )
Go--#SQL

Alter Table AjustesInv_Enc Add Constraint FK_AjustesInv_Enc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL

Alter Table AjustesInv_Enc Add Constraint FK_AjustesInv_Enc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL

--------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det' and xType = 'U' ) 
	Drop Table AjustesInv_Det 
Go--#SQL

Create Table AjustesInv_Det 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	Poliza varchar(8) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null,
	-- EsConsignacion tinyint Not Null Default 0, 
	UnidadDeSalida smallint Not Null Default 1, 	
	TasaIva Numeric(14,4) Not Null Default 0, 
	--Cantidad Numeric(14,4) Not Null Default 0, 
	ExistenciaSistema Int Not Null Default 0,
	Costo Numeric(14,4) Not Null Default 0, 
	Importe Numeric(14,4) Not Null Default 0,   
	ExistenciaFisica Int Not Null Default 0, 
	Diferencia Int Not Null Default 0,  
	--IdTipoMovto_Inv varchar(3) Not Null, 
	--TipoES varchar(1) Not Null, 
	--Referencia varchar(30) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Keyx int identity(1,1), 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL

Alter Table AjustesInv_Det Add Constraint PK_AjustesInv_Det 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN ) 
Go--#SQL

Alter Table AjustesInv_Det Add Constraint FK_AjustesInv_Det_AjustesInv_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Poliza ) References AjustesInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, Poliza ) 
Go--#SQL

Alter Table AjustesInv_Det Add Constraint FK_AjustesInv_Det_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN )  
Go--#SQL  

--Alter Table AjustesInv_Det Add Constraint FK_AjustesInv_Det_Movtos_Inv_Tipos_Farmacia 
--	Foreign Key ( IdEstado, IdFarmacia, IdTipoMovto_Inv ) References Movtos_Inv_Tipos_Farmacia ( IdEstado, IdFarmacia, IdTipoMovto_Inv ) 
--Go--#SQL 

--Alter Table AjustesInv_Det Add Constraint FK_AjustesInv_Det_MovtosInv_Enc
--	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Referencia ) References MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 
--Go--#SQL 

--Alter Table AjustesInv_Det With NoCheck 
--	Add Constraint CK_AjustesInv_Det_Cantidad Check Not For Replication (Cantidad > 0)
--Go--#SQL

--------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes' and xType = 'U' ) 
	Drop Table AjustesInv_Det_Lotes 
Go--#SQL

Create Table AjustesInv_Det_Lotes 
(
 	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	IdSubFarmacia varchar(2) Not Null,	
	Poliza varchar(8) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	EsConsignacion bit Not Null Default 'false',  	
	--Cantidad Numeric(14,4) Not Null Default 0, 
	ExistenciaSistema Int Not Null Default 0,  
	Costo Numeric(14,4) Not Null Default 0, 
	Importe Numeric(14,4) Not Null Default 0,
	ExistenciaFisica Int Not Null Default 0, 
	Diferencia Int Not Null Default 0, 
	--IdTipoMovto_Inv varchar(3) Not Null, 
	--TipoES varchar(1) Not Null, 
	Referencia varchar(30) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Keyx int identity(1,1), 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL

Alter Table AjustesInv_Det_Lotes Add Constraint PK_AjustesInv_Det_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL

Alter Table AjustesInv_Det_Lotes Add Constraint FK_AjustesInv_Det_Lotes_AjustesInv_Det
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN ) 
	References AjustesInv_Det ( IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN ) 
Go--#SQL

Alter Table AjustesInv_Det_Lotes Add Constraint FK_AjustesInv_Det_Lotes_CatFarmacias_SubFarmacias 
	Foreign Key ( IdEstado, IdFarmacia, IdSubFarmacia ) References CatFarmacias_SubFarmacias ( IdEstado, IdFarmacia, IdSubFarmacia ) 
Go--#SQL


--Alter Table AjustesInv_Det_Lotes Add Constraint FK_AjustesInv_Det_Lotes_Movtos_Inv_Tipos_Farmacia 
--	Foreign Key ( IdEstado, IdFarmacia, IdTipoMovto_Inv ) References Movtos_Inv_Tipos_Farmacia ( IdEstado, IdFarmacia, IdTipoMovto_Inv ) 
--Go--#SQL 

--Alter Table AjustesInv_Det_Lotes Add Constraint FK_AjustesInv_Det_Lotes_MovtosInv_Enc
--	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Referencia ) References MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 
--Go--#SQL 

--Alter Table AjustesInv_Det_Lotes Add Constraint FK_AjustesInv_Det_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
--	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
--	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
--Go--#SQL 

--Alter Table AjustesInv_Det_Lotes With NoCheck 
--	Add Constraint CK_AjustesInv_Det_Lotes_Cantidad Check Not For Replication (Cantidad > 0)
--Go--#SQL


-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AjustesInv_Det_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table AjustesInv_Det_Lotes_Ubicaciones 
Go--#SQL 

Create Table AjustesInv_Det_Lotes_Ubicaciones 
(
 	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	IdSubFarmacia varchar(2) Not Null,	
	Poliza varchar(8) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	EsConsignacion bit Not Null Default 'false',  
	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null,	
	--Cantidad Numeric(14,4) Not Null Default 0, 
	ExistenciaSistema Int Not Null Default 0,  
	Costo Numeric(14,4) Not Null Default 0, 
	Importe Numeric(14,4) Not Null Default 0,
	ExistenciaFisica Int Not Null Default 0, 
	Diferencia Int Not Null Default 0, 
	--IdTipoMovto_Inv varchar(3) Not Null, 
	--TipoES varchar(1) Not Null, 
	Referencia varchar(30) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Keyx int identity(1,1), 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL

Alter Table AjustesInv_Det_Lotes_Ubicaciones Add Constraint PK_AjustesInv_Det_Lotes_Ubicaciones 
    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL

Alter Table AjustesInv_Det_Lotes_Ubicaciones Add Constraint FK_AjustesInv_Det_Lotes_Ubicaciones_AjustesInv_Det_Lotes
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote ) 
    References AjustesInv_Det_Lotes ( IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL

Alter Table AjustesInv_Det_Lotes_Ubicaciones Add Constraint FK_AjustesInv_Det_Lotes_Ubicaciones_AjustesInv_Det_Lotes_Ubicaciones
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN ) 
    References AjustesInv_Det ( IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN ) 
Go--#SQL

Alter Table AjustesInv_Det_Lotes_Ubicaciones Add Constraint FK_AjustesInv_Det_Lotes_Ubicaciones_CatFarmacias_SubFarmacias 
    Foreign Key ( IdEstado, IdFarmacia, IdSubFarmacia ) References CatFarmacias_SubFarmacias ( IdEstado, IdFarmacia, IdSubFarmacia ) 
Go--#SQL

Alter Table AjustesInv_Det_Lotes_Ubicaciones Add Constraint FK_AjustesInv_Det_Lotes_Ubicaciones_CatPasillos_Estantes_Entrepaños
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 
    References CatPasillos_Estantes_Entrepaños( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL
