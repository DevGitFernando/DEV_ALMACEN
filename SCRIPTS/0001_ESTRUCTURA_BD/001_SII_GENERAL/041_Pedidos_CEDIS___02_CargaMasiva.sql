----------------------------------------------------------------------------------------------   
---------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Pedidos_Cedis__CargaMasiva' and xType = 'U' ) 
   Drop Table Pedidos_Cedis__CargaMasiva 
Go--#SQL   

Create Table Pedidos_Cedis__CargaMasiva
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdPersonal varchar(4) Not Null,
	FolioPedido varchar(6) Not Null Default '',
	TipoPedido Int Not Null Default 1, 
	IdEstadoSolicita varchar(2) Not Null default '', 
	IdFarmaciaSolicita varchar(4) Not Null default '', 
	EsTransferencia bit Not Null Default 1, 
	IdCliente varchar(4) Not Null Default '', 
	IdSubCliente varchar(4) Not Null Default '', 
	IdPrograma varchar(4) Not Null Default '', 
	IdSubPrograma varchar(4) Not Null Default '', 	
	Observaciones varchar(200) Not Null Default '', 
	ReferenciaInterna varchar(100) Not Null Default '' ,
	TipoDeClavesDePedido int Not Null Default 0 ,
	IdBeneficiario varchar(8) Not Null Default '',
	FechaEntrega datetime DEFAULT GetDate()  Not Null,
	IdClaveSSA varchar(4) Not Null Default '',
	ClaveSSA varchar(50) Not Null Default '',  
	ContenidoPaquete int Not Null Default 0, 
	Cantidad int Not Null Default 0,   -- Cajas Completas 
	Existencia numeric(14,4) Not Null Default 0,
	CantidadEnCajas int Not Null Default 0,
	GUID Varchar(200) Not Null Default '',
	FechaRegistro DateTime Not Null Default GetDate()
) 
Go--#SQL  