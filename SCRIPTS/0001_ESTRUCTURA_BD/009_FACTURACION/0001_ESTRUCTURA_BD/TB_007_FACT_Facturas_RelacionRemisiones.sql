
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionFacturas_x_Importes'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionFacturas_x_Importes   
Go--#SQL  

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionFacturas_x_Farmacia'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionFacturas_x_Farmacia   
Go--#SQL    

If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionFacturas'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionFacturas   
Go--#SQL   



------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionFacturas_Enc'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionFacturas_Enc   
Go--#SQL    

Create Table FACT_Remisiones__RelacionFacturas_Enc 
( 
	FolioRelacion varchar(6) Not Null, 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 	
	Serie_Relacionada varchar(10) Not Null Default '', 
	Folio_Relacionado int Not Null Default 0, 	

	IdFuenteFinanciamiento varchar(4) Not Null Default '', 
	IdFinanciamiento varchar(4) Not Null Default '', 
	
	FechaRegistro datetime Not Null Default getdate(), 

	EsFacturaEnCajas bit Not null Default 'false',  
	EsComprobada bit not null Default 'false', 
	TipoDeUnidades int Not Null Default 0 

) 
Go--#SQL 

Alter Table FACT_Remisiones__RelacionFacturas_Enc Add Constraint PK_FACT_Remisiones__RelacionFacturas_Enc Primary Key ( FolioRelacion ) 
Go--#SQL 

Alter Table FACT_Remisiones__RelacionFacturas_Enc Add Constraint FK_FACT_Remisiones__RelacionFacturas_Enc___FACT_CFD_Documentos_Generados 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
	References FACT_CFD_Documentos_Generados ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
Go--#SQL 



------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionFacturas'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionFacturas   
Go--#SQL    

Create Table FACT_Remisiones__RelacionFacturas
( 
	FolioRelacion varchar(6) Not Null, 

	ClaveSSA varchar(20) Not Null, 
	ContenidoPaquete int Not Null Default 1, 
	Cantidad_Facturada numeric(14,4) Not Null Default 0, 	
	Cantidad_Distribuida numeric(14,4) Not Null Default 0,  	
	CantidadAgrupada_Facturada numeric(14,4) Not Null Default 0, 	
	CantidadAgrupada_Distribuida numeric(14,4) Not Null Default 0, 
	EsClaveComprobada bit not null Default 'false'
	
) 
Go--#SQL 

Alter Table FACT_Remisiones__RelacionFacturas Add Constraint PK_FACT_Remisiones__RelacionFacturas 
	Primary Key ( FolioRelacion, ClaveSSA ) 
Go--#SQL 

Alter Table FACT_Remisiones__RelacionFacturas Add Constraint FK_FACT_Remisiones__RelacionFacturas___FACT_Remisiones__RelacionFacturas_Enc 
	Foreign Key ( FolioRelacion ) References FACT_Remisiones__RelacionFacturas_Enc ( FolioRelacion ) 
Go--#SQL 

--Alter Table FACT_Remisiones__RelacionFacturas Add Constraint FK_FACT_Remisiones__RelacionFacturas___FACT_CFD_Documentos_Generados 
--	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
--	References FACT_CFD_Documentos_Generados ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
--Go--xx#SQL 



------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionFacturas_x_Importes'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionFacturas_x_Importes   
Go--#SQL    

Create Table FACT_Remisiones__RelacionFacturas_x_Importes
( 
	FolioRelacion varchar(6) Not Null, 
	--IdEmpresa varchar(3) Not Null,		
	--IdEstado varchar(2) Not Null, 
	--IdFarmacia varchar(4) Not Null,	
	--Serie varchar(10) Not Null Default '', 
	--Folio int Not Null Default 0, 	

	FechaRegistro datetime Not Null Default getdate(), 
	Importe_Facturado numeric(14,4) Not Null Default 0, 	
	Importe_Distribuido numeric(14,4) Not Null Default 0 	
) 
Go--#SQL    


Alter Table FACT_Remisiones__RelacionFacturas_x_Importes Add Constraint PK_FACT_Remisiones__RelacionFacturas_x_Importes 
	Primary Key ( FolioRelacion ) 
Go--#SQL 

Alter Table FACT_Remisiones__RelacionFacturas_x_Importes Add Constraint FK_FACT_Remisiones__RelacionFacturas_x_Importes___FACT_Remisiones__RelacionFacturas_Enc 
	Foreign Key ( FolioRelacion ) References FACT_Remisiones__RelacionFacturas_Enc ( FolioRelacion ) 
Go--#SQL 

----Alter Table FACT_Remisiones__RelacionFacturas_x_Importes Add Constraint FK_FACT_Remisiones__RelacionFacturas_x_Importes___FACT_CFD_Documentos_Generados 
----	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
----	References FACT_CFD_Documentos_Generados ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
----Go--xx#SQL 




------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionFacturas_x_Farmacia'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionFacturas_x_Farmacia   
Go--#SQL    

Create Table FACT_Remisiones__RelacionFacturas_x_Farmacia
( 
	FolioRelacion varchar(6) Not Null, 
	--IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	--Serie varchar(10) Not Null Default '', 
	--Folio int Not Null Default 0, 	
	
	--IdFuenteFinanciamiento varchar(4) Not Null Default '', 
	--IdFinanciamiento varchar(4) Not Null Default '', 

	--FechaRegistro datetime Not Null Default getdate(), 

	IdFarmacia_Relacionada varchar(4) Not Null,	
	ClaveSSA varchar(20) Not Null, 
	ContenidoPaquete int Not Null Default 1, 
	Cantidad_Facturada numeric(14,4) Not Null Default 0, 	
	Cantidad_Distribuida numeric(14,4) Not Null Default 0,  	
	CantidadAgrupada_Facturada numeric(14,4) Not Null Default 0, 	
	CantidadAgrupada_Distribuida numeric(14,4) Not Null Default 0 	
	
) 
Go--#SQL 

Alter Table FACT_Remisiones__RelacionFacturas_x_Farmacia Add Constraint PK_FACT_Remisiones__RelacionFacturas_x_Farmacia 
	Primary Key ( IdEstado, IdFarmacia, IdFarmacia_Relacionada, ClaveSSA ) 
Go--#SQL 

Alter Table FACT_Remisiones__RelacionFacturas_x_Farmacia Add Constraint FK_FACT_Remisiones__RelacionFacturas_x_Farmacia___FACT_Remisiones__RelacionFacturas_Enc 
	Foreign Key ( FolioRelacion ) References FACT_Remisiones__RelacionFacturas_Enc ( FolioRelacion ) 
Go--#SQL 

----Alter Table FACT_Remisiones__RelacionFacturas_x_Farmacia Add Constraint FK_FACT_Remisiones__RelacionFacturas_x_Farmacia___FACT_Remisiones__RelacionFacturas 
----	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, ClaveSSA ) 
----	References FACT_Remisiones__RelacionFacturas ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, ClaveSSA ) 
----Go--xx#SQL 


