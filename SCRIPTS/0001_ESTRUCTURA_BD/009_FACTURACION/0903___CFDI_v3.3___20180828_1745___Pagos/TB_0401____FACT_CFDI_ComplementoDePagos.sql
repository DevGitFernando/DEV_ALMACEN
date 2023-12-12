------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_ComplementoDePagos_DoctosRelacionados' and xType = 'U' ) 
	Drop Table FACT_CFDI_ComplementoDePagos_DoctosRelacionados   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_ComplementoDePagos_Detalles' and xType = 'U' ) 
	Drop Table FACT_CFDI_ComplementoDePagos_Detalles   
Go--#SQL 




------------------------------------------------------------------------------------------------ 
/* 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_ComplementoDePagos' and xType = 'U' ) 
	Drop Table FACT_CFDI_ComplementoDePagos  
Go--#xSQL 

Create Table FACT_CFDI_ComplementoDePagos  
( 
	Keyx int identity(1, 1) Not Null, 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	FechaRegistro datetime Not Null Default getdate(), 	

	UsoDeCFDI varchar(6)  Not Null Default '', 
	IdTipoDocumento varchar(3) Not Null,  
	IdentificadorSerie int Not Null, 
	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 	
	
	RFC varchar(15) Not Null Default '', 
	NombreReceptor varchar(250) Not Null Default '', 

	SAT_ClaveDeConfirmacion varchar(20) Not Null Default '', 

	CFDI_Relacionado varchar(50) Not Null Default '',  
		-- Se debe registrar el folio fiscal (UUID) de un CFDI con complemento para recepción de pagos relacionado que se sustituye con el presente comprobante. 
	Serie_Relacionada varchar(10) Not Null Default '', 
	Folio_Relacionado int Not Null Default 0, 	


	SubTotal numeric(14,4) Not Null Default 0,   	
	Iva numeric(14,4) Not Null Default 0, 	
	ImporteNeto numeric(14,4) Not Null Default 0, 	
		
	XMLFormaPago varchar(500) Not Null Default '', 
	XMLMetodoPago varchar(500) Not Null Default '', 

		
	Observaciones_01 varchar(500) Not Null Default '', 
	Observaciones_02 varchar(500) Not Null Default '', 
	Observaciones_03 varchar(500) Not Null Default '', 	

	Status varchar(2) Not Null Default 'A',  	
	IdPersonalEmite varchar(6) Not Null Default '', 
	IdPersonalCancela varchar(6) Not Null Default '', 
	MotivoCancelacion varchar(200) Not Null Default '', 	
	FechaCancelacion datetime Not Null Default getdate() 	
) 
Go--#xSQL 

Alter Table FACT_CFDI_ComplementoDePagos Add Constraint PK_FACT_CFDI_ComplementoDePagos 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
Go--#xSQL 

Alter Table FACT_CFDI_ComplementoDePagos Add Constraint FK_FACT_CFDI_ComplementoDePagos__FACT_CFD_Sucursales_Series  
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdentificadorSerie ) 
	References FACT_CFD_Sucursales_Series ( IdEmpresa, IdEstado, IdFarmacia, IdentificadorSerie )  
Go--#xSQL 

Alter Table FACT_CFDI_ComplementoDePagos Add Constraint FK_FACT_CFDI_ComplementoDePagos__FACT_CFD_TiposDeDocumentos 
	Foreign Key ( IdTipoDocumento ) 
	References FACT_CFD_TiposDeDocumentos ( IdTipoDocumento )  
Go--#xSQL

*/ 
 





---------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_ComplementoDePagos_Detalles' and xType = 'U' ) 
	Drop Table FACT_CFDI_ComplementoDePagos_Detalles   
Go--#SQL 

Create Table FACT_CFDI_ComplementoDePagos_Detalles  
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 	
	
	FechaDePago datetime Not Null Default getdate(), 
	FormaDePago  varchar(4) Not Null Default '', 

	Moneda varchar(10) Not Null Default 'MXN', 
	TipoCambio numeric(14,4) Not Null Default 1, 
	Monto numeric(14,4) Not Null Default 0, 
	NumeroDeOperacion varchar(500) Not Null Default '', 


	RfcEmisorCtaOrd varchar(20) Not Null Default '', --- Origen de pago 
	NomBancoOrdExt varchar(200) Not Null Default '', --- Origen de pago 
	CtaOrdenante varchar(50) Not Null Default '',	 --- Origen de pago 
	RfcEmisorCtaBen varchar(20) Not Null Default '', --- Repector de pago 
	CtaBeneficiario varchar(50) Not Null Default '', --- Repector de pago 

	TipoCadPago varchar(4) Not Null Default '',			--- Informacion tipo CFDI 
	CertificadoPago varchar(500) Not Null Default '',   --- Informacion tipo CFDI 
	CadenaPago varchar(500) Not Null Default '',		--- Informacion tipo CFDI 
	SelloPago varchar(500) Not Null Default ''			--- Informacion tipo CFDI 


) 
Go--#SQL 

Alter Table FACT_CFDI_ComplementoDePagos_Detalles Add Constraint PK_FACT_CFDI_ComplementoDePagos_Detalles 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
Go--#SQL 
	
Alter Table FACT_CFDI_ComplementoDePagos_Detalles Add Constraint FK_FACT_CFDI_ComplementoDePagos_Detalles___FACT_CFD_Documentos_Generados 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
	References FACT_CFD_Documentos_Generados ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
Go--#SQL 	




---------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_ComplementoDePagos_DoctosRelacionados' and xType = 'U' ) 
	Drop Table FACT_CFDI_ComplementoDePagos_DoctosRelacionados   
Go--#SQL 

Create Table FACT_CFDI_ComplementoDePagos_DoctosRelacionados   
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 	

	Serie_Relacionada varchar(10) Not Null Default '', 
	Folio_Relacionado int Not Null Default 0, 	
	UUID_Relacionado varchar(50) Not Null Default '', 


	Moneda varchar(10) Not Null Default 'MXN', 
	TipoCambio numeric(14,4) Not Null Default 1, 
	MetodoDePago varchar(10) Not Null Default '', 

	NumParcialidad int Not Null Default 1, 
	Importe_SaldoAnterior numeric(14, 4) Not Null Default 0, 
	Importe_Pagado numeric(14, 4) Not Null Default 0, 
	Importe_SaldoInsoluto numeric(14, 4) Not Null Default 0 


) 
Go--#SQL 

Alter Table FACT_CFDI_ComplementoDePagos_DoctosRelacionados Add Constraint PK_FACT_CFDI_ComplementoDePagos_DoctosRelacionados 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Serie_Relacionada, Folio_Relacionado ) 
Go--#SQL 
	
Alter Table FACT_CFDI_ComplementoDePagos_DoctosRelacionados Add Constraint FK_FACT_CFDI_ComplementoDePagos_DoctosRelacionados___FACT_CFD_Documentos_Generados 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
	References FACT_CFD_Documentos_Generados ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
Go--#SQL 	
