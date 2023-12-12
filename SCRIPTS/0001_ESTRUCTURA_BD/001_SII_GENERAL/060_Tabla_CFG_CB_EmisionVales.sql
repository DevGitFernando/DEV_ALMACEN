


	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CB_EmisionVales' and xType = 'U' ) 
	Drop Table CFG_CB_EmisionVales
Go--#SQL 

Create Table CFG_CB_EmisionVales 
(
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null,
	IdSubCliente varchar(4) Not Null,
	IdClaveSSA_Sal varchar(4) Not Null,
	ClaveSSA varchar(30) Not Null,
	EmiteVales bit Not Null Default 0,	
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CFG_CB_EmisionVales Add Constraint PK_CFG_CB_EmisionVales Primary Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, ClaveSSA ) 
Go--#SQL

Alter Table CFG_CB_EmisionVales Add Constraint FK_CFG_CB_EmisionVales_CFG_ClavesSSA_Precios
	Foreign Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal ) 
	References CFG_ClavesSSA_Precios ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal )
Go--#SQL   