Set NoCount On 
Go--#SQL   


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas' and xType = 'U' ) 
   Drop Table INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas 
Go--#SQL   

Create Table INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas 
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	Folio varchar(12) Not Null Default '', 

	FolioReceta varchar(50) Not Null Default '', 		
	FechaReceta datetime Not Null Default getdate(), 
	FechaEnvioReceta datetime Not Null Default getdate(), 	

	Expediente varchar(50) Not Null Default '', 
	
	Procesado bit Not Null Default 'false', 
	FechaProcesado datetime Not Null Default getdate() 
) 
Go--#SQL   

Alter Table INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas 
	Add Constraint PK_INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL  	

Alter Table INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas 
	Add Constraint FK_INT_SIADISSEP__RecetasElectronicas_0005_CancelacionRecetas___INT_SIADISSEP__RecetasElectronicas_XML 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
	References INT_SIADISSEP__RecetasElectronicas_XML ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
Go--#SQL   


