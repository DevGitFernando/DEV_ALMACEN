------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Documentos_Conceptos' and xType = 'U' ) 
	Drop Table CFDI_Documentos_Conceptos   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Documentos_InformacionAdicional' and xType = 'U' ) 
	Drop Table CFDI_Documentos_InformacionAdicional   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Documentos' and xType = 'U' ) 
	Drop Table CFDI_Documentos   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_XML' and xType = 'U' ) 
	Drop Table CFDI_XML   
Go--#SQL 


------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Documentos_VP_Conceptos' and xType = 'U' ) 
	Drop Table CFDI_Documentos_VP_Conceptos   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Documentos_InformacionAdicional_VP' and xType = 'U' ) 
	Drop Table CFDI_Documentos_InformacionAdicional_VP   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Documentos_VP' and xType = 'U' ) 
	Drop Table CFDI_Documentos_VP   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_XML_VP' and xType = 'U' ) 
	Drop Table CFDI_XML_VP   
Go--#SQL 



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

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_PAC' and xType = 'U' ) 
	Drop Table CFDI_Emisores_PAC
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_Regimenes' and xType = 'U' ) 
	Drop Table CFDI_Emisores_Regimenes   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_FormatosCFDI' and xType = 'U' ) 
	Drop Table CFDI_Emisores_FormatosCFDI   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores' and xType = 'U' ) 
	Drop Table CFDI_Emisores   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Emisores_PAC_Cuentas' and xType = 'U' ) 
	Drop Table CFDI_Emisores_PAC_Cuentas   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_PACs' and xType = 'U' ) 
	Drop Table CFDI_PACs   
Go--#SQL  


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_RegimenFiscal' and xType = 'U' ) 
	Drop Table CFDI_RegimenFiscal    
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_TiposDeDocumentos' and xType = 'U' ) 
	Drop Table CFDI_TiposDeDocumentos  
Go--#SQL 


------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_UnidadesDeMedida' and xType = 'U' ) 
	Drop Table CFDI_UnidadesDeMedida   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_MetodoDePago' and xType = 'U' ) 
	Drop Table CFDI_MetodoDePago   
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Impuestos' and xType = 'U' ) 
	Drop Table CFDI_Impuestos   
Go--#SQL  


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_FormaDePago' and xType = 'U' ) 
	Drop Table CFDI_FormaDePago    
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Divisas' and xType = 'U' ) 
	Drop Table CFDI_Divisas  
Go--#SQL 


------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Clientes_Direcciones' and xType = 'U' ) 
	Drop Table CFDI_Clientes_Direcciones  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Clientes_Telefonos' and xType = 'U' ) 
	Drop Table CFDI_Clientes_Telefonos   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Clientes_EMails' and xType = 'U' ) 
	Drop Table CFDI_Clientes_EMails   
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Clientes' and xType = 'U' ) 
	Drop Table CFDI_Clientes 
Go--#SQL 