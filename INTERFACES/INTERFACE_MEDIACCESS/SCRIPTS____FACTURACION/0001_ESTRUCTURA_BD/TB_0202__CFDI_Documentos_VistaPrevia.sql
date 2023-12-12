------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Documentos_VP' and xType = 'U' ) 
	Drop Table CFDI_Documentos_VP    
Go--#SQL  

Create Table CFDI_Documentos_VP 
(
	GUID varchar(100) Not Null Default '', 
	Keyx int identity(1, 1) Not Null, 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdEstadoReferencia Varchar(2) Not Null,
	IdFarmaciaReferencia varchar(4) Not Null Default '', 

	FechaRegistro datetime Not Null Default getdate(), 	
	IdPersonalEmite varchar(4) Not Null Default '', 
	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 			
	
	IdTipoDocumento varchar(3) Not Null, 
	IdentificadorSerie int Not Null, 
	NombreDocumento varchar(50) Not Null Default '', 	
	Importe numeric(14,4) Not Null Default 0, 
	
	RFC varchar(15) Not Null Default '', 
	NombreReceptor varchar(250) Not Null Default '', 
	
	IdCFDI varchar(100) Not Null Default '', 
		
	SubTotal numeric(14,4) Not Null Default 0,   	
	Iva numeric(14,4) Not Null Default 0, 	
	ImporteNeto numeric(14,4) Not Null Default 0, 	
		
	TipoDocumento smallint Not Null Default 0, 		
	TipoInsumo smallint Not Null Default 0,    
		
	Observaciones_01 varchar(500) Not Null Default '', 
	Observaciones_02 varchar(500) Not Null Default '', 
	Observaciones_03 varchar(500) Not Null Default '', 		
	
	Referencia varchar(500) Not Null Default '', 
	IdFormaDePago varchar(2) Not Null Default '00', 
	NumeroCuentaPredial varchar(500) Not Null Default '', 
	XMLFormaPago varchar(500) Not Null Default '', 
	XMLCondicionesPago varchar(500) Not Null Default '', 
	XMLMetodoPago varchar(500) Not Null Default '', 
	
	
	
	Status varchar(2) Not Null Default 'A',  
	
	IdPersonalCancela varchar(6) Not Null Default '', 
	MotivoCancelacion varchar(200) Not Null Default '', 	
	FechaCancelacion datetime Not Null Default getdate(),
	
	TienePagoRelacionado bit Not Null Default 'false',
	SAT_ClaveDeConfirmacion varchar(20) Not Null Default '',
	CFDI_Relacionado_CPago varchar(50) Not Null Default '',
	Serie_Relacionada_CPago varchar(10) Not Null Default '',     
	Folio_Relacionado_CPago int Not Null Default 0   
) 
Go--#SQL  



------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Documentos_VP_Conceptos' and xType = 'U' ) 
	Drop Table CFDI_Documentos_VP_Conceptos     
Go--#SQL  

Create Table CFDI_Documentos_VP_Conceptos
(
	GUID varchar(100) Not Null Default '', 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 
	Partida int Not Null Default 0, 	
	
	IdProducto varchar(8) Not Null Default '', 
	CodigoEAN varchar(30) Not Null Default '', 
	Notas varchar(200) Not Null Default '', 	
	UnidadDeMedida varchar(50) Not Null Default '', 
	Cantidad numeric(14, 4) Not Null Default 0, 
	PrecioUnitario numeric(14, 4) Not Null Default 0,  
	DescuentoPorc numeric(14, 4) Not Null Default 0, 
	PrecioUnitarioFinal numeric(14, 4) Not Null Default 0,  	
	TasaIva numeric(14, 4) Not Null Default 0,  
		
	SubTotal numeric(14, 4) Not Null Default 0, 
	Iva numeric(14, 4) Not Null Default 0, 	 	
	Importe numeric(14, 4) Not Null Default 0, 
	TipoImpuesto varchar(20) Not Null Default '', 
	Status varchar(1) Not Null Default 'A',  
	Keyx int Not Null Identity(1, 1) 
	
) 
Go--#SQL 

/* 
---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Documentos_InformacionAdicional_VP' and xType = 'U' ) 
	Drop Table CFDI_Documentos_InformacionAdicional_VP  
Go--#xSQL 

Create Table CFDI_Documentos_InformacionAdicional_VP 
(
	GUID varchar(100) Not Null Default '', 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Serie varchar(10) Not Null Default '', 
	Folio varchar(10) Not Null Default '', 
	Observaciones_01 varchar(500) Not Null, 
	Observaciones_02 varchar(500) Not Null, 
	Observaciones_03 varchar(500) Not Null, 
	Observaciones_04 varchar(500) Not Null, 
	Observaciones_05 varchar(500) Not Null, 
	Observaciones_06 varchar(500) Not Null, 
	Observaciones_07 varchar(500) Not Null, 
	Observaciones_08 varchar(500) Not Null, 
	Observaciones_09 varchar(500) Not Null, 
	Observaciones_10 varchar(500) Not Null 
) 
Go--#xSQL 
*/ 

---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Documentos_MetodosDePago_VP' and xType = 'U' ) 
	Drop Table CFDI_Documentos_MetodosDePago_VP  
Go--#SQL  

Create Table CFDI_Documentos_MetodosDePago_VP 
(
	GUID varchar(100) Not Null Default '', 

	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 			
	
	IdMetodoDePago varchar(2) Not Null, 
	Importe numeric(14,4) Not Null Default 0, 
	Referencia varchar(10) Not Null Default ''   
) 
Go--#SQL 
