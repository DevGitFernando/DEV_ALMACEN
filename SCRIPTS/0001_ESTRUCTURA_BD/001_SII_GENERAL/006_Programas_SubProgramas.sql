If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSubProgramas_Farmacias' and xType = 'U' )
	Drop Table CatSubProgramas_Farmacias 
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSubProgramas' and xType = 'U' )
	Drop Table CatSubProgramas 
Go--#SQL

----------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProgramas' and xType = 'U' )
	Drop Table CatProgramas  
Go--#SQL 

Create Table CatProgramas 
(
	IdPrograma varchar(4) Not Null, 
	Descripcion varchar(250) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL

Alter Table CatProgramas Add Constraint PK_CatProgramas Primary Key ( IdPrograma )
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSubProgramas' and xType = 'U' )
	Drop Table CatSubProgramas 
Go--#SQL

Create Table CatSubProgramas 
(
	IdPrograma varchar(4) Not Null, 
	IdSubPrograma varchar(4) Not Null, 
	Descripcion varchar(250) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatSubProgramas Add Constraint PK_CatSubProgramas Primary Key ( IdPrograma, IdSubPrograma )
Go--#SQL 

Alter Table CatSubProgramas Add Constraint FK_CatProgramas_CatSubProgramas 
	Foreign Key ( IdPrograma ) References CatProgramas ( IdPrograma ) 
Go--#SQL
---------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSubProgramas_Farmacias' and xType = 'U' )
	Drop Table CatSubProgramas_Farmacias 
Go--#SQL

Create Table CatSubProgramas_Farmacias 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPrograma varchar(4) Not Null, 
	IdSubPrograma varchar(4) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatSubProgramas_Farmacias Add Constraint PK_CatSubProgramas_Farmacias Primary Key ( IdEstado, IdFarmacia, IdPrograma, IdSubPrograma )
Go--#SQL 

Alter Table CatSubProgramas_Farmacias Add Constraint FK_CatFarmacias_CatSubProgramas_Farmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL

Alter Table CatSubProgramas_Farmacias Add Constraint FK_CatSubProgramas_CatSubProgramas_Farmacias 
	Foreign Key ( IdPrograma, IdSubPrograma ) References CatSubProgramas ( IdPrograma, IdSubPrograma ) 
Go--#SQL
