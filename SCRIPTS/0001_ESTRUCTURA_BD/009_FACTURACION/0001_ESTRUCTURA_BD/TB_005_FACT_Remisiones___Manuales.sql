
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Manuales_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Manuales_Detalles   
Go--#SQL    

------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Manuales'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Manuales  
Go--#SQL    

Create Table FACT_Remisiones_Manuales
( 
	GUID varchar(200) Not Null Default newid(), 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,		
	IdFarmaciaGenera varchar(4) Not Null,				-- Referencia solo para el Personal 
	FechaRemision datetime Not Null Default getdate(), 
	FechaValidacion datetime Not Null Default getdate(), 
	FolioRemision varchar(10) Not Null, 
	TipoDeRemision smallint Not Null Default 0,  -- 1 ==> Insumos, 2 ==> Administracion 
	TipoInsumo varchar(2) Not Null Default '',
	OrigenInsumo smallint Not Null Default 0,  -- 0 ==> Venta, 1 ==> Consigna  
	EsFacturada bit Not Null Default 'false',
	EsFacturable bit Not Null Default 'false',  
	EsExcedente smallint Not Null Default 0, 
	
	IdPersonalRemision varchar(4) Not Null Default '', 
	IdPersonalValida varchar(4) Not Null Default '', 
	IdFuenteFinanciamiento varchar(4) Not Null Default '', 
	IdFinanciamiento varchar(4) Not Null Default '', 
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Total numeric(14,4) Not Null Default 0, 		

	Observaciones varchar(200) Not Null Default '', 
    
	FechaInicial datetime Not Null Default convert(varchar(10), getdate(), 120),
	FechaFinal datetime Not Null Default convert(varchar(10), getdate(), 120),

	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0    --- 100 ==> Actualizado 
) 
Go--#SQL 

Alter Table FACT_Remisiones_Manuales Add Constraint PK_FACT_Remisiones_Manuales 
	Primary Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 

Alter Table FACT_Remisiones_Manuales Add Constraint FK_FACT_Remisiones_Manuales__CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
Go--#SQL 

Alter Table FACT_Remisiones_Manuales Add Constraint FK_FACT_Remisiones_Manuales__CatFarmacias 
	Foreign Key ( IdEstado, IdFarmaciaGenera ) References CatFarmacias ( IdEstado, IdFarmacia )  
Go--#SQL 

Alter Table FACT_Remisiones_Manuales Add Constraint FK_FACT_Remisiones_Manuales__FACT_TiposDeRemisiones
	Foreign Key ( TipoDeRemision ) References FACT_TiposDeRemisiones ( TipoDeRemision )  
Go--#SQL 

Alter Table FACT_Remisiones_Manuales Add Constraint FK_FACT_Remisiones_Manuales__FACT_Fuentes_De_Financiamiento_Detalles 
	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento ) 
	References FACT_Fuentes_De_Financiamiento_Detalles ( IdFuenteFinanciamiento, IdFinanciamiento )  
Go--#SQL 


------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Manuales_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Manuales_Detalles   
Go--#SQL    

Create Table FACT_Remisiones_Manuales_Detalles
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,			
	IdFarmaciaGenera varchar(4) Not Null,	
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null, 
	FolioVenta varchar(8) Not Null, 
	Partida int Not Null, 
	FolioRemision varchar(10) Not Null,		-- Consecutivo de Facturacion 		
	
	IdCliente varchar(4) Not Null Default '',	
	IdSubCliente varchar(4) Not Null Default '',	
	IdBeneficiario varchar(8) Not Null Default '',	

	IdFuenteFinanciamiento varchar(4) Not Null Default '',	
	IdFinanciamiento varchar(4) Not Null Default '',	
	IdPrograma varchar(4) Not Null Default '',	
	IdSubPrograma varchar(4) Not Null Default '',					
	
	ClaveSSA varchar(20) Not Null Default '', 
	IdProducto varchar(8) Not Null Default '', 
	CodigoEAN varchar(30) Not Null Default '', 
	ClaveLote varchar(30) Not Null Default '', 
	PrecioLicitado numeric(14,4) Not Null Default 0, 	
	PrecioLicitadoUnitario numeric(14,4) Not Null Default 0, 		
	Cantidad_Agrupada numeric(14,4) Not Null Default 0, 	
	Cantidad numeric(14,4) Not Null Default 0, 	

	Cantidad_Agrupada__Distribuida numeric(14,4) Not Null Default 0, 	
	Cantidad__Distribuida numeric(14,4) Not Null Default 0, 	

	
	TasaIva numeric(14,4) Not Null Default 0, 	
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Importe numeric(14,4) Not Null Default 0,  
	
	Referencia_01 varchar(500) Not Null Default '', 
	Referencia_02 varchar(500) Not Null Default '', 
	Referencia_03 varchar(500) Not Null Default '', 
	Referencia_04 varchar(500) Not Null Default '',   
	Referencia_05 varchar(500) Not Null Default '',   
	Referencia_06 varchar(500) Not Null Default ''  

) 
Go--#SQL 

Alter Table FACT_Remisiones_Manuales_Detalles Add Constraint PK_FACT_Remisiones_Manuales_Detalles 
	Primary Key 
	( 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, --Partida, 
		FolioRemision, 
		IdCliente, IdSubCliente, IdBeneficiario, 
		IdFuenteFinanciamiento, IdFinanciamiento, --IdPrograma, IdSubPrograma, 
		ClaveSSA, IdProducto, CodigoEAN, ClaveLote   
	) 
Go--#SQL 

Alter Table FACT_Remisiones_Manuales_Detalles Add Constraint FK_FACT_Remisiones_Manuales_Detalles___FACT_Remisiones_Manuales 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
	References FACT_Remisiones_Manuales ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 

