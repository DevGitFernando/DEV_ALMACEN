---------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFG__FarmaciasRemisionado'  and xType = 'U' ) 
   Drop Table FACT_CFG__FarmaciasRemisionado  
Go--#SQL    

Create Table FACT_CFG__FarmaciasRemisionado 
( 

	IdEstado varchar(2) Not Null Default '',		
	IdFarmacia varchar(4) Not Null Default '', 
	Prioridad int Not Null Default 0, 

	Status varchar(2) Not Null Default 'A' 
	
) 
Go--#SQL 


Alter Table FACT_CFG__FarmaciasRemisionado Add Constraint PK_FACT_CFG__FarmaciasRemisionado Primary Key ( IdEstado, IdFarmacia )   
Go--#SQL 



