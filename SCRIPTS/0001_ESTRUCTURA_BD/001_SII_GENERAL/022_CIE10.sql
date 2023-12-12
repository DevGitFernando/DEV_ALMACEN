Set NoCount On 
Go--#SQL  

If Exists ( Select Name From Sysobjects Where Name = 'CatCIE10_Grupos' and xType = 'U' )
      Drop Table CatCIE10_Grupos
Go--#SQL 

Create Table CatCIE10_Grupos
(
     IdGrupo int Not Null,
     Claves varchar(30) Not Null,
     Descripcion varchar(300) Not Null,
     nPadre int Null,
     Status varchar(1) Not Null Default 'A',
     Actualizado tinyint Not Null Default 0
)
Go--#SQL 

Alter Table CatCIE10_Grupos Add Constraint PK_CatCIE10_Grupos Primary Key ( IdGrupo ) 
Go--#SQL  	

If Exists ( Select Name From Sysobjects Where Name = 'CatCIE10_Diagnosticos' and xType = 'U' )
      Drop Table CatCIE10_Diagnosticos
Go--#SQL 

Create Table CatCIE10_Diagnosticos
(
     IdDiagnostico varchar(6) Not Null,
     ClaveDiagnostico varchar(4) Not Null Unique,
     Descripcion varchar(300) Not Null,
     cPadre varchar(50) Null,
     nPadre int Null,
     Status varchar(1) Not Null Default 'A',
     Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatCIE10_Diagnosticos Add Constraint PK_CatCIE10_Diagnosticos Primary key ( IdDiagnostico ) 
Go--#SQL  
