----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_FirmasUnidad' and xType = 'U' ) 
   Drop Table INT_ND_FirmasUnidad 
Go--#SQL 

Create Table INT_ND_FirmasUnidad 
(	
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	
	NombreEncargado varchar(200) Not Null Default '', 
	NombreDirector varchar(200) Not Null Default '', 	
	NombreAdministrador varchar(200) Not Null Default '', 	
	
	Status varchar(1)  Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 	
Go--#SQL 

Alter Table INT_ND_FirmasUnidad Add Constraint PK_INT_ND_FirmasUnidad Primary Key ( IdEstado, IdFarmacia ) 
Go--#SQL   

