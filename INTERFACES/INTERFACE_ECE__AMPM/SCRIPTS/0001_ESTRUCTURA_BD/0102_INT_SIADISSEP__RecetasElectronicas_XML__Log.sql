Set NoCount On 
Go--#SQL   


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_SIADISSEP__RecetasElectronicas_XML__Log' and xType = 'U' ) 
   Drop Table INT_SIADISSEP__RecetasElectronicas_XML__Log  
Go--#SQL   

Create Table INT_SIADISSEP__RecetasElectronicas_XML__Log 
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

Alter Table INT_SIADISSEP__RecetasElectronicas_XML__Log 
	Add Constraint PK_INT_SIADISSEP__RecetasElectronicas_XML__Log Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   

