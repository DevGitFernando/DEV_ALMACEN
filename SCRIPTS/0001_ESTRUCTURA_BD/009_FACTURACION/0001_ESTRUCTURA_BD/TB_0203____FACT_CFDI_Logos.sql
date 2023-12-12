------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_Emisores_Logos' and xType = 'U' ) 
	Drop Table FACT_CFDI_Emisores_Logos   
Go--#SQL 

Create Table FACT_CFDI_Emisores_Logos
( 
	IdEmpresa varchar(3) Not Null,  	
	Logo text default ''  
) 
Go--#SQL  

Alter Table FACT_CFDI_Emisores_Logos Add Constraint PK_FACT_CFDI_Emisores_Logos Primary Key ( IdEmpresa )  
Go--#SQL 
