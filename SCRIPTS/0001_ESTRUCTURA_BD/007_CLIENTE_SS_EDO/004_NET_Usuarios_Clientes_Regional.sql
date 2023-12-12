---------------------------------------------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Regional_Usuarios' and xType = 'U' ) 
   Drop Table Net_Regional_Usuarios 
Go--#SQL 

Create Table Net_Regional_Usuarios 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 	
	IdUsuario varchar(4) Not Null, 
	Nombre varchar(200) Not Null Unique, 
	Login varchar(20) Not Null Unique, 
	Password varchar(500) Not Null Default '',  
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL 

Alter Table Net_Regional_Usuarios Add Constraint PK_Net_Regional_Usuarios Primary Key ( IdEstado, IdFarmacia, IdUsuario ) 
Go--#SQL 
