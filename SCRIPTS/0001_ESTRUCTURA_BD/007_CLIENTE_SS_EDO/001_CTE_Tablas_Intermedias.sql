If Exists ( Select Name From Sysobjects Where Name = 'CTE_FarmaciasProcesar' and xType = 'U' )
      Drop Table CTE_FarmaciasProcesar
Go--#SQL 

Create Table CTE_FarmaciasProcesar
(
     IdEstado varchar(2) Not Null Default '',
     IdFarmacia varchar(4) Not Null Default '',
     Status varchar(1) Not Null Default 'A',
     Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CTE_FarmaciasProcesar Add Constraint Pk_CTE_FarmaciasProcesar Primary Key ( IdEstado, IdFarmacia )
Go--#SQL 

------------------- 
If Exists ( Select Name From Sysobjects Where Name = 'CteReg_Farmacias_Procesar_Existencia' and xType = 'U' )
      Drop Table CteReg_Farmacias_Procesar_Existencia
Go--#SQL 

Create Table CteReg_Farmacias_Procesar_Existencia
(
     IdEstado varchar(2) Not Null Default '',
     IdFarmacia varchar(4) Not Null Default '',
     Status varchar(1) Not Null Default 'A',
     Actualizado tinyint Not Null Default 0 
)
Go--#SQL

Alter Table CteReg_Farmacias_Procesar_Existencia Add Constraint Pk_CteReg_Farmacias_Procesar_Existencia Primary Key ( IdEstado, IdFarmacia )
Go--#SQL 	
	
If Exists ( Select Name From Sysobjects (NOLock) Where Name = 'CTE_ClavesAProcesar' and xType = 'U' ) 
	Drop Table CTE_ClavesAProcesar
Go--#SQL

Create Table CTE_ClavesAProcesar
(	
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,
	IdClaveSSA varchar(4) Not Null,	
	ClaveSSA varchar(30)Not Null,		
	Status varchar(1) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0	
) 
Go--#SQL 