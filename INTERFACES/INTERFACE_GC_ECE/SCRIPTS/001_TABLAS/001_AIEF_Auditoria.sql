If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AIEF_ADT_Accesos' and xType = 'U' )
   Drop Table AIEF_ADT_Accesos 
Go--#SQL   
 
----------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AIEF_ADT_Accesos' and xType = 'U' )
   Drop Table AIEF_ADT_Accesos 
Go--#SQL   

Create Table AIEF_ADT_Accesos 
(
	IdAccesoExterno varchar(4) Not Null, 
	FechaRegistro datetime Not Null Default getdate(),  
	
	NumOpcion int Not Null Default 0, 
	Sentencia varchar(500) Not Null Default '', 

	MAC varchar(20) Not Null Default '', 
	NombreHost varchar(100) Not Null Default '', 
	Keyx int identity  	
)
Go--#SQL

Alter Table AIEF_ADT_Accesos Add Constraint PK_AIEF_ADT_Accesos Primary Key ( IdAccesoExterno, FechaRegistro ) 
Go--#SQL 

----------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'AIEF_ADT_Existencia_Claves' and xType = 'U' )
   Drop Table AIEF_ADT_Existencia_Claves 
Go--#SQL   

Create Table AIEF_ADT_Existencia_Claves 
(
	FechaRegistro datetime Not Null Default getdate(),  
	ClaveSSA varchar(30) Not Null Default '', 	

	MAC varchar(20) Not Null Default '', 
	NombreHost varchar(100) Not Null Default '', 
	Keyx int identity  	
)
Go--#SQL

Alter Table AIEF_ADT_Existencia_Claves Add Constraint PK_AIEF_ADT_Existencia_Claves Primary Key ( FechaRegistro, ClaveSSA ) 
Go--#SQL 