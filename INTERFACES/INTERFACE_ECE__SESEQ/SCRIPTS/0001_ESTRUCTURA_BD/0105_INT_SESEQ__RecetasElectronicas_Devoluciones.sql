Set NoCount On 
Go--#SQL   

--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_SESEQ__RecetasElectronicas_0007_Devoluciones' and xType = 'U' ) 
   Drop Table INT_SESEQ__RecetasElectronicas_0007_Devoluciones  
Go--#SQL   

Create Table INT_SESEQ__RecetasElectronicas_0007_Devoluciones 
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 	
	Folio varchar(12) Not Null Default '', 

	FolioDevolucion varchar(10) Not Null Default '', 
	FechaDeDevolucion datetime Default getdate(),  
	FolioSurtido varchar(10) Not Null Default '', 		
	
	TipoDeProceso int Not Null Default 1, 
	FolioReceta varchar(50) Not Null Default '', 		
	FechaReceta datetime Not Null Default getdate(), 
	
	Procesado bit Not Null Default 'false', 
	FechaProcesado datetime Not Null Default getdate(),  	
	IntentosDeEnvio int Not Null Default 0   
) 
Go--#SQL   

Alter Table INT_SESEQ__RecetasElectronicas_0007_Devoluciones 
	Add Constraint PK_INT_SESEQ__RecetasElectronicas_0007_Devoluciones Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioDevolucion, FolioSurtido ) 
Go--#SQL   

----Alter Table INT_SESEQ__RecetasElectronicas_0007_Devoluciones 
----	Add Constraint FK_INT_SESEQ__RecetasElectronicas_0007_Devoluciones___INT_SESEQ__RecetasElectronicas_XML 
----	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioXML ) 
----	References INT_SESEQ__RecetasElectronicas_XML ( IdEmpresa, IdEstado, IdFarmacia, Folio ) 
----Go--#SQL   

