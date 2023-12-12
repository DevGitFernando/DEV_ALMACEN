If Exists ( Select * From sysobjects (NoLock) Where Name = 'CNT_CatBancos' and xType = 'U'  ) 
   Drop Table CNT_CatBancos 
Go--#SQL 

Create Table CNT_CatBancos 
(
	IdBanco varchar(3) Not Null, 		
	Descripcion varchar(500) Not Null, 
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL  

Alter Table CNT_CatBancos Add Constraint PK_CNT_CatBancos
	Primary Key ( IdBanco ) 
Go--#SQL