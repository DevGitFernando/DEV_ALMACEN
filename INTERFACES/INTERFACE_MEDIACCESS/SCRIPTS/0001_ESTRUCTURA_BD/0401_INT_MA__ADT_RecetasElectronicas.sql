Set NoCount On 
Go--#SQL   

--------------------------------------------------------------------------------------------------------------------------------------------  
--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__ADT_RecetasElectronicas_002_Productos' and xType = 'U' ) 
   Drop Table INT_MA__ADT_RecetasElectronicas_002_Productos 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__ADT_RecetasElectronicas_001_Encabezado' and xType = 'U' ) 
   Drop Table INT_MA__ADT_RecetasElectronicas_001_Encabezado  
Go--#SQL   

Create Table INT_MA__ADT_RecetasElectronicas_001_Encabezado 
( 
	Folio varchar(12) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate(), 	

	Surtido smallint Not Null Default 0, 
	FechaSurtido datetime Not Null Default getdate(), 		
	IdEmpresaSurtido varchar(3) Not Null Default '', 
	IdEstadoSurtido varchar(2) Not Null Default '', 
	IdFarmaciaSurtido varchar(4) Not Null Default '',
	IdPersonalSurtido varchar(4) Not Null Default '', 
	
	
	Folio_MA Bigint Not Null Default 0, 
	EsRecetaManual Bit Not Null Default 'false', 
	IdFarmacia varchar(15) Not Null Default '', --- Solo aplica para las farmacias asociadas a Clinicas 
	NombrePaciente varchar(200) Not Null Default '', 
	NombreMedico varchar(200) Not Null Default '', 
	Especialidad varchar(200) Not Null Default '', 
	Copago smallint Not Null Default 0, 
	PlanBeneficiario varchar(500) Not Null Default '', 
	FechaEmision varchar(10) Not Null Default '', 
	Elegibilidad varchar(50) Not Null Default '', 
	CIE_01 varchar(10) Not Null Default '', 
	CIE_02 varchar(10) Not Null Default '', 
	CIE_03 varchar(10) Not Null Default '', 
	CIE_04 varchar(10) Not Null Default '',
	IdEstadoModifica varchar(2) NOT NULL Default '',
	IdFarmaciaModifica varchar(4) NOT NULL Default '',
	IdPersonalModifica Varchar(4) Not Null,
	FechaUpdate datetime Not Null Default getdate()
) 
Go--#SQL   

--Alter Table INT_MA__ADT_RecetasElectronicas_001_Encabezado 
--	Add Constraint PK_INT_MA__ADT_RecetasElectronicas_001_Encabezado Primary Key ( Folio_MA ) 
--Go--#SQL   

--Alter Table INT_MA__ADT_RecetasElectronicas_001_Encabezado 
--	Add Constraint FK_INT_MA__ADT_RecetasElectronicas_001_Encabezado__INT_MA__RecetasElectronicas_001_Encabezado
--	Foreign Key ( Folio_MA )  
--	References INT_MA__RecetasElectronicas_001_Encabezado ( Folio_MA ) 
--Go--#SQL

Alter Table INT_MA__ADT_RecetasElectronicas_001_Encabezado 
	Add Constraint FK_INT_MA__ADT_RecetasElectronicas_001_Encabezado__CatPersonal
	Foreign Key ( IdEstadoModifica, IdfarmaciaModifica, IdPersonalModifica )  
	References CatPersonal ( IdEstado, Idfarmacia, IdPersonal ) 
Go--#SQL

--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__ADT_RecetasElectronicas_002_Productos' and xType = 'U' ) 
   Drop Table INT_MA__ADT_RecetasElectronicas_002_Productos 
Go--#SQL 

Create Table INT_MA__ADT_RecetasElectronicas_002_Productos 
( 
	Folio_MA Bigint Not Null Default 0, 
	Partida int Not Null Default 0, 
	CodigoEAN varchar(30) Not Null Default '',  
	CantidadSolicitada int Not Null Default 0, 
	CantidadSurtida int Not Null Default 0,
	FechaUpdate datetime Not Null Default getdate()
)
Go--#SQL  

--Alter Table INT_MA__ADT_RecetasElectronicas_002_Productos 
--	Add Constraint PK_INT_MA__ADT_RecetasElectronicas_002_Productos Primary Key ( Folio_MA, CodigoEAN ) 
--Go--#SQL   

--Alter Table INT_MA__ADT_RecetasElectronicas_002_Productos 
--	Add Constraint FK_INT_MA__ADT_RecetasElectronicas_002_Productos__INT_MA__RecetasElectronicas_002_Productos
--	Foreign Key ( Folio_MA, CodigoEAN )  
--	References INT_MA__RecetasElectronicas_002_Productos ( Folio_MA, CodigoEAN ) 
--Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__ADT_Elegibilidades' and xType = 'U' ) 
   Drop Table INT_MA__ADT_Elegibilidades 
Go--#SQL 

Create Table INT_MA__ADT_Elegibilidades 
( 	Folio varchar(12) NOT NULL  Default '',
	FechaRegistro datetime NOT NULL DEFAULT getdate(),
	IdEmpresa varchar(3) NOT NULL Default '',
	IdEstado varchar(2) NOT NULL Default '',
	IdFarmacia varchar(4) NOT NULL Default '',
	IdPersonal varchar(4) NOT NULL  Default '',
	Elegibilidad varchar(50) NOT NULL Default '',
	Empresa_y_RazonSocial varchar(500) NOT NULL Default '',
	Elegibilidad_DisponibleSurtido bit NOT NULL DEFAULT 1,
	NombreEmpleado varchar(200) NOT NULL Default '',
	ReferenciaEmpleado varchar(20) NOT NULL Default '',
	NombreMedico varchar(200) NOT NULL Default '',
	ReferenciaMedico varchar(20) NOT NULL Default '',
	Copago smallint NOT NULL DEFAULT 0,
	CopagoEn smallint NOT NULL DEFAULT 0,
	IdPersonalModifica Varchar(4) Not Null,
	FechaUpdate datetime Not Null Default getdate()
)
Go--#SQL

Alter Table INT_MA__ADT_Elegibilidades 
	Add Constraint FK_INT_MA__ADT_Elegibilidades__CatPersonal
	Foreign Key ( IdEstado, Idfarmacia, IdPersonalModifica )  
	References CatPersonal ( IdEstado, Idfarmacia, IdPersonal ) 
Go--#SQL