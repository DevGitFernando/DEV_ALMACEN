------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_XML_CancelarDocumentos' and xType = 'P' ) 
	Drop Proc spp_Mtto_CFDI_XML_CancelarDocumentos    
Go--#SQL  

Create Proc spp_Mtto_CFDI_XML_CancelarDocumentos 
(	
	@uf_FolioSAT varchar(50) = '',   
	@uf_ackCancelacion_SAT varchar(max) = ''  
	--- @IdEmisor varchar(4) = '00000001', @Serie varchar(10) = 'F', @Folio varchar(10) = '3', 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	-- Select top 1 * From CFDI_XML (NoLock) Where uf_FolioSAT = @uf_FolioSAT 

	If Exists ( Select top 1 * From CFDI_XML (NoLock) Where uf_FolioSAT = @uf_FolioSAT ) 
	Begin 

		If Exists ( Select top 1 * From CFDI_XML (NoLock) Where uf_FolioSAT = @uf_FolioSAT and uf_CanceladoSAT = 0 ) 
		Begin 
			Update X Set uf_CanceladoSAT = 1, uf_CanceladoSAT_FechaCancelacion = getdate(), uf_ackCancelacion_SAT = @uf_ackCancelacion_SAT 
			From CFDI_XML X (NoLock) 
			Where uf_FolioSAT = @uf_FolioSAT 
		End 

		If Exists ( Select top 1 * From CFDI_XML (NoLock) Where uf_FolioSAT = @uf_FolioSAT and uf_CanceladoSAT = 1 and uf_ackCancelacion_SAT = '' ) 
		Begin 
			Update X Set uf_ackCancelacion_SAT = @uf_ackCancelacion_SAT 
			From CFDI_XML X (NoLock) 
			Where uf_FolioSAT = @uf_FolioSAT 
		End 

	End 

	
End 
Go--#SQL 

