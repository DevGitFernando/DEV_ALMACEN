------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Documentos '  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Documentos    
Go--#SQL    

------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_InformacionAdicional'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_InformacionAdicional   
Go--#SQL    

------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Resumen'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Resumen   
Go--#SQL    

------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Concentrado'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Concentrado   
Go--#SQL   

------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Detalles   
Go--#SQL    

------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones'  and xType = 'U' ) 
   Drop Table FACT_Remisiones  
Go--#SQL    

Create Table FACT_Remisiones
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

Alter Table FACT_Remisiones Add Constraint PK_FACT_Remisiones 
	Primary Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 

Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones__CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
Go--#SQL 

Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones__CatFarmacias 
	Foreign Key ( IdEstado, IdFarmaciaGenera ) References CatFarmacias ( IdEstado, IdFarmacia )  
Go--#SQL 

Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones__FACT_TiposDeRemisiones
	Foreign Key ( TipoDeRemision ) References FACT_TiposDeRemisiones ( TipoDeRemision )  
Go--#SQL 



------------------------------------ Revisar FK 
------------------Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones__CatPersonal 
------------------	Foreign Key ( IdEstado, IdFarmaciaGenera, IdPersonalFactura ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal )  
------------------Go--#SQL 

Alter Table FACT_Remisiones Add Constraint FK_FACT_Remisiones__FACT_Fuentes_De_Financiamiento_Detalles 
	Foreign Key ( IdFuenteFinanciamiento, IdFinanciamiento ) 
	References FACT_Fuentes_De_Financiamiento_Detalles ( IdFuenteFinanciamiento, IdFinanciamiento )  
Go--#SQL 

------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Detalles   
Go--#SQL    

Create Table FACT_Remisiones_Detalles
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,			
	IdFarmaciaGenera varchar(4) Not Null,	
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null, 
	FolioVenta varchar(8) Not Null, 
	Partida int Not Null, 
	FolioRemision varchar(10) Not Null,		-- Consecutivo de Facturacion 		
	
	IdFuenteFinanciamiento varchar(4) Not Null,	
	IdFinanciamiento varchar(4) Not Null,	
	IdPrograma varchar(4) Not Null,	
	IdSubPrograma varchar(4) Not Null,					
	
	ClaveSSA varchar(20) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	PrecioLicitado numeric(14,4) Not Null Default 0, 	
	PrecioLicitadoUnitario numeric(14,4) Not Null Default 0, 		
	Cantidad_Agrupada numeric(14,4) Not Null Default 0, 	
	Cantidad numeric(14,4) Not Null Default 0, 	
	
	TasaIva numeric(14,4) Not Null Default 0, 	
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Importe numeric(14,4) Not Null Default 0 
	
) 
Go--#SQL 

Alter Table FACT_Remisiones_Detalles Add Constraint PK_FACT_Remisiones_Detalles 
	Primary Key 
	( 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, Partida, FolioRemision, 
		IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, 
		ClaveSSA, IdProducto, CodigoEAN, ClaveLote, Partida  
	) 
Go--#SQL 

Alter Table FACT_Remisiones_Detalles Add Constraint FK_FACT_Remisiones_Detalles___FACT_Remisiones 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
	References FACT_Remisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Concentrado'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Concentrado   
Go--#SQL    

Create Table FACT_Remisiones_Concentrado
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null, 
	IdFarmaciaGenera varchar(4) Not Null,	
	IdFarmacia varchar(4) Not Null,	
	IdSubFarmacia varchar(2) Not Null, 
	--FolioVenta varchar(8) Not Null, 
	
	FolioRemision varchar(10) Not Null,		-- Consecutivo de Facturacion 		
	
	IdFuenteFinanciamiento varchar(4) Not Null,	
	IdFinanciamiento varchar(4) Not Null,	
	IdPrograma varchar(4) Not Null,	
	IdSubPrograma varchar(4) Not Null,					
	
	ClaveSSA varchar(20) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	PrecioLicitado numeric(14,4) Not Null Default 0, 	
	PrecioLicitadoUnitario numeric(14,4) Not Null Default 0, 		
	Cantidad_Agrupada numeric(14,4) Not Null Default 0, 	
	Cantidad numeric(14,4) Not Null Default 0, 
		
	TasaIva numeric(14,4) Not Null Default 0, 	
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Importe numeric(14,4) Not Null Default 0 
	
) 
Go--#SQL 

