----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_Remisiones_Separar' and xType = 'U' ) 
   Drop Table INT_ND_CFG_Remisiones_Separar 
Go--#SQL 

Create Table INT_ND_CFG_Remisiones_Separar 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioRemision varchar(100) Not Null, 
	Status varchar(1) Not Null Default '' 
) 	
Go--#SQL 

Alter Table INT_ND_CFG_Remisiones_Separar Add Constraint PK_INT_ND_CFG_Remisiones_Separar Primary Key  ( IdEstado, IdFarmacia, FolioRemision ) 
Go--#SQL 

Alter Table INT_ND_CFG_Remisiones_Separar Add Constraint FK_INT_ND_CFG_Remisiones_Separar__CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 


Insert Into INT_ND_CFG_Remisiones_Separar ( IdEstado, IdFarmacia, FolioRemision, Status ) Select '16', '0042', '1.1.1/01/040215/RN', 'A' 
Go--#SQL 

