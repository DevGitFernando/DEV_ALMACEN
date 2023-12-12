If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_SobrePrecioEnc' and xType = 'U' ) 
   Drop Table COM_OCEN_SobrePrecioEnc
Go--#SQL  

Create Table COM_OCEN_SobrePrecioEnc
( 
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	FolioOrden varchar(8) Not Null,
	IdProducto Varchar(8) Not Null,
	CodigoEAN Varchar(30) Not Null,
	PrecioCaja Numeric(14,4),
	PrecioReferencia Numeric(14,4),
	ObServaciones  varchar(200) Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL  

Alter Table COM_OCEN_SobrePrecioEnc Add Constraint PK_COM_OCEN_SobrePrecioEnc
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdProducto, CodigoEAN )
Go--#SQL

Alter Table COM_OCEN_SobrePrecioEnc Add Constraint FK_COM_OCEN_SobrePrecioEnc_COM_OCEN_OrdenesCompra_Claves_Enc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden) References COM_OCEN_OrdenesCompra_Claves_Enc
Go--#SQL

Alter Table COM_OCEN_SobrePrecioEnc Add Constraint FK_COM_OCEN_SobrePrecioEnc_CatProductos
	Foreign Key ( IdProducto) References CatProductos
Go--#SQL

Alter Table COM_OCEN_SobrePrecioEnc Add Constraint FK_COM_OCEN_SobrePrecioEnc_CatProductos_CodigosRelacionados
	Foreign Key ( IdProducto, CodigoEAN) References CatProductos_CodigosRelacionados
Go--#SQL