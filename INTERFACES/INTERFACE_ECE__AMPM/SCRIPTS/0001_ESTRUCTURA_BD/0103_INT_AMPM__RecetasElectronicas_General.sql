Set NoCount On 
Go--#SQL   

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__RecetasElectronicas_0004_Insumos' and xType = 'U' ) 
   Drop Table INT_AMPM__RecetasElectronicas_0004_Insumos 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__RecetasElectronicas_0003_Diagnosticos' and xType = 'U' ) 
   Drop Table INT_AMPM__RecetasElectronicas_0003_Diagnosticos 
Go--#SQL   

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__RecetasElectronicas_0002_Causes' and xType = 'U' ) 
   Drop Table INT_AMPM__RecetasElectronicas_0002_Causes 
Go--#SQL  


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__RecetasElectronicas_0001_General' and xType = 'U' ) 
   Drop Table INT_AMPM__RecetasElectronicas_0001_General  
Go--#SQL   

Create Table INT_AMPM__RecetasElectronicas_0001_General 
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	Folio varchar(12) Not Null Default '', 

	EsSurtido bit Not Null Default 'False',  
	FolioSurtido varchar(10) Not Null Default '', 		
	FechaDeSurtido datetime Default getdate(), 

	EsCancelado bit Not Null Default 'False', 
	FechaDeCancelacion datetime Default getdate(), 
	FechaSolicitudDeCancelacion datetime Default getdate(), 
		

	FolioReceta varchar(50) Not Null Default '', 
	FechaReceta datetime Not Null Default getdate(), 
	FechaEnvioReceta datetime Not Null Default getdate(), 

	FolioConsulta varchar(50) Not Null Default '', 
	IdUsuario varchar(50) Not Null Default '', 
	IdEstudiosPaciente varchar(50) Not Null Default '', 
	Indicaciones varchar(500) Not Null Default '', 
	Diagnostico varchar(500) Not Null Default '',
	CIE10 Varchar(30) Not Null Default '',
	NHC Varchar(30) Not Null Default '',

	FolioAfiliacionSPSS varchar(50) Not Null Default '', 	
	FechaIniciaVigencia datetime Not Null Default getdate(), 
	FechaTerminaVigencia datetime Not Null Default getdate(), 

	Expediente varchar(50) Not Null Default '',  
	NombreBeneficiario varchar(100) Not Null Default '', 		
	ApPaternoBeneficiario varchar(100) Not Null Default '', 		
	ApMaternoBeneficiario varchar(100) Not Null Default '', 		
	Sexo varchar(1) Not Null Default '', 	
	FechaNacimientoBeneficiario datetime Not Null Default getdate(), 	
		
	ClaveDeMedico varchar(50) Not Null Default '', 			
	NombreMedico varchar(100) Not Null Default '', 		
	ApPaternoMedico varchar(100) Not Null Default '', 		
	ApMaternoMedico varchar(100) Not Null Default '', 	
	CedulaDeMedico varchar(50) Not Null Default '',   
	LicenciaturaDeMedico varchar(100) Not Null Default '',   	
	FirmaImagen varchar(300) Not Null Default '',
	procedencia  varchar(300) Not Null Default '',

	Procesado bit Not Null Default 'false', 
	FechaProcesado datetime Not Null Default getdate() 	
)  
Go--#SQL   

Alter Table INT_AMPM__RecetasElectronicas_0001_General 
	Add Constraint PK_INT_AMPM__RecetasElectronicas_0001_General Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   

Alter Table INT_AMPM__RecetasElectronicas_0001_General 
	Add Constraint FK_INT_AMPM__RecetasElectronicas_0001_General___INT_AMPM__RecetasElectronicas_XML 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References INT_AMPM__RecetasElectronicas_XML ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   

--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__RecetasElectronicas_0002_Somatometria' and xType = 'U' ) 
   Drop Table INT_AMPM__RecetasElectronicas_0002_Somatometria 
Go--#SQL   

Create Table INT_AMPM__RecetasElectronicas_0002_Somatometria  
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	Folio varchar(12) Not Null Default '', 

	IdEnfermeria varchar(10) Not Null Default '', 
	Estatura numeric(14,4) Not Null Default 0, 
	Peso numeric(14,4) Not Null Default 0, 
	Cintura numeric(14,4) Not Null Default 0, 
	PerimetroCadera numeric(14,4) Not Null Default 0, 
	DiametroCefalico numeric(14,4) Not Null Default 0, 
	DiametroAbdominal numeric(14,4) Not Null Default 0, 

	PresionSistolica numeric(14,4) Not Null Default 0, 
	PresionDiastolica numeric(14,4) Not Null Default 0, 
	FrecuenciaCardiaca numeric(14,4) Not Null Default 0, 
	FrecuenciaRespiratoria numeric(14,4) Not Null Default 0, 
	Temperatura numeric(14,4) Not Null Default 0, 
	IdUsuario varchar(10) Not Null Default '', 
	FechaEnfermeria varchar(10) Not Null Default '', 
	HoraEnfermeria varchar(10) Not Null Default '', 

	Glucosa varchar(100) Not Null Default '', 
	TipoGlucosa varchar(100) Not Null Default '', 

	Diagnostico varchar(500) Not Null Default '', 
	Observaciones varchar(500) Not Null Default '' 
) 
Go--#SQL   

