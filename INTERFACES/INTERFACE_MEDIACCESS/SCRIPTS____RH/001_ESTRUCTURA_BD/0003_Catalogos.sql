



----------------------------------------------------------------------------------------------     
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatTipoDocumentos' and xType = 'U' ) 
   Drop Table CatTipoDocumentos 
Go--#SQL 

Create Table CatTipoDocumentos  
(
	IdDocumento varchar(2) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 
    
Alter Table CatTipoDocumentos Add Constraint PK_CatTipoDocumentos Primary key ( IdDocumento ) 
Go--#SQL     


----------------------------------------------------------------------------------------------

----------------------------------------------------------------------------------------------     
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatPersonal_Doctos' and xType = 'U' ) 
   Drop Table CatPersonal_Doctos 
Go--#SQL 

Create Table CatPersonal_Doctos 
(
	IdPersonal varchar(8) Not Null Default '',	
	IdDocumento varchar(2) Not Null Default '',
	NombreArchivo varchar(200) Not Null Default '',
	Archivo text Not Null Default '',
	Status varchar(1) Not Null Default 'A',				--- 
	Actualizado tinyint Not Null Default 0				--- 
) 
Go--#SQL 

Alter Table CatPersonal_Doctos Add Constraint PK_CatPersonal_Doctos Primary Key ( IdPersonal, IdDocumento ) 
Go--#SQL 


Alter Table CatPersonal_Doctos Add Constraint FK_CatPersonal_Doctos_CatPersonal 
	Foreign Key ( IdPersonal ) References CatPersonal ( IdPersonal ) 
Go--#SQL 

Alter Table CatPersonal_Doctos Add Constraint FK_CatPersonal_Doctos_CatTipoDocumentos
	Foreign Key ( IdDocumento ) References CatTipoDocumentos ( IdDocumento ) 
Go--#SQL 

----------------------------------------------------------------------------------------------     



----------------------------------------------------------------------------------------------     
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatActasAdministrativas' and xType = 'U' ) 
   Drop Table CatActasAdministrativas 
Go--#SQL 

Create Table CatActasAdministrativas 
(
	IdActa varchar(8) Not Null,
	IdEstado varchar(2) Not Null Default '',			--- PLAZA 
	IdFarmacia varchar(4) Not Null Default '', 			--- PLAZA
	IdPersonal_Acta varchar(8) Not Null Default '',	
	IdPersonal_Representante varchar(8) Not Null Default '',
	IdPersonal_Testigo_01 varchar(8) Not Null Default '',
	IdPersonal_Testigo_02 varchar(8) Not Null Default '',	
	FechaActa datetime Not Null Default getDate(),
	FechaRegistro datetime Not Null Default getDate(),
	Hechos  varchar(1000) Not Null Default '',
	Status varchar(1) Not Null Default 'A',				
	Actualizado tinyint Not Null Default 0				
) 
Go--#SQL 

Alter Table CatActasAdministrativas Add Constraint PK_CatActasAdministrativas Primary Key ( IdActa ) 
Go--#SQL 


Alter Table CatActasAdministrativas Add Constraint FK_CatActasAdministrativas_CatPersonal 
	Foreign Key ( IdPersonal_Acta ) References CatPersonal ( IdPersonal ) 
Go--#SQL 

Alter Table CatActasAdministrativas Add Constraint FK_CatActasAdministrativas_CatPersonal_Representante 
	Foreign Key ( IdPersonal_Representante ) References CatPersonal ( IdPersonal ) 
Go--#SQL 

Alter Table CatActasAdministrativas Add Constraint FK_CatActasAdministrativas_CatPersonal_Testigo_01 
	Foreign Key ( IdPersonal_Testigo_01 ) References CatPersonal ( IdPersonal ) 
Go--#SQL 

Alter Table CatActasAdministrativas Add Constraint FK_CatActasAdministrativas_CatPersonal_Testigo_02 
	Foreign Key ( IdPersonal_Testigo_02 ) References CatPersonal ( IdPersonal ) 
Go--#SQL 