

---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From sysobjects (NoLock) Where Name = 'CatClavesSSA_Causes' and xType = 'U' ) 
   Drop Table CatClavesSSA_Causes    
Go--#SQL 
   
Create Table CatClavesSSA_Causes 
( 
	-- IdClaveSSA varchar(4) Not Null Default '', 
	Año int Not Null Default 0,		
	ClaveSSA varchar(30) Not Null Default '',	
	Descripcion varchar(7500) Not Null Default '', 	
	IdPresentacion varchar(3) Not Null Default '',
	ContenidoPaquete int Not Null Default 0,
	EsSeguroPopular tinyint Not Null Default 0,
	EsControlado bit Not Null Default 'False',
	EsAntibiotico bit Not Null Default 'False',  
	PrecioBase numeric(14, 4) Not Null Default 0,
	Porcentaje numeric(14, 4) Not Null Default 0,  
	PrecioAdmon numeric(14,4) Not Null Default 0,
	PrecioNeto numeric(14,4) Not Null Default 0,
	EsDollar tinyint Not Null Default 0,
	Status varchar(1) Not Null Default '', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table CatClavesSSA_Causes Add Constraint PK_CatClavesSSA_Causes Primary Key ( Año, ClaveSSA ) 
Go--#SQL  

