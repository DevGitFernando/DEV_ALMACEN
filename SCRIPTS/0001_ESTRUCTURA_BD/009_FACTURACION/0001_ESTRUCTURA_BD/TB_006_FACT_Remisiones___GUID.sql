---------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones___GUID'  and xType = 'U' ) 
   Drop Table FACT_Remisiones___GUID  
Go--#SQL    

Create Table FACT_Remisiones___GUID 
( 
	IdEmpresa varchar(3) Not Null Default '',		
	IdEstado varchar(2) Not Null Default '',		
	IdFarmaciaGenera varchar(4) Not Null Default '', 
	GUID varchar(100) Not Null Default '', --Host_name() + convert(varchar(10), getdate(), 120 ), 

	HostName varchar(100) Not Null Default Host_name(), 
	FechaRegistro datetime Not Null Default getdate(), 
	Keyx int identity(1,1) 
	
) 
Go--#SQL 


Alter Table FACT_Remisiones___GUID Add Constraint PK_FACT_Remisiones___GUID Primary Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, GUID )   
Go--#SQL 


