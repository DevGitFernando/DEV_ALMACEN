----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_CodigosEAN' and xType = 'U' ) 
   Drop Table INT_ND_CFG_CodigosEAN 
Go--#SQL 

Create Table INT_ND_CFG_CodigosEAN 
( 
	IdEstado varchar(2) Not Null, 
	CodigoEAN varchar(30) Not Null, 	
	CodigoEAN_ND varchar(30) Not Null, 
	Status varchar(1) Not Null Default '' 
) 	
Go--#SQL 

Alter Table INT_ND_CFG_CodigosEAN Add Constraint PK_INT_ND_CFG_CodigosEAN Primary Key  ( IdEstado, CodigoEAN, CodigoEAN_ND ) 
Go--#SQL 

Alter Table INT_ND_CFG_CodigosEAN Add Constraint FK_INT_ND_CFG_CodigosEAN__CatProductos_CodigosRelacionados 
	Foreign Key ( CodigoEAN ) References CatProductos_CodigosRelacionados ( CodigoEAN ) 
Go--#SQL 


----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_CodigosEAN_Descomponer' and xType = 'U' ) 
   Drop Table INT_ND_CFG_CodigosEAN_Descomponer 
Go--#SQL 

Create Table INT_ND_CFG_CodigosEAN_Descomponer 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	CodigoEAN varchar(30) Not Null, 	
	Factor int Not Null Default 0, 
	Status varchar(1) Not Null Default '' 
) 	
Go--#SQL 

Alter Table INT_ND_CFG_CodigosEAN_Descomponer Add Constraint PK_ Primary Key  ( IdEstado, IdFarmacia, CodigoEAN ) 
Go--#SQL 

