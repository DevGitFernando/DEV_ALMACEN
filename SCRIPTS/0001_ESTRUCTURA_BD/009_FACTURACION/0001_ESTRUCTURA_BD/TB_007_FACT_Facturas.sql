------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Contrarecibos_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Contrarecibos_Detalles  
Go--#SQL    


------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Facturas'  and xType = 'U' ) 
   Drop Table FACT_Facturas  
Go--#SQL    

Create Table FACT_Facturas 
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,		
	IdFarmacia varchar(4) Not Null,				-- Referencia solo para el Personal 

	FolioFactura varchar(10) Not Null,  
	TipoDeFactura smallint Not Null Default 0,  -- 1 ==> Insumos, 2 ==> Administracion 
	FolioRemision varchar(10) Not Null, 
	FechaRegistro datetime Not Null Default getdate(), 
	FolioFacturaElectronica varchar(100) Not Null Default '', 		
	EstaEnCobro bit Not Null Default 0, 
	IdPersonalFactura varchar(4) Not Null Default '', 
		
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Total numeric(14,4) Not Null Default 0, 		

	Observaciones varchar(200) Not Null Default '', 
    
	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0,     --- 100 ==> Actualizado 
	Keyx int Identity(1, 1) 
) 
Go--#SQL 

Alter Table FACT_Facturas Add Constraint PK_FACT_Facturas Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioFactura ) 
Go--#SQL 

Alter Table FACT_Facturas Add Constraint FK_FACT_Facturas__CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
Go--#SQL 

Alter Table FACT_Facturas Add Constraint FK_FACT_Facturas__CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )  
Go--#SQL 

Alter Table FACT_Facturas Add Constraint FK_FACT_Facturas__CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonalFactura ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal )  
Go--#SQL 

Alter Table FACT_Facturas Add Constraint FK_FACT_Facturas__FACT_Remisiones
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision ) 
	References FACT_Remisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision )  
Go--#SQL 

--	9,999,999,999   


------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Contrarecibos'  and xType = 'U' ) 
   Drop Table FACT_Contrarecibos 
Go--#SQL    

Create Table FACT_Contrarecibos 
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,		
	IdFarmacia varchar(4) Not Null,				-- Referencia solo para el Personal 
	FolioContrarecibo varchar(10) Not Null,  
	FechaRegistro datetime Not Null Default getdate(), 
		
	Contrarecibo varchar(100) Not Null Default '', 
	FechaDocumento datetime Not Null Default getdate(), 
	Observaciones varchar(200) Not Null Default '', 	
	
	
	IdPersonalFactura varchar(4) Not Null Default '', 		
	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0,     --- 100 ==> Actualizado 
	Keyx int Identity(1, 1) 	
)
Go--#SQL 	

Alter Table FACT_Contrarecibos Add Constraint PK_FACT_Contrarecibos Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioContrarecibo ) 
Go--#SQL 

Alter Table FACT_Contrarecibos Add Constraint FK_FACT_Contrarecibos__CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonalFactura ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal )  
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
	IdFarmacia varchar(4) Not Null,				-- Referencia solo para el Personal 
	FolioContrarecibo varchar(10) Not Null,  
	FolioFactura varchar(10) Not Null,  	

	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0,     --- 100 ==> Actualizado 	
)
Go--#SQL 	

Alter Table FACT_Contrarecibos_Detalles Add Constraint PK_FACT_Contrarecibos_Detalles 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioContrarecibo, FolioFactura ) 
Go--#SQL 

Alter Table FACT_Contrarecibos_Detalles Add Constraint FK_FACT_Contrarecibos_Detalles__FACT_Contrarecibos
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioContrarecibo ) 
	References FACT_Contrarecibos ( IdEmpresa, IdEstado, IdFarmacia, FolioContrarecibo )  
Go--#SQL 

Alter Table FACT_Contrarecibos_Detalles Add Constraint FK_FACT_Contrarecibos_Detalles__FACT_Facturas
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioFactura ) 
	References FACT_Facturas ( IdEmpresa, IdEstado, IdFarmacia, FolioFactura )  
Go--#SQL 

