If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CFG_ClavesSSA_Precios_Programas_Historico' And xType = 'U' )
	Drop Table CFG_ClavesSSA_Precios_Programas_Historico
Go--#SQL
---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CFG_ClavesSSA_Precios_Programas' And xType = 'U' )
	Drop Table CFG_ClavesSSA_Precios_Programas
Go--#SQL 

Create Table CFG_ClavesSSA_Precios_Programas
(
	IdEstado varchar(2) Not Null,
	IdCliente varchar(4) Not Null,
	IdSubCliente varchar(4) Not Null,
	IdClaveSSA_Sal varchar(4) Not Null,
	IdPrograma Varchar(4) Not Null,
	IdSubPrograma Varchar(4) Not Null,
	Precio numeric(14, 4) Not Null,
	Status varchar(1) Not Null,
	Actualizado tinyint Not Null
)
Go--#SQL

Alter Table CFG_ClavesSSA_Precios_Programas Add Constraint PK_CFG_ClavesSSA_Precios_Programas
	Primary Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, IdPrograma, IdSubPrograma)
Go--#SQL

Alter Table CFG_ClavesSSA_Precios_Programas Add Constraint FK_CFG_ClavesSSA_Precios_Programas_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL

Alter Table CFG_ClavesSSA_Precios_Programas Add Constraint FK_CFG_ClavesSSA_Precios_Programas_CFG_Clientes_SubClientes_Claves 
	Foreign Key ( IdCliente, IdSubCliente, IdClaveSSA_Sal ) 
	References CFG_Clientes_SubClientes_Claves ( IdCliente, IdSubCliente, IdClaveSSA_Sal ) 
Go--#SQL

Alter Table CFG_ClavesSSA_Precios_Programas Add Constraint FK_CFG_ClavesSSA_Precios_Programas_CatSubProgramas
	Foreign Key ( IdPrograma, IdSubPrograma ) References CatSubProgramas ( IdPrograma, IdSubPrograma ) 
Go--#SQL 

--------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CFG_ClavesSSA_Precios_Programas_Historico' And xType = 'U' )
	Drop Table CFG_ClavesSSA_Precios_Programas_Historico
Go--#SQL 

Create Table CFG_ClavesSSA_Precios_Programas_Historico
(
	IdEstado varchar(2) Not Null,
	IdCliente varchar(4) Not Null,
	IdSubCliente varchar(4) Not Null,
	IdClaveSSA_Sal varchar(4) Not Null,
	IdPrograma Varchar(4) Not Null,
	IdSubPrograma Varchar(4) Not Null,
	Precio numeric(14, 4) Not Null,
	Status varchar(1) Not Null,
	IdEstadoPersonal varchar(2) Not Null,
	IdFarmaciaPersonal varchar(4) Not Null,
	IdPersonal varchar(4) Not Null,
	FechaUpdate datetime Not Null,
	Actualizado tinyint Not Null
)
Go--#SQL

Alter Table CFG_ClavesSSA_Precios_Programas_Historico Add Constraint PK_CFG_ClavesSSA_Precios_Programas_Historico
	Primary Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, IdPrograma, IdSubPrograma, FechaUpdate)
Go--#SQL

Alter Table CFG_ClavesSSA_Precios_Programas_Historico Add Constraint FK_CFG_ClavesSSA_Precios_Programas_Historico_CFG_ClavesSSA_Precios_Programas
	Foreign Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, IdPrograma,  IdSubPrograma ) References CFG_ClavesSSA_Precios_Programas 
Go--#SQL