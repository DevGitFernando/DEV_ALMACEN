------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Conciliacion_OrdenesDeCompra'  and xType = 'U' ) 
   Drop Table FACT_Conciliacion_OrdenesDeCompra 
Go--#SQL    

Create Table FACT_Conciliacion_OrdenesDeCompra
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,		
	IdFarmacia varchar(4) Not Null,		
	FolioOrden varchar(8) Not Null,		
	IdProveedor varchar(4) Not Null, 
	
	FechaRegistro datetime Not Null Default getdate(),		
	FechaColocacion datetime Not Null Default getdate(),	
	EsContado bit Not Null Default 0,	
    
	SubTotal_SinGrabar numeric(14,4) Not Null Default 0,	
	SubTotal_Grabado numeric(14,4) Not Null Default 0,	
	Iva numeric(14,4) Not Null Default 0,		
	Total numeric(14,4) Not Null Default 0,		  
    
	SubTotal_SinGrabar_Recibido numeric(14,4) Not Null Default 0, 
	SubTotal_Grabado_Recibido numeric(14,4) Not Null Default 0, 
	Iva_Recibido numeric(14,4) Not Null Default 0, 
	Total_Recibido numeric(14,4) Not Null Default 0,         
    
    
	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0    --- 100 ==> Actualizado 
) 
Go--#SQL 

Alter Table FACT_Conciliacion_OrdenesDeCompra 
	Add Constraint PK_FACT_Conciliacion_OrdenesDeCompra Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden ) 
Go--#SQL 

Alter Table FACT_Conciliacion_OrdenesDeCompra Add Constraint FK_FACT_Conciliacion_OrdenesDeCompra__CatEmpresas  
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table FACT_Conciliacion_OrdenesDeCompra Add Constraint FK_FACT_Conciliacion_OrdenesDeCompra__CatFarmacias  
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 
