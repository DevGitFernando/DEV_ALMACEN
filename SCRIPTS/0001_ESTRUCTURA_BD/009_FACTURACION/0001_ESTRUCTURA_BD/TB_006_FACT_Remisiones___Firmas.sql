---------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones___Firmas'  and xType = 'U' ) 
   Drop Table FACT_Remisiones___Firmas  
Go--#SQL    

Create Table FACT_Remisiones___Firmas 
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	
	Nombre_01_Director varchar(200) Not Null Default '', 
	Nombre_02_Administrador varchar(200) Not Null Default '', 
	Nombre_03_Responsable varchar(200) Not Null Default '', 
	Nombre_04_PersonalPropio varchar(200) Not Null Default '', 
	TipoDeInsumo int Not Null Default 0  
	
) 
Go--#SQL 


Alter Table FACT_Remisiones___Firmas Add Constraint PK_FACT_Remisiones___Firmas Primary Key ( IdEmpresa, IdEstado, IdFarmacia, TipoDeInsumo )   
Go--#SQL 
