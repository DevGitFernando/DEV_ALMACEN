----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_Servicios' and xType = 'U' ) 
   Drop Table INT_ND_CFG_Servicios 
Go--#SQL 

Create Table INT_ND_CFG_Servicios 
( 
	IdEstado varchar(2) Not Null, 
	IdTipoUnidad varchar(3) Not Null DEfault '', 
	IdServicio varchar(3) Not Null Default '', 
	FechaInicial varchar(10) Not Null Default '', 
	FechaFinal varchar(10) Not Null Default '', 	
	Status varchar(1) Not Null Default '' 
) 	
Go--#SQL 

Alter Table INT_ND_CFG_Servicios Add Constraint PK_INT_ND_CFG_Servicios Primary Key  ( IdEstado, IdTipoUnidad ) 
Go--#SQL 



