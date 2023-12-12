
-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia   
Go--#SQL   


-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdFinanciamiento varchar(4) Not Null, 
	ClaveSSA varchar(50) Not Null, 
	PorcParticipacion numeric(14,4) Not Null Default 100, 
	CantidadPresupuestadaPiezas int Not Null Default 0,   
	CantidadPresupuestada int Not Null Default 0,   
	CantidadAsignada int Not Null Default 0,   
	CantidadRestante int Not Null Default 0,   
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL 
 
Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento Primary Key (  IdFuenteFinanciamiento, IdFinanciamiento , ClaveSSA ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento 
Add Constraint FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento___FACT_Fuentes_De_Financiamiento_Detalles   
	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento ) 
	References FACT_Fuentes_De_Financiamiento_Detalles ( IdFuenteFinanciamiento, IdFinanciamiento ) 
Go--#SQL 


-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdFinanciamiento varchar(4) Not Null, 

	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	ClaveSSA varchar(50) Not Null, 
	CantidadPresupuestadaPiezas int Not Null Default 0,   
	CantidadPresupuestada int Not Null Default 0,   
	CantidadAsignada int Not Null Default 0,   
	CantidadRestante int Not Null Default 0,   
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia 
	Primary Key (  IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia 
Add Constraint FK_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento__Farmacia___FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA ) 
	References FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Incremento ( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA ) 
Go--#SQL  