Alter Table FACT_Remisiones_Concentrado Add Constraint PK_FACT_Remisiones_Concentrado 
	Primary Key 
	( 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, 
		IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, 
		ClaveSSA, IdProducto, CodigoEAN, ClaveLote  
	) 
Go--#SQL 

Alter Table FACT_Remisiones_Concentrado Add Constraint FK_FACT_Remisiones_Concentrado___FACT_Remisiones 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
	References FACT_Remisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Resumen'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Resumen   
Go--#SQL    

Create Table FACT_Remisiones_Resumen
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,
	IdFarmaciaGenera varchar(4) Not Null,		
	IdFarmacia varchar(4) Not Null,		
	FolioRemision varchar(10) Not Null,		-- Consecutivo de Facturacion 		
	
	IdFuenteFinanciamiento varchar(4) Not Null,	
	IdFinanciamiento varchar(4) Not Null,	
	IdPrograma varchar(4) Not Null,	
	IdSubPrograma varchar(4) Not Null,					
	
	ClaveSSA varchar(20) Not Null, 
	PrecioLicitado numeric(14,4) Not Null Default 0, 	
	PrecioLicitadoUnitario numeric(14,4) Not Null Default 0, 		
	Cantidad numeric(14,4) Not Null Default 0, 
	Cantidad_Agrupada numeric(14,4) Not Null Default 0, 	
	
	TasaIva numeric(14,4) Not Null Default 0, 	
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Importe numeric(14,4) Not Null Default 0 
	
) 
Go--#SQL 

Alter Table FACT_Remisiones_Resumen Add Constraint PK_FACT_Remisiones_Resumen 
	Primary Key 
	( 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, 
		IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, ClaveSSA
	) 
Go--#SQL 

Alter Table FACT_Remisiones_Resumen Add Constraint FK_FACT_Remisiones_Resumen___FACT_Remisiones 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
	References FACT_Remisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_InformacionAdicional'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_InformacionAdicional   
Go--#SQL    

Create Table FACT_Remisiones_InformacionAdicional
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,
	IdFarmaciaGenera varchar(4) Not Null,		
	FolioRemision varchar(10) Not Null,		-- Consecutivo de Facturacion 		
	
	Observaciones varchar(2000) Not Null Default '' 	
) 
Go--#SQL 

Alter Table FACT_Remisiones_InformacionAdicional Add Constraint PK_FACT_Remisiones_InformacionAdicional 
	Primary Key  ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 

Alter Table FACT_Remisiones_InformacionAdicional Add Constraint FK_FACT_Remisiones_InformacionAdicional___FACT_Remisiones 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
	References FACT_Remisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 



------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Documentos '  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Documentos    
Go--#SQL    

Create Table FACT_Remisiones_Documentos 
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,
	IdFarmaciaGenera varchar(4) Not Null,		
	FolioRemision varchar(10) Not Null,		-- Consecutivo de Facturacion 		
	
	IdFuenteFinanciamiento varchar(4) Not Null,	
	IdFinanciamiento varchar(4) Not Null,	
	IdDocumento varchar(4) Not Null,	
	
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Importe numeric(14,4) Not Null Default 0 
	
) 
Go--#SQL 

Alter Table FACT_Remisiones_Documentos  Add Constraint PK_FACT_Remisiones_Documentos  
	Primary Key 
	( 
		IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento 
	) 
Go--#SQL 

Alter Table FACT_Remisiones_Documentos  Add Constraint FK_FACT_Remisiones_Documentos___FACT_Remisiones 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
	References FACT_Remisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_InformacionAdicional_Almacenes'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_InformacionAdicional_Almacenes   
Go--#SQL    

Create Table FACT_Remisiones_InformacionAdicional_Almacenes
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,
	IdFarmaciaGenera varchar(4) Not Null,		
	FolioRemision varchar(10) Not Null,		-- Consecutivo de Facturacion 	
	TipoDeBeneficiario int Not Null Default 0, 	
	Beneficiario varchar(8) Not Null Default '',  	
	NombreBeneficiario varchar(500) Not Null Default '' 	
) 
Go--#SQL 

Alter Table FACT_Remisiones_InformacionAdicional_Almacenes Add Constraint PK_FACT_Remisiones_InformacionAdicional_Almacenes 
	Primary Key  ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 

Alter Table FACT_Remisiones_InformacionAdicional_Almacenes Add Constraint FK_FACT_Remisiones_InformacionAdicional_Almacenes___FACT_Remisiones 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
	References FACT_Remisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 
