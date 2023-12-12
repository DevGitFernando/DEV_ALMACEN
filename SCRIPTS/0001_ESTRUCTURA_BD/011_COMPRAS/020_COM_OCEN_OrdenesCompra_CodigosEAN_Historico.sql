
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_OrdenesCompra_CodigosEAN_Historico' and xType = 'U' ) 
   Drop Table COM_OCEN_OrdenesCompra_CodigosEAN_Historico  
Go--#SQL 

Create Table COM_OCEN_OrdenesCompra_CodigosEAN_Historico 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	FolioOrden varchar(8) Not Null, 			
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null,
	Cantidad int Not Null Default 0, 	
	Precio Numeric(14,4) Not Null Default 0, 
	Descuento Numeric(14,4) Not Null Default 0, 
	TasaIva Numeric(14,4) Not Null Default 0, 		
	Iva Numeric(14,4) Not Null Default 0,
	PrecioUnitario Numeric(14,4) Not Null Default 0,
	ImpteIva Numeric(14,4) Not Null Default 0, 	
	Importe Numeric(14,4) Not Null Default 0,
	IdPersonalRegistra Varchar(8) Not Null Default '',
	Observaciones varchar(200) Not Null Default '', 
		
	FechaActualizacion datetime Not Null Default getdate(), 	
	
	Keyx int identity(1,1), 
		
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table COM_OCEN_OrdenesCompra_CodigosEAN_Historico 
	Add Constraint PK_COM_OCEN_OrdenesCompra_CodigosEAN_Historico Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdProducto, CodigoEAN, FechaActualizacion ) 
Go--#SQL 

--Alter Table COM_OCEN_OrdenesCompra_CodigosEAN_Historico 
--	Add Constraint FK_COM_OCEN_OrdenesCompra_CodigosEAN_Historico_COM_OCEN_OrdenesCompra_CodigosEAN_Det
--	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdProducto, CodigoEAN ) References COM_OCEN_OrdenesCompra_CodigosEAN_Det 
--Go--#SQL 