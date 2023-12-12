----------------------------------------------------------------------------------------------   
---------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_Fuentes_De_Financiamiento_Detalles_Documentos__CargaMasiva' and xType = 'U' ) 
   Drop Table FACT_Fuentes_De_Financiamiento_Detalles_Documentos__CargaMasiva
Go--#SQL

CREATE TABLE FACT_Fuentes_De_Financiamiento_Detalles_Documentos__CargaMasiva
(
	IdFuenteFinanciamiento varchar(4) NOT NULL,
	IdFinanciamiento varchar(4) NOT NULL,
	IdDocumento varchar(4) NOT NULL,
	NombreDocumento varchar(200) NOT NULL DEFAULT '',
	IdFuenteFinanciamiento_Relacionado varchar(4) NOT NULL DEFAULT '',
	IdFinanciamiento_Relacionado varchar(4) NOT NULL DEFAULT '',
	IdDocumento_Relacionado varchar(4) NOT NULL DEFAULT '',
	EsRelacionado bit NOT NULL DEFAULT 0,
	OrigenDeInsumo int NOT NULL DEFAULT 0,
	TipoDeDocumento int NOT NULL DEFAULT 0,
	TipoDeInsumo int NOT NULL DEFAULT 0,
	ValorNominal numeric(14, 4) NOT NULL DEFAULT 0,
	ImporteAplicado numeric(14, 4) NOT NULL DEFAULT 0,
	ImporteAplicado_Aux numeric(14, 4) NOT NULL DEFAULT 0,
	ImporteRestante numeric(14, 4) NOT NULL DEFAULT 0,
	AplicaFarmacia bit NOT NULL DEFAULT 0,
	AplicaAlmacen bit NOT NULL DEFAULT 0,
	EsProgramaEspecial bit NOT NULL DEFAULT 0,
	TipoDeBeneficiario int NOT NULL DEFAULT 0,
	Status varchar(1) NOT NULL DEFAULT 'A'
) 
Go--#SQL


--Select * From FACT_Fuentes_De_Financiamiento_Detalles_Documentos__CargaMasiva