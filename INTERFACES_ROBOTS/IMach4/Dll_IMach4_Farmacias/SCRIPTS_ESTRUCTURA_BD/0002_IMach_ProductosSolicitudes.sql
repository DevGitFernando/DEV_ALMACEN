If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_ProductosSolicitudes' and xType = 'U' ) 
   Drop Table IMach_ProductosSolicitudes 
Go 

Create Table IMach_ProductosSolicitudes 
(
/* 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(8) Not Null, 
*/ 

	NumOrden bigint identity(1,1), 
	IdCliente varchar(4) Not Null, 	
	IdTerminal varchar(3) Not Null, 
	PuertoDeSalida varchar(3) Not Null, 
	
	FolioVenta varchar(14) Not Null,  --- Combinara la Farmacia y el Folio de Venta
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	CantidadSolicitada int Not Null Default 0, 
	CantidadSurtida int Not Null Default 0, 
	
	StatusIMach tinyint Not Null Default -1, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go 

Alter Table IMach_ProductosSolicitudes Add Constraint PK_IMach_ProductosSolicitudes Primary Key ( FolioVenta, IdProducto, CodigoEAN ) 
Go 

Alter Table IMach_ProductosSolicitudes Add Constraint FK_IMach_ProductosSolicitudes_IMach_CFGC_Clientes_Productos 
	Foreign Key ( IdCliente, IdProducto, CodigoEAN ) References IMach_CFGC_Clientes_Productos ( IdCliente, IdProducto, CodigoEAN ) 
Go 
