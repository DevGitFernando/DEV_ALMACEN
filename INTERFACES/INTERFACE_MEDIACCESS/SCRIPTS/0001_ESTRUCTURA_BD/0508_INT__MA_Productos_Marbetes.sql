If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT__MA_Productos_Marbetes' and xType = 'U' ) 
	Drop Table INT__MA_Productos_Marbetes 
Go--#SQL  

Create Table INT__MA_Productos_Marbetes
(
	IdEstado Varchar(2) Not Null,
	IdFarmacia Varchar(4) Not Null,
	IdProducto varchar(8) Not Null, 
	MarbeteActualizado Bit Not Null default 0,
    FechaUpdate DateTime Not Null Default getdate()
)
Go--#SQL  

Alter Table INT__MA_Productos_Marbetes Add Constraint PK_INT__MA_Productos_Marbetes Primary Key ( IdEstado, IdFarmacia, IdProducto ) 
Go--#SQL

Alter Table INT__MA_Productos_Marbetes Add Constraint FK_INT__MA_Productos_Marbetes_CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )
Go--#SQL

Alter Table INT__MA_Productos_Marbetes Add Constraint FK_INT__MA_Productos_Marbetes_CatProductos
	Foreign Key ( IdProducto ) References CatProductos ( IdProducto )
Go--#SQL