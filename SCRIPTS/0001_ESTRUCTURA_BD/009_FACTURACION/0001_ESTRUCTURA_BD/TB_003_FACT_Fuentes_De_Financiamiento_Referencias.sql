-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	ClaveSSA varchar(50) Not Null, 
	Referencia_01 varchar(500) Not Null Default '', 
	Referencia_02 varchar(500) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
)
Go--#SQL 
 
Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias Primary Key (  IdFuenteFinanciamiento, ClaveSSA ) 
Go--#SQL 


----Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias 
----Add Constraint FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_Referencias___FACT_Fuentes_De_Financiamiento_Detalles   
----	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento ) 
----	References FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA ( IdFuenteFinanciamiento, IdFinanciamiento ) 
----Go--#SQL 


-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_Referencias'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_Referencias   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Detalles_Referencias 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdFinanciamiento varchar(4) Not Null, 
	Referencia_01 varchar(500) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
)
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Detalles_Referencias 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Detalles_Referencias Primary Key  ( IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01 ) 
Go--#SQL 



/*  


Begin Tran 

--		commit tran 

--		rollback tran 

	Delete from FACT_Fuentes_De_Financiamiento_Detalles_Referencias 



	Insert Into FACT_Fuentes_De_Financiamiento_Detalles_Referencias (  IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, Status ) 
	select IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01, 'A' as Status 
	from FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia 
	where IdFuenteFinanciamiento >= 11 
	group by IdFuenteFinanciamiento, IdFinanciamiento, Referencia_01 
	Order by IdFuenteFinanciamiento, IdFinanciamiento 



*/ 


