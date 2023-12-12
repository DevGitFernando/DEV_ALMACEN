
	
--	sp_ObtenerEstructuraTabla 'FACT_CFD_Documentos_Generados' , 'CFDI_Documentos_VP' 	

--	sp_ObtenerEstructuraTabla 'FACT_CFD_Documentos_Generados_Detalles' , 'CFDI_Documentos_VP_Conceptos' 
	

-------------------------------------------------------------------------------------------------------------------------------------------- 	
-------------------------------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects Where Name = 'CFDI_Documentos_VP' and xType = 'U' )
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
	
	UsoDeCFDI varchar(6)  Not Null Default '', 
	TipoRelacion varchar(6)  Not Null Default '', 
	
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
	
	IdEmpresaCancela varchar(3) Not Null Default '', 
	IdEstadoCancela varchar(2) Not Null Default '', 
	IdFarmaciaCancela varchar(4) Not Null Default '', 	
	
	IdPersonalCancela varchar(6) Not Null Default '', 
	MotivoCancelacion varchar(200) Not Null Default '', 	
	FechaCancelacion datetime Not Null Default getdate()   
          
)
Go--#SQL

Alter Table CFDI_Documentos_VP Add Constraint Pk_CFDI_Documentos_VP Primary Key
(
         IdEmpresa, IdEstado, IdFarmacia, Serie, Folio
)
Go--#SQL


----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Fecha__602A9B27 Default (getdate()) For FechaRegistro
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Serie__611EBF60 Default ('') For Serie
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Nombr__6212E399 Default ('') For NombreDocumento
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Folio__630707D2 Default ((0)) For Folio
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Impor__0EB07FE6 Default ((0)) For Importe
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP_Do__RFC__63FB2C0B Default ('') For RFC
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Nombr__64EF5044 Default ('') For NombreReceptor
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___IdCFD__65E3747D Default ('') For IdCFDI

----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Tiene__68BFE128 Default ('false') For TieneXML
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Tiene__69B40561 Default ('false') For TienePDF
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Statu__6AA8299A Default ('A') For Status
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___IdPer__6B9C4DD3 Default ('') For IdPersonalCancela
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Fecha__6C90720C Default (getdate()) For FechaCancelacion
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Motiv__64F0E6F2 Default ('') For MotivoCancelacion
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Tiene__65E50B2B Default ('false') For TieneTimbre
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Forma__66D92F64 Default ('') For FormatoBase
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___SubTo__67CD539D Default ((0)) For SubTotal
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP_Do__Iva__68C177D6 Default ((0)) For Iva
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Obser__69B59C0F Default ('') For Observaciones_01
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Obser__6AA9C048 Default ('') For Observaciones_02
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Obser__6B9DE481 Default ('') For Observaciones_03
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___IdFor__6C9208BA Default ('00') For IdFormaDePago
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Numer__6D862CF3 Default ('') For NumeroCuentaPredial
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___XMLFo__6E7A512C Default ('') For XMLFormaPago
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___XMLCo__6F6E7565 Default ('') For XMLCondicionesPago
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___XMLMe__7062999E Default ('') For XMLMetodoPago
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___Refer__7DBC94BC Default ('') For Referencia

----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___TipoD__55B9905A Default ((0)) For TipoDocumento
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___TipoI__56ADB493 Default ((0)) For TipoInsumo
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___IdRub__57A1D8CC Default ('') For IdRubroFinanciamiento
----Alter Table CFDI_Documentos_VP Add Constraint DF__FACT_CFD_VP___IdFue__5895FD05 Default ('') For IdFuenteFinanciamiento
----Go--#xSQL

	
-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects Where Name = 'CFDI_Documentos_VP_Conceptos' and xType = 'U' )
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
	
	SAT_ClaveProducto_Servicio varchar(20)  Not Null Default '',  
	SAT_UnidadDeMedida varchar(5)  Not Null Default '',  


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

----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint Pk_CFDI_Documentos_VP_Conceptos Primary Key
----(
----         IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Identificador
----)
----Go--#xSQL

----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___Serie__71552729 Default ('') For Serie
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___Folio__72494B62 Default ((0)) For Folio
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___Ident__733D6F9B Default ('') For Identificador
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___Clave__743193D4 Default ('') For Clave
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___Descr__7525B80D Default ('') For DescripcionConcepto
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___Unida__7619DC46 Default ('') For UnidadDeMedida
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___Canti__770E007F Default ((0)) For Cantidad
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___Preci__780224B8 Default ((0)) For PrecioUnitario
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___TasaI__78F648F1 Default ((0)) For TasaIva
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___SubTo__79EA6D2A Default ((0)) For SubTotal
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP_Do__Iva__7ADE9163 Default ((0)) For Iva
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___Total__7BD2B59C Default ((0)) For Total
----Alter Table CFDI_Documentos_VP_Conceptos Add Constraint DF__FACT_CFD_VP___TipoI__09B6C09F Default ('') For TipoImpuesto
----Go--#xxSQL
	