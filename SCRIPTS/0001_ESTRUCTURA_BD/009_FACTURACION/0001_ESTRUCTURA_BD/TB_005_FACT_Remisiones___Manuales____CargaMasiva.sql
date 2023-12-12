
-----------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones_Manuales__CargaMasiva'  and xType = 'U' ) 
   Drop Table FACT_Remisiones_Manuales__CargaMasiva 
Go--#SQL    

Create Table FACT_Remisiones_Manuales__CargaMasiva
( 
	IdEmpresa varchar(3) Not Null Default '',		
	IdEstado varchar(2) Not Null Default '',			
	IdFarmaciaGenera varchar(4) Not Null Default '',	
	IdFarmacia varchar(4) Not Null Default '',
	IdSubFarmacia varchar(2) Not Null Default '', 
	FolioVenta varchar(8) Not Null Default '', 
	Partida int Not Null Default '', 
	FolioRemision varchar(10) Not Null Default '',		-- Consecutivo de Facturacion 		
	
	IdCliente varchar(4) Not Null Default '',	
	IdSubCliente varchar(4) Not Null Default '',	
	IdBeneficiario varchar(8) Not Null Default '',	

	IdFuenteFinanciamiento varchar(4) Not Null Default '',	
	IdFinanciamiento varchar(4) Not Null Default '',	
	IdPrograma varchar(4) Not Null Default '',	
	IdSubPrograma varchar(4) Not Null Default '',					
	
	FechaRemision datetime Not Null Default convert(varchar(10), getdate(), 120), 
	TipoDeRemision smallint Not Null Default 0,  -- 1 ==> Insumos, 2 ==> Administracion 
	TipoInsumo varchar(2) Not Null Default '',
	OrigenInsumo smallint Not Null Default 0,  -- 0 ==> Venta, 1 ==> Consigna  

	FechaInicial datetime Not Null Default convert(varchar(10), getdate(), 120),
	FechaFinal datetime Not Null Default convert(varchar(10), getdate(), 120),


	ClaveSSA varchar(20) Not Null Default '', 
	IdProducto varchar(8) Not Null Default '', 
	CodigoEAN varchar(30) Not Null Default '', 
	ClaveLote varchar(30) Not Null Default '', 
	PrecioLicitado numeric(14,4) Not Null Default 0, 	
	PrecioLicitadoUnitario numeric(14,4) Not Null Default 0, 		
	Cantidad_Agrupada numeric(14,4) Not Null Default 0, 	
	Cantidad numeric(14,4) Not Null Default 0, 	
	
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

