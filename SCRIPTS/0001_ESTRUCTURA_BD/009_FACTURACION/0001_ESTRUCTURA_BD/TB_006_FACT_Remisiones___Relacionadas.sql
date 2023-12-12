---------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Relacionadas'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Relacionadas  
Go--#SQL    

Create Table FACT_Remisiones_Relacionadas 
( 
	IdEmpresa varchar(3) Not Null Default '',		
	IdEstado varchar(2) Not Null Default '',		
	IdFarmaciaGenera varchar(4) Not Null Default '', 
	FolioRemision varchar(10) Not Null Default '',  
	FolioRemision_Relacionado varchar(10) Not Null Default '' 	
) 
Go--#SQL 


Alter Table FACT_Remisiones_Relacionadas Add Constraint PK_FACT_Remisiones_Relacionadas Primary key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, FolioRemision_Relacionado )  
Go--#SQL 


Alter Table FACT_Remisiones_Relacionadas Add Constraint FACT_Remisiones_Relacionadas___FACT_Remisiones 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
	References FACT_Remisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 

Alter Table FACT_Remisiones_Relacionadas Add Constraint FACT_Remisiones_Relacionadas___FACT_Remisiones__Relacionadas  
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision_Relacionado ) 
	References FACT_Remisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 

