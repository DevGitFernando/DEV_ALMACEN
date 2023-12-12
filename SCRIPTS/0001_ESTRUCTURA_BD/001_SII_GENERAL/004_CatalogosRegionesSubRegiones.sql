If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSubRegiones' and xType = 'U' )
	Drop Table CatSubRegiones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSubFamiliaSegmentos' and xType = 'U' )
	Drop Table CatSubFamiliaSegmentos  
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSubFamilias' and xType = 'U' )
	Drop Table CatSubFamilias 
Go--#SQL


----------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatRegiones' and xType = 'U' )
	Drop Table CatRegiones 
Go--#SQL 

Create Table CatRegiones 
(
	IdRegion varchar(2) Not Null, 
	Descripcion varchar(50) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatRegiones Add Constraint PK_CatRegiones Primary Key ( IdRegion )
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSubRegiones' and xType = 'U' )
	Drop Table CatSubRegiones 
Go--#SQL 

Create Table CatSubRegiones 
(
	IdRegion varchar(2) Not Null, 
	IdSubRegion varchar(2) Not Null, 
	Descripcion varchar(50) Not Null Default '', 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table CatSubRegiones Add Constraint PK_CatSubRegiones Primary Key ( IdRegion, IdSubRegion )
Go--#SQL

Alter Table CatSubRegiones Add Constraint FK_CatRegiones_CatSubRegiones 
	Foreign Key ( IdRegion ) References CatRegiones ( IdRegion ) 
Go--#SQL


------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatFamilias' and xType = 'U' )
	Drop Table CatFamilias 
Go--#SQL 

Create Table CatFamilias 
(
	IdFamilia varchar(2) Not Null, 
	Descripcion varchar(50) Not Null Default '', 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatFamilias Add Constraint PK_CatFamilias Primary Key ( IdFamilia )
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSubFamilias' and xType = 'U' )
	Drop Table CatSubFamilias 
Go--#SQL 

Create Table CatSubFamilias 
(
	IdFamilia varchar(2) Not Null, 
	IdSubFamilia varchar(2) Not Null, 
	Descripcion varchar(50) Not Null Default '', 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table CatSubFamilias Add Constraint PK_CatSubFamilias Primary Key ( IdFamilia, IdSubFamilia )
Go--#SQL

Alter Table CatSubFamilias Add Constraint FK_CatFamilias_CatSubFamilias 
	Foreign Key ( IdFamilia ) References CatFamilias ( IdFamilia ) 
Go--#SQL


----
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSegmentosSubFamilias' and xType = 'U' )
	Drop Table CatSegmentosSubFamilias  
Go--#SQL 

Create Table CatSegmentosSubFamilias 
(
	IdFamilia varchar(2) Not Null, 
	IdSubFamilia varchar(2) Not Null, 
	IdSegmento varchar(2) Not Null, 	
	Descripcion varchar(50) Not Null Default '', 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table CatSegmentosSubFamilias Add Constraint PK_CatSegmentosSubFamilias Primary Key ( IdFamilia, IdSubFamilia, IdSegmento )
Go--#SQL

Alter Table CatSegmentosSubFamilias Add Constraint FK_CatSubFamilias_CatSegmentosSubFamilias 
	Foreign Key ( IdFamilia, IdSubFamilia ) References CatSubFamilias ( IdFamilia, IdSubFamilia ) 
Go--#SQL