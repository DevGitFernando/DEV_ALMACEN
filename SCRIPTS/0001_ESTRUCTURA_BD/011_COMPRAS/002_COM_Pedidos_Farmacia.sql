If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_FAR_Pedidos' and xType = 'U' ) 
Begin 
Create Table COM_FAR_Pedidos 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdTipoPedido varchar(2) Not Null, 		
	TipoDePedido int Not Null, 	
	FolioPedido varchar(6) Not Null, 	
	IdPersonal varchar(4) Not Null, 
	FechaSistema datetime Not Null Default convert(varchar(10), getdate(), 120), 	
	FechaRegistro datetime Not Null Default getdate(), 
	Observaciones varchar(200) Not Null Default '', 
	StatusPedido tinyint Not Null Default 0, 
	TipoDeClavesDePedido int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_COM_FAR_Pedidos' and xType = 'PK' ) 
Begin 
	Alter Table COM_FAR_Pedidos Add Constraint PK_COM_FAR_Pedidos 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_COM_FAR_Pedidos_COM_FAR_Pedidos_Tipos_Farmacias' and xType = 'F' ) 
Begin 
	Alter Table COM_FAR_Pedidos Add Constraint FK_COM_FAR_Pedidos_COM_FAR_Pedidos_Tipos_Farmacias 
		Foreign Key ( IdEstado, IdFarmacia, IdTipoPedido ) 
		References COM_FAR_Pedidos_Tipos_Farmacias ( IdEstado, IdFarmacia, IdTipoPedido )  
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_COM_FAR_Pedidos_CatEmpresas' and xType = 'F' ) 
Begin 
	Alter Table COM_FAR_Pedidos Add Constraint FK_COM_FAR_Pedidos_CatEmpresas 
		Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
End 
Go--#SQL 
 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_COM_FAR_Pedidos_CatFarmacias' and xType = 'F' ) 
Begin  
Alter Table COM_FAR_Pedidos Add Constraint FK_COM_FAR_Pedidos_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )  
End 
Go--#SQL  

 
------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_FAR_Pedidos_Productos' and xType = 'U' ) 
Begin 
Create Table COM_FAR_Pedidos_Productos 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdTipoPedido varchar(2) Not Null, 
	FolioPedido varchar(6) Not Null, 			
	IdClaveSSA varchar(4) Not Null, 
	CodigoEAN varchar(30) Not Null, 	
	Cantidad_Pedido int Not Null Default 0, 
	Cantidad_Surtir int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 			
) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_COM_FAR_Pedidos_ContadoCOM_FAR_Pedidos_Productos' and xType = 'PK' ) 
Begin 
	Alter Table COM_FAR_Pedidos_Productos Add Constraint PK_COM_FAR_Pedidos_ContadoCOM_FAR_Pedidos_Productos 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdClaveSSA, CodigoEAN  ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_COM_FAR_Pedidos_Productos_COM_FAR_Pedidos_Contado' and xType = 'F' ) 
Begin 
	Alter Table COM_FAR_Pedidos_Productos Add Constraint FK_COM_FAR_Pedidos_Productos_COM_FAR_Pedidos_Contado 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
		References COM_FAR_Pedidos ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido )
End 
Go--#SQL  
	 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_COM_FAR_Pedidos_Productos_CatClavesSSA_Sales' and xType = 'F' ) 
Begin 	 
	Alter Table COM_FAR_Pedidos_Productos Add Constraint FK_COM_FAR_Pedidos_Productos_CatClavesSSA_Sales 
		Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
End 
Go--#SQL  	 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_COM_FAR_Pedidos_Productos_CatProductos_CodigosRelacionados' and xType = 'F' ) 
Begin 	 
	Alter Table COM_FAR_Pedidos_Productos Add Constraint FK_COM_FAR_Pedidos_Productos_CatProductos_CodigosRelacionados 
		Foreign Key ( CodigoEAN ) 
		References CatProductos_CodigosRelacionados ( CodigoEAN ) 
End 
Go--#SQL 


--------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_FAR_Pedidos_Claves' and xType = 'U' ) 
Begin 
Create Table COM_FAR_Pedidos_Claves 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdTipoPedido varchar(2) Not Null, 
	FolioPedido varchar(6) Not Null, 			
	IdClaveSSA varchar(4) Not Null, 
	--CodigoEAN varchar(30) Not Null, 	
	Cantidad_Pedido int Not Null Default 0, 
	Cantidad_Surtir int Not Null Default 0, 
	CantidadPiezas int Not Null Default 0,  	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 			
) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_COM_FAR_Pedidos_Claves' and xType = 'PK' ) 
Begin 
	Alter Table COM_FAR_Pedidos_Claves Add Constraint PK_COM_FAR_Pedidos_Claves 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdClaveSSA  ) 
End 
Go--#SQL 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_COM_FAR_Pedidos_Claves_COM_FAR_Pedidos' and xType = 'F' ) 
Begin 
	Alter Table COM_FAR_Pedidos_Claves Add Constraint FK_COM_FAR_Pedidos_Claves_COM_FAR_Pedidos
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
		References COM_FAR_Pedidos ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
End 
Go--#SQL  
	 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_COM_FAR_Pedidos_Claves_CatClavesSSA_Sales' and xType = 'F' ) 
Begin 	 
	Alter Table COM_FAR_Pedidos_Claves Add Constraint FK_COM_FAR_Pedidos_Claves_CatClavesSSA_Sales 
		Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
End 
Go--#SQL  	 

----Alter Table COM_FAR_Pedidos_Productos Add Constraint FK_COM_FAR_Pedidos_Productos_CatProductos_CodigosRelacionados 
----	Foreign Key ( CodigoEAN ) 
----	References CatProductos_CodigosRelacionados ( CodigoEAN ) 
----Go--#mnSQL