If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatCajeros' and xType = 'U' ) 
	Drop Table CatCajeros 
Go--#SQL  

Create Table CatCajeros 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null,
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CatCajeros Add Constraint PK_CatCajeros Primary Key ( IdEstado, IdFarmacia, IdPersonal )
Go--#SQL  

Alter Table CatCajeros Add Constraint FK_CatCajeros_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatCajas' and xType = 'U' ) 
	Drop Table CatCajas 
Go--#SQL  

Create Table CatCajas 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdCaja varchar(2) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CatCajas Add Constraint PK_CatCajas Primary Key ( IdEstado, IdFarmacia, IdCaja )
Go--#SQL  

Alter Table CatCajas Add Constraint FK_CatCajas_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 
