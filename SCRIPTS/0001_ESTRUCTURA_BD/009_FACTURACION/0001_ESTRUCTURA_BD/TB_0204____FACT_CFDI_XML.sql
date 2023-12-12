------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_XML' and xType = 'U' ) 
	Drop Table FACT_CFDI_XML   
Go--#SQL  

Create Table FACT_CFDI_XML 
(
	Keyx int Not Null identity(1, 1), 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 		
	Serie varchar(10) Not Null Default '', 	
	Folio int Not Null Default 0, 
	IdPAC varchar(3) Not Null Default '', 	
	uf_CadenaOriginal varchar(max) Not Null Default '', 
	uf_SelloDigital varchar(max) Not Null Default '', 
	uf_CFDFolio int Not Null Default 0, 
	uf_IVenta int Not Null Default 0, 
	uf_CFDI_Info int Not Null Default 0, 
	uf_Tipo int Not Null Default 0,
	uf_CanceladoSAT int Not Null Default 0,
	uf_CadenaOriginalSAT varchar(max) Not Null Default '',
	uf_SelloDigitalSAT varchar(max) Not Null Default '',
	uf_FolioSAT varchar(50) Not Null Default '',
	uf_NoCertificadoSAT varchar(20) Not Null Default '',
	uf_FechaHoraCerSAT datetime Null ,
	uf_CBB image NULL,
	uf_ackCancelacion_SAT varchar(max) Not Null Default '', 
	uf_Xml_Base varchar(max) Not Null Default '', 
	uf_Xml_Timbrado varchar(max) Not Null Default '', 	
	uf_Xml_Impresion varchar(max) Not Null Default '',
	uf_Pdf text Default ''  	
) 
Go--#SQL 

Alter Table FACT_CFDI_XML Add Constraint PK_FACT_CFDI_XML Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
Go--#SQL 

Alter Table FACT_CFDI_XML Add Constraint FK_FACT_CFDI_XML___FACT_CFD_Documentos_Generados 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio ) 
	References FACT_CFD_Documentos_Generados ( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio )  
Go--#SQL 



