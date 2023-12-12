------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_XML' and xType = 'P' ) 
	Drop Proc spp_Mtto_CFDI_XML    
Go--#SQL  

Create Proc spp_Mtto_CFDI_XML 
(	
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '09', 
	@IdFarmacia varchar(4) = '0001', 		
	@Serie varchar(10) = 'F', @Folio varchar(10) = '3', 
	@uf_CadenaOriginal varchar(max) = '', 
	@uf_SelloDigital varchar(max) = '', 
	@uf_CFDFolio int = 0, 
	@uf_IVenta int = 0, 
	@uf_CFDI_Info int = 0, 
	@uf_Tipo int = 0,
	@uf_CanceladoSAT int = 0,
	@uf_CadenaOriginalSAT varchar(max) = '',
	@uf_SelloDigitalSAT varchar(max) = '',
	@uf_FolioSAT varchar(50) = '',
	@uf_NoCertificadoSAT varchar(20) = '',
	@uf_FechaHoraCerSAT datetime = null,
	@uf_CBB image = NULL,
	@uf_ackCancelacion_SAT varchar(max) = '', 
	@uf_Xml_Base varchar(max) = '', 
	@uf_Xml_Timbrado varchar(max) = '', 	
	@uf_Xml_Impresion varchar(max) = '',
	@uf_Pdf text = '',  	
	@IdPAC varchar(3) = '', 
	@GUID varchar(100) = ''  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD

	If Not Exists ( Select * From CFDI_XML (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Serie = @Serie and Folio = @Folio ) 
	Begin 
		Insert Into CFDI_XML ( GUID, IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, IdPAC, uf_CadenaOriginal, uf_SelloDigital, uf_CFDFolio, uf_IVenta, uf_CFDI_Info, uf_Tipo, uf_CanceladoSAT, uf_CadenaOriginalSAT, uf_SelloDigitalSAT, uf_FolioSAT, uf_NoCertificadoSAT, uf_FechaHoraCerSAT, uf_CBB, uf_ackCancelacion_SAT, uf_Xml_Base, uf_Xml_Timbrado, uf_Xml_Impresion, uf_Pdf ) 
		Select @GUID, @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, @IdPAC, @uf_CadenaOriginal, @uf_SelloDigital, @uf_CFDFolio, @uf_IVenta, @uf_CFDI_Info, @uf_Tipo, @uf_CanceladoSAT, @uf_CadenaOriginalSAT, @uf_SelloDigitalSAT, @uf_FolioSAT, @uf_NoCertificadoSAT, @uf_FechaHoraCerSAT, @uf_CBB, @uf_ackCancelacion_SAT, @uf_Xml_Base, @uf_Xml_Timbrado, @uf_Xml_Impresion, @uf_Pdf
	End 	

	Select Keyx as FolioDocumento 
	From CFDI_XML (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Serie = @Serie and Folio = @Folio
	
		
---		sp_listacolumnas  CFDI_XML  
	
---		sp_listacolumnas__Stores spp_Mtto_CFDI_XML , 1  	
	
End 
Go--#SQL 

