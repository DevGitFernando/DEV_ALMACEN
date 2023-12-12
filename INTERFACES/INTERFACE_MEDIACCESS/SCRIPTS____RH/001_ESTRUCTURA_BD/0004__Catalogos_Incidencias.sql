
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonal_Incidencias' and xType = 'U' ) 
Drop Table CatPersonal_Incidencias
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatIncidencias' and xType = 'U' ) 
Drop Table CatIncidencias
Go--#SQL 

Create Table CatIncidencias  
(
	IdIncidencia varchar(2) Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 
    
Alter Table CatIncidencias Add Constraint PK_CatIncidencias Primary key ( IdIncidencia ) 
Go--#SQL     


----------------------------------------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonal_Incidencias' and xType = 'U' ) 
Drop Table CatPersonal_Incidencias
Go--#SQL 

Create Table CatPersonal_Incidencias  
(
	IdPersonal varchar(8) Not Null,
	IdIncidencia varchar(2) Not Null, 
	FechaInicio datetime Not Null,
	FechaFin datetime Not Null,
	FechaRegistro datetime Not Null Default GetDate(), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 
    
Alter Table CatPersonal_Incidencias Add Constraint PK_CatPersonal_Incidencias Primary key ( IdPersonal, IdIncidencia, FechaInicio, FechaFin ) 
Go--#SQL     


Alter Table CatPersonal_Incidencias Add Constraint FK_CatPersonal_Incidencias_CatPersonal 
	Foreign Key ( IdPersonal ) References CatPersonal ( IdPersonal ) 
Go--#SQL 

Alter Table CatPersonal_Incidencias Add Constraint FK_CatPersonal_Incidencias_CatIncidencias
	Foreign Key ( IdIncidencia ) References CatIncidencias ( IdIncidencia ) 
Go--#SQL 

