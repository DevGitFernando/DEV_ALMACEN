Set NoCount On 
Go--#SQL   


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_SESEQ__Informacion_Bitacora' and xType = 'U' ) 
   Drop Table INT_SESEQ__Informacion_Bitacora  
Go--#SQL   

Create Table INT_SESEQ__Informacion_Bitacora 
( 
	Keyx int Identity, 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FechaProcesada varchar(10) Not Null Default convert(varchar(10), getdate(), 120), 
	Tipo smallint Not Null Default 0,  
	ExistenDatos bit Not Null Default 'false',  
	HuboError bit Not Null Default 'false',  
	Respuesta_Integracion varchar(2000) Not Null Default '', 
	FechaRegistro datetime Not Null Default getdate()   
) 
Go--#SQL   

Alter Table INT_SESEQ__Informacion_Bitacora 
	Add Constraint PK_INT_SESEQ__Informacion_Bitacora Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FechaProcesada, Tipo ) 
Go--#SQL   




