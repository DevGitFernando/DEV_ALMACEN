
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonal_Prestaciones' and xType = 'U' ) 
Drop Table CatPersonal_Prestaciones
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPrestacionesAdicionales' and xType = 'U' ) 
Drop Table CatPrestacionesAdicionales
Go--#SQL 

Create Table CatPrestacionesAdicionales  
(
	IdPrestacion varchar(2) Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 
    
Alter Table CatPrestacionesAdicionales Add Constraint PK_CatPrestacionesAdicionales Primary key ( IdPrestacion ) 
Go--#SQL     


----------------------------------------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonal_Prestaciones' and xType = 'U' ) 
Drop Table CatPersonal_Prestaciones
Go--#SQL 

Create Table CatPersonal_Prestaciones  
(
	IdPersonal varchar(8) Not Null,
	IdPrestacion varchar(2) Not Null, 	
	FechaRegistro datetime Not Null Default GetDate(),
	FechaModificacion datetime Not Null Default GetDate(), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 
    
Alter Table CatPersonal_Prestaciones Add Constraint PK_CatPersonal_Prestaciones Primary key ( IdPersonal, IdPrestacion ) 
Go--#SQL     


Alter Table CatPersonal_Prestaciones Add Constraint FK_CatPersonal_Prestaciones_CatPersonal 
	Foreign Key ( IdPersonal ) References CatPersonal ( IdPersonal ) 
Go--#SQL 

Alter Table CatPersonal_Prestaciones Add Constraint FK_CatPersonal_Prestaciones_CatPrestacionesAdicionales
	Foreign Key ( IdPrestacion ) References CatPrestacionesAdicionales ( IdPrestacion ) 
Go--#SQL 

