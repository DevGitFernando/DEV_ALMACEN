If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CFG_ClavesSSA_Precios_Historico' And xType = 'U' )
	Drop Table CFG_ClavesSSA_Precios_Historico
Go--#SQL 

If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CFG_ClavesSSA_Precios' And xType = 'U' )
	Drop Table CFG_ClavesSSA_Precios
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Clientes_SubClientes_Claves' and xType = 'U' )
   Drop Table CFG_Clientes_SubClientes_Claves 
Go--#SQL  

-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Clientes_Claves' and xType = 'U' )
   Drop Table CFG_Clientes_Claves 
Go--#SQL  

Create Table CFG_Clientes_Claves 
(
	IdCliente varchar(4) Not Null, 
	IdClaveSSA_Sal varchar(4) Not Null, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CFG_Clientes_Claves Add Constraint PK_CFG_Clientes_Claves Primary Key ( IdCliente, IdClaveSSA_Sal ) 
Go--#SQL  

Alter Table CFG_Clientes_Claves Add Constraint PK_CFG_Clientes_Claves_CatClientes  
	Foreign Key ( IdCliente ) References CatClientes ( IdCliente ) 
Go--#SQL  

Alter Table CFG_Clientes_Claves Add Constraint PK_CFG_Clientes_Claves_CatClavesSSA_Sales  
	Foreign Key ( IdClaveSSA_Sal ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL  


---------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Clientes_SubClientes_Claves' and xType = 'U' )
   Drop Table CFG_Clientes_SubClientes_Claves 
Go--#SQL  

Create Table CFG_Clientes_SubClientes_Claves 
(
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 	
	IdClaveSSA_Sal varchar(4) Not Null, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CFG_Clientes_SubClientes_Claves Add Constraint PK_CFG_Clientes_SubClientes_Claves Primary Key ( IdCliente, IdSubCliente, IdClaveSSA_Sal ) 
Go--#SQL 

Alter Table CFG_Clientes_SubClientes_Claves Add Constraint FK_CFG_Clientes_SubClientes_Claves_CFG_Clientes_Claves 
	Foreign Key ( IdCliente, IdClaveSSA_Sal ) References CFG_Clientes_Claves ( IdCliente, IdClaveSSA_Sal ) 
Go--#SQL  

Alter Table CFG_Clientes_SubClientes_Claves Add Constraint FK_CFG_Clientes_SubClientes_Claves_CatSubClientes 
	Foreign Key ( IdCliente, IdSubCliente ) References CatSubClientes ( IdCliente, IdSubCliente ) 
Go--#SQL  


------------------------ 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CFG_ClavesSSA_Precios' And xType = 'U' )
	Drop Table CFG_ClavesSSA_Precios
Go--#SQL 

Create Table CFG_ClavesSSA_Precios
(
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdClaveSSA_Sal varchar(4) Not Null, 
	Precio numeric(14, 4) Not Null, 
	Factor Numeric(14,4) Not Null Default 1, 

	ContenidoPaquete_Licitado Int Not Null Default 0,   
	IdPresentacion_Licitado varchar(3) Not Null Default '',    
	Dispensacion_CajasCompletas bit Not Null Default 'false',
	SAT_ClaveDeProducto_Servicio varchar(20) Not Null Default '',   
	SAT_UnidadDeMedida varchar(5) Not Null Default '', 

	Status varchar(1) Not Null, 
	Actualizado tinyint Not Null 
)
Go--#SQL 

Alter Table CFG_ClavesSSA_Precios Add Constraint PK_CGF_ClavesSSA_Precios 
	Primary Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal )
Go--#SQL 

Alter Table CFG_ClavesSSA_Precios Add Constraint FK_CFG_ClavesSSA_Precios_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL 

Alter Table CFG_ClavesSSA_Precios Add Constraint FK_CFG_ClavesSSA_Precios_CFG_Clientes_SubClientes_Claves 
	Foreign Key ( IdCliente, IdSubCliente, IdClaveSSA_Sal ) 
	References CFG_Clientes_SubClientes_Claves ( IdCliente, IdSubCliente, IdClaveSSA_Sal ) 
Go--#SQL  

------------------------------------------------  
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CFG_ClavesSSA_Precios_Historico' And xType = 'U' )
	Drop Table CFG_ClavesSSA_Precios_Historico
Go--#SQL 

Create Table CFG_ClavesSSA_Precios_Historico
(
	IdEstado varchar(2) Not Null,
	IdCliente varchar(4) Not Null,
	IdSubCliente varchar(4) Not Null,
	IdClaveSSA_Sal varchar(4) Not Null,
	FechaUpdate datetime Not Null Default getdate(), 
	Precio numeric(14, 4) Not Null, 
	Factor Numeric(14,4) Not Null Default 1, 

	ContenidoPaquete_Licitado Int Not Null Default 0,   
	IdPresentacion_Licitado varchar(3) Not Null Default '',    
	Dispensacion_CajasCompletas bit Not Null Default 'false',
	SAT_ClaveDeProducto_Servicio varchar(20) Not Null Default '',   
	SAT_UnidadDeMedida varchar(5) Not Null Default '', 

	IdEstadoPersonal varchar(2) Not Null,
	IdFarmaciaPersonal varchar(4) Not Null,
	IdPersonal varchar(4) Not Null, 
	Keyx int identity(1,1) Not Null, 
	Status varchar(1) Not Null,  
	Actualizado tinyint Not Null 	 	 
)
Go--#SQL  

Alter Table CFG_ClavesSSA_Precios_Historico Add Constraint PK_CFG_ClavesSSA_Precios_Historico Primary Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSa_Sal, FechaUpdate ) 
Go--#SQL   

Alter Table CFG_ClavesSSA_Precios_Historico Add Constraint FK_CFG_ClavesSSA_Precios_Historico_CFG_ClavesSSA_Precios
	Foreign Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSa_Sal ) 
	References CFG_ClavesSSA_Precios ( IdEstado, IdCliente, IdSubCliente, IdClaveSSa_Sal ) 
Go--#SQL  
	