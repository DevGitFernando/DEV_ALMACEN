If Exists( Select Name From SysObjects(NoLock) Where Name = 'Ventas_ALMJ_PedidosRC_Surtido' And xType = 'U' )
	Drop Table Ventas_ALMJ_PedidosRC_Surtido 
Go--#SQL 

Create Table Ventas_ALMJ_PedidosRC_Surtido 
(
	IdEmpresaRC varchar(3) Not Null, 
	IdEstadoRC varchar(2) Not Null, 
	IdFarmaciaRC varchar(4) Not Null, 
	FolioPedidoRC varchar(6) Not Null, 
	
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null, 
	CantidadSurtida numeric(14, 4) Not Null Default 0, 

	Status varchar(1) Not Null Default 'A',  
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table Ventas_ALMJ_PedidosRC_Surtido Add Constraint PK_Ventas_ALMJ_PedidosRC_Surtido 
	Primary Key ( IdEmpresaRC, IdEstadoRC, IdFarmaciaRC, FolioPedidoRC, IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL  	

Alter Table Ventas_ALMJ_PedidosRC_Surtido Add Constraint FK_Ventas_ALMJ_PedidosRC_Surtido_ALMJ_Pedidos_RC 
	Foreign Key ( IdEmpresaRC, IdEstadoRC, IdFarmaciaRC, FolioPedidoRC ) 
	References ALMJ_Pedidos_RC ( IdEmpresa, IdEstado, IdFarmacia, FolioPedidoRC ) 
Go--#SQL   

Alter Table Ventas_ALMJ_PedidosRC_Surtido Add Constraint FK_Ventas_ALMJ_PedidosRC_Surtido_VentasEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
	References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta )  
Go--#SQL  
