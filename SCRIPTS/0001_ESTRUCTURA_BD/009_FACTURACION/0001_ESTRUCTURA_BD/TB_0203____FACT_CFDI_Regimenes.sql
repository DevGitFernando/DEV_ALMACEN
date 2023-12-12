------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_RegimenFiscal' and xType = 'U' ) 
	Drop Table FACT_CFDI_RegimenFiscal    
Go--#SQL  

Create Table FACT_CFDI_RegimenFiscal 
(	
	IdRegimen varchar(2) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table FACT_CFDI_RegimenFiscal Add Constraint PK_FACT_CFDI_RegimenFiscal Primary	Key ( IdRegimen ) 
Go--#SQL 

Insert Into FACT_CFDI_RegimenFiscal Select '01', 'REGIMEN GENERAL DE LEY', 'A' 
Go--#SQL 

------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_Emisores_Regimenes' and xType = 'U' ) 
	Drop Table FACT_CFDI_Emisores_Regimenes    
Go--#SQL 

Create Table FACT_CFDI_Emisores_Regimenes
( 
	IdEmpresa varchar(3) Not Null,  	
	IdRegimen varchar(2) Not Null 
) 
Go--#SQL  

Alter Table FACT_CFDI_Emisores_Regimenes Add Constraint PK_FACT_CFDI_Emisores_Regimenes Primary Key ( IdEmpresa )  
Go--#SQL 


Alter Table FACT_CFDI_Emisores_Regimenes Add Constraint FK_FACT_CFDI_Emisores_Regimenes___FACT_CFDI_RegimenFiscal 
	Foreign Key ( IdRegimen ) References FACT_CFDI_RegimenFiscal ( IdRegimen ) 
Go--#SQL 
  