------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CFG_ClavesSSA_Unidosis_Precios_Historico' And xType = 'U' )
	Drop Table CFG_ClavesSSA_Unidosis_Precios_Historico 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CFG_ClavesSSA_Unidosis_Precios' And xType = 'U' )
	Drop Table CFG_ClavesSSA_Unidosis_Precios
Go--#SQL 

Create Table CFG_ClavesSSA_Unidosis_Precios
(
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdClaveSSA_Sal varchar(4) Not Null, 
	Precio numeric(14, 4) Not Null, 
	Factor Numeric(14,4) Not Null Default 1, 
	Status varchar(1) Not Null, 
	Actualizado tinyint Not Null 
)
Go--#SQL 

Alter Table CFG_ClavesSSA_Unidosis_Precios Add Constraint PK_CFG_ClavesSSA_Unidosis_Precios  
	Primary Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal )
Go--#SQL 

Alter Table CFG_ClavesSSA_Unidosis_Precios Add Constraint FK_CFG_ClavesSSA_Unidosis_Precios_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL 

Alter Table CFG_ClavesSSA_Unidosis_Precios Add Constraint FK_CFG_ClavesSSA_Unidosis_Precios_CFG_Clientes_SubClientes_Claves 
	Foreign Key ( IdCliente, IdSubCliente, IdClaveSSA_Sal ) 
	References CFG_Clientes_SubClientes_Claves ( IdCliente, IdSubCliente, IdClaveSSA_Sal ) 
Go--#SQL  


------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CFG_ClavesSSA_Unidosis_Precios_Historico' And xType = 'U' )
	Drop Table CFG_ClavesSSA_Unidosis_Precios_Historico
Go--#SQL 

Create Table CFG_ClavesSSA_Unidosis_Precios_Historico
(
	IdEstado varchar(2) Not Null,
	IdCliente varchar(4) Not Null,
	IdSubCliente varchar(4) Not Null,
	IdClaveSSA_Sal varchar(4) Not Null,
	FechaUpdate datetime Not Null Default getdate(), 
	Precio numeric(14, 4) Not Null, 
	IdEstadoPersonal varchar(2) Not Null,
	IdFarmaciaPersonal varchar(4) Not Null,
	IdPersonal varchar(4) Not Null, 
	Keyx int identity(1,1) Not Null, 
	Status varchar(1) Not Null,  
	Actualizado tinyint Not Null 	 	 
)
Go--#SQL  

Alter Table CFG_ClavesSSA_Unidosis_Precios_Historico Add Constraint PK_CFG_ClavesSSA_Unidosis_Precios_Historico Primary Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSa_Sal, FechaUpdate ) 
Go--#SQL   

Alter Table CFG_ClavesSSA_Unidosis_Precios_Historico Add Constraint FK_CFG_ClavesSSA_Unidosis_Precios_Historico_CFG_ClavesSSA_Unidosis_Precios
	Foreign Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSa_Sal ) 
	References CFG_ClavesSSA_Unidosis_Precios ( IdEstado, IdCliente, IdSubCliente, IdClaveSSa_Sal ) 
Go--#SQL  
	