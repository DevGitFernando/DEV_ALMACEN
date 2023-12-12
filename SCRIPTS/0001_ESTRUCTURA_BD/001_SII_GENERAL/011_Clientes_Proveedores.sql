If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSubClientes' and xType = 'U' ) 
	Drop Table CatSubClientes 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatClientes' and xType = 'U' ) 
	Drop Table CatClientes 
Go--#SQL 

--------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatTiposDeClientes' and xType = 'U' ) 
	Drop Table CatTiposDeClientes 
Go--#SQL  

Create Table CatTiposDeClientes 
(
	IdTipoCliente varchar(2) Not Null, 
	Descripcion varchar(100) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 		 
) 
Go--#SQL  

Alter Table CatTiposDeClientes Add Constraint PK_CatTiposDeClientes Primary Key ( IdTipoCliente ) 
Go--#SQL  
-------------- 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatClientes' and xType = 'U' ) 
	Drop Table CatClientes 
Go--#SQL  

Create Table CatClientes 
(
	IdCliente varchar(4) Not Null, 
	Nombre varchar(100) Not Null, 
	RFC varchar(15) Not Null Unique NonClustered, ---- Asegurar que no se repitan los RFC
	IdTipoCliente varchar(2) Not Null, 

	IdEstado varchar(2) Not Null Default '',     --- Descripcion en Catalogo aparte
	IdMunicipio varchar(4) Not Null Default '',  --- Descripcion en Catalogo aparte
	IdColonia varchar(4) Not Null Default '',    --- Descripcion en Catalogo aparte
	Domicilio varchar(100) Not Null Default '', 
	CodigoPostal varchar(10) Not Null Default '', 
	Telefonos varchar(30) Not Null Default '',	

	TieneLimiteDeCredito bit default 'false', 
	LimiteDeCredito Numeric(14,4) Not Null Default 0, 
	CreditoSuspendido bit default 'false', 
	SaldoActual Numeric(14,4) Not Null Default 0, 
	CtaMay varchar(4) Not Null Default '', 
	SubCta varchar(4) Not Null Default '', 	
	SSbCta varchar(4) Not Null Default '', 
	SSSCta varchar(4) Not Null Default '', 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CatClientes Add Constraint PK_CatClientes Primary Key ( IdCliente ) 
Go--#SQL  

Alter Table CatClientes Add Constraint PK_CatClientes_CatTiposDeClientes 
	Foreign Key ( IdTipoCliente ) References CatTiposDeClientes ( IdTipoCliente )  
Go--#SQL 

----------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatSubClientes' and xType = 'U' ) 
	Drop Table CatSubClientes 
Go--#SQL  

Create Table CatSubClientes
(
	IdCliente varchar(4) Not Null,
	IdSubCliente varchar(4) Not Null,
	Nombre varchar(100) Not Null Default '', 

	PorcUtilidad Numeric(10,2) Not Null Default 0, 
	PermitirCapturaBeneficiarios bit Not Null Default 'false', 
	ImportaBeneficiarios bit Not Null Default 'false', 

	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table CatSubClientes Add Constraint PK_CatSubClientes Primary Key ( IdCliente, IdSubCliente ) 
Go--#SQL  

Alter Table CatSubClientes Add Constraint FK_CatSubClientes_CatClientes 
	Foreign Key ( IdCliente ) References CatClientes ( IdCliente ) 
Go--#SQL 

----------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProveedores' and xType = 'U' ) 
	Drop Table CatProveedores 
Go--#SQL  

Create Table CatProveedores 
(
	IdProveedor varchar(4) Not Null, 
	Nombre varchar(100) Not Null, 
	RFC varchar(15) Not Null Unique NonClustered, ---- Asegurar que no se repitan los RFC
    AliasNombre varchar(100) Not Null Default '', 

	IdEstado varchar(2) Not Null Default '',     --- Descripcion en Catalogo aparte
	IdMunicipio varchar(4) Not Null Default '',  --- Descripcion en Catalogo aparte
	IdColonia varchar(4) Not Null Default '',    --- Descripcion en Catalogo aparte
	Domicilio varchar(100) Not Null Default '', 
	CodigoPostal varchar(10) Not Null Default '', 
	Telefonos varchar(30) Not Null Default '',	

	TieneLimiteDeCredito bit default 'false', 
	LimiteDeCredito Numeric(14,4) Not Null Default 0, 
	CreditoSuspendido bit default 'false', 
	SaldoActual Numeric(14,4) Not Null Default 0, 
	CtaMay varchar(4) Not Null Default '', 
	SubCta varchar(4) Not Null Default '', 	
	SSbCta varchar(4) Not Null Default '', 
	SSSCta varchar(4) Not Null Default '', 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CatProveedores Add Constraint PK_CatProveedores Primary Key ( IdProveedor ) 
Go--#SQL  

----------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProveedores_Historico' and xType = 'U' ) 
	Drop Table CatProveedores_Historico 
Go--#SQL  

Create Table CatProveedores_Historico 
(
	IdProveedor varchar(4) Not Null, 
	FechaRegistro datetime Not Null default getdate(), 
	Nombre varchar(100) Not Null, 
	RFC varchar(15) Not Null, ---- Asegurar que no se repitan los RFC

	IdPersonalRegistra varchar(4) Not Null Default '', 

	IdEstado varchar(2) Not Null Default '',     --- Descripcion en Catalogo aparte
	IdMunicipio varchar(4) Not Null Default '',  --- Descripcion en Catalogo aparte
	IdColonia varchar(4) Not Null Default '',    --- Descripcion en Catalogo aparte
	Domicilio varchar(100) Not Null Default '', 
	CodigoPostal varchar(10) Not Null Default '', 
	Telefonos varchar(30) Not Null Default '',	

	TieneLimiteDeCredito bit default 'false', 
	LimiteDeCredito Numeric(14,4) Not Null Default 0, 
	CreditoSuspendido bit default 'false', 
	SaldoActual Numeric(14,4) Not Null Default 0, 
	CtaMay varchar(4) Not Null Default '', 
	SubCta varchar(4) Not Null Default '', 	
	SSbCta varchar(4) Not Null Default '', 
	SSSCta varchar(4) Not Null Default '', 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	Keyx int identity(1,1)  
)
Go--#SQL  

Alter Table CatProveedores_Historico Add Constraint PK_CatProveedores_Historico Primary Key ( IdProveedor, FechaRegistro ) 
Go--#SQL  

/*
	Insert Into CatProveedores_Historico ( IdProveedor, Nombre, RFC, IdEstado, IdMunicipio, IdColonia, Domicilio, CodigoPostal, Telefonos, TieneLimiteDeCredito, LimiteDeCredito, CreditoSuspendido, SaldoActual, CtaMay, SubCta, SSbCta, SSSCta, Status, Actualizado ) 
	Select 
		IdProveedor, Nombre, RFC, IdEstado, IdMunicipio, IdColonia, Domicilio, CodigoPostal, Telefonos, TieneLimiteDeCredito, LimiteDeCredito, CreditoSuspendido, SaldoActual, CtaMay, SubCta, SSbCta, SSSCta, Status, Actualizado 
	from CatProveedores	
*/		