----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_ClavesSSA_ContenidoPaquete' and xType = 'U' ) 
   Drop Table INT_ND_CFG_ClavesSSA_ContenidoPaquete 
Go--#SQL 

Create Table INT_ND_CFG_ClavesSSA_ContenidoPaquete 
( 
	IdEstado varchar(2) Not Null, 
	ClaveSSA varchar(50) Not Null, 	
	ContenidoPaquete numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A' 
) 	
Go--#SQL 

Alter Table INT_ND_CFG_ClavesSSA_ContenidoPaquete 
	Add Constraint PK_INT_ND_CFG_ClavesSSA_ContenidoPaquete Primary Key  ( IdEstado, ClaveSSA ) 
Go--#SQL 

----Alter Table INT_ND_CFG_ClavesSSA_ContenidoPaquete Add Constraint FK_INT_ND_CFG_ClavesSSA_ContenidoPaquete____CatClavesSSA_Sales 
----	Foreign Key ( ClaveSSA ) References CatClavesSSA_Sales ( ClaveSSA ) 
----Go--#xxSQL 


