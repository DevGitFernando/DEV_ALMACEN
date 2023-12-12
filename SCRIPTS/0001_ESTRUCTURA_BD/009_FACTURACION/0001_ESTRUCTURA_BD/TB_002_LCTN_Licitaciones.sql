---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From sysobjects (NoLock) Where Name = 'LCTCN_Cotizaciones' and xType = 'U' ) 
   Drop Table LCTCN_Cotizaciones    
Go--#SQL 
   
Create Table LCTCN_Cotizaciones 
( 
	FolioCotizacion varchar(8) Not Null Default '',   
	IdEmpresa varchar(3) Not Null Default '', 

--	IdEstado varchar(2) Not Null, 
	NombreCliente varchar(100) Not Null Default '', 	
	Licitacion varchar(200) Not Null Default '',
	SubTotalSinGrabar_Min numeric(14,4) Not Null Default 0,
	SubTotalSinGrabar_Max numeric(14,4) Not Null Default 0, 
	SubTotalGrabado_Min numeric(14,4) Not Null Default 0,
	SubTotalGrabado_Max numeric(14,4) Not Null Default 0, 	
	Iva_Min numeric(14,4) Not Null Default 0,
	Iva_Max numeric(14,4) Not Null Default 0, 		
	Total_Min numeric(14,4) Not Null Default 0,
	Total_Max numeric(14,4) Not Null Default 0,
 	
	Tipo smallint Not Null Default 0,  --- 1 ==> Maximos y Minimos, 2 ==> CantidadFija 
	
	Observaciones varchar(500) Not Null Default '', 
	
	FechaRegistro datetime Not Null Default getdate(), 
	
	Status varchar(1) Not Null Default '', 
	Actualizado smallint Not Null Default 0   
) 
Go--#SQL 

Alter Table LCTCN_Cotizaciones Add Constraint PK_LCTCN_Cotizaciones Primary Key ( FolioCotizacion ) 
Go--#SQL 

Alter Table LCTCN_Cotizaciones Add Constraint FK_LCTCN_Cotizaciones_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 


---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From sysobjects (NoLock) Where Name = 'LCTCN_Cotizaciones_Claves' and xType = 'U' ) 
   Drop Table LCTCN_Cotizaciones_Claves    
Go--#SQL 
   
Create Table LCTCN_Cotizaciones_Claves 
(
	FolioCotizacion varchar(8) Not Null Default '',   
	IdClaveSSA varchar(20) Not Null Default 0, 
	Partida int Not Null Default 0, 	
	
	CantidadMinima numeric(14,4) Not Null Default 0, 
	CantidadMaxima numeric(14,4) Not Null Default 0, 	

	TipoManejo smallint Not Null Default 0,  -- 1 ==> Caja, 2 ==> Pieza 
	ContenidoPaquete numeric(14,4) Not Null Default 0,

	CostoCompra numeric(14, 4) Not Null Default 0,
  
	PrecioPaquete numeric(14,4) Not Null Default 0, 
	PrecioPieza numeric(14,4) Not Null Default 0,
	Porcentaje numeric(14, 4) Not Null Default 0,
	EsCause Bit Not Null Default 'false',
	Admon Bit Not Null Default 'false', 		
	
	Status varchar(1) Not Null Default '', 
	Actualizado smallint Not Null Default 0   	
) 
Go--#SQL 	

Alter Table LCTCN_Cotizaciones_Claves Add Constraint PK_LCTCN_Cotizaciones_Claves Primary Key ( FolioCotizacion, IdClaveSSA, Partida ) 
Go--#SQL 

Alter Table LCTCN_Cotizaciones Add Constraint FK_LCTCN_Cotizaciones___LCTCN_Cotizaciones 
	Foreign Key ( FolioCotizacion ) References LCTCN_Cotizaciones ( FolioCotizacion ) 
Go--#SQL 

