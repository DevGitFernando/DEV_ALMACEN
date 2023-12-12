If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CPXP_Conciliacion_Historico' and xType = 'U' )
	Drop Table CPXP_Conciliacion_Historico 
Go--#SQL 
---------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CPXP_Conciliacion' and xType = 'U' )
	Drop Table CPXP_Conciliacion 
Go--#SQL 

CREATE TABLE CPXP_Conciliacion
(
	IdEmpresa varchar(3) NOT NULL,
	IdEstado varchar(2) NOT NULL,
	IdFarmacia varchar(4) NOT NULL,
	OrdenCompra varchar(30) NOT NULL,
	IdProveedor varchar(4) NOT NULL,
	Factura varchar(20) NOT NULL,
	FechaRegistro varchar(10) NULL,
	FechaColocacion varchar(10) NULL,
	FechaDocto varchar(10) NULL,
	FechaVenceDocto varchar(10) NULL,
	SubTotal numeric(38, 4) NULL,
	Iva numeric(38, 4) NULL,
	Total numeric(38, 4) NULL,
	FechaUpdate datetime NOT NULL
)
Go--#SQL 

	Alter Table CPXP_Conciliacion Add Constraint PK_CPXP_Conciliacion Primary Key ( IdEmpresa, IdEstado, IdFarmacia, OrdenCompra, Factura ) 
Go--#SQL 


----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CPXP_Conciliacion_Historico' and xType = 'U' )
	Drop Table CPXP_Conciliacion_Historico 
Go--#SQL 

CREATE TABLE CPXP_Conciliacion_Historico
(
	IdEmpresa varchar(3) NOT NULL,
	IdEstado varchar(2) NOT NULL,
	IdFarmacia varchar(4) NOT NULL,
	OrdenCompra varchar(30) NOT NULL,
	IdProveedor varchar(4) NOT NULL,
	Factura varchar(20) NOT NULL,
	FechaRegistro varchar(10) NULL,
	FechaColocacion varchar(10) NULL,
	FechaDocto varchar(10) NULL,
	FechaVenceDocto varchar(10) NULL,
	SubTotal numeric(38, 4) NULL,
	Iva numeric(38, 4) NULL,
	Total numeric(38, 4) NULL,
	FechaUpdate datetime NOT NULL
)
Go--#SQL 

	Alter Table CPXP_Conciliacion_Historico Add Constraint PK_CPXP_Conciliacion_Historico Primary Key ( IdEmpresa, IdEstado, IdFarmacia, OrdenCompra, Factura, FechaUpdate ) 
Go--#SQL 

Go--#SQL 

