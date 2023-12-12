----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_ClavesSSA' and xType = 'U' ) 
   Drop Table INT_ND_CFG_ClavesSSA 
Go--#SQL 

Create Table INT_ND_CFG_ClavesSSA 
( 
	IdEstado varchar(2) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	ClaveSSA varchar(50) Not Null, 	
	ClaveSSA_ND varchar(20) Not Null, 
	Status varchar(1) Not Null Default '', 
	Existe bit Not Null Default 'False'  
) 	
Go--#SQL 

Alter Table INT_ND_CFG_ClavesSSA Add Constraint PK_INT_ND_CFG_ClavesSSA Primary Key  ( IdEstado, IdClaveSSA, ClaveSSA, ClaveSSA_ND ) 
Go--#SQL 

Alter Table INT_ND_CFG_ClavesSSA Add Constraint FK_INT_ND_CFG_ClavesSSA__CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL 


---alter table INT_ND_CFG_ClavesSSA drop  Constraint PK_INT_ND_CFG_ClavesSSA

-- Alter Table INT_ND_CFG_ClavesSSA Add Constraint PK_INT_ND_CFG_ClavesSSA Primary Key  ( IdEstado, IdClaveSSA, ClaveSSA, ClaveSSA_ND ) 
Go--#SQL 
