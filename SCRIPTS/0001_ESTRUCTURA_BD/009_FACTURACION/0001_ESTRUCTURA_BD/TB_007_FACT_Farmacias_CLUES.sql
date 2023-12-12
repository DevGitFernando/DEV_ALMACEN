

------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Farmacias_CLUES'  and xType = 'U' ) 
   Drop Table FACT_Farmacias_CLUES  
Go--#SQL    

Create Table FACT_Farmacias_CLUES 
( 
	IdEstado varchar(2) Not Null,		
	IdFarmacia varchar(4) Not Null,				
	CLUES varchar(20) Not Null Default '', 
	NombreOficial varchar(200) Not Null Default '',

	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0,     
) 
Go--#SQL 

Alter Table FACT_Farmacias_CLUES Add Constraint PK_FACT_Farmacias_CLUES Primary Key ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table FACT_Farmacias_CLUES Add Constraint FK_FACT_Farmacias_CLUES__CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )  
Go--#SQL 



------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Farmacias_CLUES__CentroDeCostos'  and xType = 'U' ) 
   Drop Table FACT_Farmacias_CLUES__CentroDeCostos  
Go--#SQL    

CREATE TABLE FACT_Farmacias_CLUES__CentroDeCostos
(
	IdEstado varchar(2) Not Null,		
	IdFarmacia varchar(4) Not Null,				
	CLUES varchar(20) Not Null Default '', 
	NombreOficial varchar(200) Not Null Default '', 
	CentroDeCosto varchar(200) Not Null Default '',
	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0,     
) 
Go--#SQL  

Alter Table FACT_Farmacias_CLUES__CentroDeCostos Add Constraint PK_FACT_Farmacias_CLUES__CentroDeCostos Primary Key ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table FACT_Farmacias_CLUES__CentroDeCostos Add Constraint FK_FACT_Farmacias_CLUES__CentroDeCostos__CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )  
Go--#SQL 




