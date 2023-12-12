If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Claves_Excluir_Generacion_Pedidos' and xType = 'U' ) 
	Drop Table CFG_Claves_Excluir_Generacion_Pedidos
Go--#SQL 

Create Table CFG_Claves_Excluir_Generacion_Pedidos 
(
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null,
	IdSubCliente varchar(4) Not Null, 
	IdClaveSSA_Sal varchar(4) Not Null,
	ClaveSSA varchar(30) Not Null,
	ExcluirDePedido bit Not Null Default 0,	
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CFG_Claves_Excluir_Generacion_Pedidos Add Constraint PK_CFG_Claves_Excluir_Generacion_Pedidos 
	Primary Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, ClaveSSA ) 
Go--#SQL

Alter Table CFG_Claves_Excluir_Generacion_Pedidos Add Constraint FK_CFG_Claves_Excluir_Generacion_Pedidos_CFG_ClavesSSA_Precios
	Foreign Key ( IdClaveSSA_Sal ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
Go--#SQL   
