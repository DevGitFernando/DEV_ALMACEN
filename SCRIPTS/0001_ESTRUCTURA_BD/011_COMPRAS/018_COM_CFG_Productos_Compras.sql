If Exists ( Select * From Sysobjects (NoLock) Where Name = 'COM_CFG_Productos_Compras' and xType = 'U' ) 
   Drop Table COM_CFG_Productos_Compras 
Go--#SQL 

Create Table COM_CFG_Productos_Compras 
(
	IdEstado varchar(2) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	Status varchar(1) Not Null Default 'A' 
)    
Go--#SQL 

Alter Table COM_CFG_Productos_Compras Add Constraint PK_COM_CFG_Productos_Compras Primary Key ( IdEstado, IdProducto, CodigoEAN ) 
Go--#SQL 
