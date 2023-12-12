
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_COM_Perfiles_Personal_ClavesSSA' and xType = 'U' ) 
Drop Table CFG_COM_Perfiles_Personal_ClavesSSA
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_COM_Perfiles_Personal' and xType = 'U' ) 
Drop Table CFG_COM_Perfiles_Personal
Go--#SQL 

Create Table CFG_COM_Perfiles_Personal 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdPersonal varchar(4) Not Null,
	Status varchar(2) Not Null Default 'A',		
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CFG_COM_Perfiles_Personal Add Constraint PK_CFG_COM_Perfiles_Personal Primary Key ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL

Alter Table CFG_COM_Perfiles_Personal Add Constraint FK_CFG_COM_Perfiles_Personal_CatPersonal
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal )
Go--#SQL   



If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_COM_Perfiles_Personal_ClavesSSA' and xType = 'U' ) 
Drop Table CFG_COM_Perfiles_Personal_ClavesSSA
Go--#SQL 

Create Table CFG_COM_Perfiles_Personal_ClavesSSA 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdPersonal varchar(4) Not Null,
	IdClaveSSA_Sal varchar(4) Not Null,
	ClaveSSA varchar(30) Not Null,
	Status varchar(2) Not Null Default 'A',		
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CFG_COM_Perfiles_Personal_ClavesSSA Add Constraint PK_CFG_COM_Perfiles_Personal_ClavesSSA 
	Primary Key ( IdEstado, IdFarmacia, IdPersonal, IdClaveSSA_Sal, ClaveSSA ) 
Go--#SQL

Alter Table CFG_COM_Perfiles_Personal_ClavesSSA Add Constraint FK_CFG_COM_Perfiles_Personal_ClavesSSA_CFG_COM_Perfiles_Personal
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) 
	References CFG_COM_Perfiles_Personal ( IdEstado, IdFarmacia, IdPersonal )
Go--#SQL   