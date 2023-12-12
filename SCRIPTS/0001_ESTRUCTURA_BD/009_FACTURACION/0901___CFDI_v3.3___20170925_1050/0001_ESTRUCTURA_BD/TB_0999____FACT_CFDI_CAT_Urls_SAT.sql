------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_SAT_Urls' and xType = 'U' ) 
	Drop Table FACT_CFDI_SAT_Urls    
Go--#SQL  

Create Table FACT_CFDI_SAT_Urls 
(	
	Clave varchar(100) Not Null Default '', 
	Url varchar(500) Not Null Default '' Unique, 
	Descripcion varchar(200) Not Null Default '', 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table FACT_CFDI_SAT_Urls Add Constraint PK_FACT_CFDI_SAT_Urls Primary	Key ( Clave  ) 
Go--#SQL 


