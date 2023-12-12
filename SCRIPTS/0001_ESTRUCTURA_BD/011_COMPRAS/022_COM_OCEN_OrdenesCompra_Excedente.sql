
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_OrdenesCompra_Excedente' and xType = 'U' )
   Drop Table COM_OCEN_OrdenesCompra_Excedente
Go--#SQL

Create Table COM_OCEN_OrdenesCompra_Excedente
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	FolioOrden varchar(8) Not Null,
	IdPersonal Varchar(4) Not Null,
	IdClaveSSA Varchar(8) Not Null,
	Porcentaje Numeric(14, 4) Not Null,
	Observaciones varchar(300) Not Null,
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
)
Go--#SQL

Alter Table COM_OCEN_OrdenesCompra_Excedente
	Add Constraint PK_COM_OCEN_OrdenesCompra_Excedente Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdClaveSSA )
Go--#SQL

Alter Table COM_OCEN_OrdenesCompra_Excedente Add Constraint FK_COM_OCEN_OrdenesCompra_Excedente_COM_OCEN_OrdenesCompra_Claves_Enc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden) References COM_OCEN_OrdenesCompra_Claves_Enc
Go--#SQL

Alter Table COM_OCEN_OrdenesCompra_Excedente Add Constraint FK_COM_OCEN_OrdenesCompra_Excedente_CatPersonal
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal) References CatPersonal
Go--#SQL

Alter Table COM_OCEN_OrdenesCompra_Excedente Add Constraint FK_COM_OCEN_OrdenesCompra_Excedente_CatFarmacia
	Foreign Key ( IdEstado, IdFarmacia) References CatFarmacias
Go--#SQL