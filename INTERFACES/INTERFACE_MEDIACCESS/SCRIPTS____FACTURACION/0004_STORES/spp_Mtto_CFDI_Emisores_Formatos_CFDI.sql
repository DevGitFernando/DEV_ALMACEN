----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_Emisores_FormatosCFDI' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFDI_Emisores_FormatosCFDI
Go--#SQL    

Create Proc spp_Mtto_CFDI_Emisores_FormatosCFDI 
( 
	@IdEmisor varchar(4) = '00000001', 
	@TipoDeFormato smallint = 0, @NombreFormato varchar(200) = '' 
	
) 
With Encryption 
As 
Begin 
Set NoCount On 

	----Delete From CFDI_Emisores_FormatosCFDI Where IdEmisor = @IdEmisor and TipoDeFormato = @TipoDeFormato 
	
	----Insert Into CFDI_Emisores_FormatosCFDI ( IdEmisor, TipoDeFormato, NombreFormato )
	----Select @IdEmisor, @TipoDeFormato, @NombreFormato 
	

End 
Go--#SQL    

