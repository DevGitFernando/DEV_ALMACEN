If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CteReg_Productos_Movimientos_Secretaria_Detallado' and xType = 'U' ) 
	Drop Table CteReg_Productos_Movimientos_Secretaria_Detallado 
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CteReg_Productos_Movimientos_Secretaria_Concentrado' and xType = 'U' ) 
	Drop Table CteReg_Productos_Movimientos_Secretaria_Concentrado 
Go--#SQL  

Create Table CteReg_Productos_Movimientos_Secretaria_Concentrado 
( 
	Año int Not Null Default 0,
	Mes int Not Null Default 0,
	IdEmpresa varchar(3) Not Null Default '',
	IdEstado varchar(2) Not Null Default '',
	ClaveSSA varchar(50) Not Null Default '',
	InventarioInicial int Null Default 0, 
	EntradasDisur int Null Default 0, 
	EntradasFarmaco int Null Default 0, 
	VentasDisur int Null Default 0, 
	VentasFarmaco int Null Default 0,
	DiferenciaLogica int Null Default 0, 
	TipoDeClave int Not Null Default 0,
	TipoDeClaveDescripcion varchar(50) Not Null Default ''
)
Go--#SQL 

Alter Table CteReg_Productos_Movimientos_Secretaria_Concentrado Add Constraint PK_CteReg_Productos_Movimientos_Secretaria_Concentrado 
	Primary Key ( Año, Mes, IdEmpresa, IdEstado, ClaveSSA )
Go--#SQL  

Alter Table CteReg_Productos_Movimientos_Secretaria_Concentrado Add Constraint FK_CteReg_Productos_Movimientos_Secretaria_Concentrado_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )
Go--#SQL  

------------------------------------------------------------

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CteReg_Productos_Movimientos_Secretaria_Detallado' and xType = 'U' ) 
	Drop Table CteReg_Productos_Movimientos_Secretaria_Detallado 
Go--#SQL  

Create Table CteReg_Productos_Movimientos_Secretaria_Detallado 
( 
	Año int Not Null Default 0,
	Mes int Not Null Default 0,
	IdEmpresa varchar(3) Not Null Default '',
	IdEstado varchar(2) Not Null Default '',
	IdFarmacia varchar(4) Not Null Default '',
	IdSubFarmacia varchar(2) Not Null Default '',
	IdProducto varchar(8) Not Null Default '',
	CodigoEAN varchar(30) Not Null Default '',	
	ClaveLote varchar(30) Not Null Default '',
	InventarioInicial int Null Default 0, 
	EntradasDisur int Null Default 0, 
	EntradasFarmaco int Null Default 0, 
	VentasDisur int Null Default 0, 
	VentasFarmaco int Null Default 0
)
Go--#SQL 

Alter Table CteReg_Productos_Movimientos_Secretaria_Detallado Add Constraint PK_CteReg_Productos_Movimientos_Secretaria_Detallado 
	Primary Key ( Año, Mes, IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote )
Go--#SQL  

Alter Table CteReg_Productos_Movimientos_Secretaria_Detallado Add Constraint FK_CteReg_Productos_Movimientos_Secretaria_Detallado_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )
Go--#SQL 

Alter Table CteReg_Productos_Movimientos_Secretaria_Detallado Add Constraint FK_CteReg_Productos_Movimientos_Secretaria_Detallado_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )
Go--#SQL 

Alter Table CteReg_Productos_Movimientos_Secretaria_Detallado Add Constraint FK_CteReg_Productos_Movimientos_Secretaria_Detallado_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL 

