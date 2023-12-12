
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_AsignacionDePedidosCompradores' and xType = 'U' ) 
   Drop Table COM_OCEN_AsignacionDePedidosCompradores 
Go--#SQL 

Create Table COM_OCEN_AsignacionDePedidosCompradores 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null,
	 
	IdFarmacia varchar(4) Not Null, 
	IdTipoPedido varchar(2) Not Null, 		
	FolioPedido varchar(6) Not Null, 	
	IdPersonal varchar(4) Not Null,

	IdEstadoRegistra varchar(2) Not Null,
	IdFarmaciaRegistra varchar(4) Not Null,
	IdPersonalRegistra varchar(4) Not Null,
 
	FechaRegistro datetime Not Null Default getdate(), 
	Observaciones varchar(200) Not Null Default '', 
	StatusPedido tinyint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 		
) 
Go--#SQL 

Alter Table COM_OCEN_AsignacionDePedidosCompradores Add Constraint PK_COM_OCEN_AsignacionDePedidosCompradores 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
Go--#SQL 

Alter Table COM_OCEN_AsignacionDePedidosCompradores Add Constraint FK_COM_OCEN_AsignacionDePedidosCompradores_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table COM_OCEN_AsignacionDePedidosCompradores Add Constraint FK_COM_OCEN_AsignacionDePedidosCompradores_CatPersonalRegistra 
	Foreign Key ( IdEstadoRegistra, IdFarmaciaRegistra, IdPersonalRegistra ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL 

/* 
Alter Table COM_OCEN_AsignacionDePedidosCompradores Add Constraint FK_COM_OCEN_AsignacionDePedidosCompradores_COM_OCEN_Pedidos 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido ) 
	References COM_OCEN_Pedidos ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido )  
Go 
*/ 