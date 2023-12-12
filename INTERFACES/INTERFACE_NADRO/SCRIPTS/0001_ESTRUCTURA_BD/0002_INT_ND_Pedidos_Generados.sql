------------------------------------------------------------------------------------------------------------------------------------------------  
------------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'INT_ND_Pedidos_Generados_Detalles' and xType = 'U' ) 
   Drop Table INT_ND_Pedidos_Generados_Detalles 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'INT_ND_Pedidos_Generados' and xType = 'U' ) 
   Drop Table INT_ND_Pedidos_Generados   
Go--#SQL 

Create Table INT_ND_Pedidos_Generados 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	FolioPedido varchar(8) Not Null, 
	
	Fecha varchar(8) Not Null, 
	Consecutivo varchar(3) Not Null, 

	IdPersonal varchar(4) Not Null, 
	FechaRegistro datetime Not Null Default getdate(), 
	
	Status varchar(2) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table INT_ND_Pedidos_Generados Add Constraint PK_INT_ND_Pedidos_Generados 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
Go--#SQL 

Alter Table INT_ND_Pedidos_Generados Add Constraint FK_INT_ND_Pedidos_Generados___CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL

Alter Table INT_ND_Pedidos_Generados Add Constraint FK_INT_ND_Pedidos_Generados___CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table INT_ND_Pedidos_Generados Add Constraint FK_INT_ND_Pedidos_Generados___CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  



------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'INT_ND_Pedidos_Generados_Detalles' and xType = 'U' ) 
   Drop Table INT_ND_Pedidos_Generados_Detalles 
Go--#SQL 

Create Table INT_ND_Pedidos_Generados_Detalles  
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	FolioPedido varchar(8) Not Null, 
	
	IdFarmaciaControl varchar(4) Not Null, 	
	IdFarmaciaSolicita varchar(4) Not Null, 
	FolioPedidoSolicita varchar(8) Not Null, 
	
	Status varchar(2) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 	
) 
Go--#SQL 

Alter Table INT_ND_Pedidos_Generados_Detalles Add Constraint PK_INT_ND_Pedidos_Generados_Detalles  
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdFarmaciaControl, IdFarmaciaSolicita, FolioPedidoSolicita ) 
Go--#SQL 

Alter Table INT_ND_Pedidos_Generados_Detalles Add Constraint FK_INT_ND_Pedidos_Generados_Detalles___INT_ND_Pedidos_Generados 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido ) 
	References INT_ND_Pedidos_Generados ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido )
Go--#SQL

