----------------------------------------------------------------------------------------------   
---------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento__CargaMasiva' and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento__CargaMasiva
Go--#SQL

CREATE TABLE FACT_Fuentes_De_Financiamiento__CargaMasiva
(
	IdFuenteFinanciamiento varchar(4) Not Null Default '', 
	IdFinanciamiento varchar(4) Not Null Default '', 
	ClaveSSA varchar(50) Not Null Default '', 
	PorcParticipacion numeric(14,  4) Not Null Default 100, 
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

	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	Referencia_01 varchar(100) Not Null Default '', 
	Referencia_02 varchar(100) Not Null Default '', 
	Referencia_03 varchar(100) Not Null Default '', 
	Referencia_04 varchar(100) Not Null Default '', 
	Referencia_05 varchar(100) Not Null Default '', 

	IdBeneficiario varchar(8) Not Null Default '', 
	NombreBeneficiario varchar(200) Not Null Default '', 
	IdCliente varchar(4) Not Null Default '', 
	IdSubCliente varchar(4) Not Null Default '', 
	IdBeneficiario_Relacionado varchar(8) NOT NULL  DEFAULT '', 

	Tipo int Not Null Default 0, 
	PrecioBase numeric(14,  4) Not Null Default 0, 
	Incremento numeric(14,  4) Not Null Default 0, 
	PorcentajeIncremento numeric(14,  4) Not Null Default 0, 
	PrecioFinal numeric(14,  4) Not Null Default 0, 

	IdGrupo Int Not Null Default 0, 
	TipoRemision Int Not Null Default 0, 
	FechaVigencia Varchar(10) Not Null Default ''
) 
Go--#SQL

--Select * From FACT_Fuentes_De_Financiamiento__CargaMasiva
--sp_listaColumnas FACT_Fuentes_De_Financiamiento_Beneficiarios_Relacionados