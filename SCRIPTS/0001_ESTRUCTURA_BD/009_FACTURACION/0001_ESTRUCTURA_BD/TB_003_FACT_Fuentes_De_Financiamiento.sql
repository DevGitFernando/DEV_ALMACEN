-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario   
Go--#SQL   

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles  
Go--#SQL   


----------------------------------------------------------------------------------------------------------------------------  
----------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento 
Go--#SQL    

Create Table FACT_Fuentes_De_Financiamiento 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdEstado varchar(2) Not Null, 	
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	NumeroDeContrato varchar(100) Not Null Default '',   
	NumeroDeLicitacion varchar(100) Not Null Default '',   

	EsSoloControlados bit Not Null Default 'false', 
	TipoClasificacionSSA Int Not Null Default 3, 
	
	FechaInicial datetime Not Null Default getdate(),  
	FechaFinal datetime Not Null Default getdate(),  
		
	Monto numeric(14,4) Not Null Default 0, 	
	Piezas int Not Null Default 0, 			
		
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL 
 
Alter Table FACT_Fuentes_De_Financiamiento Add Constraint PK_FACT_Fuentes_De_Financiamiento Primary Key ( IdFuenteFinanciamiento ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento Add Constraint FK_FACT_Fuentes_De_Financiamiento__CatSubClientes  
	Foreign Key ( IdCliente, IdSubCliente ) References CatSubClientes ( IdCliente, IdSubCliente )  
Go--#SQL  


-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles  
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Detalles 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdFinanciamiento varchar(4) Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	Monto numeric(14,4) Not Null Default 0, 
	Piezas int Not Null Default 0, 

	Prioridad int Not Null Default 0, 

	ValidarPolizaBeneficiario bit Not Null Default 'false', 
	LongitudMinimaPoliza int not Null Default 0, 
	LongitudMaximaPoliza int not Null Default 0, 		
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL 
 
Alter Table FACT_Fuentes_De_Financiamiento_Detalles 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Detalles Primary Key ( IdFuenteFinanciamiento, IdFinanciamiento ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Detalles Add Constraint FK_FACT_Fuentes_De_Financiamiento_Detalles__FACT_Fuentes_De_Financiamiento  
	Foreign Key ( IdFuenteFinanciamiento ) References FACT_Fuentes_De_Financiamiento ( IdFuenteFinanciamiento ) 
Go--#SQL 


-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
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
 
Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA Primary Key (  IdFuenteFinanciamiento, IdFinanciamiento , ClaveSSA ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
Add Constraint FK_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA_FACT_Fuentes_De_Financiamiento_Detalles   
	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento ) 
	References FACT_Fuentes_De_Financiamiento_Detalles ( IdFuenteFinanciamiento, IdFinanciamiento ) 
Go--#SQL 


-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdFinanciamiento varchar(4) Not Null, 

	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	Referencia_01 varchar(100) Not Null Default '', 
	Referencia_02 varchar(100) Not Null Default '', 
	Referencia_03 varchar(100) Not Null Default '', 
	Referencia_04 varchar(100) Not Null Default '', 

	ClaveSSA varchar(50) Not Null, 
	CantidadPresupuestadaPiezas int Not Null Default 0,   
	CantidadPresupuestada int Not Null Default 0,   
	CantidadAsignada int Not Null Default 0,   
	CantidadRestante int Not Null Default 0,   
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia Primary Key (  IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA, Referencia_01 ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia 
Add Constraint FK_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia___FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA ) 
	References FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA ( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA ) 
Go--#SQL  





-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdFinanciamiento varchar(4) Not Null, 

	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdBeneficiario varchar(8) Not Null, 

	Referencia_01 varchar(100) Not Null Default '',  --- H's en el caso de HGO 
	Referencia_02 varchar(100) Not Null Default '', 
	Referencia_03 varchar(100) Not Null Default '', 
	Referencia_04 varchar(100) Not Null Default '', 
	Referencia_05 varchar(100) Not Null Default '', 

	ClaveSSA varchar(50) Not Null, 
	CantidadPresupuestadaPiezas int Not Null Default 0,   
	CantidadPresupuestada int Not Null Default 0,   
	CantidadAsignada int Not Null Default 0,   
	CantidadRestante int Not Null Default 0,   
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario 
		Primary Key (  IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, Referencia_01, Referencia_05, ClaveSSA ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario 
Add Constraint FK_FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Beneficiario___FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA ) 
	References FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA ( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA ) 
Go--#SQL  
