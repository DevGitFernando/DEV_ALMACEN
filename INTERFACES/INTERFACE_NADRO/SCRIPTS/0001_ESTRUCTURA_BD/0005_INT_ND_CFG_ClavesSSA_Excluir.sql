----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_ClavesSSA_Excluir' and xType = 'U' ) 
   Drop Table INT_ND_CFG_ClavesSSA_Excluir 
Go--#SQL 

Create Table INT_ND_CFG_ClavesSSA_Excluir 
( 
	IdEstado varchar(2) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	ClaveSSA varchar(50) Not Null, 	
	Status varchar(1) Not Null Default '' 
) 	
Go--#SQL 

Alter Table INT_ND_CFG_ClavesSSA_Excluir Add Constraint PK_INT_ND_CFG_ClavesSSA_Excluir Primary Key  ( IdEstado, IdClaveSSA, ClaveSSA ) 
Go--#SQL 

Alter Table INT_ND_CFG_ClavesSSA_Excluir Add Constraint FK_INT_ND_CFG_ClavesSSA_Excluir__CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL 


