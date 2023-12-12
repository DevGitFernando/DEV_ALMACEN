Set NoCount On 
Go--#SQL   

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RE_INTERMED__RecetasElectronicas_0004_Insumos' and xType = 'U' ) 
   Drop Table INT_RE_INTERMED__RecetasElectronicas_0004_Insumos 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RE_INTERMED__RecetasElectronicas_0003_Diagnosticos' and xType = 'U' ) 
   Drop Table INT_RE_INTERMED__RecetasElectronicas_0003_Diagnosticos 
Go--#SQL   

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RE_INTERMED__RecetasElectronicas_0002_Causes' and xType = 'U' ) 
   Drop Table INT_RE_INTERMED__RecetasElectronicas_0002_Causes 
Go--#SQL  


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RE_INTERMED__RecetasElectronicas_0001_General' and xType = 'U' ) 
   Drop Table INT_RE_INTERMED__RecetasElectronicas_0001_General  
Go--#SQL   

Create Table INT_RE_INTERMED__RecetasElectronicas_0001_General 
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
		
	CLUES_Emisora varchar(50) Not Null Default '', 
	FolioReceta varchar(50) Not Null Default '', 
	FechaReceta datetime Not Null Default getdate(), 
	FechaEnvioReceta datetime Not Null Default getdate(), 

	FolioAfiliacionSPSS varchar(50) Not Null Default '', 	
	FechaIniciaVigencia datetime Not Null Default getdate(), 
	FechaTerminaVigencia datetime Not Null Default getdate(), 

	Expediente varchar(50) Not Null Default '',  
	NombreBeneficiario varchar(100) Not Null Default '', 		
	ApPaternoBeneficiario varchar(100) Not Null Default '', 		
	ApMaternoBeneficiario varchar(100) Not Null Default '', 		
	Sexo varchar(1) Not Null Default '', 	
	FechaNacimientoBeneficiario datetime Not Null Default getdate(), 	
	
	FolioAfiliacionOportunidades varchar(50) Not Null Default '', 		
	EsPoblacionAbierta varchar(4) Not Null Default '', 
		
	ClaveDeMedico varchar(50) Not Null Default '', 			
	NombreMedico varchar(100) Not Null Default '', 		
	ApPaternoMedico varchar(100) Not Null Default '', 		
	ApMaternoMedico varchar(100) Not Null Default '', 	
	CedulaDeMedico varchar(50) Not Null Default '',   
	
	Procesado bit Not Null Default 'false', 
	FechaProcesado datetime Not Null Default getdate() 	
) 
Go--#SQL   

Alter Table INT_RE_INTERMED__RecetasElectronicas_0001_General 
	Add Constraint PK_INT_RE_INTERMED__RecetasElectronicas_0001_General Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   

Alter Table INT_RE_INTERMED__RecetasElectronicas_0001_General 
	Add Constraint FK_INT_RE_INTERMED__RecetasElectronicas_0001_General___INT_RE_INTERMED__RecetasElectronicas_XML 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References INT_RE_INTERMED__RecetasElectronicas_XML ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RE_INTERMED__RecetasElectronicas_0002_Causes' and xType = 'U' ) 
   Drop Table INT_RE_INTERMED__RecetasElectronicas_0002_Causes 
Go--#SQL   

Create Table INT_RE_INTERMED__RecetasElectronicas_0002_Causes 
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	Folio varchar(12) Not Null Default '', 

	NoIntervencionCause varchar(10) Not Null Default '' 
) 
Go--#SQL   

Alter Table INT_RE_INTERMED__RecetasElectronicas_0002_Causes 
	Add Constraint PK_INT_RE_INTERMED__RecetasElectronicas_0002_Causes Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, NoIntervencionCause ) 
Go--#SQL  	

Alter Table INT_RE_INTERMED__RecetasElectronicas_0002_Causes 
	Add Constraint FK_INT_RE_INTERMED__RecetasElectronicas_0002_Causes___INT_RE_INTERMED__RecetasElectronicas_0001_General
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References INT_RE_INTERMED__RecetasElectronicas_0001_General ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   



--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RE_INTERMED__RecetasElectronicas_0003_Diagnosticos' and xType = 'U' ) 
   Drop Table INT_RE_INTERMED__RecetasElectronicas_0003_Diagnosticos 
Go--#SQL   

Create Table INT_RE_INTERMED__RecetasElectronicas_0003_Diagnosticos  
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	Folio varchar(12) Not Null Default '', 
	
	CIE10 varchar(10) Not Null Default '',  
	DescripcionDiagnostico varchar(3000) Not Null Default '' 
) 
Go--#SQL   

Alter Table INT_RE_INTERMED__RecetasElectronicas_0003_Diagnosticos 
	Add Constraint PK_INT_RE_INTERMED__RecetasElectronicas_0003_Diagnosticos Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, CIE10 ) 
Go--#SQL  	

Alter Table INT_RE_INTERMED__RecetasElectronicas_0003_Diagnosticos 
	Add Constraint FK_INT_RE_INTERMED__RecetasElectronicas_0003_Diagnosticos___INT_RE_INTERMED__RecetasElectronicas_0001_General
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References INT_RE_INTERMED__RecetasElectronicas_0001_General ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RE_INTERMED__RecetasElectronicas_0004_Insumos' and xType = 'U' ) 
   Drop Table INT_RE_INTERMED__RecetasElectronicas_0004_Insumos 
Go--#SQL   

Create Table INT_RE_INTERMED__RecetasElectronicas_0004_Insumos  
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	Folio varchar(12) Not Null Default '', 
	
	ClaveSSA varchar(50) Not Null Default '', 
	CantidadRequerida int Not Null Default 0, 
	CantidadEntregada int Not Null Default 0, 
	
	EmitioVale int Not Null Default 0, 	
	CantidadVale int Not Null Default 0 
		 	
) 
Go--#SQL   

Alter Table INT_RE_INTERMED__RecetasElectronicas_0004_Insumos 
	Add Constraint PK_INT_RE_INTERMED__RecetasElectronicas_0004_Insumos Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio, ClaveSSA ) 
Go--#SQL  	

Alter Table INT_RE_INTERMED__RecetasElectronicas_0004_Insumos 
	Add Constraint FK_INT_RE_INTERMED__RecetasElectronicas_0004_Insumos___INT_RE_INTERMED__RecetasElectronicas_0001_General
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References INT_RE_INTERMED__RecetasElectronicas_0001_General ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   