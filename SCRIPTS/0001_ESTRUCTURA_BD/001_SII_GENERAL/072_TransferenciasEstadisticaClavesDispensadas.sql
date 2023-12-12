If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TransferenciasEstadisticaClavesDispensadas' and xType = 'U' ) 	
   Drop Table TransferenciasEstadisticaClavesDispensadas
Go--#SQL    

Create Table TransferenciasEstadisticaClavesDispensadas 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioTransferencia varchar(30) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	
	Observaciones varchar(100) Not Null Default '', 	
	EsCapturada bit Not Null Default 'false', 
	
	CantidadRequerida numeric(14,4) Not Null Default 0, 
	CantidadEntregada numeric(14,4) Not Null Default 0, 
	
	ExistenciaSistema numeric(14,4) Not Null Default 0, 
	TieneCartaFaltante bit Not Null Default 'false', 
		
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL


Alter Table TransferenciasEstadisticaClavesDispensadas Add Constraint PK_TransferenciasEstadisticaClavesDispensadas
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdClaveSSA ) 
Go--#SQL 

Alter Table TransferenciasEstadisticaClavesDispensadas Add Constraint FK_TransferenciasEstadisticaClavesDispensadas_TransferenciasEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia ) References TransferenciasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia ) 
Go--#SQL 	

Alter Table TransferenciasEstadisticaClavesDispensadas Add Constraint FK_TransferenciasEstadisticaClavesDispensadas_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL 	