------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_SeriesFolios' and xType = 'U' ) 
	Drop Table CFDI_Emisores_SeriesFolios   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_Logos' and xType = 'U' ) 
	Drop Table CFDI_Emisores_Logos   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_Certificados' and xType = 'U' ) 
	Drop Table CFDI_Emisores_Certificados   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_Mail' and xType = 'U' ) 
	Drop Table CFDI_Emisores_Mail
Go--#SQL  

------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_PAC_Cuentas' and xType = 'U' ) 
	Drop Table CFDI_Emisores_PAC_Cuentas
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_PAC' and xType = 'U' ) 
	Drop Table CFDI_Emisores_PAC
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores' and xType = 'U' ) 
	Drop Table CFDI_Emisores   
Go--#SQL  



------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores' and xType = 'U' ) 
	Drop Table CFDI_Emisores   
Go--#SQL  

Create Table CFDI_Emisores 
(
	IdEmpresa varchar(3) Not Null, 
	NombreFiscal varchar(100) Not Null, 
	NombreComercial varchar(100) Not Null, 	
	RFC varchar(15) Not Null Unique, 	

	EsPersonaFisica bit Not Null Default 'false', 
	PublicoGeneral_AplicaIva bit Not Null Default 'false', 

	Telefonos varchar(100) Not Null Default '', 
	Fax varchar(100) Not Null Default '', 	
	Email varchar(100) Not Null Default '', 	
	
	DomExpedicion_DomFiscal bit Not Null Default 'false', 
	
	Pais varchar(100) Not Null Default '', 
	Estado varchar(100) Not Null Default '', 		
	Municipio varchar(100) Not Null Default '', 
	Colonia varchar(100) Not Null Default '', 		
	Calle varchar(100) Not Null Default '',  
	NoExterior varchar(8) Not Null Default '',  
	NoInterior varchar(8) Not Null Default '', 
	CodigoPostal varchar(100) Not Null Default '', 
	Referencia varchar(100) Not Null Default '', 	
	EPais varchar(100) Not Null Default '', 
	EEstado varchar(100) Not Null Default '', 		
	EMunicipio varchar(100) Not Null Default '', 
	EColonia varchar(100) Not Null Default '', 		
	ECalle varchar(100) Not Null Default '',  
	ENoExterior varchar(8) Not Null Default '',  
	ENoInterior varchar(8) Not Null Default '', 
	ECodigoPostal varchar(100) Not Null Default '', 
	EReferencia varchar(100) Not Null Default '',   
			
	Status varchar(1) Not Null Default 'A' 
) 	
Go--#SQL 

Alter Table CFDI_Emisores Add Constraint PK_CFDI_Emisores Primary Key ( IdEmpresa  ) 
Go--#SQL 

Alter Table CFDI_Emisores Add Constraint FK_CFDI_Emisores___CatEmpresas 
	Foreign Key ( IdEmpresa  ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_Sucursales' and xType = 'U' ) 
	Drop Table CFDI_Emisores_Sucursales   
Go--#SQL  

Create Table CFDI_Emisores_Sucursales 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 		
	NombreFiscal varchar(100) Not Null, 
	NombreComercial varchar(100) Not Null, 	
	RFC varchar(15) Not Null Unique, 	

	EsPersonaFisica bit Not Null Default 'false', 
	PublicoGeneral_AplicaIva bit Not Null Default 'false', 

	Telefonos varchar(100) Not Null Default '', 
	Fax varchar(100) Not Null Default '', 	
	Email varchar(100) Not Null Default '', 	
	
	DomExpedicion_DomFiscal bit Not Null Default 'false', 
	
	Pais varchar(100) Not Null Default '', 
	Estado varchar(100) Not Null Default '', 		
	Municipio varchar(100) Not Null Default '', 
	Colonia varchar(100) Not Null Default '', 		
	Calle varchar(100) Not Null Default '',  
	NoExterior varchar(8) Not Null Default '',  
	NoInterior varchar(8) Not Null Default '', 
	CodigoPostal varchar(100) Not Null Default '', 
	Referencia varchar(100) Not Null Default '', 	
	EPais varchar(100) Not Null Default '', 
	EEstado varchar(100) Not Null Default '', 		
	EMunicipio varchar(100) Not Null Default '', 
	EColonia varchar(100) Not Null Default '', 		
	ECalle varchar(100) Not Null Default '',  
	ENoExterior varchar(8) Not Null Default '',  
	ENoInterior varchar(8) Not Null Default '', 
	ECodigoPostal varchar(100) Not Null Default '', 
	EReferencia varchar(100) Not Null Default '',   
			
	Status varchar(1) Not Null Default 'A' 
) 	
Go--#SQL 

Alter Table CFDI_Emisores_Sucursales Add Constraint PK_CFDI_Emisores_Sucursales Primary Key ( IdEmpresa, IdEstado, IdFarmacia  ) 
Go--#SQL 

Alter Table CFDI_Emisores_Sucursales Add Constraint FK_CFDI_Emisores_Sucursales___CFDI_Emisores 
	Foreign Key ( IdEmpresa  ) References CFDI_Emisores ( IdEmpresa ) 
