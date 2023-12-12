If Exists ( Select * From Sysobjects (NoLock) where Name = 'ADMI_Consumos_Claves' and xType = 'U' ) 
   Drop Table ADMI_Consumos_Claves 
Go--#SQL 

Create Table ADMI_Consumos_Claves 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdJurisdiccion varchar(3) Not Null, 
	
	Año int Not Null, 
	Mes int Not Null, 
	ClaveSSA varchar(20) Not Null, 
	Cantidad int Not Null Default 0 	
) 
Go--#SQL 

Alter Table ADMI_Consumos_Claves Add Constraint PK_ADMI_Consumos_Claves 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Año, Mes, ClaveSSA ) 
Go--#SQL    

------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) where Name = 'INV_Distribucion_Excedentes' and xType = 'U' ) 
   Drop Table INV_Distribucion_Excedentes 
Go--#SQL 
   
Create Table INV_Distribucion_Excedentes 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	
	MesesAnalisis int Not Null Default 0, 
	MesesStock int Not Null Default 0,  
		
	ClaveSSA varchar(20) Not Null, 
	
	Excedente int Not Null Default 0, 
	Distribuido int Not Null Default 0 
) 
Go--#SQL 

------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) where Name = 'INV_Distribucion_Existencias' and xType = 'U' ) 
   Drop Table INV_Distribucion_Existencias 
Go--#SQL 
   
Create Table INV_Distribucion_Existencias 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdJurisdiccion varchar(3) Not Null, 
		
	ClaveSSA varchar(20) Not Null, 
    Existencia int Not Null Default 0 
)  
Go--#SQL 


------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) where Name = 'INV_Distribucion_Faltantes' and xType = 'U' ) 
   Drop Table INV_Distribucion_Faltantes 
Go--#SQL 
   
Create Table INV_Distribucion_Faltantes 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdJurisdiccion varchar(3) Not Null, 
		
	ClaveSSA varchar(20) Not Null, 
    Consumo int Not Null Default 0, 
    ConsumoMensual int Not Null Default 0,     
	StockSugerido int Not Null Default 0,  
	StockAsignado int Not Null Default 0 	
) 
Go--#SQL 

