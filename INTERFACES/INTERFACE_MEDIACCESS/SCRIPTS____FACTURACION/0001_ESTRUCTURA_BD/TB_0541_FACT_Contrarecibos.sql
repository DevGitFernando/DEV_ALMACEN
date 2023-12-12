------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Contrarecibos_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Contrarecibos_Detalles  
Go--#SQL    


------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Contrarecibos'  and xType = 'U' ) 
   Drop Table FACT_Contrarecibos 
Go--#SQL    

Create Table FACT_Contrarecibos 
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,		
	IdFarmaciaGenera varchar(4) Not Null,				-- Referencia solo para el Personal 
	FolioContrarecibo varchar(10) Not Null,  
	FechaRegistro datetime Not Null Default getdate(), 
	
	EsEntregaConfirmada bit Not Null Default 'false', 	
	FechaEntrega datetime Not Null Default getdate(), 
	Contrarecibo varchar(100) Not Null Default '', 
	FechaDocumento datetime Not Null Default getdate(), 
	Observaciones varchar(200) Not Null Default '', 	
	
	
	IdPersonalCaptura varchar(4) Not Null Default '', 
	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0,     --- 100 ==> Actualizado 
	Keyx int Identity(1, 1) 	
	
)
Go--#SQL 	

Alter Table FACT_Contrarecibos Add Constraint PK_FACT_Contrarecibos Primary Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioContrarecibo ) 
Go--#SQL 

Alter Table FACT_Contrarecibos Add Constraint FK_FACT_Contrarecibos__CatPersonal 
	Foreign Key ( IdEstado, IdFarmaciaGenera, IdPersonalCaptura ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal )  
Go--#SQL 


------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Contrarecibos_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Contrarecibos_Detalles  
Go--#SQL    

Create Table FACT_Contrarecibos_Detalles 
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,		
	IdFarmaciaGenera varchar(4) Not Null,				-- Referencia solo para el Personal 
	FolioContrarecibo varchar(10) Not Null,  
	
	FolioFactura varchar(10) Not Null,  	

	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0,     --- 100 ==> Actualizado 	
)
Go--#SQL 	

Alter Table FACT_Contrarecibos_Detalles Add Constraint PK_FACT_Contrarecibos_Detalles 
	Primary Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioContrarecibo, FolioFactura ) 
Go--#SQL 

Alter Table FACT_Contrarecibos_Detalles Add Constraint FK_FACT_Contrarecibos_Detalles__FACT_Contrarecibos
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioContrarecibo ) 
	References FACT_Contrarecibos ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioContrarecibo )  
Go--#SQL 

Alter Table FACT_Contrarecibos_Detalles Add Constraint FK_FACT_Contrarecibos_Detalles__FACT_Facturas
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioFactura ) 
	References FACT_Facturas ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioFactura )  
Go--#SQL 

