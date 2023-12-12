If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_Concentrado_Pedidos_Claves' and xType = 'U' ) 
   Drop Table COM_OCEN_Concentrado_Pedidos_Claves
Go--#SQL 

Create Table COM_OCEN_Concentrado_Pedidos_Claves 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdFarmaciaPedido varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	
	Cant_Requerida Numeric(14,4) Not Null, 
	Cant_PreSolicitada Numeric(14,4) Not Null, 
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL 

Alter Table COM_OCEN_Concentrado_Pedidos_Claves Add Constraint PK_COM_OCEN_Concentrado_Pedidos_Claves 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaPedido, IdPersonal, IdClaveSSA, CodigoEAN ) 
Go--#SQL 


------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_REG_Concentrado_Pedidos_Claves' and xType = 'U' ) 
   Drop Table COM_OCEN_REG_Concentrado_Pedidos_Claves
Go--#SQL 

Create Table COM_OCEN_REG_Concentrado_Pedidos_Claves 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdFarmaciaPedido varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	
	Cant_Requerida Numeric(14,4) Not Null, 
	Cant_PreSolicitada Numeric(14,4) Not Null, 
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL 

Alter Table COM_OCEN_REG_Concentrado_Pedidos_Claves Add Constraint PK_COM_OCEN_REG_Concentrado_Pedidos_Claves
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaPedido, IdPersonal, IdClaveSSA, CodigoEAN ) 
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COMREG_Concentrado_Pedidos_Claves' and xType = 'U' ) 
   Drop Table COMREG_Concentrado_Pedidos_Claves
Go--#SQL 

Create Table COMREG_Concentrado_Pedidos_Claves 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdFarmaciaPedido varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	--CodigoEAN varchar(30) Not Null, 
	
	Cant_Requerida Numeric(14,4) Not Null, 
	Cant_PreSolicitada Numeric(14,4) Not Null, 
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL 

Alter Table COMREG_Concentrado_Pedidos_Claves Add Constraint PK_COMREG_Concentrado_Pedidos_Claves 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaPedido, IdPersonal, IdClaveSSA ) 
Go--#SQL 


------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_Concentrado_Pedidos_Claves_OCEN' and xType = 'U' ) 
   Drop Table COM_Concentrado_Pedidos_Claves_OCEN
Go--#SQL 

Create Table COM_Concentrado_Pedidos_Claves_OCEN 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdFarmaciaPedido varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	-- CodigoEAN varchar(30) Not Null, 
	
	Cant_Requerida Numeric(14,4) Not Null, 
	Cant_PreSolicitada Numeric(14,4) Not Null, 
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL 

Alter Table COM_Concentrado_Pedidos_Claves_OCEN Add Constraint PK_COM_Concentrado_Pedidos_Claves_OCEN
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaPedido, IdPersonal, IdClaveSSA ) 
Go--#SQL 


----------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COMREG_Concentrado_Pedidos_Claves' and xType = 'U' ) 
   Drop Table COMREG_Concentrado_Pedidos_Claves
Go--#SQL 

Create Table COMREG_Concentrado_Pedidos_Claves 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdFarmaciaPedido varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	--CodigoEAN varchar(30) Not Null, 
	
	Cant_Requerida Numeric(14,4) Not Null, 
	Cant_PreSolicitada Numeric(14,4) Not Null, 
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL 

Alter Table COMREG_Concentrado_Pedidos_Claves Add Constraint PK_COMREG_Concentrado_Pedidos_Claves 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaPedido, IdPersonal, IdClaveSSA ) 
Go--#SQL 