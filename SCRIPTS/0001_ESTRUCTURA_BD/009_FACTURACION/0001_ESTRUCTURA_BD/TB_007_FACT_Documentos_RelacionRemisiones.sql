
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionDocumentos'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionDocumentos   
Go--#SQL   


------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionDocumentos_Enc'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionDocumentos_Enc   
Go--#SQL    

Create Table FACT_Remisiones__RelacionDocumentos_Enc 
( 
	FolioRelacion varchar(6) Not Null, 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	

	IdFuenteFinanciamiento varchar(4) Not Null Default '', 
	IdFinanciamiento varchar(4) Not Null Default '', 
	
	ReferenciaDocumento varchar(100) Not Null Default '', 
	NombreDocumento varchar(500) Not Null Default '', 
	MD5_Documento varchar(100) Not Null Default '', 
	Documento varchar(max) Not Null Default '', 


	Procesa_Venta bit Not Null Default 'false', 
	Procesa_Consigna bit Not Null Default 'false', 

	Procesa_Producto bit Not Null Default 'false', 
	Procesa_Servicio bit Not Null Default 'false', 


	FechaRegistro datetime Not Null Default getdate(), 

	EsDocumentoEnCajas bit Not null Default 'false',  
	EsComprobada bit not null Default 'false', 
	TipoDeUnidades int Not Null Default 0 

) 
Go--#SQL 

Alter Table FACT_Remisiones__RelacionDocumentos_Enc Add Constraint PK_FACT_Remisiones__RelacionDocumentos_Enc Primary Key ( FolioRelacion ) 
Go--#SQL 

----Alter Table FACT_Remisiones__RelacionDocumentos_Enc Add Constraint FK_FACT_Remisiones__RelacionDocumentos_Enc___FACT_CFD_Documentos_Generados 
----	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
----	References FACT_CFD_Documentos_Generados ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
----Go--xxx#SQL 



------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionDocumentos'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionDocumentos   
Go--#SQL    

Create Table FACT_Remisiones__RelacionDocumentos
( 
	FolioRelacion varchar(6) Not Null, 

	ClaveSSA varchar(20) Not Null, 
	ContenidoPaquete int Not Null Default 1, 
	Cantidad_A_Comprobar numeric(14,4) Not Null Default 0, 	
	Cantidad_Distribuida numeric(14,4) Not Null Default 0,  	
	CantidadAgrupada_A_Comprobar numeric(14,4) Not Null Default 0, 	
	CantidadAgrupada_Distribuida numeric(14,4) Not Null Default 0, 
	EsClaveComprobada bit not null Default 'false'
	
) 
Go--#SQL 

Alter Table FACT_Remisiones__RelacionDocumentos Add Constraint PK_FACT_Remisiones__RelacionDocumentos 
	Primary Key ( FolioRelacion, ClaveSSA ) 
Go--#SQL 

Alter Table FACT_Remisiones__RelacionDocumentos Add Constraint FK_FACT_Remisiones__RelacionDocumentos___FACT_Remisiones__RelacionDocumentos_Enc 
	Foreign Key ( FolioRelacion ) References FACT_Remisiones__RelacionDocumentos_Enc ( FolioRelacion ) 
Go--#SQL 
