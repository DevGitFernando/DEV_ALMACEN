------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_XML_VP' and xType = 'U' ) 
	Drop Table CFDI_XML_VP   
Go--#SQL  

Create Table CFDI_XML_VP 
(
	GUID varchar(100) Not Null Default '', 
	Keyx int Not Null identity(1, 1), 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	Serie varchar(10) Not Null Default '', 	
	Folio int Not Null Default 0, 		
	IdPAC varchar(3) Not Null Default '', 	
	uf_CadenaOriginal varchar(max) Not Null Default '', 
	uf_SelloDigital varchar(max) Not Null Default '', 
	uf_CFDFolio int Not Null, 
	uf_IVenta int Not Null, 
	uf_CFDI_Info int Not Null, 
	uf_Tipo int Not Null,
	uf_CanceladoSAT int Not Null, 
	uf_CanceladoSAT_FechaCancelacion datetime Not Null Default getdate(), 
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