Alter Table INT_AMPM__RecetasElectronicas_0002_Somatometria 
	Add Constraint PK_INT_AMPM__RecetasElectronicas_0002_Somatometria Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL  	

Alter Table INT_AMPM__RecetasElectronicas_0002_Somatometria 
	Add Constraint FK_INT_AMPM__RecetasElectronicas_0002_Somatometria___INT_AMPM__RecetasElectronicas_XML 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References INT_AMPM__RecetasElectronicas_XML ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__RecetasElectronicas_0003_Diagnosticos' and xType = 'U' ) 
   Drop Table INT_AMPM__RecetasElectronicas_0003_Diagnosticos 
Go--#SQL   

Create Table INT_AMPM__RecetasElectronicas_0003_Diagnosticos  
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	Folio varchar(12) Not Null Default '', 
	
	CIE10 varchar(10) Not Null Default '',  
	DescripcionDiagnostico varchar(3000) Not Null Default '' 
) 
Go--#SQL   

Alter Table INT_AMPM__RecetasElectronicas_0003_Diagnosticos 
	Add Constraint PK_INT_AMPM__RecetasElectronicas_0003_Diagnosticos Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, CIE10 ) 
Go--#SQL  	

Alter Table INT_AMPM__RecetasElectronicas_0003_Diagnosticos 
	Add Constraint FK_INT_AMPM__RecetasElectronicas_0003_Diagnosticos___INT_AMPM__RecetasElectronicas_XML 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References INT_AMPM__RecetasElectronicas_XML ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__RecetasElectronicas_0004_Insumos' and xType = 'U' ) 
   Drop Table INT_AMPM__RecetasElectronicas_0004_Insumos 
Go--#SQL   

Create Table INT_AMPM__RecetasElectronicas_0004_Insumos  
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	Folio varchar(12) Not Null Default '', 
	
	CodigoEAN varchar(30) Not Null Default '', 
	-- Presentacion varchar(100) Not Null Default '', 
	-- ClaveSSA varchar(50) Not Null Default '', 

	CantidadRequerida int Not Null Default 0, 
	CantidadEntregada int Not Null Default 0,
	Existencia int Not Null Default 0, 
	
	IdMedicina varchar(10) Not Null Default '', 
	Via varchar(10) Not Null Default '', 
	Dosis varchar(10) Not Null Default '', 
	Unidades varchar(10) Not Null Default '', 
	Frecuencia varchar(10) Not Null Default '', 
	FechaInicio varchar(10) Not Null Default '', 
	FechaFin varchar(10) Not Null Default '', 
	Observaciones varchar(200) Not Null Default '' 			 	
) 
Go--#SQL   

Alter Table INT_AMPM__RecetasElectronicas_0004_Insumos 
	Add Constraint PK_INT_AMPM__RecetasElectronicas_0004_Insumos Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, CodigoEAN ) 
Go--#SQL  	

Alter Table INT_AMPM__RecetasElectronicas_0004_Insumos 
	Add Constraint FK_INT_AMPM__RecetasElectronicas_0004_Insumos___INT_AMPM__RecetasElectronicas_XML 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References INT_AMPM__RecetasElectronicas_XML ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   

--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_AMPM__RecetasElectronicas_0005_InsumosNoCatalogo' and xType = 'U' ) 
   Drop Table INT_AMPM__RecetasElectronicas_0005_InsumosNoCatalogo 
Go--#SQL   

Create Table INT_AMPM__RecetasElectronicas_0005_InsumosNoCatalogo
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	Folio varchar(12) Not Null Default '', 
	
	Partida int Not Null Default 0,
	DescripcionGenerica varchar(500) Not Null Default '' 			 	
) 
Go--#SQL   

Alter Table INT_AMPM__RecetasElectronicas_0005_InsumosNoCatalogo 
	Add Constraint PK_INT_AMPM__RecetasElectronicas_0005_InsumosNoCatalogo Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, Partida ) 
Go--#SQL  	

Alter Table INT_AMPM__RecetasElectronicas_0005_InsumosNoCatalogo 
	Add Constraint FK_INT_AMPM__RecetasElectronicas_0005_InsumosNoCatalogo___INT_AMPM__RecetasElectronicas_XML 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References INT_AMPM__RecetasElectronicas_XML ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   
