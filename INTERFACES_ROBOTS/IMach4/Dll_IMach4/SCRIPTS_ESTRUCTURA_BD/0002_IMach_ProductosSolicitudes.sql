If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_SolicitudesProductos' and xType = 'U' ) 
   Drop Table IMach_SolicitudesProductos 
Go--#SQL   

---------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_Solicitudes' and xType = 'U' ) 
   Drop Table IMach_Solicitudes 
Go--#SQL   

Create Table IMach_Solicitudes 
( 
	FolioSolicitud varchar(8) Not Null, 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(8) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate(), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL   


Alter Table IMach_Solicitudes Add Constraint PK_IMach_Solicitudes Primary Key ( FolioSolicitud )
Go--#SQL   

----------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IMach_SolicitudesProductos' and xType = 'U' ) 
   Drop Table IMach_SolicitudesProductos 
Go--#SQL   

Create Table IMach_SolicitudesProductos 
(
	IdCliente varchar(4) Not Null, 		
	FolioSolicitud varchar(8) Not Null, 
	Consecutivo bigint Not Null Unique, --- identity(1,1), 
	
	IdTerminal varchar(3) Not Null, 
	PuertoDeSalida varchar(3) Not Null, 		
	
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	CantidadSolicitada int Not Null Default 0, 
	CantidadSurtida int Not Null Default 0, 
	
	FechaRegistro datetime Not Null Default getdate(), 
	FechaSurtido datetime Not Null Default getdate(), 	
	
	StatusIMach smallint Not Null Default -2,  ---> -2 Registrado, -1 Solicitado 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL   

Alter Table IMach_SolicitudesProductos Add Constraint PK_IMach_SolicitudesProductos Primary Key ( FolioSolicitud, Consecutivo, IdProducto, CodigoEAN ) 
Go--#SQL  

Alter Table IMach_SolicitudesProductos Add Constraint PK_IMach_SolicitudesProductos_IMach_Solicitudes  
	Foreign Key ( FolioSolicitud ) References IMach_Solicitudes ( FolioSolicitud ) 
Go--#SQL  

Alter Table IMach_SolicitudesProductos Add Constraint FK_IMach_SolicitudesProductos_IMach_CFGC_Clientes_Productos 
	Foreign Key ( IdCliente, IdProducto, CodigoEAN ) References IMach_CFGC_Clientes_Productos ( IdCliente, IdProducto, CodigoEAN ) 
Go--#SQL   
