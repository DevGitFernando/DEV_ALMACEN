-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDistEnc' and xType = 'U' ) 
   Drop Table RemisionesDistEnc 
Go--#SQL 
   
Create Table RemisionesDistEnc
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioRemision varchar(30) Not Null, 
	IdDistribuidor varchar(4) Not Null Default '', 
	ReferenciaPedido varchar(20) Not Null Default '', 
	CodigoCliente varchar(20) Not Null, 
	FechaDocumento datetime Not Null Default getdate(), 
	Observaciones varchar(100) Not Null Default '', 	
	IdPersonal varchar(4) Not Null, 
	FechaRegistro datetime Not Null Default getdate(),
	EsConsignacion bit Not Null Default 0,
	FolioCargaMasiva int Not Null Default 0,
	EsExcepcion bit Not Null Default 0,
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table RemisionesDistEnc Add Constraint PK_RemisionesDistEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision ) 
Go--#SQL 

Alter Table RemisionesDistEnc Add Constraint FK_RemisionesDistEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table RemisionesDistEnc Add Constraint FK_RemisionesDistEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table RemisionesDistEnc Add Constraint FK_RemisionesDistEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 

Alter Table RemisionesDistEnc Add Constraint FK_RemisionesDistEnc_CatDistribuidores_Clientes 
	Foreign Key ( IdEstado, IdDistribuidor, CodigoCliente ) 
	References CatDistribuidores_Clientes ( IdEstado, IdDistribuidor, CodigoCliente ) 
Go--#SQL 


------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'RemisionesDistDet' and xType = 'U' ) 
   Drop Table RemisionesDistDet 
Go--#SQL  
  
Create Table RemisionesDistDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,  
	FolioRemision varchar(30) Not Null, 
	IdClaveSSA varchar(4) Not Null, 	
	-- Renglon int Not Null,  
	-- EsConsignacion tinyint Not Null Default 0,
	Precio numeric(14, 4) Not Null Default 0,	
	Cant_Recibida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table RemisionesDistDet Add Constraint PK_RemisionesDistDet Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision, IdClaveSSA ) 
Go--#SQL 

Alter Table RemisionesDistDet Add Constraint FK_RemisionesDistDet_RemisionesDistEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision ) References RemisionesDistEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision ) 
Go--#SQL 

Alter Table RemisionesDistDet Add Constraint FK_RemisionesDistDet_CatClavesSSA_Sales  
	Foreign Key ( IdClaveSSA ) 
	References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL  



------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Remisiones_CargaMasiva' and xType = 'U' ) 
   Drop Table Remisiones_CargaMasiva 
Go--#SQL 

Create Table Remisiones_CargaMasiva 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdDistribuidor varchar(4) Not Null Default '', 
	CodigoCliente varchar(20) Not Null Default '', 
	Referencia varchar(20) Not Null Default '', 		
	FechaDocumento datetime Not Null Default getdate(), 
	EsConsignacion bit Not Null Default 0, 
	ClaveSSA varchar(20) Not Null Default '', 
	Cantidad int Not Null Default 0 
) 
Go--#SQL 

--		sp_listacolumnas  Remisiones_CargaMasiva 
		