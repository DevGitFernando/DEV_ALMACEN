------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Documentos_Generados_Detalles' and xType = 'U' ) 
	Drop Table FACT_CFD_Documentos_Generados_Detalles   
Go--#SQL 

------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Documentos_Generados' and xType = 'U' ) 
	Drop Table FACT_CFD_Documentos_Generados  
Go--#SQL 

Create Table FACT_CFD_Documentos_Generados  
( 
	Keyx int identity(1, 1) Not Null, 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	FechaRegistro datetime Not Null Default getdate(), 	
	IdTipoDocumento varchar(3) Not Null, 
	UsoDeCFDI varchar(6)  Not Null Default '', 
	TipoRelacion varchar(6)  Not Null Default '',  

	IdentificadorSerie int Not Null, 
	Serie varchar(10) Not Null Default '', 
	NombreDocumento varchar(50) Not Null Default '', 	
	Folio int Not Null Default 0, 	
	Importe numeric(14,4) Not Null Default 0, 
	
	RFC varchar(15) Not Null Default '', 
	NombreReceptor varchar(250) Not Null Default '', 
	
	IdCFDI varchar(100) Not Null Default '', 
	
	FormatoXML text Not null default '', 
	FormatoPDF text Not null default '', 
	TieneXML bit Not Null Default 'false', 
	TienePDF bit Not Null Default 'false', 	
	TieneTimbre bit Not Null Default 'false', 
	FormatoBase varchar(max) Not Null Default '', 
	
	SubTotal numeric(14,4) Not Null Default 0,   	
	Iva numeric(14,4) Not Null Default 0, 	
	ImporteNeto numeric(14,4) Not Null Default 0, 	
		
	TipoDocumento smallint Not Null Default 0, 		
	TipoInsumo smallint Not Null Default 0,    
	
	IdRubroFinanciamiento varchar(4) Not Null Default '', 
	IdFuenteFinanciamiento varchar(4) Not Null Default '', 
	
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
	
	IdPersonalEmite varchar(6) Not Null Default '', 
	IdPersonalCancela varchar(6) Not Null Default '', 
	MotivoCancelacion varchar(200) Not Null Default '', 	
	FechaCancelacion datetime Not Null Default getdate() 
		
) 
Go--#SQL 

Alter Table FACT_CFD_Documentos_Generados Add Constraint PK_FACT_CFD_Documentos_Generados 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
Go--#SQL 

Alter Table FACT_CFD_Documentos_Generados Add Constraint FK_FACT_CFD_Documentos_Generados__FACT_CFD_Sucursales_Series  
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdentificadorSerie ) 
	References FACT_CFD_Sucursales_Series ( IdEmpresa, IdEstado, IdFarmacia, IdentificadorSerie )  
Go--#SQL 

Alter Table FACT_CFD_Documentos_Generados Add Constraint FK_FACT_CFD_Documentos_Generados__FACT_CFD_TiposDeDocumentos 
	Foreign Key ( IdTipoDocumento ) 
	References FACT_CFD_TiposDeDocumentos ( IdTipoDocumento )  
Go--#SQL 



---------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Documentos_Generados_Detalles' and xType = 'U' ) 
	Drop Table FACT_CFD_Documentos_Generados_Detalles   
Go--#SQL 

Create Table FACT_CFD_Documentos_Generados_Detalles  
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 	
	
	SAT_ClaveProducto_Servicio varchar(20)  Not Null Default '',  
	SAT_UnidadDeMedida varchar(5)  Not Null Default '',  

	Identificador varchar(10) Not Null Default '',   --- Automatico 
	Partida int Not Null Default 1, 
	Clave varchar(20) Not Null Default '', 
	DescripcionConcepto varchar(250) Not Null Default '', 
	UnidadDeMedida varchar(50) Not Null Default '', 
	Cantidad numeric(14,4) Not Null Default 0, 
	PrecioUnitario numeric(14,4) Not Null Default 0, 
	TasaIva numeric(14,4) Not Null Default 0, 
	SubTotal numeric(14,4) Not Null Default 0,
	Iva numeric(14,4) Not Null Default 0,
	Total numeric(14,4) Not Null Default 0, 
	TipoImpuesto varchar(20) Not Null Default ''  
) 
Go--#SQL 

Alter Table FACT_CFD_Documentos_Generados_Detalles Add Constraint PK_FACT_CFD_Documentos_Generados_Detalles 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Identificador, Partida ) 
Go--#SQL 
	
Alter Table FACT_CFD_Documentos_Generados_Detalles Add Constraint FK_FACT_CFD_Documentos_Generados_Detalles___FACT_CFD_Documentos_Generados 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
	References FACT_CFD_Documentos_Generados ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
Go--#SQL 	

---------------------------------------------------------------------------------------------------------------------  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFD_Conceptos' and xType = 'U' ) 
	Drop Table FACT_CFD_Conceptos 
Go--#SQL  

Create Table FACT_CFD_Conceptos 
( 
	Identificador int identity(1,1), 
	IdConcepto varchar(20) Not Null Unique, 
	Descripcion varchar(250) Not Null Default '', 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table FACT_CFD_Conceptos Add Constraint PK_FACT_CFD_Conceptos Primary Key ( IdConcepto )
Go--#SQL  

