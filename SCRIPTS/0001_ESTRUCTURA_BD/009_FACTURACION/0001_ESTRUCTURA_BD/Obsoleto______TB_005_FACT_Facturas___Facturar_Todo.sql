------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Facturas_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Facturas_Detalles   
Go--#SQL    


------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Facturas'  and xType = 'U' ) 
   Drop Table FACT_Facturas  
Go--#SQL    

Create Table FACT_Facturas
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,		
	IdFarmacia varchar(4) Not Null,				-- Referencia solo para el Personal 
	FechaRegistro datetime Not Null Default getdate(), 
	FolioFactura varchar(8) Not Null, 
	TipoDeFactura smallint Not Null Default 0,  -- 1 ==> Insumos, 2 ==> Administracion 
	
	IdPersonalFactura varchar(4) Not Null Default '', 
	IdCliente varchar(4) Not Null Default '', 
	IdSubCliente varchar(4) Not Null Default '', 
	
	SubTotalSinGrabar numeric(14,4) Not Null Default 0, 
	SubTotalGrabado numeric(14,4) Not Null Default 0, 
	Iva numeric(14,4) Not Null Default 0, 
	Total numeric(14,4) Not Null Default 0, 		

	Observaciones varchar(200) Not Null Default '', 
    
	Status varchar(4) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0    --- 100 ==> Actualizado 
) 
Go--#SQL 

Alter Table FACT_Facturas Add Constraint PK_FACT_Facturas Primary Key ( IdEmpresa, IdEstado, FolioFactura ) 
Go--#SQL 

Alter Table FACT_Facturas Add Constraint FK_FACT_Facturas__CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
Go--#SQL 

Alter Table FACT_Facturas Add Constraint FK_FACT_Facturas__CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )  
Go--#SQL 

Alter Table FACT_Facturas Add Constraint FK_FACT_Facturas__CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonalFactura ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal )  
Go--#SQL 

Alter Table FACT_Facturas Add Constraint FK_FACT_Facturas__CatSubClientes 
	Foreign Key ( IdCliente, IdSubCliente ) References CatSubClientes ( IdCliente, IdSubCliente )  
Go--#SQL 


------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Facturas_Detalles'  and xType = 'U' ) 
   Drop Table FACT_Facturas_Detalles   
Go--#SQL    

Create Table FACT_Facturas_Detalles
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null,			
	IdFarmacia varchar(4) Not Null,	
	FolioVenta varchar(8) Not Null, 
	
	FolioFactura varchar(8) Not Null,		-- Consecutivo de Facturacion 		
	
	IdCliente varchar(4) Not Null,	
	IdSubCliente varchar(4) Not Null,	
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

Alter Table FACT_Facturas_Detalles Add Constraint PK_FACT_Facturas_Detalles 
	Primary Key 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FolioFactura, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
		ClaveSSA, IdProducto, CodigoEAN, ClaveLote  
	) 
Go--#SQL 

Alter Table FACT_Facturas_Detalles Add Constraint FK_FACT_Facturas_Detalles___FACT_Facturas 
	Foreign Key ( IdEmpresa, IdEstado, FolioFactura ) References FACT_Facturas ( IdEmpresa, IdEstado, FolioFactura ) 
Go--#SQL 
