If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
	Drop Table PreSalidasPedidosDet_Lotes_Ubicaciones  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosDet' and xType = 'U' ) 
	Drop Table PreSalidasPedidosDet 
Go--#SQL 

--------------------------------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosEnc' and xType = 'U' ) 
	Drop Table PreSalidasPedidosEnc 
Go--#SQL 

Create Table PreSalidasPedidosEnc
(
	IdEmpresa varchar(3) Not Null,  
	IdEstado varchar(2) Not Null,             
	IdFarmacia varchar(4) Not Null,  
	FolioPreSalida varchar(30) Not Null,
	IdSubFarmacia varchar(2) Not Null,	
	FechaPreSalida datetime Not Null Default getdate(), 
	FechaRegistro datetime Not Null Default getdate(),  
	IdPersonal varchar(4) Not Null, 
	Observaciones varchar(500) Not Null Default '',	 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table PreSalidasPedidosEnc Add Constraint PK_PreSalidasPedidosEnc 
    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPreSalida ) 
Go--#SQL 

Alter Table PreSalidasPedidosEnc Add Constraint FK_PreSalidasPedidosEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL

Alter Table PreSalidasPedidosEnc Add Constraint FK_PreSalidasPedidosEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL

Alter Table PreSalidasPedidosEnc Add Constraint FK_PreSalidasPedidosEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 
---

------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosDet' and xType = 'U' ) 
	Drop Table PreSalidasPedidosDet 
Go--#SQL 

Create Table PreSalidasPedidosDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioPreSalida varchar(30) Not Null,	 
	IdClaveSSA varchar(4) Not Null, 
	CantidadRequerida Numeric(14,4) Not Null Default 0,
	CantidadAsignada Numeric(14,4) Not Null Default 0,		 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL

Alter Table PreSalidasPedidosDet Add Constraint PK_PreSalidasPedidosDet 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPreSalida, IdClaveSSA ) 
Go--#SQL

Alter Table PreSalidasPedidosDet Add Constraint FK_PreSalidasPedidosDet_PreSalidasPedidosEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPreSalida ) 
	References PreSalidasPedidosEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioPreSalida ) 
Go--#SQL 

Alter Table PreSalidasPedidosDet Add Constraint FK_PreSalidasPedidosDet_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) 
	References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL

----------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PreSalidasPedidosDet_Lotes_Ubicaciones' and xType = 'U' ) 
	Drop Table PreSalidasPedidosDet_Lotes_Ubicaciones  
Go--#SQL 

Create Table PreSalidasPedidosDet_Lotes_Ubicaciones 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,		 
	FolioPreSalida varchar(30) Not Null,
	IdClaveSSA varchar(4) Not Null,
	IdPasillo int Not Null,
	IdEstante int Not Null,
	IdEntrepaño int Not Null,
	IdSubFarmacia varchar(2) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null,
	--FechaCaducidad datetime Not Null Default getdate(), 
	--EsConsignacion tinyint Not Null Default 0,  	
	Cantidad Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0	 
)
Go--#SQL

Alter Table PreSalidasPedidosDet_Lotes_Ubicaciones Add Constraint PK_PreSalidasPedidosDet_Lotes_Ubicaciones 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia,  FolioPreSalida, IdClaveSSA, 
	IdPasillo, IdEstante, IdEntrepaño, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL 

Alter Table PreSalidasPedidosDet_Lotes_Ubicaciones Add Constraint FK_PreSalidasPedidosDet_Lotes_Ubicaciones_PreSalidasPedidosDet 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPreSalida, IdClaveSSA ) 
	References PreSalidasPedidosDet ( IdEmpresa, IdEstado, IdFarmacia, FolioPreSalida, IdClaveSSA ) 
Go--#SQL 

/*
Alter Table PreSalidasPedidosDet_Lotes_Ubicaciones Add Constraint FK_PreSalidasPedidosDet_Lotes_Ubicaciones_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go 
*/

---------------------------------------------------
