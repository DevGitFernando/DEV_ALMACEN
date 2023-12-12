Set NoCount On 
Go--#SQL   

--------------------------------------------------------------------------------------------------------------------------------------------  
--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__RecetasElectronicas_002_Productos' and xType = 'U' ) 
   Drop Table INT_MA__RecetasElectronicas_002_Productos 
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__RecetasElectronicas_001_Encabezado' and xType = 'U' ) 
   Drop Table INT_MA__RecetasElectronicas_001_Encabezado  
Go--#SQL   

Create Table INT_MA__RecetasElectronicas_001_Encabezado 
( 
	Folio varchar(12) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate(), 	

	Surtido smallint Not Null Default 0, 
	FechaSurtido datetime Not Null Default getdate(), 		
	IdEmpresaSurtido varchar(3) Not Null Default '', 
	IdEstadoSurtido varchar(2) Not Null Default '', 
	IdFarmaciaSurtido varchar(4) Not Null Default '',
	IdPersonalSurtido varchar(4) Not Null Default '', 
	
	
	Folio_MA varchar(30) Not Null Default '', 
	Consecutivo Varchar(2) Not Null Default '', 	
	EsRecetaManual Bit Not Null Default 'false', 
	IdFarmacia varchar(15) Not Null Default '', --- Solo aplica para las farmacias asociadas a Clinicas 
	NombrePaciente varchar(200) Not Null Default '', 
	NombreMedico varchar(200) Not Null Default '', 
	Especialidad varchar(200) Not Null Default '', 
	Copago smallint Not Null Default 0, 
	PlanBeneficiario varchar(500) Not Null Default '', 
	FechaEmision varchar(10) Not Null Default '', 
	Elegibilidad varchar(50) Not Null Default '', ---- 2K160426.0850 -- Unique, 
	CIE_01 varchar(10) Not Null Default '', 
	CIE_02 varchar(10) Not Null Default '', 
	CIE_03 varchar(10) Not Null Default '', 
	CIE_04 varchar(10) Not Null Default '' 
) 
Go--#SQL   

Alter Table INT_MA__RecetasElectronicas_001_Encabezado 
	Add Constraint PK_INT_MA__RecetasElectronicas_001_Encabezado Primary Key ( Folio_MA, Consecutivo ) --, Elegibilidad ) 
Go--#SQL   


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__RecetasElectronicas_002_Productos' and xType = 'U' ) 
   Drop Table INT_MA__RecetasElectronicas_002_Productos 
Go--#SQL 

Create Table INT_MA__RecetasElectronicas_002_Productos 
( 
	Folio_MA varchar(30) Not Null Default '', 
	Consecutivo Varchar(2) Not Null Default '', 		
	Partida int Not Null Default 0, 
	CodigoEAN varchar(30) Not Null Default '',  
	CantidadSolicitada int Not Null Default 0, 
	CantidadSurtida int Not Null Default 0 
)
Go--#SQL   

Alter Table INT_MA__RecetasElectronicas_002_Productos 
	Add Constraint PK_INT_MA__RecetasElectronicas_002_Productos Primary Key ( Folio_MA, Consecutivo, CodigoEAN ) 
Go--#SQL   

Alter Table INT_MA__RecetasElectronicas_002_Productos 
	Add Constraint FK_INT_MA__RecetasElectronicas_002_Productos____INT_MA__RecetasElectronicas_001_Encabezado  
	Foreign Key ( Folio_MA, Consecutivo )  
	References INT_MA__RecetasElectronicas_001_Encabezado ( Folio_MA, Consecutivo ) 
Go--#SQL   



--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA__RecetasElectronicas_003_ReservaExistencia' and xType = 'U' ) 
   Drop Table INT_MA__RecetasElectronicas_003_ReservaExistencia 
Go--#SQL 

Create Table INT_MA__RecetasElectronicas_003_ReservaExistencia 
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '',
	IdProducto varchar(30) Not Null Default '',  
	CantidadReservada int Not Null Default 0 
)
Go--#SQL  


