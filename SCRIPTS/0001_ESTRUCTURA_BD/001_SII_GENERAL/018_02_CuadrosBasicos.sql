
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CB_CuadroBasico_Claves' and xType = 'U' ) 
	Drop Table CFG_CB_CuadroBasico_Claves
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CB_NivelesAtencion_Miembros' and xType = 'U' ) 
	Drop Table CFG_CB_NivelesAtencion_Miembros
Go--#SQL 

------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CB_NivelesAtencion' and xType = 'U' ) 
	Drop Table CFG_CB_NivelesAtencion
Go--#SQL 

Create Table CFG_CB_NivelesAtencion 
(
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdNivel int Not Null, 
	Descripcion varchar(100) Not Null, 
	FechaUpdate datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CFG_CB_NivelesAtencion Add Constraint PK_CFG_CB_NivelesAtencion Primary Key ( IdEstado, IdCliente, IdNivel ) 
Go--#SQL  

------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CB_NivelesAtencion_Miembros' and xType = 'U' ) 
	Drop Table CFG_CB_NivelesAtencion_Miembros
Go--#SQL 

Create Table CFG_CB_NivelesAtencion_Miembros 
(
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdNivel int Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FechaUpdate datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CFG_CB_NivelesAtencion_Miembros Add Constraint PK_CatCFG_CB_NivelesAtencion_Miembros Primary Key ( IdEstado, IdCliente, IdNivel, IdFarmacia ) 
Go--#SQL  

Alter Table CFG_CB_NivelesAtencion_Miembros Add Constraint FK_CFG_CB_NivelesAtencion_Miembros_CFG_CB_NivelesAtencion
	Foreign Key ( IdEstado, IdCliente, IdNivel ) References CFG_CB_NivelesAtencion ( IdEstado, IdCliente, IdNivel )
Go--#SQL 


------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_CB_CuadroBasico_Claves' and xType = 'U' ) 
	Drop Table CFG_CB_CuadroBasico_Claves
Go--#SQL 

Create Table CFG_CB_CuadroBasico_Claves 
(
	IdEstado varchar(2) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdNivel int Not Null, 
	IdClaveSSA_Sal varchar(4) Not Null, 
	FechaUpdate datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
	
)
Go--#SQL  

Alter Table CFG_CB_CuadroBasico_Claves Add Constraint PK_CFG_CB_CuadroBasico_Claves Primary Key ( IdEstado, IdCliente, IdNivel, IdClaveSSA_Sal ) 
Go--#SQL  

Alter Table CFG_CB_CuadroBasico_Claves Add Constraint FK_CFG_CB_CuadroBasico_Claves_CFG_CB_NivelesAtencion
	Foreign Key ( IdEstado, IdCliente, IdNivel ) References CFG_CB_NivelesAtencion ( IdEstado, IdCliente, IdNivel )
Go--#SQL 

Alter Table CFG_CB_CuadroBasico_Claves Add Constraint FK_CFG_CB_CuadroBasico_Claves_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA_Sal ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
Go--#SQL 