Go--#SQL 

Alter Table CFDI_Emisores_Sucursales Add Constraint FK_CFDI_Emisores_Sucursales___CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia  ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 


------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_Certificados' and xType = 'U' ) 
	Drop Table CFDI_Emisores_Certificados   
Go--#SQL  

Create Table CFDI_Emisores_Certificados 
( 
	IdEmpresa varchar(3) Not Null, 
	NumeroDeCertificado varchar (100) Not Null,  
	NombreCertificado varchar(100) Not Null, 
	Certificado varchar(max) Not Null Default '', 
	ValidoDesde varchar(100) Not Null Default '', 
	ValidoHasta varchar(100) Not Null Default '', 
	FechaInicio datetime Not Null Default getdate(), 
	FechaFinal datetime Not Null Default getdate(), 

	Serie varchar(100) Not Null Default '', 
	Serial varchar(100) Not Null Default '', 	
	
	NombreLlavePrivada varchar(100) Not Null Default '',
	LlavePrivada varchar(max) Not Null Default '',
	PasswordPublico varchar(100) Not Null Default '', 

	NombreCertificadoPfx varchar(100) Not Null, 		
	CertificadoPfx varchar(max) Not Null Default '', 	
	
	AvisoVencimiento bit Not Null Default 'true', 
	TiempoAviso int Not Null Default 15, 
	
	Status varchar(1) Not Null Default '', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL  

Alter Table CFDI_Emisores_Certificados Add Constraint PK_CFDI_Emisores_Certificados Primary Key ( IdEmpresa ) 
Go--#SQL 

Alter Table CFDI_Emisores_Certificados Add Constraint FK_CFDI_Emisores_Certificados___CFDI_Emisores 
	Foreign Key ( IdEmpresa  ) References CFDI_Emisores ( IdEmpresa ) 
Go--#SQL 


------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_Mail' and xType = 'U' ) 
	Drop Table CFDI_Emisores_Mail
Go--#SQL  

Create Table CFDI_Emisores_Mail 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 		
	Servidor varchar(100) Not Null Default '', 
	Puerto int Not Null Default 0, 
	TiempoDeEspera int Not Null Default 0, 	
	Usuario varchar(100) Not Null Default '',
	Password varchar(max) Not Null Default '', 
	EnableSSL bit Not Null Default 'True', 
	
	EmailRespuesta varchar(100) Not Null Default '', 
	NombreParaMostrar varchar(100) Not Null Default '', 
	CC varchar(100) Not Null Default '', 
	Asunto varchar(100) Not Null Default '', 
	MensajePredeterminado varchar(100) Not Null Default ''  
	
) 	
Alter Table CFDI_Emisores_Mail Add Constraint PK_CFDI_Emisores_Mail 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table CFDI_Emisores_Mail Add Constraint FK_CFDI_Emisores_Mail___CFDI_Emisores_Sucursales
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia  ) References CFDI_Emisores_Sucursales ( IdEmpresa, IdEstado, IdFarmacia ) 
Go--#SQL 



------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_SeriesFolios' and xType = 'U' ) 
	Drop Table CFDI_Emisores_SeriesFolios   
Go--#SQL 

Create Table CFDI_Emisores_SeriesFolios 
( 
	IdEmpresa varchar(3) Not Null,  	
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 			
	Serie varchar(10) Not Null Default '', 
	IdTipoDocumento varchar(3) Not Null, 	
	FolioInicial int Not Null Default 0, 
	FolioFinal int Not Null Default 0, 	
	FolioUtilizado int Not Null Default 0, 

	Status varchar(1) Not Null Default '', 
	Actualizado tinyint Not Null Default 0, 
	IdentificadorSerie int identity(1, 1)  Unique	
) 
Go--#SQL  

Alter Table CFDI_Emisores_SeriesFolios Add Constraint PK_CFDI_Emisores_SeriesFolios 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Serie )  
Go--#SQL 

Alter Table CFDI_Emisores_SeriesFolios Add Constraint FK_CFDI_Emisores_SeriesFolios___CFDI_Emisores 
	Foreign Key ( IdEmpresa ) References CFDI_Emisores ( IdEmpresa ) 
Go--#SQL 
 
Alter Table CFDI_Emisores_SeriesFolios Add Constraint FK_CFDI_Emisores_SeriesFolios___CFDI_TiposDeDocumentos 
	Foreign Key ( IdTipoDocumento ) References CFDI_TiposDeDocumentos ( IdTipoDocumento ) 
Go--#SQL  


------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_Logos' and xType = 'U' ) 
	Drop Table CFDI_Emisores_Logos   
Go--#SQL 

Create Table CFDI_Emisores_Logos
( 
	IdEmpresa varchar(3) Not Null,  	
	Logo text default ''  
) 
Go--#SQL  

Alter Table CFDI_Emisores_Logos Add Constraint PK_CFDI_Emisores_Logos Primary Key ( IdEmpresa )  
Go--#SQL 

Alter Table CFDI_Emisores_Logos Add Constraint FK_CFDI_Emisores_Logos___CFDI_Emisores 
	Foreign Key ( IdEmpresa ) References CFDI_Emisores ( IdEmpresa ) 
