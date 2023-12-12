Set NoCount On 
Go--#SQL   

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RE_INTERMED__RecetasElectronicas_0005_CancelacionRecetas' and xType = 'U' ) 
   Drop Table INT_RE_INTERMED__RecetasElectronicas_0005_CancelacionRecetas 
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

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RE_INTERMED__RecetasElectronicas_0001_General' and xType = 'U' ) 
   Drop Table INT_RE_INTERMED__RecetasElectronicas_0001_General  
Go--#SQL 

--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RE_INTERMED__RecetasElectronicas_XML' and xType = 'U' ) 
   Drop Table INT_RE_INTERMED__RecetasElectronicas_XML  
Go--#SQL   

Create Table INT_RE_INTERMED__RecetasElectronicas_XML 
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	Folio varchar(12) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate(), 	
		
	
	UMedica varchar(50) Not Null Default '', 
	Folio_SIADISSEP varchar(50) Not Null Default '', 
	TipoDeProceso int Not Null Default 0, 

	DisponibleSurtido bit Not Null Default 'true', 
	Surtidos Int Not Null Default 0, 
	Surtidos_Aplicados Int Not Null Default 0, 
	
	InformacionXML xml Not Null Default '' 
) 
Go--#SQL   

Alter Table INT_RE_INTERMED__RecetasElectronicas_XML 
	Add Constraint PK_INT_RE_INTERMED__RecetasElectronicas_XML Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   

