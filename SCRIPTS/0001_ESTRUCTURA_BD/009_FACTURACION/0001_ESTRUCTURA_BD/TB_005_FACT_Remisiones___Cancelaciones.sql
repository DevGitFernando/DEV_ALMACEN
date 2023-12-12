
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Cancelaciones_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Cancelaciones_Detalles   
Go--#SQL   


------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Cancelaciones'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Cancelaciones  
Go--#SQL    

Create Table FACT_Remisiones_Cancelaciones
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,		
	IdFarmaciaGenera varchar(4) Not Null,				-- Referencia solo para el Personal 
	FechaCancelacion datetime Not Null Default getdate(), 
	FechaRemision datetime Not Null Default getdate(), 
	FolioRemision varchar(10) Not Null, 
	TipoDeRemision smallint Not Null Default 0,  -- 1 ==> Insumos, 2 ==> Administracion 
	
	IdPersonalCancela varchar(4) Not Null Default '', 
	IdFuenteFinanciamiento varchar(4) Not Null Default '', 
	IdFinanciamiento varchar(4) Not Null Default '', 
	TipoInsumo varchar(2) Not Null Default '',
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Total numeric(14,4) Not Null Default 0, 		

	Observaciones varchar(200) Not Null Default '', 
    
	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0    --- 100 ==> Actualizado 
) 
Go--#SQL 

Alter Table FACT_Remisiones_Cancelaciones Add Constraint PK_FACT_Remisiones_Cancelaciones 
	Primary Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 


------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Cancelaciones_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Cancelaciones_Detalles   
Go--#SQL    

Create Table FACT_Remisiones_Cancelaciones_Detalles
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,			
	IdFarmaciaGenera varchar(4) Not Null,	
	IdFarmacia varchar(4) Not Null,
	IdSubFarmacia varchar(2) Not Null, 
	FolioVenta varchar(8) Not Null, 
	
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
	Cantidad numeric(14,4) Not Null Default 0, 
	
	TasaIva numeric(14,4) Not Null Default 0, 	
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Importe numeric(14,4) Not Null Default 0 
	
) 
Go--#SQL 

Alter Table FACT_Remisiones_Cancelaciones_Detalles Add Constraint PK_FACT_Remisiones_Cancelaciones_Detalles 
	Primary Key 
	( 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, FolioRemision, 
		IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, 
		ClaveSSA, IdProducto, CodigoEAN, ClaveLote  
	) 
Go--#SQL 

Alter Table FACT_Remisiones_Cancelaciones_Detalles Add Constraint FK_FACT_Remisiones_Cancelaciones_Detalles___FACT_Remisiones_Cancelaciones 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
	References FACT_Remisiones_Cancelaciones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision ) 
Go--#SQL 