Go--#SQL 

------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_Regimenes' and xType = 'U' ) 
	Drop Table CFDI_Emisores_Regimenes    
Go--#SQL 

Create Table CFDI_Emisores_Regimenes
( 
	IdEmpresa varchar(3) Not Null,  	
	IdRegimen varchar(2) Not Null 
) 
Go--#SQL  

Alter Table CFDI_Emisores_Regimenes Add Constraint PK_CFDI_Emisores_Regimenes Primary Key ( IdEmpresa )  
Go--#SQL 

Alter Table CFDI_Emisores_Regimenes Add Constraint FK_CFDI_Emisores_Regimenes___CFDI_RegimenFiscal 
	Foreign Key ( IdRegimen ) References CFDI_RegimenFiscal ( IdRegimen ) 
Go--#SQL 


------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_FormatosCFDI' and xType = 'U' ) 
	Drop Table CFDI_Emisores_FormatosCFDI    
Go--#SQL 

Create Table CFDI_Emisores_FormatosCFDI
( 
	IdEmpresa varchar(3) Not Null,  	
	TipoDeFormato smallint Not Null Default 0, 
	NombreFormato varchar(200) Not Null Default '' 
) 
Go--#SQL  

Alter Table CFDI_Emisores_FormatosCFDI Add Constraint PK_CFDI_Emisores_FormatosCFDI Primary Key ( IdEmpresa, TipoDeFormato )  
Go--#SQL 

Alter Table CFDI_Emisores_FormatosCFDI Add Constraint FK_CFDI_Emisores_FormatosCFDI___CFDI_Emisores 
	Foreign Key ( IdEmpresa ) References CFDI_Emisores ( IdEmpresa ) 
Go--#SQL 



------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_PAC' and xType = 'U' ) 
	Drop Table CFDI_Emisores_PAC
Go--#SQL  

Create Table CFDI_Emisores_PAC 
( 
	IdEmpresa varchar(3) Not Null, 
	IdPAC varchar(3) Not Null, 
	Usuario varchar(1000) Not Null Default '', 
	Password varchar(1000) Not Null Default '', 
	EnProduccion bit Not Null Default 'False'  
	
) 	
Go--#SQL 

Alter Table CFDI_Emisores_PAC Add Constraint PK_CFDI_Emisores_PAC Primary Key ( IdEmpresa ) 
Go--#SQL 

Alter Table CFDI_Emisores_PAC Add Constraint FK_CFDI_Emisores_PAC___CFDI_Emisores 
	Foreign Key ( IdEmpresa  ) References CFDI_Emisores ( IdEmpresa ) 
Go--#SQL 

Alter Table CFDI_Emisores_PAC Add Constraint FK_CFDI_Emisores_PAC___CFDI_PACs 
	Foreign Key ( IdPAC  ) References CFDI_PACs ( IdPAC ) 
Go--#SQL 

--Insert Into CFDI_Emisores_PAC select '0001', '002', 'IME970211V92', '98706e390a7aeba1728ebeeb7ac2c488', 1  
--Insert Into FACT_CFDI_Emisores_PAC select '001', '002', 'WSDL_PAX', 'wqfCr8O3xLfEhMOHw4nEjMSrxJnvv7bvvr4cVcKuKkBEM++/ke+/gCPvv4nvvrfvvaDvvb/vvqTvvoA=', 'false' 
Go--#SQL 

------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_PAC_Cuentas' and xType = 'U' ) 
	Drop Table CFDI_Emisores_PAC_Cuentas
Go--#SQL  

Create Table CFDI_Emisores_PAC_Cuentas 
( 
	IdEmpresa varchar(3) Not Null,
	IdPAC varchar(3) Not Null, 
	Usuario varchar(1000) Not Null Default '', 
	Password varchar(1000) Not Null Default '', 
	EnProduccion bit Not Null Default 'false'  
	
) 	
Go--#SQL 

Alter Table CFDI_Emisores_PAC_Cuentas Add Constraint PK_CFDI_Emisores_PAC_Cuentas Primary Key ( IdEmpresa, IdPAC ) 
Go--#SQL 

----Alter Table CFDI_Emisores_PAC_Cuentas Add Constraint FK_CFDI_Emisores_PAC_Cuentas___FACT_CFDI_Emisores 
----	Foreign Key ( IdEmpresa  ) References FACT_CFDI_Emisores ( IdEmpresa ) 
----Go--#SQL 

Alter Table CFDI_Emisores_PAC_Cuentas Add Constraint FK_CFDI_Emisores_PAC_Cuentas___CFDI_PACs  
	Foreign Key ( IdPAC  ) References CFDI_PACs ( IdPAC ) 
Go--#SQL 

--Insert Into CFDI_Emisores_PAC_Cuentas select '0001', '002', 'IME_tmb_systems', 'wpHCmcSKw7bCnMK7w53EhcK9wrTvv5vvv7dwNV3vv7JZwpYZRO+/ue+/ve++k+++vO+/nO++qO+9v++9uDI=', 'false' 
Go--#SQL 

