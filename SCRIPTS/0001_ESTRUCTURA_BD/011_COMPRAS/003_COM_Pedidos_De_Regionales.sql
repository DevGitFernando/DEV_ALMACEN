------------------------------------------------ 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_REG_Pedidos_Productos' and xType = 'U' ) 
   Drop Table COM_REG_Pedidos_Productos 
Go--#SQL 

------------------------------------------------ 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_REG_Pedidos' and xType = 'U' ) 
   Drop Table COM_REG_Pedidos 
Go--#SQL 

Create Table COM_REG_Pedidos 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdTipoPedido varchar(2) Not Null, 		
	FolioPedido varchar(6) Not Null, 	
	IdPersonal varchar(4) Not Null, 
	FechaSistema datetime Not Null Default convert(varchar(10), getdate(), 120), 	
	FechaRegistro datetime Not Null Default getdate(), 
	Observaciones varchar(200) Not Null Default '', 
	StatusPedido tinyint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table COM_REG_Pedidos Add Constraint PK_COM_REG_Pedidos 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
Go--#SQL 

Alter Table COM_REG_Pedidos Add Constraint PK_COM_REG_Pedidos_COM_FAR_Pedidos_Tipos_Farmacias 
	Foreign Key ( IdEstado, IdFarmacia, IdTipoPedido ) References COM_FAR_Pedidos_Tipos_Farmacias ( IdEstado, IdFarmacia, IdTipoPedido )  
Go--#SQL 

Alter Table COM_REG_Pedidos Add Constraint PK_COM_REG_Pedidos_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
Go--#SQL 
 
Alter Table COM_REG_Pedidos Add Constraint PK_COM_REG_Pedidos_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )  
Go--#SQL  

 
------------------------- 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_REG_Pedidos_Productos' and xType = 'U' ) 
   Drop Table COM_REG_Pedidos_Productos 
Go--#SQL 

Create Table COM_REG_Pedidos_Productos 
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
Go--#SQL 

Alter Table COM_REG_Pedidos_Productos Add Constraint PK_COM_REG_Pedidos_ContadoCOM_REG_Pedidos_Productos 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdClaveSSA, CodigoEAN  ) 
Go--#SQL 

----Alter Table COM_REG_Pedidos_Productos Add Constraint FK_COM_REG_Pedidos_Productos_COM_REG_Pedidos_Contado 
----	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
----	References COM_REG_Pedidos ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido )
----Go  
	 
Alter Table COM_REG_Pedidos_Productos Add Constraint FK_COM_REG_Pedidos_Productos_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal )
Go--#SQL  	 

Alter Table COM_REG_Pedidos_Productos Add Constraint FK_COM_REG_Pedidos_Productos_CatProductos_CodigosRelacionados 
	Foreign Key ( CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( CodigoEAN ) 
Go--#SQL