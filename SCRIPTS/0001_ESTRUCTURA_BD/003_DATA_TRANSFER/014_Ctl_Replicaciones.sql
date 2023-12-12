If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_Replicaciones_Historico' and xType = 'U' )
	Drop Table Ctl_Replicaciones_Historico 
Go--#SQL 
---------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_Replicaciones' and xType = 'U' )
	Drop Table Ctl_Replicaciones 
Go--#SQL 

CREATE TABLE Ctl_Replicaciones
(
	IdEmpresa varchar(3) NOT NULL,
	IdEstado varchar(2) NOT NULL,
	IdFarmacia varchar(4) NOT NULL,
	FechaInicial datetime NOT NULL,
	FechaFinal datetime NOT NULL,
	NombreBaseDeDatos varchar(200),
	Host varchar(200),
	FechaUpdate datetime NOT NULL Default GetDate(), 

	Cuadrada Bit Not Null Default 0,    
	RegistrosVentasAdicional Int Not Null Default 0, 
	RegistrosVentasLotes Int Not Null Default 0, 
	RegionalVentasAdcional Int Not Null Default 0, 
	RegionalVentasLotes Int Not Null Default 0, 
	VersionBD  Varchar(200) Not Null Default '', 
	VersionExe Varchar(200) Not Null Default '' 
) 
Go--#SQL

Alter Table Ctl_Replicaciones Add Constraint PK_Ctl_Replicaciones
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia) 
Go--#SQL  

Alter Table Ctl_Replicaciones Add Constraint FK_Ctl_Replicaciones__CatEmpresas
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa) 
Go--#SQL 

Alter Table Ctl_Replicaciones Add Constraint FK_Ctl_Replicaciones__CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) 
	References CatFarmacias ( IdEstado, IdFarmacia) 
Go--#SQL 

----------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctl_Replicaciones_Historico' and xType = 'U' )
	Drop Table Ctl_Replicaciones_Historico 
Go--#SQL 

CREATE TABLE Ctl_Replicaciones_Historico
(
	IdEmpresa varchar(3) NOT NULL,
	IdEstado varchar(2) NOT NULL,
	IdFarmacia varchar(4) NOT NULL,
	FechaInicial datetime NOT NULL,
	FechaFinal datetime NOT NULL,
	NombreBaseDeDatos varchar(200),
	Host varchar(200),
	FechaUpdate datetime NOT NULL Default GetDate(), 

	Cuadrada Bit Not Null Default 0,    
	RegistrosVentasAdicional Int Not Null Default 0, 
	RegistrosVentasLotes Int Not Null Default 0, 
	RegionalVentasAdcional Int Not Null Default 0, 
	RegionalVentasLotes Int Not Null Default 0, 
	VersionBD  Varchar(200) Not Null Default '', 
	VersionExe Varchar(200) Not Null Default ''  
)
Go--#SQL 
