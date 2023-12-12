------------------------------------------------ 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_Pedidos_Contado' and xType = 'U' ) 
   Drop Table COM_OCEN_Pedidos_Contado 
Go--#SQL 

If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_PedidosDet' and xType = 'U' ) 
   Drop Table COM_OCEN_PedidosDet 
Go--#SQL 

------------------------------------------------ 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_Pedidos' and xType = 'U' ) 
   Drop Table COM_OCEN_Pedidos 
Go--#SQL 

Create Table COM_OCEN_Pedidos 
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
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table COM_OCEN_Pedidos Add Constraint PK_COM_OCEN_Pedidos 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
Go--#SQL 


Alter Table COM_OCEN_Pedidos Add Constraint FK_COM_OCEN_Pedidos_COM_FAR_Pedidos  
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
	References COM_FAR_Pedidos ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido )  
Go--#SQL 


Alter Table COM_OCEN_Pedidos Add Constraint FK_COM_OCEN_Pedidos_COM_OCEN_AsignacionDePedidosCompradores 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
	References COM_OCEN_AsignacionDePedidosCompradores ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
Go--#SQL 

/* 
------------------------- 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_Pedidos_Contado' and xType = 'U' ) 
   Drop Table COM_OCEN_Pedidos_Contado 
Go 

Create Table COM_OCEN_Pedidos_Contado 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdTipoPedido varchar(2) Not Null, 
	FolioPedido varchar(6) Not Null, 		
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	Cantidad_Pedido int Not Null Default 0, 	
	Cantidad_Surtida int Not Null Default 0, 		
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 			
) 
Go 

Alter Table COM_OCEN_Pedidos_Contado Add Constraint PK_COM_OCEN_Pedidos_Contado 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdProducto, CodigoEAN ) 
Go 

Alter Table COM_OCEN_Pedidos_Contado Add Constraint FK_COM_OCEN_Pedidos_Contado_COM_OCEN_Pedidos_Contado 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
	References COM_OCEN_Pedidos ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido )
Go  	

Alter Table COM_OCEN_Pedidos_Contado Add Constraint FK_COM_OCEN_Pedidos_Contado_COM_FAR_Pedidos_Contado 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdProducto, CodigoEAN ) 
	References COM_FAR_Pedidos_Contado ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdProducto, CodigoEAN )
Go  	
*/

------------------------- 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_PedidosDet' and xType = 'U' ) 
   Drop Table COM_OCEN_PedidosDet 
Go--#SQL 

Create Table COM_OCEN_PedidosDet 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdTipoPedido varchar(2) Not Null, 
	FolioPedido varchar(6) Not Null, 			
	IdClaveSSA varchar(4) Not Null, 
	CodigoEAN varchar(30) Not Null, 	
	Cantidad_Pedido int Not Null Default 0, 
	Cantidad_Surtida int Not Null Default 0, 
	Cantidad_EnviadaCentral int Not Null Default 0, 							
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 			
) 
Go--#SQL 

Alter Table COM_OCEN_PedidosDet Add Constraint PK_COM_OCEN_Pedidos_ContadoCOM_OCEN_PedidosDet 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdClaveSSA, CodigoEAN  ) 
Go--#SQL 

Alter Table COM_OCEN_PedidosDet Add Constraint FK_COM_OCEN_PedidosDet_COM_OCEN_Pedidos_Contado 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido  ) 
	References COM_OCEN_Pedidos ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido )
Go--#SQL  

/* 
Alter Table COM_OCEN_PedidosDet Add Constraint FK_COM_OCEN_PedidosDet_COM_FAR_Pedidos_Contado 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdClaveSSA, CodigoEAN ) 
	References COM_FAR_Pedidos_Credito ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdClaveSSA, CodigoEAN )
Go  
*/ 	 
