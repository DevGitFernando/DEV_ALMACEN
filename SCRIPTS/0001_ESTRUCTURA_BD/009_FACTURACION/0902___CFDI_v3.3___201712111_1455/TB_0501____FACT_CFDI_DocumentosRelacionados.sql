---------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_NotasDeCredito_DoctosRelacionados' and xType = 'U' ) 
	Drop Table FACT_CFDI_NotasDeCredito_DoctosRelacionados   
Go--#SQL 

Create Table FACT_CFDI_NotasDeCredito_DoctosRelacionados   
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 	

	Serie_Relacionada varchar(10) Not Null Default '', 
	Folio_Relacionado int Not Null Default 0, 	
	UUID_Relacionado varchar(50) Not Null Default '', 

	Total_Base numeric(14,4) Not Null Default 0,	---- Importe Original del CFDI  

	SubTotal numeric(14,4) Not Null Default 0, 
	IVA numeric(14,4) Not Null Default 0, 
	Total numeric(14,4) Not Null Default 0 

) 
Go--#SQL 

Alter Table FACT_CFDI_NotasDeCredito_DoctosRelacionados Add Constraint PK_FACT_CFDI_NotasDeCredito_DoctosRelacionados 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Serie_Relacionada, Folio_Relacionado ) 
Go--#SQL 


----Alter Table FACT_CFDI_NotasDeCredito_DoctosRelacionados Add Constraint FK_FACT_CFDI_NotasDeCredito_DoctosRelacionados___FACT_CFD_Documentos_Generados 
----	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
----	References FACT_CFD_Documentos_Generados ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
----Go--#SQL 	


---------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP' and xType = 'U' ) 
	Drop Table FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP
Go--#SQL 

Create Table FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 	

	Serie_Relacionada varchar(10) Not Null Default '', 
	Folio_Relacionado int Not Null Default 0, 	
	UUID_Relacionado varchar(50) Not Null Default '', 

	Total_Base numeric(14,4) Not Null Default 0,	---- Importe Original del CFDI  

	SubTotal numeric(14,4) Not Null Default 0, 
	IVA numeric(14,4) Not Null Default 0, 
	Total numeric(14,4) Not Null Default 0 

) 
Go--#SQL 

Alter Table FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP Add Constraint PK_FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP 
--	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Serie_Relacionada, Folio_Relacionado ) 
Go--#SQL 


----Alter Table FACT_CFDI_NotasDeCredito_DoctosRelacionados Add Constraint FK_FACT_CFDI_NotasDeCredito_DoctosRelacionados___FACT_CFD_Documentos_Generados 
----	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
----	References FACT_CFD_Documentos_Generados ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
----Go--#SQL 	
