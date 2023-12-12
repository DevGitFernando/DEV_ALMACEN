If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_SobrePrecioDet' and xType = 'U' ) 
   Drop Table COM_OCEN_SobrePrecioDet
Go--#SQL  

Create Table COM_OCEN_SobrePrecioDet
( 
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	FolioOrden varchar(8) Not Null,
	IdProducto Varchar(8) Not Null,
	CodigoEAN Varchar(30) Not Null,
	IdMotivoSobrePrecio Varchar(4) Not Null,
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL  

Alter Table COM_OCEN_SobrePrecioDet Add Constraint PK_COM_OCEN_SobrePrecioDet
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdProducto, CodigoEAN, IdMotivoSobrePrecio )
Go--#SQL

Alter Table COM_OCEN_SobrePrecioDet Add Constraint FK_COM_OCEN_SobrePrecioDet_COM_OCEN_OrdenesCompra_Claves_Enc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden) References COM_OCEN_OrdenesCompra_Claves_Enc
Go--#SQL

Alter Table COM_OCEN_SobrePrecioDet Add Constraint FK_COM_OCEN_SobrePrecioDet_CatProductos
	Foreign Key ( IdProducto) References CatProductos
Go--#SQL

Alter Table COM_OCEN_SobrePrecioDet Add Constraint FK_COM_OCEN_SobrePrecioDet_CatProductos_CodigosRelacionados
	Foreign Key ( IdProducto, CodigoEAN) References CatProductos_CodigosRelacionados
Go--#SQL

Alter Table COM_OCEN_SobrePrecioDet Add Constraint FK_COM_OCEN_SobrePrecioDet_CatMotivosSobrePrecio
	Foreign Key ( IdMotivoSobrePrecio) References CatMotivosSobrePrecio
Go--#SQL