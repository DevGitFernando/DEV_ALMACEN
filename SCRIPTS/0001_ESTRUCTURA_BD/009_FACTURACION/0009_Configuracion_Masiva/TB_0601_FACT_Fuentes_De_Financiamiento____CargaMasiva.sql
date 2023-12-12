
-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_FF__CargaMasiva_01___Detalles__Claves'  and xType = 'U' ) 
   Drop Table FACT_FF__CargaMasiva_01___Detalles__Claves   
Go--#SQL   

Create Table FACT_FF__CargaMasiva_01___Detalles__Claves 
(
	Orden int Not Null Default 0, 
	IdFuenteFinanciamiento varchar(10) Null Default '', 
	IdFinanciamiento varchar(10) Null Default '', 

	ClaveSSA varchar(50) Null Default '', 
	PorcentajeParticipacion numeric(14, 4) Not Null Default 0,   
	CantidadPresupuestadaPiezas int Null Default 0,   
	CantidadPresupuestada int Null Default 0,   
	CantidadAsignada int Null Default 0,   
	CantidadRestante int Null Default 0,   
	Status varchar(1) Null Default 'A', 
	Actualizado smallint Null Default 0,  

	Referencia_01 varchar(500) Null Default '',  --- H's en el caso de HGO 
	Referencia_02 varchar(500) Null Default '', 
	Referencia_03 varchar(500) Null Default '', 
	Referencia_04 varchar(500) Null Default '' ,
	Referencia_05 varchar(500) Null Default '' 
) 
Go--#SQL 



-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_FF__CargaMasiva_02___Detalles__Claves_Admon'  and xType = 'U' ) 
   Drop Table FACT_FF__CargaMasiva_02___Detalles__Claves_Admon   
Go--#SQL   

Create Table FACT_FF__CargaMasiva_02___Detalles__Claves_Admon 
(
	Orden int Not Null Default 0, 
	IdFuenteFinanciamiento varchar(10) Null Default '', 
	IdFinanciamiento varchar(10) Null Default '', 

	ClaveSSA varchar(50) Null Default '', 
	PorcentajeParticipacion numeric(14, 4) Not Null Default 0,   
	CantidadPresupuestadaPiezas int Null Default 0,   
	CantidadPresupuestada int Null Default 0,   
	CantidadAsignada int Null Default 0,   
	CantidadRestante int Null Default 0, 
	
	Costo numeric(14, 4) Not Null Default 0,   
	Agrupacion numeric(14, 4) Not Null Default 0,   	
	CostoUnitario numeric(14, 4) Not Null Default 0,   
	TasaIVA numeric(14, 4) Not Null Default 0, 	
	IVA numeric(14, 4) Not Null Default 0, 
	ImporteNeto numeric(14, 4) Not Null Default 0, 

	Status varchar(1) Null Default 'A', 
	Actualizado smallint Null Default 0,  

	Referencia_01 varchar(500) Null Default '',  --- H's en el caso de HGO 
	Referencia_02 varchar(500) Null Default '', 
	Referencia_03 varchar(500) Null Default '', 
	Referencia_04 varchar(500) Null Default '' ,
	Referencia_05 varchar(500) Null Default '' 
) 
Go--#SQL 



/* 
(
	Orden int Not Null Default 0, 
	IdFuenteFinanciamiento varchar(4) Null Default '', 
	IdFinanciamiento varchar(4) Null Default '', 

	IdEstado varchar(2) Null, 
	IdFarmacia varchar(4) Null, 
	IdCliente varchar(4) Null, 
	IdSubCliente varchar(4) Null, 
	IdBeneficiario varchar(8) Null, 

	Referencia_01 varchar(100) Null Default '',  --- H's en el caso de HGO 
	Referencia_02 varchar(100) Null Default '', 
	Referencia_03 varchar(100) Null Default '', 
	Referencia_04 varchar(100) Null Default '', 

	EsPrincipalReferencia_01 bit Null Default 'false',  

	ClaveSSA varchar(50) Null Default '', 
	CantidadPresupuestadaPiezas int Null Default 0,   
	CantidadPresupuestada int Null Default 0,   
	CantidadAsignada int Null Default 0,   
	CantidadRestante int Null Default 0,   
	Status varchar(1) Null Default 'A', 
	Actualizado smallint Null Default 0 
) 
*/ 
Go--#SQL 

