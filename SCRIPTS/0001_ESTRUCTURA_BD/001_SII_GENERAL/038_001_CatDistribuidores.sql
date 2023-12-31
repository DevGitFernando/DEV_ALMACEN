----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatDistribuidores_Clientes' and xType = 'U' ) 
   Drop Table CatDistribuidores_Clientes 
Go--#SQL 


----------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatDistribuidores' and xType = 'U' ) 
	Drop Table CatDistribuidores 
Go--#SQL 

Create Table CatDistribuidores 
(
	-- IdEstado varchar(2) Not Null,
	IdDistribuidor varchar(4) Not Null, 
	NombreDistribuidor varchar(100) Not Null, 	
	Status varchar(1)  Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 	
) 
Go--#SQL 

Alter Table CatDistribuidores Add Constraint PK_CatDistribuidores Primary Key ( IdDistribuidor ) 
Go--#SQL 


----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CatDistribuidores_Clientes' and xType = 'U' ) 
   Drop Table CatDistribuidores_Clientes 
Go--#SQL 

Create Table CatDistribuidores_Clientes 
(
	IdEstado varchar(2) Not Null,
	IdDistribuidor varchar(4)  Not Null,
	CodigoCliente varchar(20)  Not Null,
	NombreCliente varchar(200)  Not Null,
	IdFarmacia varchar(4)  Not Null Default '',
	Status varchar(1)  Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 	
Go--#SQL 

Alter Table CatDistribuidores_Clientes Add Constraint PK_CatDistribuidores_Clientes Primary Key ( IdEstado, IdDistribuidor, CodigoCliente ) 
Go--#SQL 

Alter Table CatDistribuidores_Clientes Add Constraint FK_CatDistribuidores_Clientes__CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL 

Alter Table CatDistribuidores_Clientes Add Constraint FK_CatDistribuidores_Clientes__CatDistribuidores
	Foreign Key ( IdDistribuidor ) References CatDistribuidores ( IdDistribuidor ) 
Go--#SQL 




