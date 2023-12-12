If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_ClavesSSA_ClavesRelacionadas' and xType = 'U' ) 
   Drop Table CFG_ClavesSSA_ClavesRelacionadas  
Go--#SQL  

Create Table CFG_ClavesSSA_ClavesRelacionadas 
( 
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null Default '', 
	IdSubCliente varchar(4) Not Null Default '', 
	IdClaveSSA varchar(4) Not Null, 
	IdClaveSSA_Relacionada varchar(4) Not Null, 	
	Multiplo Int DEFAULT 1  Not Null, 
	AfectaVenta bit Not Null Default 'true', 
	AfectaConsigna bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL  

Alter Table CFG_ClavesSSA_ClavesRelacionadas Add Constraint PK_CFG_ClavesSSA_ClavesRelacionadas 
	Primary Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA, IdClaveSSA_Relacionada ) 
Go--#SQL  

Alter Table CFG_ClavesSSA_ClavesRelacionadas Add Constraint FK_CFG_ClavesSSA_ClavesRelacionadas_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL  

Alter Table CFG_ClavesSSA_ClavesRelacionadas Add Constraint FK_CFG_ClavesSSA_ClavesRelacionadas___ClientesSubClientes 
			Foreign Key ( IdCliente, IdSubCliente ) References CatSubClientes ( IdCliente, IdSubCliente )   
Go--#SQL  

Alter Table CFG_ClavesSSA_ClavesRelacionadas Add Constraint FK_CFG_ClavesSSA_ClavesRelacionadas_CatClavesSSA_Sales  
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL  

Alter Table CFG_ClavesSSA_ClavesRelacionadas Add Constraint FK_CFG_ClavesSSA_ClavesRelacionadas_CatClavesSSA_SalesRelacionada  
	Foreign Key ( IdClaveSSA_Relacionada ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL  
