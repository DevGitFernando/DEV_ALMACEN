----------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario   
Go--#SQL   


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Admon_Detalles  
Go--#SQL   


----------------------------------------------------------------------------------------------------------------------------  
----------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Admon 
Go--#SQL    

Create Table FACT_Fuentes_De_Financiamiento_Admon 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdEstado varchar(2) Not Null, 	
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 

	FechaInicial datetime Not Null Default getdate(),  
	FechaFinal datetime Not Null Default getdate(),  
		
	Monto numeric(14,4) Not Null Default 0, 	
	Piezas int Not Null Default 0, 			
		
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL 
 
Alter Table FACT_Fuentes_De_Financiamiento_Admon Add Constraint PK_FACT_Fuentes_De_Financiamiento_Admon Primary Key ( IdFuenteFinanciamiento ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Admon Add Constraint FK_FACT_Fuentes_De_Financiamiento_Admon__CatSubClientes  
	Foreign Key ( IdCliente, IdSubCliente ) References CatSubClientes ( IdCliente, IdSubCliente )  
Go--#SQL  


-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Admon_Detalles  
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Admon_Detalles 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdFinanciamiento varchar(4) Not Null, 
	Descripcion varchar(100) Not Null Default '', 
	Monto numeric(14,4) Not Null Default 0, 
	Piezas int Not Null Default 0, 
	ValidarPolizaBeneficiario bit Not Null Default 'false', 
	LongitudMinimaPoliza int not Null Default 0, 
	LongitudMaximaPoliza int not Null Default 0, 		
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL 
 
Alter Table FACT_Fuentes_De_Financiamiento_Admon_Detalles 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Admon_Detalles Primary Key ( IdFuenteFinanciamiento, IdFinanciamiento ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Admon_Detalles Add Constraint FK_FACT_Fuentes_De_Financiamiento_Admon_Detalles__FACT_Fuentes_De_Financiamiento_Admon  
	Foreign Key ( IdFuenteFinanciamiento ) References FACT_Fuentes_De_Financiamiento_Admon ( IdFuenteFinanciamiento ) 
Go--#SQL 


-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdFinanciamiento varchar(4) Not Null, 
	ClaveSSA varchar(50) Not Null, 

	CantidadPresupuestadaPiezas int Not Null Default 0,   
	CantidadPresupuestada int Not Null Default 0,   
	CantidadAsignada int Not Null Default 0,   
	CantidadRestante int Not Null Default 0,   

	Costo Numeric(14,4) Not Null Default 0,
	Agrupacion Int Not Null Default 0,
	CostoUnitario Numeric(14,4) Not Null Default 0,
	TasaIva Numeric(14,4) Not Null Default 0,
	Iva Numeric(14,4) Not Null Default 0,
	ImporteNeto Numeric(14,4) Not Null Default 0,
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL 
 
Alter Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA Primary Key (  IdFuenteFinanciamiento, IdFinanciamiento , ClaveSSA ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA 
Add Constraint FK_FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA_FACT_Fuentes_De_Financiamiento_Detalles   
	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento ) 
	References FACT_Fuentes_De_Financiamiento_Detalles ( IdFuenteFinanciamiento, IdFinanciamiento ) 
Go--#SQL 


-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdFinanciamiento varchar(4) Not Null, 

	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

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

Alter Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia Primary Key (  IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA, Referencia_01, Referencia_05 ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia 
Add Constraint FK_FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia___FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA 
	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA ) 
	References FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA ( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA ) 
Go--#SQL  
 

-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze 
(
	IdFuenteFinanciamiento varchar(4) Not Null, 
	IdFinanciamiento varchar(4) Not Null, 

	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	ClaveSSA varchar(50) Not Null, 
	Referencia_01 varchar(100) Not Null Default '',  --- H's en el caso de HGO 
	Referencia_02 varchar(100) Not Null Default '', 
	Referencia_03 varchar(100) Not Null Default '', 
	Referencia_04 varchar(100) Not Null Default '', 
	Referencia_05 varchar(100) Not Null Default '', 

	Partida int Not Null Default 0, 
	AfectaEstadistica int Not Null Default 0, 
	AfectaEstadisticaMontos int Not Null Default 0, 

	TasaIva Numeric(14,4) Not Null Default 0,
	Costo Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0,
	ImporteNeto Numeric(14,4) Not Null Default 0,

	Status varchar(1) Not Null Default 'A' 
)
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze 
	Primary Key (  IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA, Referencia_01, Referencia_05, Partida ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze 
Add Constraint FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia_Desgloze____FK_FACT_FF__Admon_Detalles_ClavesSSA__Farmacia 
	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA, Referencia_01, Referencia_05 ) 
	References FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia ( IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA, Referencia_01, Referencia_05 ) 
Go--#SQL  



-- FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Farmacia
-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario 
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

Alter Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario 
	Add Constraint PK_FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario 
		Primary Key (  IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, Referencia_01, Referencia_05, ClaveSSA ) 
Go--#SQL 

Alter Table FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario 
Add Constraint FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA__Beneficiario____FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA
	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA ) 
	References FACT_Fuentes_De_Financiamiento_Admon_Detalles_ClavesSSA ( IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA ) 
Go--#SQL  

