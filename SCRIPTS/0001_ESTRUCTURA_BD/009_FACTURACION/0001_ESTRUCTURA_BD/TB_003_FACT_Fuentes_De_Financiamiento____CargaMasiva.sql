
-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles__Beneficiario__LOAD'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles__Beneficiario__LOAD   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento_Detalles__Beneficiario__LOAD 
(
	IdFuenteFinanciamiento varchar(4) Null,  
	IdFinanciamiento varchar(4) Null,  

	IdEstado varchar(2) Null,  
	IdFarmacia varchar(4) Null,  
	IdCliente varchar(4) Null,  
	IdSubCliente varchar(4) Null,  
	IdBeneficiario varchar(8) Null,  

	Referencia_01 varchar(100) Null Default '',   --- H's en el caso de HGO 
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
Go--#SQL 


-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento__CargaMasiva'  and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento__CargaMasiva   
Go--#SQL   

Create Table FACT_Fuentes_De_Financiamiento__CargaMasiva 
(
	IdFuenteFinanciamiento varchar(4) Not Null Default '', 
	IdFinanciamiento varchar(4) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	ClaveSSA varchar(50) Not Null Default '', 
	PorcParticipacion numeric(14,  4) Not Null Default '', 
	CantidadPresupuestadaPiezas int Not Null Default 0, 
	CantidadPresupuestada int Not Null Default 0, 
	CantidadAsignada int Not Null Default 0, 
	CantidadRestante int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0, 
	SAT_ClaveProducto_Servicio varchar(20) Not Null Default '', 
	SAT_UnidadDeMedida varchar(5) Not Null Default '', 
	CostoBase numeric(14,  4) Not Null Default 0, 
	Porcentaje_01 numeric(14,  4) Not Null Default 0, 
	Porcentaje_02 numeric(14,  4) Not Null Default 0, 
	Costo numeric(14,  4) Not Null Default 0, 
	Agrupacion int Not Null Default 0, 
	CostoUnitario numeric(14,  4) Not Null Default 0, 
	TasaIva numeric(14,  4) Not Null Default 0, 
	Iva numeric(14,  4) Not Null Default 0, 
	ImporteNeto numeric(14,  4) Not Null Default 0, 
	Referencia_01 varchar(100) Not Null Default '', 
	Referencia_02 varchar(100) Not Null Default '', 
	Referencia_03 varchar(100) Not Null Default '', 
	Referencia_04 varchar(100) Not Null Default '', 
	Referencia_05 varchar(100) Not Null Default '', 
	IdBeneficiario varchar(8) Not Null Default '', 
	NombreBeneficiario varchar(200) Not Null Default '', 
	IdCliente varchar(4) Not Null Default '', 
	IdSubCliente varchar(4) Not Null Default '', 
	IdBeneficiario_Relacionado varchar(8) Not Null Default '', 
	Tipo int Not Null Default 0, 
	PrecioBase numeric(14,  4) Not Null Default 0, 
	Incremento numeric(14,  4) Not Null Default 0, 
	PorcentajeIncremento numeric(14,  4) Not Null Default 0, 
	PrecioFinal numeric(14,  4) Not Null Default 0, 
	IdGrupo int Not Null Default 0, 
	TipoRemision int Not Null Default 0, 
	FechaVigencia varchar(10) Not Null Default 0
) 
Go--#SQL 
