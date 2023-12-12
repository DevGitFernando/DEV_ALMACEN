-----------------------------------------------------------------------------------------------------------------------------  
-----------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Detalles   
Go--#SQL    


-----------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones'  and xType = 'U' ) 
   Drop Table FACT_Remisiones  
Go--#SQL    

Create Table FACT_Remisiones 
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstadoGenera varchar(2) Not Null,		
	IdFarmaciaGenera varchar(4) Not Null,				-- Referencia solo para el Personal 
	IdEstado varchar(2) Not Null,		
	IdFarmacia varchar(4) Not Null, 
	FechaRemision datetime Not Null Default getdate(), 
	
	FolioRemision varchar(10) Not Null Unique, 
	TipoDeRemision smallint Not Null Default 0,  -- 1 ==> Insumos, 2 ==> Administracion 

	IdCliente varchar(4) Not Null Default '', 
	IdSubCliente varchar(4) Not Null Default '', 

	IdPrograma varchar(4) Not Null Default '', 
	IdSubPrograma varchar(4) Not Null Default '', 
	
	EsFacturada bit Not Null Default 'false',
	EsFacturable bit Not Null Default 'false',  
	EsExcedente smallint Not Null Default 0, 
	
	IdPersonalRemision varchar(4) Not Null Default '', 
	IdPersonalValida varchar(4) Not Null Default '', 
	FechaValidacion datetime Not Null Default getdate(), 	
	
	EsCancelada bit Not Null Default 'false', 
	IdPersonalCancela varchar(4) Not Null Default '', 
	FechaCancelacion datetime Not Null Default getdate(), 	
	
	TipoInsumo varchar(2) Not Null Default '2',		-- 1 ==> Material de curación, 2 ==> Medicamento 
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Total numeric(14,4) Not Null Default 0, 		

	FechaInicial datetime Not Null Default getdate(), 	
	FechaFinal datetime Not Null Default getdate(), 	

	Observaciones varchar(200) Not Null Default '', 
    
	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0    --- 100 ==> Actualizado 
) 
Go--#SQL 

Alter Table FACT_Remisiones Add Constraint PK_FACT_Remisiones 
	Primary Key ( IdEmpresa, IdEstadoGenera, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 

Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones__CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
Go--#SQL 

Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones__CatFarmacias 
	Foreign Key ( IdEstadoGenera, IdFarmaciaGenera ) References CatFarmacias ( IdEstado, IdFarmacia )  
Go--#SQL 

Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones_PersonalRemision__CatPersonal 
	Foreign Key ( IdEstadoGenera, IdFarmaciaGenera, IdPersonalRemision ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal )  
Go--#SQL 

Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones_PersonalValida__CatPersonal 
	Foreign Key ( IdEstadoGenera, IdFarmaciaGenera, IdPersonalValida ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal )  
Go--#SQL 

Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones_PersonalCancela__CatPersonal 
	Foreign Key ( IdEstadoGenera, IdFarmaciaGenera, IdPersonalCancela ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal )  
Go--#SQL 

Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones_Cliente_SubCliente__CatSubClientes 
	Foreign Key ( IdCliente, IdSubCliente ) 
	References CatSubClientes ( IdCliente, IdSubCliente )  
Go--#SQL 

Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones_Programa_SubPrograma__CatSubProgramas 
	Foreign Key ( IdPrograma, IdSubPrograma ) 
	References CatSubProgramas ( IdPrograma, IdSubPrograma )  
Go--#SQL 



-----------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Detalles   
Go--#SQL    

Create Table FACT_Remisiones_Detalles
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,			
	IdFarmaciaGenera varchar(4) Not Null,	
	FolioRemision varchar(10) Not Null,		-- Consecutivo de Facturacion 
	
	
	IdFarmacia varchar(4) Not Null, 
	
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Importe numeric(14,4) Not Null Default 0 
	
) 
Go--#SQL 

Alter Table FACT_Remisiones_Detalles Add Constraint PK_FACT_Remisiones_Detalles 
	Primary Key 
	( 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision 
	) 
Go--#SQL 

Alter Table FACT_Remisiones_Detalles Add Constraint FK_FACT_Remisiones_Detalles___FACT_Remisiones 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
	References FACT_Remisiones ( IdEmpresa, IdEstadoGenera, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 

