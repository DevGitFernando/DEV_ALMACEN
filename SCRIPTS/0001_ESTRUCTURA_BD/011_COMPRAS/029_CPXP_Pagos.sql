If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CPXP_PagosDet_Historico' and xType = 'U' )
	Drop Table CPXP_PagosDet_Historico 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CPXP_PagosDet' and xType = 'U' )
	Drop Table CPXP_PagosDet 
Go--#SQL 
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CPXP_PagosEnc' and xType = 'U' )
	Drop Table CPXP_PagosEnc 
Go--#SQL 

Create Table CPXP_PagosEnc
(
	IdEmpresa varchar(3) Not Null, 
	IdProveedor varchar(4) Not Null, 
	Folio varchar(8) Not Null,
	Observaciones varchar(400),
	Importe Numeric(38, 4),
	FechaRegistro datetime Default GetDate(),
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CPXP_PagosEnc Add Constraint PK_CPXP_PagosEnc Primary Key ( IdEmpresa, IdProveedor, Folio )
Go--#SQL

Alter Table CPXP_PagosEnc Add Constraint FK_CPXP_PagosEnc_CatEmpresas
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa  ) 
Go--#SQL

Alter Table CPXP_PagosEnc Add Constraint FK_CPXP_PagosEnc_CatProveedores
	Foreign Key ( IdProveedor) References CatProveedores ( IdProveedor ) 
Go--#SQL

----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CPXP_PagosDet' and xType = 'U' )
	Drop Table CPXP_PagosDet 
Go--#SQL 

Create Table CPXP_PagosDet
(
	IdEmpresa varchar(3) Not Null, 
	IdProveedor varchar(4) Not Null, 
	Folio varchar(8) Not Null,
	IdEstado Varchar(2) Not Null,
	FolioOrdeneCompra Varchar(8) Not Null,
	TipoDeCompra Varchar(30) Not Null,
	Factura varchar(20) NOT Null,
	Pago numeric(38,4) Not Null,
	FechaRegistro datetime Default GetDate(),
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL

Alter Table CPXP_PagosDet Add Constraint PK_CPXP_PagosDet Primary Key ( IdEmpresa, IdProveedor, Folio, IdEstado, FolioOrdeneCompra, Factura)
Go--#SQL

Alter Table CPXP_PagosDet Add Constraint FK_CPXP_PagosDet_CatEmpresas
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa  ) 
Go--#SQL

Alter Table CPXP_PagosDet Add Constraint FK_CPXP_PagosDet_CatProveedores
	Foreign Key ( IdProveedor) References CatProveedores ( IdProveedor ) 
Go--#SQL

Alter Table CPXP_PagosDet Add Constraint FK_CPXP_PagosDet_CatEstados
	Foreign Key ( IdEstado) References CatEstados ( IdEstado ) 
Go--#SQL


----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CPXP_PagosDet_Historico' and xType = 'U' )
	Drop Table CPXP_PagosDet_Historico 
Go--#SQL 

Create Table CPXP_PagosDet_Historico
(
	IdEmpresa varchar(3) Not Null, 
	IdProveedor varchar(4) Not Null, 
	Folio varchar(8) Not Null,
	IdEstado Varchar(2) Not Null,
	FolioOrdeneCompra Varchar(8) Not Null,
	TipoDeCompra Varchar(30) Not Null,
	Factura varchar(20) NOT NULL,
	Pago numeric(38, 4) Not NULL,
	FechaRegistro datetime Default GetDate(),
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0,
	IdEstado_PersonalRegistra Varchar(2) Not Null,
	IdFarmacia_PersonalRegistra Varchar(4) Not Null,
	IdPersonal_PersonalRegistra Varchar(4) Not Null,
)
Go--#SQL

--Alter Table CPXP_PagosDet_Historico Add Constraint PK_CPXP_PagosDet_Historico Primary Key ( IdEmpresa, IdProveedor, Folio, IdEstado, FolioOrdeneCompra )
--Go--#SQL

Alter Table CPXP_PagosDet_Historico Add Constraint FK_CPXP_PagosDet_Historico_CPXP_PagosDet_Historico
	Foreign Key ( IdEmpresa, IdProveedor, Folio, IdEstado, FolioOrdeneCompra, Factura )
	References CPXP_PagosDet ( IdEmpresa, IdProveedor, Folio, IdEstado, FolioOrdeneCompra, Factura  ) 
Go--#SQL

Alter Table CPXP_PagosDet_Historico Add Constraint FK_CPXP_PagosDet_Historico_CatEmpresas
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa  ) 
Go--#SQL

Alter Table CPXP_PagosDet_Historico Add Constraint FK_CPXP_PagosDet_Historico_CatProveedores
	Foreign Key ( IdProveedor) References CatProveedores ( IdProveedor ) 
Go--#SQL

Alter Table CPXP_PagosDet_Historico Add Constraint FK_CPXP_PagosDet_Historico_CatEstados
	Foreign Key ( IdEstado) References CatEstados ( IdEstado ) 
Go--#SQL

Alter Table CPXP_PagosDet_Historico Add Constraint FK_CPXP_PagosDet_Historico_CatPersonal
	Foreign Key ( IdEstado_PersonalRegistra, IdFarmacia_PersonalRegistra, IdPersonal_PersonalRegistra)
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL