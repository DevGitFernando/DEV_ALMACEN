


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Cuentas_X_Pagar' and xType = 'U' ) 
	Drop Table FACT_Cuentas_X_Pagar   
Go--#SQL  

Create Table FACT_Cuentas_X_Pagar 
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	FolioCuenta varchar(6) Not Null,
	IdServicio varchar(3) Not Null,
	IdAcreedor varchar(4) Not Null,
	FechaRegistro datetime Not Null Default getdate(),
	IdPersonal varchar(4) Not Null, 
	ReferenciaDocumento varchar(20) Not Null Default '',
	FechaDocumento datetime Not Null Default getdate(),
	IdMetodoPago varchar(2) Not Null,
	SubTotal numeric(14, 4) Not Null Default 0,
	TasaIva numeric(14, 4) Not Null Default 0,
	Iva numeric(14, 4) Not Null Default 0,
	Total numeric(14, 4) Not Null Default 0,
	Observaciones varchar(100) Not Null Default '',	
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0 
) 	
Go--#SQL 

Alter Table FACT_Cuentas_X_Pagar Add Constraint PK_FACT_Cuentas_X_Pagar Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCuenta ) 
Go--#SQL 

Alter Table FACT_Cuentas_X_Pagar Add Constraint FK_FACT_Cuentas_X_Pagar_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table FACT_Cuentas_X_Pagar Add Constraint FK_FACT_Cuentas_X_Pagar_CatEstados 
	Foreign Key ( IdEstado  ) References CatEstados ( IdEstado ) 
Go--#SQL

Alter Table FACT_Cuentas_X_Pagar Add Constraint FK_FACT_Cuentas_X_Pagar_CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL

Alter Table FACT_Cuentas_X_Pagar Add Constraint FK_FACT_Cuentas_X_Pagar_FACT_CatServicios
	Foreign Key ( IdServicio ) References FACT_CatServicios ( IdServicio ) 
Go--#SQL 

Alter Table FACT_Cuentas_X_Pagar Add Constraint FK_FACT_Cuentas_X_Pagar_FACT_CatAcreedores 
	Foreign Key ( IdEstado, IdAcreedor ) References FACT_CatAcreedores ( IdEstado, IdAcreedor ) 
Go--#SQL

Alter Table FACT_Cuentas_X_Pagar Add Constraint FK_FACT_Cuentas_X_Pagar_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL

Alter Table FACT_Cuentas_X_Pagar Add Constraint FK_FACT_Cuentas_X_Pagar_FACT_CFD_MetodosPago 
	Foreign Key ( IdMetodoPago ) References FACT_CFD_MetodosPago ( IdMetodoPago ) 
Go--#SQL 
